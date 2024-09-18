namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A default implementation of the <see cref="INativeApiProvider"/> interface
/// that relies on the <c>Ultz.Native.GLFW</c> library.
/// </summary>
internal partial class DefaultNativeApiProvider : INativeApiProvider {
    #region Public Constants

    /// <summary>
    /// The name of the native library to load API entry points from.
    /// </summary>
    public const string LibraryName = "glfw3";

    #endregion

    #region External APIs - Core

    [LibraryImport(LibraryName)]
    private static partial void glfwInitHint(InitHint hint, [MarshalAs(UnmanagedType.Bool)] bool value);

    [LibraryImport(LibraryName)]
    private static partial void glfwInitHint(InitHint hint, int value);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwInit();

    [LibraryImport(LibraryName)]
    private static partial void glfwTerminate();

    [LibraryImport(LibraryName)]
    private static partial void glfwGetVersion(ref int major, ref int minor, ref int rev);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetVersionString();

    [LibraryImport(LibraryName)]
    private static partial ErrorCode glfwGetError(IntPtr descriptionPtrPtr);

    [LibraryImport(LibraryName)]
    private static partial INativeApiProvider.ErrorCallback? glfwSetErrorCallback(INativeApiProvider.ErrorCallback? callbackPtr);

    [LibraryImport(LibraryName)]
    private static partial Platform glfwGetPlatform();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwPlatformSupported(Platform platform);

    #endregion

    #region Public Methods - Core

    /// <inheritdoc />
    public void InitHint(InitHint hint, bool value) => glfwInitHint(hint, value);

    /// <inheritdoc />
    public void InitHint(InitHint hint, int value) => glfwInitHint(hint, value);

    /// <inheritdoc />
    public bool Init() => glfwInit();

    /// <inheritdoc />
    public void Terminate() => glfwTerminate();

    /// <inheritdoc />
    public Version? GetVersion() {
        int major = -1, minor = -1, rev = -1;
        glfwGetVersion(ref major, ref minor, ref rev);
        return major == -1 || minor == -1 || rev == -1 ? null : new(major, minor, rev);
    }

    /// <inheritdoc />
    public string? GetVersionString() => Marshal.PtrToStringUTF8(glfwGetVersionString());

    /// <inheritdoc />
    public ErrorCode GetError(out string? description) {
        var descriptionPtrPtr = Marshal.AllocHGlobal(IntPtr.Size);
        var code = glfwGetError(descriptionPtrPtr);
        description = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(descriptionPtrPtr));
        Marshal.FreeHGlobal(descriptionPtrPtr);
        return code;
    }

    /// <inheritdoc />
    public INativeApiProvider.ErrorCallback? SetErrorCallback(INativeApiProvider.ErrorCallback? callback) =>
        glfwSetErrorCallback(callback);


    /// <inheritdoc />
    public Platform GetPlatform() => glfwGetPlatform();

    /// <inheritdoc />
    public bool IsPlatformSupported(Platform platform) => glfwPlatformSupported(platform);

    #endregion
}
