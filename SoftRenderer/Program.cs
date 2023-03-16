
using SoftRenderer;
using System.Numerics;

var Renderer = new Renderer(100, 100);

var element = Renderer.CreateElement();

List<Vertex> vertices= new List<Vertex>();
List<int> indexes = new List<int>();

vertices.Add(new Vertex { Position = new(0f, 1f, 0), Color = new Vector4(1, 0, 0, 1) });
vertices.Add(new Vertex { Position = new(-1f, -1f, 0), Color = new Vector4(0, 1, 0, 1) });
vertices.Add(new Vertex { Position = new(1f, -1f, 0), Color = new Vector4(0, 0, 1, 1) });

indexes.Add(0);
indexes.Add(1);
indexes.Add(2);


element.Vertices = vertices;
element.Indexes = indexes;

element.Render();

Renderer.OutPutToFile("f:/4567.TGA");