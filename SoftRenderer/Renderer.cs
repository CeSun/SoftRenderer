using Avalonia.Media;
using SoftRenderer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;
public enum ClearFlag
{
    Color = (1 << 0),
    Depth = (1 << 1),
}
public class Renderer
{

    public Point Size;
    public Renderer(int x, int y)
    {
        ColorBuffer = new Vector4[x,y];
        DepthBuffer = new float[x, y];
        for(int i = 0; i < x;i++)
        {
            for (int j = 0; j < y;j++)
            {
                DepthBuffer[i, j] = 1;
            }
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                ColorBuffer[i, j] = new Vector4(0, 0, 0, 1);
            }
        }
        Size.X = x;
        Size.Y = y;
    }

    public Color ClearColor { get; set; }
    public void Clear(ClearFlag ClearFlag)
    {

        if ((ClearFlag | ClearFlag.Color) == ClearFlag.Color)
        {
            Vector4 vector = new Vector4
            {
                X = ClearColor.R / 255.0F,
                Y = ClearColor.G / 255.0F,
                Z = ClearColor.B / 255.0F,
                W = ClearColor.A / 255.0F,

            };
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    ColorBuffer[i, j] = vector;
                    SetColor(i, j, vector);
                }
            }
        }
        if ((ClearFlag | ClearFlag.Depth) == ClearFlag.Depth)
        {
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    DepthBuffer[i, j] = 1;
                }
            }
        }
    }

    
    public Vector4[,] ColorBuffer { get; set; }
    public float[,] DepthBuffer { get; set; }
    public Element CreateElement()
    {
        return new Element(this);
    }

    public void SetColor(int x, int y, Vector4 Color)
    {
        SetColor_Window(x, y, Color);
        SetColor_Buffer(x, y, Color);
    }

    public Vector4 GetColor(int x, int y)
    {
        return ColorBuffer[x, y];
    }
    private void SetColor_Buffer(int x, int y, Vector4 Color)
    {
        ColorBuffer[x, y] = Color;
    }

    private void SetColor_Window(int x, int y, Vector4 color)
    {
        RenderWindow.Instance.SetPixel(x, y, Color.FromArgb((byte)(color.W * 255), (byte)(color.X * 255), (byte)(color.Y * 255), (byte)(color.Z * 255)));
    }
    public void OutPutBuffer()
    {


        for (int x = 0; x < Size.X; x++)
        {
            for (int y = 0; y < Size.Y; y++)
            {
                var color = ColorBuffer[x, y];
                RenderWindow.Instance.SetPixel(x, y, Color.FromArgb((byte)(color.W * 255), (byte)(color.X * 255), (byte)(color.Y * 255), (byte)(color.Z * 255)));
            }
        }

    }


}
