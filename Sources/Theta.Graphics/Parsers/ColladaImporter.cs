using Theta.Structures;
using Theta.Mathematics;
using System.Xml;
using Theta.Measurements;
using Theta.Algorithms;
using System;

namespace Theta.Graphics.Importer
{
    #region types

    public class Vertex
    {
        private static int NO_INDEX = -1;

        private Vector<float> position;
        private int textureIndex = NO_INDEX;
        private int normalIndex = NO_INDEX;
        private Vertex duplicateVertex = null;
        private int index;
        private float length;
        private List<Vector<float>> tangents = new ListArray<Vector<float>>();
        private Vector<float> averagedTangent = new Vector<float>(0, 0, 0);

        private VertexSkinData weightsData;

        public Vertex(int index, Vector<float> position, VertexSkinData weightsData)
        {
            this.index = index;
            this.weightsData = weightsData;
            this.position = position;
            this.length = position.Dimensions;
        }

        public VertexSkinData getWeightsData()
        {
            return weightsData;
        }

        public void addTangent(Vector<float> tangent)
        {
            tangents.Add(tangent);
        }

        public void averageTangents()
        {
            if (tangents == null || tangents.Count < 1)
            {
                return;
            }
            foreach (Vector<float> tangent in tangents)
            {
                averagedTangent = Vector<float>.Add(averagedTangent, tangent);
            }
            averagedTangent.Normalize();
        }

        public Vector<float> getAverageTangent()
        {
            return averagedTangent;
        }

        public int getIndex()
        {
            return index;
        }

        public float getLength()
        {
            return length;
        }

        public bool isSet()
        {
            return textureIndex != NO_INDEX && normalIndex != NO_INDEX;
        }

        public bool hasSameTextureAndNormal(int textureIndexOther, int normalIndexOther)
        {
            return textureIndexOther == textureIndex && normalIndexOther == normalIndex;
        }

        public void setTextureIndex(int textureIndex)
        {
            this.textureIndex = textureIndex;
        }

        public void setNormalIndex(int normalIndex)
        {
            this.normalIndex = normalIndex;
        }

        public Vector<float> getPosition()
        {
            return position;
        }

        public int getTextureIndex()
        {
            return textureIndex;
        }

        public int getNormalIndex()
        {
            return normalIndex;
        }

        public Vertex getDuplicateVertex()
        {
            return duplicateVertex;
        }

        public void setDuplicateVertex(Vertex duplicateVertex)
        {
            this.duplicateVertex = duplicateVertex;
        }

    }

    public class VertexSkinData
    {
        public ListArray<int> jointIds = new ListArray<int>();
        public ListArray<float> weights = new ListArray<float>();

        public void addJointEffect(int jointId, float weight)
        {
            for (int i = 0; i < weights.Count; i++)
            {
                if (weight > weights[i])
                {
                    jointIds.Add(jointId);
                    weights.Add(weight);
                    return;
                }
            }
            jointIds.Add(jointId);
            weights.Add(weight);
        }

        public void limitJointNumber(int max)
        {
            if (jointIds.Count > max)
            {
                float[] topWeights = new float[max];
                float total = saveTopWeights(topWeights);
                refillWeightList(topWeights, total);
                removeExcessJointIds(max);
            }
            else if (jointIds.Count < max)
            {
                fillEmptyWeights(max);
            }
        }

        private void fillEmptyWeights(int max)
        {
            while (jointIds.Count < max)
            {
                jointIds.Add(0);
                weights.Add(0f);
            }
        }

        private float saveTopWeights(float[] topWeightsArray)
        {
            float total = 0;
            for (int i = 0; i < topWeightsArray.Length; i++)
            {
                topWeightsArray[i] = weights[i];
                total += topWeightsArray[i];
            }
            return total;
        }

        private void refillWeightList(float[] topWeights, float total)
        {
            weights.Clear();
            for (int i = 0; i < topWeights.Length; i++)
            {
                weights.Add(Compute<float>.Minimum(topWeights[i] / total, 1));
            }
        }

        private void removeExcessJointIds(int max)
        {
            while (jointIds.Count > max)
            {
                jointIds.Remove(jointIds.Count - 1);
            }
        }
    }

    public class SkinningData
    {
        public ListArray<string> jointOrder;
        public ListArray<VertexSkinData> verticesSkinData;

        public SkinningData(ListArray<string> jointOrder, ListArray<VertexSkinData> verticesSkinData)
        {
            this.jointOrder = jointOrder;
            this.verticesSkinData = verticesSkinData;
        }
    }

    public class SkeletonData
    {
        public int jointCount;
        public JointData headJoint;

        public SkeletonData(int jointCount, JointData headJoint)
        {
            this.jointCount = jointCount;
            this.headJoint = headJoint;
        }

    }

    public class JointData
    {
        public int index;
        public string nameId;
        public Matrix<float> bindLocalTransform;

        public List<JointData> children = new ListArray<JointData>();

        public JointData(int index, string nameId, Matrix<float> bindLocalTransform)
        {
            this.index = index;
            this.nameId = nameId;
            this.bindLocalTransform = bindLocalTransform;
        }

        public void addChild(JointData child)
        {
            children.Add(child);
        }

    }

    public class MeshData
    {
        public static int DIMENSIONS = 3;

        public float[] vertices;
        public float[] textureCoords;
        public float[] normals;
        public int[] indices;
        public int[] jointIds;
        public float[] vertexWeights;

        public MeshData(float[] vertices, float[] textureCoords, float[] normals, int[] indices,
                int[] jointIds, float[] vertexWeights)
        {
            this.vertices = vertices;
            this.textureCoords = textureCoords;
            this.normals = normals;
            this.indices = indices;
            this.jointIds = jointIds;
            this.vertexWeights = vertexWeights;
        }

        public int getVertexCount()
        {
            return vertices.Length / DIMENSIONS;
        }

    }

    public class KeyFrameData
    {

        public float time;
        public List<JointTransformData> jointTransforms = new ListArray<JointTransformData>();

        public KeyFrameData(float time)
        {
            this.time = time;
        }

        public void addJointTransform(JointTransformData transform)
        {
            jointTransforms.Add(transform);
        }

    }

    public class JointTransformData
    {

        public string jointNameId;
        public Matrix<float> jointLocalTransform;

        public JointTransformData(string jointNameId, Matrix<float> jointLocalTransform)
        {
            this.jointNameId = jointNameId;
            this.jointLocalTransform = jointLocalTransform;
        }
    }

    public class AnimationData
    {

        public float lengthSeconds;
        public KeyFrameData[] keyFrames;

        public AnimationData(float lengthSeconds, KeyFrameData[] keyFrames)
        {
            this.lengthSeconds = lengthSeconds;
            this.keyFrames = keyFrames;
        }

    }

    public class AnimatedModelData
    {
        public SkeletonData joints;
        public MeshData mesh;

        public AnimatedModelData(MeshData mesh, SkeletonData joints)
        {
            this.joints = joints;
            this.mesh = mesh;
        }
    }

    internal static class XmlHelpers
    {
        internal static XmlNode First(this XmlNode node, Predicate<XmlNode> where)
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

        internal static int Count(this XmlNode node, Predicate<XmlNode> where)
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

        internal static void All(this XmlNode node, Predicate<XmlNode> where, Step<XmlNode> step)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (where(child))
                {
                    step(child);
                }
            }
        }
    }

    #endregion

    public class Collada
    {
        private static Matrix<float> CORRECTION = Matrix<float>.Rotate4x4(Angle<float>.Factory_Degrees(-90f), new Vector<float>(1, 0, 0), Matrix<float>.FactoryIdentity(4, 4));

        public class GeometryLoader
        {
            private XmlNode meshData;

            private ListArray<VertexSkinData> vertexWeights;

            private float[] verticesArray;
            private float[] normalsArray;
            private float[] texturesArray;
            private int[] indicesArray;
            private int[] jointIdsArray;
            private float[] weightsArray;

            ListArray<Vertex> vertices = new ListArray<Vertex>();
            ListArray<Vector<float>> textures = new ListArray<Vector<float>>();
            ListArray<Vector<float>> normals = new ListArray<Vector<float>>();
            ListArray<int> indices = new ListArray<int>();

            public GeometryLoader(XmlNode geometryNode, ListArray<VertexSkinData> vertexWeights)
            {
                this.vertexWeights = vertexWeights;
                //this.meshData = geometryNode.getChild("geometry").getChild("mesh");
                XmlNode geometryChildNode = geometryNode.First(x => x.Name == "geometry");
                this.meshData = geometryChildNode.First(x => x.Name == "mesh");
            }

            public MeshData extractModelData()
            {
                readRawData();
                assembleVertices();
                removeUnusedVertices();
                initArrays();
                convertDataToArrays();
                convertIndicesListToArray();
                return new MeshData(verticesArray, texturesArray, normalsArray, indicesArray, jointIdsArray, weightsArray);
            }

            private void readRawData()
            {
                readPositions();
                readNormals();
                readTextureCoords();
            }

            private void readPositions()
            {
                XmlNode verticesNode = meshData.First(x => x.Name == "vertices");
                XmlNode inputNode = verticesNode.First(x => x.Name == "input");
                string positionsId = inputNode.Attributes["source"].Value.Substring(1);
                XmlNode positionsData = meshData.First(x => x.Name == "source" && x.Attributes["id"].Value == positionsId).First(x => x.Name == "float_array");
                int count = int.Parse(positionsData.Attributes["count"].Value);
                string[] posData = positionsData.InnerText.Split(' ');
                for (int i = 0; i < count / 3; i++)
                {
                    float x = float.Parse(posData[i * 3]);
                    float y = float.Parse(posData[i * 3 + 1]);
                    float z = float.Parse(posData[i * 3 + 2]);
                    Vector<float> position = new Vector<float>(x, y, z, 1);
                    position = CORRECTION * position;
                    vertices.Add(new Vertex(vertices.Count, new Vector<float>(position.X, position.Y, position.Z), vertexWeights[vertices.Count]));
                }
            }

            private void readNormals()
            {
                string normalsId = meshData.First(x => x.Name == "polylist").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "NORMAL").Attributes["source"].Value.Substring(1);
                XmlNode normalsData = meshData.First(x => x.Name == "source" && x.Attributes["id"].Value == normalsId).First(x => x.Name =="float_array");
                int count = int.Parse(normalsData.Attributes["count"].Value);
                string[] normData = normalsData.InnerText.Split(' ');
                for (int i = 0; i < count / 3; i++)
                {
                    float x = float.Parse(normData[i * 3]);
                    float y = float.Parse(normData[i * 3 + 1]);
                    float z = float.Parse(normData[i * 3 + 2]);
                    Vector<float> norm = new Vector<float>(x, y, z, 0f);
                    norm = CORRECTION * norm;
                    normals.Add(new Vector<float>(norm.X, norm.Y, norm.Z));
                }
            }

            private void readTextureCoords()
            {
                string texCoordsId = meshData.First(x => x.Name == "polylist").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "TEXCOORD").Attributes["source"].Value.Substring(1);
                XmlNode texCoordsData = meshData.First(x => x.Name == "source" && x.Attributes["id"].Value == texCoordsId).First(x => x.Name == "float_array");
                int count = int.Parse(texCoordsData.Attributes["count"].Value);
                string[] texData = texCoordsData.InnerText.Split(' ');
                for (int i = 0; i < count / 2; i++)
                {
                    float s = float.Parse(texData[i * 2]);
                    float t = float.Parse(texData[i * 2 + 1]);
                    textures.Add(new Vector<float>(s, t));
                }
            }

            private void assembleVertices()
            {
                XmlNode poly = meshData.First(x => x.Name == "polylist");
                int typeCount = poly.Count(x => x.Name == "input");
                string[] indexData = poly.First(x => x.Name == "p").InnerText.Split(' ');
                for (int i = 0; i < indexData.Length / typeCount; i++)
                {
                    int positionIndex = int.Parse(indexData[i * typeCount]);
                    int normalIndex = int.Parse(indexData[i * typeCount + 1]);
                    int texCoordIndex = int.Parse(indexData[i * typeCount + 2]);
                    processVertex(positionIndex, normalIndex, texCoordIndex);
                }
            }

            private Vertex processVertex(int posIndex, int normIndex, int texIndex)
            {
                Vertex currentVertex = vertices[posIndex];
                if (!currentVertex.isSet())
                {
                    currentVertex.setTextureIndex(texIndex);
                    currentVertex.setNormalIndex(normIndex);
                    indices.Add(posIndex);
                    return currentVertex;
                }
                else
                {
                    return dealWithAlreadyProcessedVertex(currentVertex, texIndex, normIndex);
                }
            }

            private int[] convertIndicesListToArray()
            {
                this.indicesArray = new int[indices.Count];
                for (int i = 0; i < indicesArray.Length; i++)
                {
                    indicesArray[i] = indices[i];
                }
                return indicesArray;
            }

            private float convertDataToArrays()
            {
                float furthestPoint = 0;
                for (int i = 0; i < vertices.Count; i++)
                {
                    Vertex currentVertex = vertices[i];
                    if (currentVertex.getLength() > furthestPoint)
                    {
                        furthestPoint = currentVertex.getLength();
                    }
                    Vector<float> position = currentVertex.getPosition();
                    Vector<float> textureCoord = textures[currentVertex.getTextureIndex()];
                    Vector<float> normalVector = normals[currentVertex.getNormalIndex()];

                    verticesArray[i * 3] = position.X;
                    verticesArray[i * 3 + 1] = position.Y;
                    verticesArray[i * 3 + 2] = position.Z;

                    texturesArray[i * 2] = textureCoord.X;
                    texturesArray[i * 2 + 1] = 1 - textureCoord.Y;

                    normalsArray[i * 3] = normalVector.X;
                    normalsArray[i * 3 + 1] = normalVector.Y;
                    normalsArray[i * 3 + 2] = normalVector.Z;

                    VertexSkinData weights = currentVertex.getWeightsData();

                    jointIdsArray[i * 3] = weights.jointIds[0];
                    jointIdsArray[i * 3 + 1] = weights.jointIds[1];
                    jointIdsArray[i * 3 + 2] = weights.jointIds[2];

                    weightsArray[i * 3] = weights.weights[0];
                    weightsArray[i * 3 + 1] = weights.weights[1];
                    weightsArray[i * 3 + 2] = weights.weights[2];

                }
                return furthestPoint;
            }

            private Vertex dealWithAlreadyProcessedVertex(Vertex previousVertex, int newTextureIndex, int newNormalIndex)
            {
                if (previousVertex.hasSameTextureAndNormal(newTextureIndex, newNormalIndex))
                {
                    indices.Add(previousVertex.getIndex());
                    return previousVertex;
                }
                else
                {
                    Vertex anotherVertex = previousVertex.getDuplicateVertex();
                    if (anotherVertex != null)
                    {
                        return dealWithAlreadyProcessedVertex(anotherVertex, newTextureIndex, newNormalIndex);
                    }
                    else
                    {
                        Vertex duplicateVertex = new Vertex(vertices.Count, previousVertex.getPosition(), previousVertex.getWeightsData());
                        duplicateVertex.setTextureIndex(newTextureIndex);
                        duplicateVertex.setNormalIndex(newNormalIndex);
                        previousVertex.setDuplicateVertex(duplicateVertex);
                        vertices.Add(duplicateVertex);
                        indices.Add(duplicateVertex.getIndex());
                        return duplicateVertex;
                    }

                }
            }

            private void initArrays()
            {
                this.verticesArray = new float[vertices.Count * 3];
                this.texturesArray = new float[vertices.Count * 2];
                this.normalsArray = new float[vertices.Count * 3];
                this.jointIdsArray = new int[vertices.Count * 3];
                this.weightsArray = new float[vertices.Count * 3];
            }

            private void removeUnusedVertices()
            {
                foreach (Vertex vertex in vertices)
                {
                    vertex.averageTangents();
                    if (!vertex.isSet())
                    {
                        vertex.setTextureIndex(0);
                        vertex.setNormalIndex(0);
                    }
                }
            }

        }

        public class SkeletonLoader
        {

            private XmlNode armatureData;

            private ListArray<string> boneOrder;

            private int jointCount = 0;

            public SkeletonLoader(XmlNode visualSceneNode, ListArray<string> boneOrder)
            {
                this.armatureData = visualSceneNode.First(x => x.Name == "visual_scene").First(x => x.Name == "node" && x.Attributes["id"].Value == "Armature");
                this.boneOrder = boneOrder;
            }

            public SkeletonData extractBoneData()
            {
                XmlNode headNode = armatureData.First(x => x.Name == "node");
                JointData headJoint = loadJointData(headNode, true);
                return new SkeletonData(jointCount, headJoint);
            }

            private JointData loadJointData(XmlNode jointNode, bool isRoot)
            {
                JointData joint = extractMainJointData(jointNode, isRoot);
                jointNode.All(x => x.Name == "node", (XmlNode child) => { joint.addChild(loadJointData(child, false)); });
                return joint;
            }

            private JointData extractMainJointData(XmlNode jointNode, bool isRoot)
            {
                string nameId = jointNode.Attributes["id"].Value;
                int index = -1;
                for (int i = 0; i < boneOrder.Count; i++)
                {
                    if (boneOrder[i] == nameId)
                    {
                        index = i;
                        break;
                    }
                }
                string[] matrixData = jointNode.First(x => x.Name == "matrix").InnerText.Split(' ');
                Matrix<float> matrix = new Matrix<float>(new float[,]
                {
                    { float.Parse(matrixData[0]), float.Parse(matrixData[1]), float.Parse(matrixData[2]), float.Parse(matrixData[3]), },
                    { float.Parse(matrixData[4]), float.Parse(matrixData[5]), float.Parse(matrixData[6]), float.Parse(matrixData[7]), },
                    { float.Parse(matrixData[8]), float.Parse(matrixData[9]), float.Parse(matrixData[10]), float.Parse(matrixData[11]), },
                    { float.Parse(matrixData[12]), float.Parse(matrixData[13]), float.Parse(matrixData[14]), float.Parse(matrixData[15]), },
                });
                //matrix.load(convertData(matrixData));
                matrix = matrix.Transpose();
                if (isRoot)
                {
                    //because in Blender z is up, but in our game y is up.
                    matrix = CORRECTION * matrix;
                }
                jointCount++;
                return new JointData(index, nameId, matrix);
            }

            //private FloatBuffer convertData(string[] rawData)
            //{
            //    float[] matrixData = new float[16];
            //    for (int i = 0; i < matrixData.Length; i++)
            //    {
            //        matrixData[i] = float.Parse(rawData[i]);
            //    }
            //    FloatBuffer buffer = BufferUtils.createFloatBuffer(16);
            //    buffer.put(matrixData);
            //    buffer.flip();
            //    return buffer;
            //}
        }

        public class SkinLoader
        {

            private XmlNode skinningData;
            private int maxWeights;

            public SkinLoader(XmlNode controllersNode, int maxWeights)
            {
                this.skinningData = controllersNode.First(x => x.Name == "controller").First(x => x.Name == "skin");
                this.maxWeights = maxWeights;
            }

            public SkinningData extractSkinData()
            {
                ListArray<string> jointsList = loadJointsList();
                float[] weights = loadWeights();
                XmlNode weightsDataNode = skinningData.First(x => x.Name == "vertex_weights");
                int[] effectorJointCounts = getEffectiveJointsCounts(weightsDataNode);
                ListArray<VertexSkinData> vertexWeights = getSkinData(weightsDataNode, effectorJointCounts, weights);
                return new SkinningData(jointsList, vertexWeights);
            }

            private ListArray<string> loadJointsList()
            {
                XmlNode inputNode = skinningData.First(x => x.Name == "vertex_weights");
                string jointDataId = inputNode.First(x => x.Name == "input" && x.Attributes["semantic"].Value == "JOINT").Attributes["source"].Value.Substring(1);
                XmlNode jointsNode = skinningData.First(x => x.Name == "source" && x.Attributes["id"].Value == jointDataId).First(x => x.Name == "Name_array");
                string[] names = jointsNode.InnerText.Split(' ');
                ListArray<string> jointsList = new ListArray<string>();
                foreach (string name in names)
                {
                    jointsList.Add(name);
                }
                return jointsList;
            }

            private float[] loadWeights()
            {
                XmlNode inputNode = skinningData.First(x => x.Name == "vertex_weights");
                string weightsDataId = inputNode.First(x => x.Name == "input" && x.Attributes["semantic"].Value == "WEIGHT").Attributes["source"].Value.Substring(1);
                XmlNode weightsNode = skinningData.First(x => x.Name == "source" && x.Attributes["id"].Value == weightsDataId).First(x => x.Name == "float_array");
                string[] rawData = weightsNode.InnerText.Split(' ');
                float[] weights = new float[rawData.Length];
                for (int i = 0; i < weights.Length; i++)
                {
                    weights[i] = float.Parse(rawData[i]);
                }
                return weights;
            }

            private int[] getEffectiveJointsCounts(XmlNode weightsDataNode)
            {
                string[] rawData = weightsDataNode.First(x => x.Name == "vcount").InnerText.Trim().Split(' ');
                int[] counts = new int[rawData.Length];
                for (int i = 0; i < rawData.Length; i++)
                {
                    counts[i] = int.Parse(rawData[i]);
                }
                return counts;
            }

            private ListArray<VertexSkinData> getSkinData(XmlNode weightsDataNode, int[] counts, float[] weights)
            {
                string[] rawData = weightsDataNode.First(x => x.Name == "v").InnerText.Split(' ');
                ListArray<VertexSkinData> skinningData = new ListArray<VertexSkinData>();
                int pointer = 0;
                foreach (int count in counts)
                {
                    VertexSkinData skinData = new VertexSkinData();
                    for (int i = 0; i < count; i++)
                    {
                        int jointId = int.Parse(rawData[pointer++]);
                        int weightId = int.Parse(rawData[pointer++]);
                        skinData.addJointEffect(jointId, weights[weightId]);
                    }
                    skinData.limitJointNumber(maxWeights);
                    skinningData.Add(skinData);
                }
                return skinningData;
            }

        }

        public class AnimationLoader
        {
            private XmlNode animationData;
            private XmlNode jointHierarchy;

            public AnimationLoader(XmlNode animationData, XmlNode jointHierarchy)
            {
                this.animationData = animationData;
                this.jointHierarchy = jointHierarchy;
            }

            public AnimationData extractAnimation()
            {
                string rootNode = findRootJointName();
                float[] times = getKeyTimes();
                float duration = times[times.Length - 1];
                KeyFrameData[] keyFrames = initKeyFrames(times);
                ListArray<XmlNode> animationNodes = new ListArray<XmlNode>();
                animationData.All(x => x.Name == "animation", (XmlNode child) => { animationNodes.Add(child); });
                foreach (XmlNode jointNode in animationNodes)
                {
                    loadJointTransforms(keyFrames, jointNode, rootNode);
                }
                return new AnimationData(duration, keyFrames);
            }

            private float[] getKeyTimes()
            {
                XmlNode timeData = animationData.First(x => x.Name == "animation").First(x => x.Name == "source").First(x => x.Name == "float_array");
                string[] rawTimes = timeData.InnerText.Split(' ');
                float[] times = new float[rawTimes.Length];
                for (int i = 0; i < times.Length; i++)
                {
                    times[i] = float.Parse(rawTimes[i]);
                }
                return times;
            }

            private KeyFrameData[] initKeyFrames(float[] times)
            {
                KeyFrameData[] frames = new KeyFrameData[times.Length];
                for (int i = 0; i < frames.Length; i++)
                {
                    frames[i] = new KeyFrameData(times[i]);
                }
                return frames;
            }

            private void loadJointTransforms(KeyFrameData[] frames, XmlNode jointData, string rootNodeId)
            {
                string jointNameId = getJointName(jointData);
                string dataId = getDataId(jointData);
                XmlNode transformData = jointData.First(x => x.Name == "source" && x.Attributes["id"].Value == dataId);
                string[] rawData = transformData.First(x => x.Name == "float_array").InnerText.Split(' ');
                processTransforms(jointNameId, rawData, frames, jointNameId.Equals(rootNodeId));
            }

            private string getDataId(XmlNode jointData)
            {
                XmlNode node = jointData.First(x => x.Name == "sampler").First(x => x.Name == "input" && x.Attributes["semantic"].Value == "OUTPUT");
                return node.Attributes["source"].Value.Substring(1);
            }

            private string getJointName(XmlNode jointData)
            {
                XmlNode channelNode = jointData.First(x => x.Name == "channel");
                return channelNode.Attributes["target"].Value.Split('/')[0];
            }

            private void processTransforms(string jointName, string[] rawData, KeyFrameData[] keyFrames, bool root)
            {
                //FloatBuffer buffer = BufferUtils.createFloatBuffer(16);
                for (int i = 0; i < keyFrames.Length; i++)
                {
                    float[] matrixData = new float[16];
                    for (int j = 0; j < 16; j++)
                    {
                        matrixData[j] = float.Parse(rawData[i * 16 + j]);
                    }
                    //buffer.clear();
                    //buffer.put(matrixData);
                    //buffer.flip();
                    //Matrix<float> transform = new Matrix<float>(4, 4);
                    Matrix<float> transform = new Matrix<float>(new float[,]
                    {
                        { matrixData[0], matrixData[1], matrixData[2], matrixData[3], },
                        { matrixData[4], matrixData[5], matrixData[6], matrixData[7], },
                        { matrixData[8], matrixData[9], matrixData[10], matrixData[11], },
                        { matrixData[12], matrixData[13], matrixData[14], matrixData[15], },
                    });
                    transform = transform.Transpose();
                    //transform.load(buffer);
                    //transform.transpose();
                    if (root)
                    {
                        //because up axis in Blender is different to up axis in game
                        transform = CORRECTION * transform;
                    }
                    keyFrames[i].addJointTransform(new JointTransformData(jointName, transform));
                }
            }

            private string findRootJointName()
            {
                XmlNode skeleton = jointHierarchy.First(x => x.Name == "visual_scene").First( x => x.Name == "node" && x.Attributes["id"].Value == "Armature");
                return skeleton.First(x => x.Name == "node").Attributes["id"].Value;
            }
        }

        public static AnimatedModelData LoadModel(string contents, int maxWeights)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(contents);

            SkinLoader skinLoader = new SkinLoader(document.First(x => x.Name == "library_controllers"), maxWeights);
            SkinningData skinningData = skinLoader.extractSkinData();

            SkeletonLoader jointsLoader = new SkeletonLoader(document.First(x => x.Name == "library_visual_scenes"), skinningData.jointOrder);
            SkeletonData jointsData = jointsLoader.extractBoneData();

            GeometryLoader g = new GeometryLoader(document.First(x => x.Name == "library_geometries"), skinningData.verticesSkinData);
            MeshData meshData = g.extractModelData();

            return new AnimatedModelData(meshData, jointsData);
        }

        public static AnimationData LoadAnimation(string contents)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(contents);
            XmlNode animNode = document.First(x => x.Name == "library_animations");
            XmlNode jointsNode = document.First(x => x.Name == "library_visual_scenes");
            AnimationLoader loader = new AnimationLoader(animNode, jointsNode);
            AnimationData animData = loader.extractAnimation();
            return animData;
        }
    }
}
