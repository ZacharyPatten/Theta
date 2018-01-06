using System;
using System.Xml;

using Theta.Structures;
using Theta.Mathematics;
using Theta.Measurements;
using Theta.Algorithms;

namespace Theta.Graphics.Parsers
{
    public static class ColladaParser
    {
        #region XmlNode Extension Methods

        private static XmlNode First(this XmlNode node, Predicate<XmlNode> where)
        {
            if (node == null)
            {
                return null;
            }
            foreach (XmlNode child in node.ChildNodes)
            {
                if (where(child))
                {
                    return child;
                }
            }
            foreach (XmlNode child in node.ChildNodes)
            {
                XmlNode nested = child.First(where);
                if (nested != null)
                {
                    return nested;
                }
            }
            return null;
        }

        private static int Count(this XmlNode node, Predicate<XmlNode> where)
        {
            int count = 0;
            foreach (XmlNode child in node.ChildNodes)
            {
                if (where(child))
                {
                    count++;
                }
            }
            return count;
        }

        private static void All(this XmlNode node, Predicate<XmlNode> where, Step<XmlNode> step)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (where(child))
                {
                    step(child);
                }
            }
        }

        #endregion

        public static Model Parse(string contents)
        {
            int maxJointEffectors = 3;

            Matrix<float> correction = Matrix<float>.Rotate4x4(
                Angle<float>.Factory_Degrees(-90f),
                new Vector<float>(1, 0, 0),
                Matrix<float>.FactoryIdentity(4, 4));

            // Load the contents into an xml reader
            XmlDocument xml_document = new XmlDocument();
            xml_document.LoadXml(contents);

            Model model = new Model();

            #region JOINT WEIGHTS

            XmlNode xml_library_controllers = xml_document.First(x => x.Name == "library_controllers");
            XmlNode xml_skin = xml_library_controllers.First(x => x.Name == "controller").First(x => x.Name == "skin");
            XmlNode xml_vertex_weights = xml_skin.First(x => x.Name == "vertex_weights");

            // Joint Names
            string jointNameAttributeId = xml_vertex_weights.First(x => x.Name == "input" && x.Attributes["semantic"].Value == "JOINT").Attributes["source"].Value.Substring(1);
            XmlNode xml_joint_Name_array = xml_skin.First(x => x.Name == "source" && x.Attributes["id"].Value == jointNameAttributeId).First(x => x.Name == "Name_array");
            string[] jointNames = xml_joint_Name_array.InnerText.Split(' ');

            // Vertex-Joint Weight Values
            string vertexWeightAttriduteId = xml_vertex_weights.First(x => x.Name == "input" && x.Attributes["semantic"].Value == "WEIGHT").Attributes["source"].Value.Substring(1);
            XmlNode xml_weight_float_array = xml_skin.First(x => x.Name == "source" && x.Attributes["id"].Value == vertexWeightAttriduteId).First(x => x.Name == "float_array");
            string[] weightStringSplits = xml_weight_float_array.InnerText.Split(' ');
            float[] weights = new float[weightStringSplits.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = float.Parse(weightStringSplits[i]);
            }

            // Vertex-Effector Joint Counts (how many joints effect each vertex)
            string[] effectorJointCountStringSplits = xml_vertex_weights.First(x => x.Name == "vcount").InnerText.Trim().Split(' ');
            int[] effectorJointCounts = new int[effectorJointCountStringSplits.Length];
            for (int i = 0; i < effectorJointCountStringSplits.Length; i++)
            {
                effectorJointCounts[i] = int.Parse(effectorJointCountStringSplits[i]);
            }

            // Model Array Initializations
            model._jointIds = new int[effectorJointCounts.Length * maxJointEffectors];
            model._jointWeights = new float[effectorJointCounts.Length * maxJointEffectors];

            // Vertex-Joint Mappings
            string[] vertexJointMappingStringSplits = xml_vertex_weights.First(x => x.Name == "v").InnerText.Split(' ');
            int currentString = 0;
            for (int i = 0; i < effectorJointCounts.Length; i++)
            {
                int count = effectorJointCounts[i];

                Link<int, float>[] jointAndWeightsPerVertex = new Link<int, float>[count];
                int[] jointsPerVertex = new int[count];
                float[] weightsPerVertex = new float[count];

                for (int j = 0; j < count; j++)
                {
                    int jointIndex = int.Parse(vertexJointMappingStringSplits[currentString++]);
                    int weightIndex = int.Parse(vertexJointMappingStringSplits[currentString++]);
                    float weight = weights[weightIndex];
                    jointAndWeightsPerVertex[j] = new Link<int, float>(jointIndex, weight);
                }

                // Joint Effector Count Syncronization (get all verteces to have the same number of effectors)
                if (jointAndWeightsPerVertex.Length > maxJointEffectors) // too many effectors (select largest weights)
                {
                    Sort<Link<int, float>>.Quick((x, y) => Compute<float>.Compare(x._2, y._2), jointAndWeightsPerVertex);
                    Link<int, float>[] limitedJointAndWeightsPerVertex = new Link<int, float>[maxJointEffectors];
                    for (int j = 0; j < maxJointEffectors; j++)
                    {
                        limitedJointAndWeightsPerVertex[j] = jointAndWeightsPerVertex[j];
                    }
                    jointAndWeightsPerVertex = limitedJointAndWeightsPerVertex;
                }
                else if (jointAndWeightsPerVertex.Length < maxJointEffectors) // too few effectors (add zero-valued effectors)
                {
                    Link<int, float>[] synchronizedJointAndWeightsPerVertex = new Link<int, float>[maxJointEffectors];
                    int j = 0;
                    for (; j < jointAndWeightsPerVertex.Length; j++)
                    {
                        synchronizedJointAndWeightsPerVertex[j] = jointAndWeightsPerVertex[j];
                    }
                    for (; j < maxJointEffectors; j++)
                    {
                        synchronizedJointAndWeightsPerVertex[j] = new Link<int, float>(0, 0);
                    }
                    jointAndWeightsPerVertex = synchronizedJointAndWeightsPerVertex;
                }

                for (int j = 0; j < maxJointEffectors; j++)
                {
                    model._jointIds[i * maxJointEffectors + j] = jointAndWeightsPerVertex[j]._1;
                    model._jointWeights[i * maxJointEffectors + j] = jointAndWeightsPerVertex[j]._2;
                }
            }

            #endregion

            XmlNode xml_library_visual_scenes = xml_document.First(x => x.Name == "library_visual_scenes");
            XmlNode xml_armature = xml_library_visual_scenes.First(x => x.Name == "visual_scene").First(x => x.Name == "node" && x.Attributes["id"].Value == "Armature");

            #region JOINT DEFINITIONS (TREE)
            
            XmlNode xml_node = xml_armature.First(x => x.Name == "node");

            // helper function for parsing joints (used in recursive function below)
            System.Func<XmlNode, Model.Joint> ParseJoint = (XmlNode xmlNode) =>
            {
                string jointName = xmlNode.Attributes["id"].Value;
                int jointIndex = -1;
                for (int i = 0; i < jointNames.Length; i++)
                {
                    if (jointNames[i] == jointName)
                    {
                        jointIndex = i;
                        break;
                    }
                }
                string[] matrixStringSplits = xmlNode.First(x => x.Name == "matrix").InnerText.Split(' ');
                Matrix<float> jointBindLocalTransform = new Matrix<float>(new float[,]
                {
                    { float.Parse(matrixStringSplits[0]), float.Parse(matrixStringSplits[1]), float.Parse(matrixStringSplits[2]), float.Parse(matrixStringSplits[3]), },
                    { float.Parse(matrixStringSplits[4]), float.Parse(matrixStringSplits[5]), float.Parse(matrixStringSplits[6]), float.Parse(matrixStringSplits[7]), },
                    { float.Parse(matrixStringSplits[8]), float.Parse(matrixStringSplits[9]), float.Parse(matrixStringSplits[10]), float.Parse(matrixStringSplits[11]), },
                    { float.Parse(matrixStringSplits[12]), float.Parse(matrixStringSplits[13]), float.Parse(matrixStringSplits[14]), float.Parse(matrixStringSplits[15]), },
                });
                jointBindLocalTransform = jointBindLocalTransform.Transpose();

                return new Model.Joint()
                {
                    Id = jointIndex,
                    Name = jointName,
                    BindLocalTransform = jointBindLocalTransform,
                };
            };

            // initialize the model joint tree
            Model.Joint headJoint = ParseJoint(xml_node);
            model._joints = new TreeMap<Model.Joint>(headJoint, (x, y) => x.Id == y.Id, x => x.Id);

            // define the recursive function that will build the joint tree
            System.Action<XmlNode, Model.Joint> BuildJointTree = null;
            BuildJointTree = (xmlNode, parent) =>
            {
                Model.Joint joint = ParseJoint(xmlNode);
                model._joints.Add(joint, parent);
                xmlNode.All(x => x.Name == "node", xmlChildNode => BuildJointTree(xmlChildNode, joint));
            };

            // call the recursive function to actually build the tree
            xml_node.All(x => x.Name == "node", xmlChildNode => BuildJointTree(xmlChildNode, headJoint));

            #endregion

            #region ANIMATION

            model._animations = new MapHashLinked<Model.Animation, string>();

            XmlNode xml_library_animations = xml_document.First(x => x.Name == "library_animations");

            string rootJointName = xml_armature.First(x => x.Name == "node").Attributes["id"].Value;

            XmlNode xml_animationTimeFloatData = xml_library_animations.First(x => x.Name == "animation").First(x => x.Name == "source").First(x => x.Name == "float_array");

            string[] animationTimeStringSplits = xml_animationTimeFloatData.InnerText.Split(' ');
            Model.Animation.KeyFrame[] keyFrames = new Model.Animation.KeyFrame[animationTimeStringSplits.Length];
            for (int i = 0; i < animationTimeStringSplits.Length; i++)
            {
                keyFrames[i] = new Model.Animation.KeyFrame()
                {
                    TimeSpan = float.Parse(animationTimeStringSplits[i]),
                    JointTransformations = new MapHashLinked<Link<Vector<float>, Quaternion<float>>, Model.Joint>((x, y) => x.Id == y.Id, x => x.Id),
                };
            }
            float duration = keyFrames[keyFrames.Length - 1].TimeSpan;
            xml_library_animations.All(x => x.Name == "animation", (XmlNode child) =>
            {
                XmlNode channelNode = child.First(x => x.Name == "channel");
                string jointNameId = channelNode.Attributes["target"].Value.Split('/')[0];

                XmlNode node = child.First(x => x.Name == "sampler").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "OUTPUT");
                string dataId = node.Attributes["source"].Value.Substring(1);

                XmlNode transformData = child.First(x => x.Name == "source" && x.Attributes["id"].Value == dataId);
                string[] rawData = transformData.First(x => x.Name == "float_array").InnerText.Split(' ');
                
                for (int i = 0; i < keyFrames.Length; i++)
                {
                    float[] matrixData = new float[16];
                    for (int j = 0; j < 16; j++)
                    {
                        matrixData[j] = float.Parse(rawData[i * 16 + j]);
                    }
                    Matrix<float> transform = new Matrix<float>(new float[,]
                    {
                        { matrixData[0], matrixData[1], matrixData[2], matrixData[3], },
                        { matrixData[4], matrixData[5], matrixData[6], matrixData[7], },
                        { matrixData[8], matrixData[9], matrixData[10], matrixData[11], },
                        { matrixData[12], matrixData[13], matrixData[14], matrixData[15], },
                    });
                    transform = transform.Transpose();
                    
                    if (jointNameId == rootJointName)
                    {
                        transform = correction * transform;
                    }

                    Model.Joint joint = null;
                    model._joints.Stepper(x =>
                    {
                        if (x.Name == jointNameId)
                        {
                            joint = x;
                            return StepStatus.Break;
                        }
                        return StepStatus.Continue;
                    });

                    if (joint == null)
                    {
                        throw new System.FormatException("Collada file has inconsistencies. An animation transformation is referencing a non-existing joint [" + jointNameId + "].");
                    }
                    
                    keyFrames[i].JointTransformations.Add(joint, new Link<Vector<float>, Quaternion<float>>(
                        new Vector<float>(transform[0, 3], transform[1, 3], transform[2, 3]),
                        Quaternion<float>.Factory_Matrix3x3(transform.Minor(3, 3))));
                }
            });

            string animationName = "base animation";
            model._animations.Add(animationName, new Model.Animation()
            {
                Id = 0,
                Name = animationName,
                Duration = duration,
                KeyFrames = keyFrames,
            });

            #endregion

            #region STATIC GEOMETRY

            XmlNode xml_library_geometries = xml_document.First(x => x.Name == "library_geometries");
            XmlNode xml_geometry = xml_library_geometries.First(x => x.Name == "geometry");
            int meshCount = xml_geometry.Count(x => x.Name == "mesh");
            if (meshCount != 1)
            {
                throw new System.FormatException("Collada parser currently only supports single mesh models.");
            }
            XmlNode xml_mesh = xml_geometry.First(x => x.Name == "mesh");

            // Position Data
            XmlNode xml_vertices = xml_mesh.First(x => x.Name == "vertices");
            XmlNode xml_input = xml_vertices.First(x => x.Name == "input");
            string positionAttributeId = xml_input.Attributes["source"].Value.Substring(1);
            XmlNode xml_positionFloatData = xml_mesh.First(x => x.Name == "source" && x.Attributes["id"].Value == positionAttributeId).First(x => x.Name == "float_array");
            int positionFloatCount = int.Parse(xml_positionFloatData.Attributes["count"].Value);
            string[] positionStringSplits = xml_positionFloatData.InnerText.Split(' ');
            if (positionFloatCount != positionStringSplits.Length)
            {
                throw new System.FormatException("Collada file has inconsistencies. Position count is [" + positionFloatCount + "] but [" + positionStringSplits.Length + "] values were provided.");
            }
            float[] positions = new float[positionFloatCount];
            for (int i = 0; i < positionFloatCount / 3; i++)
            {
                Vector<float> position =
                    new Vector<float>(
                    float.Parse(positionStringSplits[i * 3]),
                    float.Parse(positionStringSplits[i * 3 + 1]),
                    float.Parse(positionStringSplits[i * 3 + 2]),
                    1);
                position = correction * position;
                positions[i * 3] = position.X;
                positions[i * 3 + 1] = position.Y;
                positions[i * 3 + 2] = position.Z;
            }

            if (effectorJointCounts.Length != positions.Length / 3)
            {
                throw new System.FormatException("Collada file has inconsistencies. The vertex count of the joint-vertex mapping data [" + effectorJointCounts.Length + "] does not match the number of vertex positions [" + positions.Length / 3 + "].");
            }

            // Normal Data
            string normalAttributeId = xml_mesh.First(x => x.Name == "polylist").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "NORMAL").Attributes["source"].Value.Substring(1);
            XmlNode xml_normalData = xml_mesh.First(x => x.Name == "source" && x.Attributes["id"].Value == normalAttributeId).First(x => x.Name == "float_array");
            int normalFloatCount = int.Parse(xml_normalData.Attributes["count"].Value);
            string[] normalStringSplits = xml_normalData.InnerText.Split(' ');
            if (normalFloatCount != normalStringSplits.Length)
            {
                throw new System.FormatException("Collada file has inconsistencies. Normal count is [" + normalFloatCount + "] but [" + normalStringSplits.Length + "] values were provided.");
            }
            float[] normals = new float[normalFloatCount];
            for (int i = 0; i < normalFloatCount / 3; i++)
            {
                Vector<float> normal = new Vector<float>(
                    float.Parse(normalStringSplits[i * 3]),
                    float.Parse(normalStringSplits[i * 3 + 1]),
                    float.Parse(normalStringSplits[i * 3 + 2]),
                    0);
                normal = correction * normal;
                normals[i * 3] = normal.X;
                normals[i * 3 + 1] = normal.Y;
                normals[i * 3 + 2] = normal.Z;
            }

            // Texture Data
            string textureCoordinateAttributeId = xml_mesh.First(x => x.Name == "polylist").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "TEXCOORD").Attributes["source"].Value.Substring(1);
            XmlNode xml_textureCoordinateData = xml_mesh.First(x => x.Name == "source" && x.Attributes["id"].Value == textureCoordinateAttributeId).First(x => x.Name == "float_array");
            int textureCoordinateFloatCount = int.Parse(xml_textureCoordinateData.Attributes["count"].Value);
            string[] textureCoordinateStringSplits = xml_textureCoordinateData.InnerText.Split(' ');
            if (textureCoordinateFloatCount != textureCoordinateStringSplits.Length)
            {
                throw new System.FormatException("Collada file has inconsistencies. Texture Coordinate count is [" + textureCoordinateFloatCount + "] but [" + textureCoordinateStringSplits.Length + "] values were provided.");
            }
            float[] textureCoordinates = new float[textureCoordinateFloatCount];
            for (int i = 0; i < textureCoordinateStringSplits.Length; i++)
            {
                textureCoordinates[i] = float.Parse(textureCoordinateStringSplits[i]);
            }
            
            Set<int> processedIndeces = new SetHashList<int>();
            int vertexCount = positions.Length / 3;

            // Index Data (Counting Phase)
            XmlNode xml_polylist = xml_mesh.First(x => x.Name == "polylist");
            int indexFormat = xml_polylist.Count(x => x.Name == "input");
            string[] indexStringSplits = xml_polylist.First(x => x.Name == "p").InnerText.Split(' ');
            int[] indexSplitsParsed = new int[indexStringSplits.Length];
            //model._indices = new int[indexStringSplits.Length];
            for (int i = 0; i < indexStringSplits.Length / indexFormat; i++)
            {
                int positionIndex = int.Parse(indexStringSplits[i * indexFormat]);

                indexSplitsParsed[i * indexFormat] = positionIndex;
                indexSplitsParsed[i * indexFormat + 1] = int.Parse(indexStringSplits[i * indexFormat + 1]);
                indexSplitsParsed[i * indexFormat + 2] = int.Parse(indexStringSplits[i * indexFormat + 2]);
                
                if (processedIndeces.Contains(positionIndex))
                    vertexCount++;
                else
                    processedIndeces.Add(positionIndex);
            }

            // Model Array Initializations
            model._positions = new float[vertexCount * 3];
            model._textureCoordinates = new float[vertexCount * 2];
            model._normals = new float[vertexCount * 3];
            model._jointIds = new int[vertexCount * 3];
            model._jointWeights = new float[vertexCount * 3];
            model._indices = new int[vertexCount];

            // Index Data
            for (int i = 0; i < indexSplitsParsed.Length / indexFormat; i++)
            {
                int positionIndex = int.Parse(indexStringSplits[i * indexFormat]);
                int normalIndex = int.Parse(indexStringSplits[i * indexFormat + 1]);
                int textureCoordinateIndex = int.Parse(indexStringSplits[i * indexFormat + 2]);

                model._indices[i] = positionIndex;

                int i_3 = i * 3;
                
                model._positions[i_3] = positions[positionIndex * 3];
                model._positions[i_3 + 1] = positions[positionIndex * 3 + 1];
                model._positions[i_3 + 2] = positions[positionIndex * 3 + 2];

                model._normals[i_3] = normals[normalIndex * 3];
                model._normals[i_3 + 1] = normals[normalIndex * 3 + 1];
                model._normals[i_3 + 2] = normals[normalIndex * 3 + 2];

                model._textureCoordinates[i * 2] = textureCoordinates[textureCoordinateIndex * 2];
                model._textureCoordinates[i * 2 + 1] = textureCoordinates[textureCoordinateIndex * 2 + 1];
            }

            #endregion
            
            return model;
        }
    }
}
