namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// The default native API provider.
/// </summary>
internal partial class DefaultNativeAPIProvider : INativeAPIProvider {
    #region Public Constants

    /// <summary>
    /// The name of the library to load native APIs from.
    /// </summary>
    public const string DLL_NAME = "glfw";

    #endregion

    #region Public Delegates

    /// <summary>
    /// A delegate for handling when the native library encounters an error.
    /// </summary>
    /// <param name="code">
    /// The error code of the error encountered by the native library.
    /// </param>
    /// <param name="descriptionPtr">
    /// A description, if available, of the error encountered by the native
    /// library.
    /// </param>
    public delegate void NativeErrorCallback(ErrorCode code, IntPtr descriptionPtr);

    #endregion

    #region Private External APIs

    [LibraryImport(DLL_NAME)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwInit();

    [LibraryImport(DLL_NAME)]
    private static partial void glfwTerminate();

    [LibraryImport(DLL_NAME)]
    private static partial void glfwInitHint(int hint, int value);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetVersion(ref int major, ref int minor, ref int rev);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetVersionString();

    [LibraryImport(DLL_NAME)]
    private static partial ErrorCode glfwGetError(IntPtr descriptionPtr);

    [LibraryImport(DLL_NAME)]
    private static partial NativeErrorCallback? glfwSetErrorCallback(NativeErrorCallback? callbackPtr);

    #endregion

    #region Private Methods

    /// <summary>
    /// The current error callback.
    /// </summary>
    private INativeAPIProvider.ErrorCallback? ErrorCallback {
        get;
        set;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Initialize the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the native library initialized successfully; <c>false</c>
    /// otherwise.
    /// </returns>
    public bool Init() =>
        glfwInit();

    /// <summary>
    /// Shut down the native library.
    /// </summary>
    public void Terminate() =>
        glfwTerminate();

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The hint to set the value of.
    /// </param>
    /// <param name="value">
    /// The value to set the given hint to.
    /// </param>
    public void InitHint(InitHint hint, int value) =>
        glfwInitHint((int)hint, value);

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The hint to set the value of.
    /// </param>
    /// <param name="value">
    /// The value to set the given hint to.
    /// </param>
    public void InitHint(InitHint hint, bool value) =>
        InitHint(hint, value ? 1 : 0);

    /// <summary>
    /// Get the version of the native library being used.
    /// </summary>
    /// <returns>
    /// The version of the native library being used.
    /// </returns>
    public Version? GetVersion() {
        int major, minor, rev;
        major = minor = rev = -1;
        glfwGetVersion(ref major, ref minor, ref rev);
        if (major < 0 || minor < 0 || rev < 0) {
            return null;
        }
        return new(major, minor, rev);
    }

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <returns>
    /// The version string of the native library being used.
    /// </returns>
    public string? GetVersionString() =>
        Marshal.PtrToStringUTF8(glfwGetVersionString());

    /// <summary>
    /// Get the last error that the native library encountered.
    /// </summary>
    /// <param name="description">
    /// A location to store the description of the error in, if any.
    /// </param>
    /// <returns>
    /// The error code of the last error that the native library encountered.
    /// </returns>
    public ErrorCode GetError(out string? description) {
        var descriptionPtr = Marshal.AllocHGlobal(IntPtr.Size);
        var code = glfwGetError(descriptionPtr);
        description = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(descriptionPtr));
        Marshal.FreeHGlobal(descriptionPtr);
        return code;
    }

    /// <summary>
    /// Set the method to call when the native library encounters an error.
    /// </summary>
    /// <param name="callback">
    /// The method to call when the native library encounters an error or
    /// <c>null</c> to remove an existing callback.
    /// </param>
    /// <returns>
    /// The existing callback, if any.
    /// </returns>
    public INativeAPIProvider.ErrorCallback? SetErrorCallback(INativeAPIProvider.ErrorCallback? callback) {
        var originalCallback = ErrorCallback;
        ErrorCallback = callback;
        if (callback == null) {
            _ = glfwSetErrorCallback(null);
        } else {
            _ = glfwSetErrorCallback(HandleNativeError);
        }
        return originalCallback;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handle the native library encountering an error.
    /// </summary>
    /// <param name="code">
    /// The error code of the error encountered by the native library.
    /// </param>
    /// <param name="descriptionPtr">
    /// A description, if available, of the error encountered by the native
    /// library.
    /// </param>
    private void HandleNativeError(ErrorCode code, IntPtr descriptionPtr) =>
        ErrorCallback?.Invoke(code, Marshal.PtrToStringUTF8(descriptionPtr));

    #endregion
}
