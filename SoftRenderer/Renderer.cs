using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;

public class Renderer
{
    public Renderer(int x, int y)
    {
        ColorBuffer = new float[x * y, 4];
        DepthBuffer = new float[x * y];
    }
    public float[,] ColorBuffer { get; set; }
    public float[] DepthBuffer { get; set; }
    public Element CreateElement()
    {
        return new Element(this);
    }

    public void OutPutToFile(string filename)
    {

    }



}
