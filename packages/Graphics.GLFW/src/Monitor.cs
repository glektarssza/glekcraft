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
    /// The position of the instance in the virtual desktop.
    /// </summary>
    public (int x, int y)? Position {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorPosition(Handle);
        }
    }

    /// <summary>
    /// The work area of the monitor in the virtual desktop.
    /// </summary>
    public (int x, int y, int width, int height)? WorkArea {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorWorkArea(Handle);
        }
    }

    /// <summary>
    /// The physical size of the instance.
    /// </summary>
    public (int widthMM, int heightMM)? PhysicalSize {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorPhysicalSize(Handle);
        }
    }

    /// <summary>
    /// The content scale of the instance.
    /// </summary>
    public (float x, float y)? ContentScale {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorContentScale(Handle);
        }
    }

    /// <summary>
    /// The user pointer of the instance.
    /// </summary>
    public IntPtr? UserPointer {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetMonitorUserPointer(Handle);
        }
        set {
            if (!IsValid) {
                return;
            }
            if (value == null) {
                Library.NativeApi.SetMonitorUserPointer(Handle, IntPtr.Zero);
                return;
            }
            Library.NativeApi.SetMonitorUserPointer(Handle, value == null ? IntPtr.Zero : value.Value);
        }
    }

    /// <summary>
    /// The current video mode of this instance.
    /// </summary>
    public VideoMode? VideoMode {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetVideoMode(Handle);
        }
    }

    /// <summary>
    /// The video modes this instance supports.
    /// </summary>
    public VideoMode[] SupportedVideoModes {
        get {
            if (!IsValid) {
                return [];
            }
            return Library.NativeApi.GetVideoModes(Handle);
        }
    }

    /// <summary>
    /// The gamma ramp this instance is configured with.
    /// </summary>
    public GammaRamp? GammaRamp {
        get {
            if (!IsValid) {
                return null;
            }
            return Library.NativeApi.GetGammaRamp(Handle);
        }
        set {
            if (!IsValid) {
                return;
            }
            if (value == null) {
                return;
            }
            Library.NativeApi.SetGammaRamp(Handle, value.Value);
        }
    }

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

    #region Public Methods

    /// <summary>
    /// Set the gamma value for this instance.
    /// </summary>
    /// <param name="gamma">
    /// The gamma to configure this instance with.
    /// </param>
    public void SetGamma(float gamma) {
        if (!IsValid) {
            return;
        }
        Library.NativeApi.SetGamma(Handle, gamma);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) {
        if (obj is Monitor monitor) {
            return monitor.Handle == Handle;
        }
        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Handle.GetHashCode();

    #endregion
}
