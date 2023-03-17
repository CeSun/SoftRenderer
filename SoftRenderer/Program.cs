
using Avalonia;
using Avalonia.Media;
using SoftRenderer;
using SoftRenderer.View;
using System.Numerics;




var Renderer = new Renderer(400, 400);
var element = Renderer.CreateElement();
List<Vertex> vertices= new List<Vertex>();
List<int> indexes = new List<int>();

vertices.Add(new Vertex { Position = new(-1f, 1f, 0), Color = new Vector4(1, 0, 0, 1) });
vertices.Add(new Vertex { Position = new(-1f, -0.5f, 0), Color = new Vector4(0, 1, 0, 1) });
vertices.Add(new Vertex { Position = new(0.5f, -0.5f, 0), Color = new Vector4(0, 0, 1, 1) });


indexes.Add(0);
indexes.Add(1);
indexes.Add(2);


element.Vertices = vertices;
element.Indexes = indexes;

Renderer.ClearColor = Color.FromArgb(255, 200, 200, 200);
Renderer.Clear(ClearFlag.Color);
RenderWindow.Instance.Render += DeltaTime =>
{
    element.Render();
    Renderer.OutPutBuffer();
};
RenderWindow.Instance.Run(400, 400);