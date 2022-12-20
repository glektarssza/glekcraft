namespace Glekcraft;

using Silk.NET.OpenGL;

public class Renderer : IDisposable {
    #region Public Properties

    /// <summary>
    /// Whether this instance has been disposed.
    /// </summary>
    public bool IsDisposed {
        get;
        private set;
    }

    /// <summary>
    /// The OpenGL context.
    /// </summary>
    public GL GL {
        get;
        private set;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="gl">
    /// The OpenGL context.
    /// </param>
    public Renderer(GL gl) =>
        GL = gl;

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~Renderer() =>
        Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Dispose of this instance.
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
            GL.Dispose();
        }
        IsDisposed = true;
    }

    #endregion
}
