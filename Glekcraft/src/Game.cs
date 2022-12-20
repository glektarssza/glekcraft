namespace Glekcraft;

using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// The main game class.
/// </summary>
public sealed class Game : IDisposable {
    #region Public Properties

    /// <summary>
    /// The main input context.
    /// </summary>
    public IInputContext? Input {
        get;
        private set;
    }

    /// <summary>
    /// The renderer.
    /// </summary>
    public Renderer? Renderer {
        get;
        private set;
    }

    /// <summary>
    /// The main game window.
    /// </summary>
    public IWindow? Window {
        get;
        private set;
    }

    /// <summary>
    /// Whether this instance has been disposed.
    /// </summary>
    public bool IsDisposed {
        get;
        private set;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public Game() {
        //-- Does nothing
    }

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~Game() =>
        Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    public void Initialize() {
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
        Window = Silk.NET.Windowing.Window.Create(windowOptions);
        Window.Load += () => {
            Window.Center();
            Window.IsVisible = true;
            Input = Window.CreateInput();
            Renderer = new(this, GL.GetApi(Window));
        };
        Window.Update += (deltaTime) => {
            for (var i = 0; i < (Input?.Keyboards.Count ?? 0); i++) {
                var keyboard = Input?.Keyboards[i];
                if (keyboard?.IsKeyPressed(Key.Escape) ?? false) {
                    Window.Close();
                }
            }
        };
        Window.Render += (deltaTime) => {
            Renderer?.GL.Viewport(0, 0, (uint)Window.Size.X, (uint)Window.Size.Y);
            Renderer?.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // TODO
            Window.SwapBuffers();
        };
        Window.Closing += () => {
            Input?.Dispose();
            Input = null;
            Renderer?.Dispose();
            Renderer = null;
        };
    }

    /// <summary>
    /// Run the game.
    /// </summary>
    public void Run() =>
        Window?.Run();

    /// <summary>
    /// Dispose this instance.
    /// </summary>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Dispose this instance.
    /// </summary>
    /// <param name="managed">
    /// Whether this method is being called from managed code or from unmanaged
    /// code (e.g. the garbage collector).
    /// </param>
    private void Dispose(bool managed) {
        if (IsDisposed) {
            return;
        }
        if (managed) {
            Renderer?.Dispose();
            Renderer = null;
            Window?.Dispose();
            Window = null;
        }
        IsDisposed = true;
    }

    #endregion
}
