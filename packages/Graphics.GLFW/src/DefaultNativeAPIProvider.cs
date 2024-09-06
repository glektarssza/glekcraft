namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// The default native API provider for the library.
/// </summary>
internal partial class DefaultNativeAPIProvider : INativeAPIProvider {
    #region Public Constants

    /// <summary>
    /// The name of the native library to load entry points from.
    /// </summary>
    public const string DLL_NAME = "glfw";

    #endregion

    #region Private External Methods

    [LibraryImport(DLL_NAME)]
    private static partial void glfwInitHint(InitHint hint, int value);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwInitHint(InitHint hint, [MarshalAs(UnmanagedType.Bool)] bool value);

    [LibraryImport(DLL_NAME)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwInit();

    [LibraryImport(DLL_NAME)]
    private static partial void glfwTerminate();

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetVersion(ref int major, ref int minor, ref int rev);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetVersionString();

    [LibraryImport(DLL_NAME)]
    private static partial ErrorCode glfwGetError(IntPtr descriptionPtrPtr);

    [LibraryImport(DLL_NAME)]
    private static partial INativeAPIProvider.ErrorCallback? glfwSetErrorCallback(INativeAPIProvider.ErrorCallback? callback);

    [LibraryImport(DLL_NAME)]
    private static partial Platform glfwGetPlatform();

    [LibraryImport(DLL_NAME)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwPlatformSupported(Platform platform);

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void InitHint(InitHint hint, int value) =>
        glfwInitHint(hint, value);

    /// <inheritdoc />
    public void InitHint(InitHint hint, bool value) =>
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
        if (major < 0 || minor < 0 || rev < 0) {
            return null;
        }
        return new(major, minor, rev);
    }

    /// <inheritdoc />
    public string? GetVersionString() =>
        Marshal.PtrToStringUTF8(glfwGetVersionString());

    /// <inheritdoc />
    public ErrorCode GetError(out string? description) {
        var descriptionPtrPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        var code = glfwGetError(descriptionPtrPtr);
        var descriptionPtr = Marshal.ReadIntPtr(descriptionPtrPtr);
        description = Marshal.PtrToStringUTF8(descriptionPtr);
        Marshal.FreeHGlobal(descriptionPtrPtr);
        return code;
    }

    /// <inheritdoc />
    public INativeAPIProvider.ErrorCallback? SetErrorCallback(INativeAPIProvider.ErrorCallback? callback) =>
        glfwSetErrorCallback(callback);

    /// <inheritdoc />
    public Platform GetPlatform() =>
        glfwGetPlatform();

    /// <inheritdoc />
    public bool IsPlatformSupported(Platform platform) =>
        glfwPlatformSupported(platform);

    #endregion
}
