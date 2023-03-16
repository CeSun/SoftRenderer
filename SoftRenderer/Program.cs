
using SoftRenderer;
using System.Drawing;
using System.Numerics;

var Renderer = new Renderer(800, 600);

var element = Renderer.CreateElement();
List<Vertex> vertices= new List<Vertex>();
List<int> indexes = new List<int>();

vertices.Add(new Vertex { Position = new(0f, 0.5f, 0), Color = new Vector4(1, 0, 0, 1) });
vertices.Add(new Vertex { Position = new(-0.5f, -0.5f, 0), Color = new Vector4(1, 0, 0, 1) });
vertices.Add(new Vertex { Position = new(0.5f, -0.5f, 0), Color = new Vector4(1, 0, 0, 1) });


vertices.Add(new Vertex { Position = new(0f, 0.3f, 0.5f), Color = new Vector4(0, 1, 0, 0.5f) });
vertices.Add(new Vertex { Position = new(-0.5f, -0.1f, 0.5f), Color = new Vector4(0, 1, 0, 0.5f) });
vertices.Add(new Vertex { Position = new(0.5f, -0.3f, 0.5f), Color = new Vector4(0, 1, 0, 0.5f) });

indexes.Add(0);
indexes.Add(1);
indexes.Add(2);
indexes.Add(3);
indexes.Add(4);
indexes.Add(5);


element.Vertices = vertices;
element.Indexes = indexes;

Renderer.ClearColor = Color.White;
Renderer.Clear(ClearFlag.Color);
element.Render();

Renderer.OutPutToFile("f:/4567.TGA");