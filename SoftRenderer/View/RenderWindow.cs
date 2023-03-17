using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer.View;

public class RenderWindow
{
    public event Action? Init;
    public event Action? Closing;
    public event Action<float>? Render;
    Renderer renderer;
    internal static RenderWindow? _Instance;

    internal Action<int , int , Color>? Print;

    public void SetPixel(int X, int Y, Color color)
    {
        Print?.Invoke(X, Y, color); 
    }

    public static RenderWindow Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new RenderWindow();
            }
            return _Instance;
        }
    }
    internal Point Size;
    public RenderWindow()
    {
    }

    internal void RenderInternal(float DeltaTime)
    {
        Render?.Invoke(DeltaTime);
    }

    internal void ClosingInternal()
    {
        Closing?.Invoke();
    }

    internal void InitInternal()
    {
        Init?.Invoke();
    }
    public void Run(Renderer renderer)
    {
        this.renderer = renderer;
        this.Size = new Point(renderer.Size.X, renderer.Size.Y);
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(new string[] { });
    }
    AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
                    .UsePlatformDetect()
                    .LogToTrace();
    }
}
