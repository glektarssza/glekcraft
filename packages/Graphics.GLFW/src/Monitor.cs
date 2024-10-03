namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A representation of a monitor.
/// </summary>
public sealed class Monitor {
    #region Internal Properties

    /// <summary>
    /// The handle to the native monitor.
    /// </summary>
    internal IntPtr Handle {
        get;
        private set;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The name of this instance.
    /// </summary>
    public string? Name {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorName(Handle);
        }
    }

    /// <summary>
    /// Whether this instance is the primary monitor.
    /// </summary>
    public bool IsPrimary {
        get {
            if (!IsValid) {
                return false;
            }
            return Library.NativeApi.GetPrimaryMonitor() == Handle;
        }
    }

    /// <summary>
    /// The library instance that is managing this instance.
    /// </summary>
    public LibGLFW Library {
        get;
    }

    /// <summary>
    /// Whether this instance is valid.
    /// </summary>
    public bool IsValid =>
        !Library.IsDisposed && Library.IsCurrentInstance && Handle != IntPtr.Zero;

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="library">
    /// The library instance that will manage the new instance.
    /// </param>
    /// <param name="handle">
    /// The handle to the native monitor that the new instance will wrap.
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
    internal Monitor(LibGLFW library, IntPtr handle) {
        if (library.IsDisposed) {
            throw new ObjectDisposedException(nameof(LibGLFW), "Library instance has been disposed");
        }
        if (!library.IsCurrentInstance) {
            throw new InvalidOperationException("Library instance is not the current instance");
        }
        if (handle == IntPtr.Zero) {
            throw new ArgumentException("Monitor handle is not valid", nameof(handle));
        }
        Library = library;
        Handle = handle;
    }

    #endregion
}
