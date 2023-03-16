using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;

public class Shader
{
    public virtual OutVertex VertexShader(Vertex Vertex)
    {
        return new OutVertex() {Color = Vertex.Color, Coord = Vertex.Coord, Position = Vertex.Position};
    }

    public virtual Vector4 FragmentShader(OutVertex Vertex)
    {
        return Vertex.Color;
    }
}
