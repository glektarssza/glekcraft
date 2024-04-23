namespace Glekcraft;

using System.Drawing;
using System.Linq;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public static class Program {
    /// <summary>
    /// The vertex array object.
    /// </summary>
    public static uint VAO {
        get;
        private set;
    }

    public static uint VertexBuffer {
        get;
        private set;
    }

    public static uint ColorBuffer {
        get;
        private set;
    }

    public static uint IndexBuffer {
        get;
        private set;
    }

    public static uint VertexShader {
        get;
        private set;
    }

    public static uint FragmentShader {
        get;
        private set;
    }

    public static uint ShaderProgram {
        get;
        private set;
    }

    public static Matrix4X4<float> ModelMatrix {
        get;
        private set;
    }

    public static Matrix4X4<float> ViewMatrix {
        get;
        private set;
    }

    public static Matrix4X4<float> ProjectionMatrix {
        get;
        private set;
    }

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

        MainWindow.MakeCurrent();
        VAO = GraphicsContext.GenVertexArray();
        GraphicsContext.BindVertexArray(VAO);
        VertexBuffer = GraphicsContext.GenBuffer();
        GraphicsContext.BindBuffer(BufferTargetARB.ArrayBuffer, VertexBuffer);
        GraphicsContext.BufferData(BufferTargetARB.ArrayBuffer, [
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            0f, 0.5f, 0f
        ], BufferUsageARB.StaticDraw);

        ColorBuffer = GraphicsContext.GenBuffer();
        GraphicsContext.BindBuffer(BufferTargetARB.ArrayBuffer, ColorBuffer);
        GraphicsContext.BufferData<byte>(BufferTargetARB.ArrayBuffer, [
            255, 0, 0,
            0, 255, 0,
            0, 0, 255
        ], BufferUsageARB.StaticDraw);

        IndexBuffer = GraphicsContext.GenBuffer();
        GraphicsContext.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
        GraphicsContext.BufferData<ushort>(BufferTargetARB.ElementArrayBuffer, [
            0, 1, 2
        ], BufferUsageARB.StaticDraw);

        VertexShader = GraphicsContext.CreateShader(ShaderType.VertexShader);
        GraphicsContext.ShaderSource(VertexShader, @"
#version 410

layout(location = 0) in vec3 vs_vertex;
layout(location = 1) in vec3 vs_color;

uniform mat4 vs_modelMatrix;
uniform mat4 vs_viewMatrix;
uniform mat4 vs_projectionMatrix;

layout(location = 0) out vec3 fs_color;

void main() {
    fs_color = vs_color;
    mat4 mvpMatrix = vs_projectionMatrix * vs_viewMatrix * vs_modelMatrix;
    gl_Position = mvpMatrix * vec4(vs_vertex, 1.0);
}
");
        GraphicsContext.CompileShader(VertexShader);

        Console.Out.WriteLine("=== Vertex Shader Info Log ===");
        Console.Out.WriteLine(GraphicsContext.GetShaderInfoLog(VertexShader));

        FragmentShader = GraphicsContext.CreateShader(ShaderType.FragmentShader);
        GraphicsContext.ShaderSource(FragmentShader, @"
#version 410

layout(location = 0) in vec3 fs_color;

out vec4 fragColor;

void main() {
    fragColor = vec4(fs_color, 1.0);
}
");
        GraphicsContext.CompileShader(FragmentShader);

        Console.Out.WriteLine("=== Fragment Shader Info Log ===");
        Console.Out.WriteLine(GraphicsContext.GetShaderInfoLog(FragmentShader));

        ShaderProgram = GraphicsContext.CreateProgram();
        GraphicsContext.AttachShader(ShaderProgram, VertexShader);
        GraphicsContext.AttachShader(ShaderProgram, FragmentShader);
        GraphicsContext.LinkProgram(ShaderProgram);

        Console.Out.WriteLine("=== Shader Program Info Log ===");
        Console.Out.WriteLine(GraphicsContext.GetProgramInfoLog(ShaderProgram));
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

        ModelMatrix = Matrix4X4<float>.Identity;

        ViewMatrix = Matrix4X4.CreateLookAt<float>(new(0, 0, 1), new(0, 0, 0), new(0, 1, 0));

        ProjectionMatrix = Matrix4X4.CreatePerspectiveFieldOfView(Scalar.DegreesToRadians(90f), MainWindow.Size.X / MainWindow.Size.Y, 0.001f, 1000f);
    }

    public static void OnWindowRender(double delta) {
        if (MainWindow == null || GraphicsContext == null) {
            throw new InvalidOperationException();
        }
        MainWindow.MakeCurrent();
        _ = GraphicsContext.GetError();
        GraphicsContext.Viewport(MainWindow.FramebufferSize);
        GraphicsContext.ClearColor(Color.CornflowerBlue);
        GraphicsContext.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GraphicsContext.UseProgram(ShaderProgram);
        GraphicsContext.BindVertexArray(VAO);
        GraphicsContext.EnableVertexAttribArray(0);
        GraphicsContext.EnableVertexAttribArray(1);
        GraphicsContext.BindBuffer(BufferTargetARB.ArrayBuffer, VertexBuffer);
        unsafe {
            GraphicsContext.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, null);
        }
        GraphicsContext.BindBuffer(BufferTargetARB.ArrayBuffer, ColorBuffer);
        unsafe {
            GraphicsContext.VertexAttribPointer(1, 3, VertexAttribPointerType.UnsignedByte, true, 0, null);
        }

        unsafe {
            var mm = ModelMatrix.ToSystem();
            GraphicsContext.UniformMatrix4(GraphicsContext.GetUniformLocation(ShaderProgram, "vs_modelMatrix"), 1, false, (float*)&mm);
        }
        unsafe {
            var mm = ViewMatrix.ToSystem();
            GraphicsContext.UniformMatrix4(GraphicsContext.GetUniformLocation(ShaderProgram, "vs_viewMatrix"), 1, false, (float*)&mm);
        }
        unsafe {
            var mm = ProjectionMatrix.ToSystem();
            GraphicsContext.UniformMatrix4(GraphicsContext.GetUniformLocation(ShaderProgram, "vs_projectionMatrix"), 1, false, (float*)&mm);
        }

        GraphicsContext.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
        unsafe {
            GraphicsContext.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedShort, null);
        }
        GraphicsContext.DisableVertexAttribArray(1);
        GraphicsContext.DisableVertexAttribArray(0);
    }
}
