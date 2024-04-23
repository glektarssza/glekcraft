namespace Glekcraft;

using System.Drawing;
using System.Linq;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public static class Program {
    /// <summary>
    /// The main game window.
    /// </summary>
    public static IWindow? MainWindow {
        get;
        private set;
    }

    /// <summary>
    /// The main input context.
    /// </summary>
    public static IInputContext? InputContext {
        get;
        private set;
    }

    /// <summary>
    /// The main graphics context.
    /// </summary>
    public static GL? GraphicsContext {
        get;
        private set;
    }

    /// <summary>
    /// The program entry point.
    /// </summary>
    /// <returns>
    /// A status code for the operating system.
    /// </returns>
    public static int Main() {
        var graphicsOptions = new GraphicsAPI {
            API = ContextAPI.OpenGL,
            Flags = ContextFlags.ForwardCompatible,
            Profile = ContextProfile.Core,
            Version = new(4, 1)
        };
        var windowOptions = new WindowOptions {
            API = graphicsOptions,
            FramesPerSecond = 60,
            UpdatesPerSecond = 60,
            IsVisible = false,
            Position = new(-1, -1),
            Samples = 0,
            ShouldSwapAutomatically = true,
            Size = new(1280, 720),
            Title = "G'lekcraft",
            TopMost = true,
            VSync = false,
            WindowBorder = WindowBorder.Resizable,
            WindowClass = "G'lekcraft",
            WindowState = WindowState.Normal
        };
        try {
            MainWindow = Window.Create(windowOptions);
        } catch (Exception ex) {
            Console.Error.WriteLine("Fatal error during startup:");
            Console.Error.WriteLine(ex);
            return (int)ExitCode.Failure;
        }
        MainWindow.Load += OnWindowLoad;
        MainWindow.Update += OnWindowUpdate;
        MainWindow.Render += OnWindowRender;
        try {
            MainWindow.Run();
        } catch (Exception ex) {
            Console.Error.WriteLine("Fatal error while running:");
            Console.Error.WriteLine(ex);
            return (int)ExitCode.Failure;
        }
        return (int)ExitCode.Success;
    }

    public static void OnWindowLoad() {
        if (MainWindow == null) {
            throw new InvalidOperationException();
        }
        InputContext = MainWindow.CreateInput();
        GraphicsContext = MainWindow.CreateOpenGL();
        MainWindow.Size = new(1280, 720);
        MainWindow.Center();
        MainWindow.IsVisible = true;
    }

    public static void OnWindowUpdate(double delta) {
        if (MainWindow == null || InputContext == null) {
            throw new InvalidOperationException();
        }
        var escapePressed = InputContext.Keyboards
            .Where((kb) => kb.IsConnected)
            .Select((kb) => kb.IsKeyPressed(Key.Escape))
            .Contains(true);
        if (escapePressed) {
            MainWindow.Close();
        }
    }

    public static void OnWindowRender(double delta) {
        if (MainWindow == null || GraphicsContext == null) {
            throw new InvalidOperationException();
        }
        MainWindow.MakeCurrent();
        GraphicsContext.Viewport(MainWindow.FramebufferSize);
        GraphicsContext.ClearColor(Color.CornflowerBlue);
        GraphicsContext.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }
}
