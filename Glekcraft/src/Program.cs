namespace Glekcraft;

using System.Drawing;

using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public sealed class Program {
    /// <summary>
    /// The program entry point.
    /// </summary>
    public static void Main() {
        var windowOptions = WindowOptions.Default;
        windowOptions.Size = new(640, 480);
        windowOptions.Title = "Glekcraft";
        windowOptions.WindowClass = "Glekcraft";
        windowOptions.WindowBorder = WindowBorder.Resizable;
        windowOptions.WindowState = WindowState.Normal;
        windowOptions.IsVisible = false;
        windowOptions.FramesPerSecond = 60;
        windowOptions.UpdatesPerSecond = 60;
        windowOptions.ShouldSwapAutomatically = false;
        windowOptions.Samples = 1;
        var window = Window.Create(windowOptions);
        IInputContext? input = null;
        GL? gl = null;
        window.Load += () => {
            window.Center();
            window.IsVisible = true;
            input = window.CreateInput();
            gl = GL.GetApi(window);
            gl.ClearColor(Color.CornflowerBlue);
        };
        window.Update += (deltaTime) => {
            for (var i = 0; i < (input?.Keyboards.Count ?? 0); i++) {
                var keyboard = input?.Keyboards[i];
                if (keyboard?.IsKeyPressed(Key.Escape) ?? false) {
                    window.Close();
                }
            }
        };
        window.Render += (deltaTime) => {
            gl?.Viewport(0, 0, (uint)window.Size.X, (uint)window.Size.Y);
            gl?.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // TODO
            window.SwapBuffers();
        };
        window.Closing += () => {
            gl?.Dispose();
            gl = null;
        };
        window.Run();
    }
}
