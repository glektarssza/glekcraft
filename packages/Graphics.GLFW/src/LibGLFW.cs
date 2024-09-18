namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// The main class for interacting with the GLFW library.
/// </summary>
public sealed class LibGLFW : IDisposable {
    #region Internal Static Properties

    /// <summary>
    /// The singleton instance of this class.
    /// </summary>
    internal static LibGLFW? Instance {
        get;
        private set;
    }

    #endregion

    #region Public Static Properties

    /// <summary>
    /// Whether the native library has been initialized.
    /// </summary>
    public static bool IsInitialized => Instance != null;

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Initialize the GLFW library.
    /// </summary>
    /// <param name="apiProvider">
    /// The native API provider to use or <c>null</c> to use the default
    /// provider.
    /// </param>
    /// <returns>
    /// If the native library is already initialized, the existing instance of
    /// this class is returned; a new instance otherwise.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a new instance needs to be created and the native library
    /// fails to initialize.
    /// </exception>
    public static LibGLFW Init(INativeApiProvider? apiProvider = null) {
        if (Instance != null) {
            return Instance;
        }
        var nativeApi = apiProvider ?? new DefaultNativeApiProvider();
        if (!nativeApi.Init()) {
            // TODO: Use custom exception
            throw new InvalidOperationException("Failed to initialize the native GLFW library");
        }
        Instance = new LibGLFW(nativeApi);
        Instance.PostInit();
        return Instance;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The native API provider this instance is using.
    /// </summary>
    public INativeApiProvider NativeApi {
        get;
    }

    /// <summary>
    /// Whether this instance is the one managing the native library.
    /// </summary>
    public bool IsCurrentInstance => Instance == this;

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
    /// <param name="apiProvider">
    /// The native API provider to use.
    /// </param>
    private LibGLFW(INativeApiProvider apiProvider) =>
        NativeApi = apiProvider;

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~LibGLFW() =>
        Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Terminate() =>
        Dispose();

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Private Methods

#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// Perform any post-initialization steps that require the
    /// <see cref="Instance" /> property to be set.
    /// </summary>
    private void PostInit() {
        // TODO
    }
#pragma warning restore CA1822 // Mark members as static

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
        if (managed && IsCurrentInstance) {
            NativeApi.Terminate();
        }
        if (IsCurrentInstance) {
            Instance = null;
        }
        IsDisposed = true;
    }

    #endregion
}
