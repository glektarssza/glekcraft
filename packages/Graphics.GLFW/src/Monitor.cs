namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A GLFW monitor.
/// </summary>
public class Monitor {
    #region Public Properties

    /// <summary>
    /// The library instance that owns this instance.
    /// </summary>
    public LibGLFW LibraryInstance {
        get;
    }

    /// <summary>
    /// The handle wrapped by this instance.
    /// </summary>
    public IntPtr Handle {
        get;
    }

    /// <summary>
    /// Whether this instance is valid.
    /// </summary>
    public bool IsValid =>
        Handle != IntPtr.Zero;

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="libraryInstance">
    /// The library instance that will own the new instance.
    /// </param>
    /// <param name="handle">
    /// The handle to wrap in the new instance.
    /// </param>
    /// <exception cref="ObjectDisposedException">
    /// Thrown if the provided library instance is disposed.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the provided library instance is not the instance that is
    /// currently managing the native library.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided handle is invalid.
    /// </exception>
    internal Monitor(LibGLFW libraryInstance, IntPtr handle) {
        if (libraryInstance.IsDisposed) {
            throw new ObjectDisposedException(nameof(LibGLFW), "The provided library instance is disposed");
        }
        if (!libraryInstance.IsCurrentInstance) {
            throw new InvalidOperationException("The provided library instance is not the current instance");
        }
        if (handle == IntPtr.Zero) {
            throw new ArgumentException("The provided handle is invalid", nameof(handle));
        }
        LibraryInstance = libraryInstance;
        Handle = handle;
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override bool Equals(object? obj) {
        if (obj is Monitor monitor) {
            return Handle == monitor.Handle;
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode() =>
        Handle.GetHashCode();

    #endregion
}
