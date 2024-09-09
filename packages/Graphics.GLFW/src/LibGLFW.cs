namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// The main entry point into the library.
/// </summary>
public sealed class LibGLFW : IDisposable {
    #region Internal Static Properties

    /// <summary>
    /// The instance that is currently managing the native library.
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
    public static bool IsInitialized =>
        Instance != null;

    /// <summary>
    /// The last error code set by the native library.
    /// </summary>
    public static ErrorCode? LastErrorCode {
        get;
        private set;
    }

    /// <summary>
    /// The last error description set by the native library.
    /// </summary>
    public static string? LastErrorDescription {
        get;
        private set;
    }

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Clear the last error code encountered by the native library.
    /// </summary>
    public static void ClearLastErrorCode() =>
        LastErrorCode = null;

    /// <summary>
    /// Clear the last error description encountered by the native library.
    /// </summary>
    public static void ClearLastErrorDescription() =>
        LastErrorDescription = null;

    /// <summary>
    /// Clear the last error code and description encountered by the native
    /// library.
    /// </summary>
    public static void ClearLastError() {
        ClearLastErrorCode();
        ClearLastErrorDescription();
    }

    /// <summary>
    /// Initializes the native library.
    /// </summary>
    /// <param name="apiProvider">
    /// The object which will provide access to the native library APIs.
    /// </param>
    /// <returns>
    /// The existing instance, if one is available; a new instance otherwise.
    /// </returns>
    public static LibGLFW Initialize(INativeAPIProvider? apiProvider = null) {
        if (Instance != null) {
            return Instance;
        }
        var api = apiProvider ?? new DefaultNativeAPIProvider();
        if (!api.Init()) {
            LastErrorCode = api.GetError(out var errDescription);
            LastErrorDescription = errDescription;
            throw new GLFWException("Failed to initialize the native library");
        }
        // TODO: Set error callback
        Instance = new(api);
        return Instance;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The object which provides access to the native library APIs.
    /// </summary>
    public INativeAPIProvider APIProvider {
        get;
    }

    /// <summary>
    /// Whether this instance is the one currently managing the native library.
    /// </summary>
    public bool IsCurrentInstance =>
        Instance == this;

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
    /// The object which will provide access to the native library APIs.
    /// </param>
    private LibGLFW(INativeAPIProvider apiProvider) =>
        APIProvider = apiProvider;

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~LibGLFW() =>
        Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Terminate the native library.
    /// </summary>
    /// <remarks>
    /// This method is equivalent to calling <see cref="Dispose" />.
    /// </remarks>
    /// <seealso cref="Dispose" />
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
            if (IsCurrentInstance) {
                APIProvider.Terminate();
            }
            // TODO: Dispose of managed resources
        }
        if (IsCurrentInstance) {
            Instance = null;
        }
        IsDisposed = true;
    }

    #endregion
}
