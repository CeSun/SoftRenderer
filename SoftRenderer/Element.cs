using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;

public class Element
{
    Shader Shader { get; set; }
    internal Element(Renderer renderer)
    {
        this.renderer = renderer;
    }

    Renderer renderer;

    public void Render()
    {

    }
}
