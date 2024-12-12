namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A representation of a window.
/// </summary>
public class Window : IDisposable {
    #region Internal Properties

    /// <summary>
    /// The handle to the native window.
    /// </summary>
    internal IntPtr Handle {
        get;
        private set;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The library instance that is managing this instance.
    /// </summary>
    public LibGLFW Library {
        get;
    }

    /// <summary>
    /// Whether this instance has been disposed.
    /// </summary>
    public bool IsDisposed {
        get;
        private set;
    }

    /// <summary>
    /// Whether this instance is valid.
    /// </summary>
    public bool IsValid =>
        !Library.IsDisposed && Library.IsCurrentInstance && !IsDisposed;

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="library">
    /// The library instance that will manage the new instance.
    /// </param>
    /// <param name="handle">
    /// The handle to the native window that the new instance will wrap.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    /// Thrown if the <paramref name="library" /> instance has been disposed.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <paramref name="library" /> instance is not the instance
    /// that is currently managing the native library.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="handle" /> is a null pointer.
    /// </exception>
    internal Window(LibGLFW library, IntPtr handle) {
        if (library.IsDisposed) {
            throw new ObjectDisposedException(nameof(LibGLFW), "Library instance has been disposed");
        }
        if (!library.IsCurrentInstance) {
            throw new InvalidOperationException("Library instance is not the current instance");
        }
        if (handle == IntPtr.Zero) {
            throw new ArgumentException("Window handle is not valid", nameof(handle));
        }
        Library = library;
        Handle = handle;
        // TODO: Register event handlers
    }

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~Window() =>
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

    /// <inheritdoc />
    public override bool Equals(object? obj) {
        if (obj is Window window) {
            return window.Handle == Handle;
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode() =>
        Handle.GetHashCode();

    #endregion

    #region Private Methods

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    /// <param name="managed">
    /// Whether this instance is being disposed from managed code or from
    /// unmanaged code (e.g. the garbage collector).
    /// </param>
    private void Dispose(bool managed) {
        if (IsDisposed) {
            return;
        }
        if (managed) {
            // TODO
        }
        Handle = IntPtr.Zero;
        IsDisposed = true;
    }

    #endregion
}
