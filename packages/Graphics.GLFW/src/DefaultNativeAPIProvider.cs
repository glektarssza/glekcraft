namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// The default implementation of the <see cref="INativeAPIProvider" />
/// interface.
/// </summary>
internal sealed partial class DefaultNativeAPIProvider : INativeAPIProvider {
    #region Private Constants

    /// <summary>
    /// The name of the native library.
    /// </summary>
    private const string LIBRARY_NAME = "glfw";

    #endregion

    #region Private External Methods

    [LibraryImport(LIBRARY_NAME)]
    private static partial void glfwInitHint(InitHint hint, int value);

    [LibraryImport(LIBRARY_NAME)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwInit();

    [LibraryImport(LIBRARY_NAME)]
    private static partial void glfwTerminate();

    [LibraryImport(LIBRARY_NAME)]
    private static partial void glfwGetVersion(ref int major, ref int minor, ref int rev);

    [LibraryImport(LIBRARY_NAME)]
    private static partial IntPtr glfwGetVersionString();

    [LibraryImport(LIBRARY_NAME)]
    private static partial ErrorCode glfwGetError(IntPtr descriptionPtrPtr);

    [LibraryImport(LIBRARY_NAME)]
    private static partial INativeAPIProvider.ErrorCallback? glfwSetErrorCallback(INativeAPIProvider.ErrorCallback? descriptionPtrPtr);

    [LibraryImport(LIBRARY_NAME)]
    private static partial PlatformType glfwGetPlatform();

    [LibraryImport(LIBRARY_NAME)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwPlatformSupported(PlatformType platform);

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void InitHint(InitHint hint, bool value) =>
        glfwInitHint(hint, value ? 1 : 0);

    /// <inheritdoc />
    public void InitHint(InitHint hint, int value) =>
        glfwInitHint(hint, value);

    /// <inheritdoc />
    public bool Init() =>
        glfwInit();

    /// <inheritdoc />
    public void Terminate() =>
        glfwTerminate();

    /// <inheritdoc />
    public Version? GetVersion() {
        int major, minor, rev;
        major = minor = rev = -1;
        glfwGetVersion(ref major, ref minor, ref rev);
        if (major == -1 || minor == -1 || rev == -1) {
            return null;
        }
        return new(major, minor, rev);
    }

    /// <inheritdoc />
    public string? GetVersionString() =>
        Marshal.PtrToStringUTF8(glfwGetVersionString());

    /// <inheritdoc />
    public ErrorCode GetError(out string? description) {
        var descriptionPtrPtr = Marshal.AllocHGlobal(IntPtr.Size);
        var code = glfwGetError(descriptionPtrPtr);
        if (descriptionPtrPtr == IntPtr.Zero) {
            description = null;
        } else {
            var descriptionPtr = Marshal.ReadIntPtr(descriptionPtrPtr);
            description = Marshal.PtrToStringUTF8(descriptionPtr);
        }
        Marshal.FreeHGlobal(descriptionPtrPtr);
        return code;
    }

    /// <inheritdoc />
    public INativeAPIProvider.ErrorCallback? SetErrorCallback(INativeAPIProvider.ErrorCallback? callback) =>
        glfwSetErrorCallback(callback);

    /// <inheritdoc />
    public PlatformType GetPlatform() =>
        glfwGetPlatform();

    /// <inheritdoc />
    public bool IsPlatformSupported(PlatformType platform) =>
        glfwPlatformSupported(platform);

    #endregion
}
