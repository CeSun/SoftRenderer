
using System.Drawing;
using System.Numerics;

namespace SoftRenderer;

public class Element
{
    public List<Vertex> Vertices;
    public List<int> Indexes;
    public Shader? Shader { get; set; }
    internal Element(Renderer renderer)
    {
        this.renderer = renderer;
    }

    Renderer renderer;

    public void Render()
    {
        if (Shader == null)
        {
            Shader = new Shader();
        }

        List<OutVertex> OutVertices = new List<OutVertex>();
        foreach(var vertex in Vertices)
        {
            var output = Shader.VertexShader(vertex);
            OutVertices.Add(output);
        }

        for(int i = 0; i < Indexes.Count; i += 3)
        {
            if (i + 2 >= Indexes.Count)
                break;
            Triangle(OutVertices[Indexes[i]], OutVertices[Indexes[i + 1]], OutVertices[Indexes[i + 2]]);
        }
    }

    private void Triangle(OutVertex T1, OutVertex T2, OutVertex T3)
    {
        // 背面剔除
        var normal = Vector3.Cross(T2.Position - T1.Position, T3.Position - T1.Position);
        if (normal.Z <= 0)
            return;
        // 标准空间外剔除
        if (T1.Position.X < -1 && T2.Position.X < -1 && T3.Position.X < -1)
            return;
        if (T1.Position.X > 1 && T2.Position.X > 1 && T3.Position.X > 1)
            return;
        if (T1.Position.Y < -1 && T2.Position.Y < -1 && T3.Position.Y < -1)
            return;
        if (T1.Position.Y > 1 && T2.Position.Y > 1 && T3.Position.Y > 1)
            return;
        if (T1.Position.Z < -1 && T2.Position.Z < -1 && T3.Position.Z < -1)
            return;
        if (T1.Position.Z > 1 && T2.Position.Z > 1 && T3.Position.Z > 1)
            return;
        // 光栅化到像素坐标
        Point TrueT1, TrueT2, TrueT3;
        TrueT1.X = (int)(((T1.Position.X + 1) / 2) * renderer.Size.X);
        TrueT1.Y = (int)(((T1.Position.Y * -1 + 1) / 2) * renderer.Size.Y);

        TrueT2.X = (int)(((T2.Position.X + 1) / 2) * renderer.Size.X);
        TrueT2.Y = (int)(((T2.Position.Y * -1 + 1) / 2) * renderer.Size.Y);

        TrueT3.X = (int)(((T3.Position.X + 1) / 2) * renderer.Size.X);
        TrueT3.Y = (int)(((T3.Position.Y * -1 + 1) / 2) * renderer.Size.Y);

        DrawTriangle(TrueT1, TrueT2, TrueT3, T1, T2, T3);

    }

    private void DrawTriangle(Point T1, Point T2, Point T3, OutVertex OutT1, OutVertex OutT2, OutVertex OutT3)
    {
        int Left, Top, Right, Bottom;
        Left = T1.X;
        if (T2.X < Left) Left = T2.X;
        if (T3.X < Left) Left = T3.X;
        Right = T1.X;
        if (T2.X > Right) Right = T2.X;
        if (T3.X > Right) Right = T3.X;
        Top = T1.Y;
        if (T2.Y < Top) Top = T2.Y;
        if (T3.Y < Top) Top = T3.Y;
        Bottom = T1.Y;
        if (T2.Y > Bottom) Bottom = T2.Y;
        if (T3.Y > Bottom) Bottom = T3.Y;

        for (int x = Left; x < Right; x++)
        {
            for (int y = Top; y < Bottom; y++)
            {
                var uv = BaryCentric(T1, T2, T3, new Point(x, y));
                if (uv.X < 0 || uv.Y < 0 || uv.Z < 0)
                    continue;
                if (x >= renderer.Size.X || x < 0)
                    continue;
                if (y >= renderer.Size.Y || y < 0)
                    continue;
                var InVertex = new OutVertex();
                InVertex.Position = OutT1.Position * uv.X + OutT2.Position * uv.Y + OutT3.Position * uv.Z;
                InVertex.Color = OutT1.Color * uv.X + OutT2.Color * uv.Y + OutT3.Color * uv.Z;
                InVertex.Coord = OutT1.Coord * uv.X + OutT2.Coord * uv.Y + OutT3.Coord * uv.Z;
                var Depth = ((-1 * InVertex.Position.Z) + 1) / 2;
                var Color = Shader.FragmentShader(InVertex);
                // 深度测试
                if (Depth <= renderer.DepthBuffer[x, y] || renderer.DepthBuffer[x, y] == -1)
                {
                    var NewColor = new Vector4
                    {
                        X = Color.X * Color.W,
                        Y = Color.Y * Color.W,
                        Z = Color.Z * Color.W
                    };
                    var SourceColor = new Vector4
                    {
                        X = renderer.ColorBuffer[x, y].X * (1 - Color.W),
                        Y = renderer.ColorBuffer[x, y].Y * (1 - Color.W),
                        Z = renderer.ColorBuffer[x, y].Z * (1 - Color.W)
                    };
                    renderer.ColorBuffer[x, y] = NewColor + SourceColor;
                    renderer.ColorBuffer[x, y].W = 1;
                    renderer.DepthBuffer[x, y] = Depth;
                }

            }
        }
    }


    Vector3 BaryCentric(Point T1, Point T2, Point T3, Point P)
    {
        Vector3 X, Y;
        X = new Vector3(T3.X - T1.X, T2.X - T1.X, T1.X - P.X);
        Y = new Vector3(T3.Y - T1.Y, T2.Y - T1.Y, T1.Y - P.Y);
        Vector3 U = Vector3.Cross(X, Y);
        if (Math.Abs(U.Z) > 1e-2)
        {
            return new Vector3(1f - (U.X + U.Y) / U.Z, U.Y / U.Z, U.X / U.Z);
        }
        return new Vector3(-1, 1, 1);
    }


}

public struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X;
    public int Y;
}
public class Vertex
{
    public Vector3 Position;
    public Vector4 Color;
    public Vector2 Coord;
}

public class OutVertex : Vertex
{

}