using Theta.Mathematics;

namespace Theta.Graphics.Formats
{
    public class Vertex
    {
        public Vector<float> Position { get; set; }
        public Vector<float> Normal { get; set; }
        public Vector<float> Tangent { get; set; }
        public Vector<float> BitTangent { get; set; }
        public Vector<float>[] TextureCoordinates { get; set; }
        public Color[] Colors { get; set; }
    }
}
