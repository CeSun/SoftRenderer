
using Avalonia;
using Avalonia.Media;
using SoftRenderer;
using SoftRenderer.View;
using System.Numerics;




var Renderer = new Renderer(1024, 1024);
var element = Renderer.CreateElement();
List<Vertex> vertices= new List<Vertex>();
List<int> indexes = new List<int>();

vertices.Add(new Vertex { Position = new(0.5f, 0.5f, 0.0f), Color = new Vector4(1.0f, 0.0f, 0.0f, 1f), Coord = new Vector2(1.0f, 1.0f) });
vertices.Add(new Vertex { Position = new(0.5f, -0.5f, 0.0f), Color = new Vector4(0.0f, 1.0f, 0.0f, 1), Coord = new Vector2(1.0f, 0.0f) });
vertices.Add(new Vertex { Position = new(-0.5f, -0.5f, 0.0f), Color = new Vector4(0.0f, 0.0f, 1.0f, 1), Coord = new Vector2(0.0f, 0.0f) });
vertices.Add(new Vertex { Position = new(-0.5f, 0.5f, 0.0f), Color = new Vector4(1.0f, 1.0f, 0.0f, 1), Coord = new Vector2(0.0f, 1.0f) });


indexes.Add(0);
indexes.Add(1);
indexes.Add(3);
indexes.Add(1);
indexes.Add(2);
indexes.Add(3);

var texture0 = Texture.LoadTexture("./Image/container.jpg");
var texture1 = Texture.LoadTexture("./Image/awesomeface.png");

Renderer.Textures[0] = texture0;
Renderer.Textures[1] = texture1;

element.Vertices = vertices;
element.Indexes = indexes;

Renderer.ClearColor = Color.FromArgb(255, 200, 200, 200);
Renderer.Clear(ClearFlag.Color);
RenderWindow.Instance.Render += DeltaTime =>
{
    element.Render();
    Renderer.OutPutBuffer();
};
RenderWindow.Instance.Run(Renderer);