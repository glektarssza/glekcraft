namespace Glekcraft.Graphics.GLFW;

public sealed class LibGLFW : IDisposable {
    #region Public Delegates

    /// <summary>
    /// A delegate for handling error events.
    /// </summary>
    /// <param name="code">
    /// The error code.
    /// </param>
    /// <param name="description">
    /// A description of the error.
    /// </param>
    public delegate void ErrorCallback(ErrorCode code, string? description);

    #endregion

    #region Public Static Events

    /// <summary>
    /// An event that is raised when the native library encounters an error.
    /// </summary>
    public static event ErrorCallback? OnError;

    #endregion

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
    /// Whether the native library is initialized.
    /// </summary>
    public static bool IsInitialized =>
        Instance != null;

    /// <summary>
    /// The last error code encountered by the native library.
    /// </summary>
    public static ErrorCode LastErrorCode {
        get;
        private set;
    } = ErrorCode.NoError;

    /// <summary>
    /// A description of the last error encountered by the native library.
    /// </summary>
    public static string? LastErrorDescription {
        get;
        private set;
    }

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Clear the last error encountered by the native library.
    /// </summary>
    public static void ClearLastError() {
        LastErrorCode = ErrorCode.NoError;
        LastErrorDescription = null;
    }

    /// <summary>
    /// Initialize the native library.
    /// </summary>
    /// <param name="apiProvider">
    /// The object to use to access the native GLFW APIs.
    /// </param>
    /// <returns></returns>
    /// <exception cref="Exception">
    /// Thrown if the native library fails to initialize.
    /// </exception>
    public static LibGLFW Initialize(INativeAPIProvider? apiProvider = null) {
        if (Instance == null) {
            var nativeAPIs = apiProvider ?? new DefaultNativeAPIProvider();
            _ = nativeAPIs.SetErrorCallback(HandleErrorCallback);
            if (!nativeAPIs.Init()) {
                _ = nativeAPIs.SetErrorCallback(null);
                throw new GLFWException(LastErrorCode, LastErrorDescription, "Failed to initialize native GLFW library");
            }
            Instance = new(nativeAPIs);
        }
        return Instance;
    }

    #endregion

    #region Private Static Methods

    /// <summary>
    /// A callback for handling errors being surfaced from the native library.
    /// </summary>
    /// <param name="code">
    /// The error code.
    /// </param>
    /// <param name="description">
    /// A description of the error.
    /// </param>
    private static void HandleErrorCallback(ErrorCode code, string? description) {
        LastErrorCode = code;
        LastErrorDescription = description;
        OnError?.Invoke(code, description);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The object which provides access to the native APIs.
    /// </summary>
    public INativeAPIProvider APIProvider {
        get;
    }

    /// <summary>
    /// Whether this instance is the one controlling the native library.
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
    /// The object that will provide access to the native APIs.
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
        if (managed && IsCurrentInstance) {
            _ = APIProvider.SetErrorCallback(null);
            APIProvider.Terminate();
        }
        if (IsCurrentInstance) {
            Instance = null;
        }
        IsDisposed = true;
    }

    #endregion
}
