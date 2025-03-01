namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A class which provides a default method for accessing the native library.
/// </summary>
internal sealed partial class DefaultNativeAPIProvider : INativeAPIProvider {
    #region Public Constants

    /// <summary>
    /// The name of the dynamic link library to load native library entry points
    /// from.
    /// </summary>
    public const string LibraryName = "glfw3";

    #endregion

    #region Private Static Native Methods

    [LibraryImport(LibraryName)]
    private static partial void glfwInitHint(BoolInitHint hint, [MarshalAs(UnmanagedType.Bool)] bool value);

    [LibraryImport(LibraryName)]
    private static partial void glfwInitHint(InitHint hint, int value);

    [LibraryImport(LibraryName)]
    private static partial void glfwInitAllocator(IntPtr allocator);

    [LibraryImport(LibraryName)]
    private static partial void glfwInitVulkanLoader(IntPtr loader);

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwInit();

    [LibraryImport(LibraryName)]
    private static partial void glfwTerminate();

    [LibraryImport(LibraryName)]
    private static partial void glfwGetVersion(IntPtr major, IntPtr minor, IntPtr rev);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetVersionString();

    [LibraryImport(LibraryName)]
    private static partial ErrorCode glfwGetError(IntPtr descriptionPtr);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwSetErrorCallback(IntPtr callback);

    [LibraryImport(LibraryName)]
    private static partial Platform glfwGetPlatform();

    [LibraryImport(LibraryName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool glfwPlatformSupported(Platform platform);

    #endregion

    #region Private Fields

    /// <summary>
    /// The current heap allocator information.
    /// </summary>
    private NativeAllocator HeapAllocator;

    /// <summary>
    /// The currently configured error callback, if any.
    /// </summary>
    private INativeAPIProvider.ErrorCallback? ErrorCallback;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void InitHint(BoolInitHint hint, bool value) => glfwInitHint(hint, value);

    /// <inheritdoc />
    public void InitHint(InitHint hint, int value) => glfwInitHint(hint, value);

    /// <inheritdoc />
    public void InitAllocator(NativeAllocator? allocator) {
        if (!allocator.HasValue) {
            glfwInitAllocator(IntPtr.Zero);
            return;
        }
        this.HeapAllocator.Allocate = allocator.Value.Allocate;
        this.HeapAllocator.Reallocate = allocator.Value.Reallocate;
        this.HeapAllocator.Deallocate = allocator.Value.Deallocate;
        this.HeapAllocator.UserData = allocator.Value.UserData;
        var structSize = Marshal.SizeOf<NativeAllocator>();
        var structPtr = Marshal.AllocHGlobal(structSize);
        Marshal.WriteIntPtr(IntPtr.Add(structPtr, IntPtr.Size * 0), Marshal.GetFunctionPointerForDelegate(this.HeapAllocator.Allocate));
        Marshal.WriteIntPtr(IntPtr.Add(structPtr, IntPtr.Size * 1), Marshal.GetFunctionPointerForDelegate(this.HeapAllocator.Reallocate));
        Marshal.WriteIntPtr(IntPtr.Add(structPtr, IntPtr.Size * 2), Marshal.GetFunctionPointerForDelegate(this.HeapAllocator.Deallocate));
        Marshal.WriteIntPtr(IntPtr.Add(structPtr, IntPtr.Size * 3), Marshal.GetFunctionPointerForDelegate(this.HeapAllocator.UserData));
        glfwInitAllocator(structPtr);
        Marshal.FreeHGlobal(structPtr);
    }

    /// <inheritdoc />
    public void InitVulkanLoader(IntPtr loader) =>
        glfwInitVulkanLoader(loader);

    /// <inheritdoc />
    public bool Init() =>
        glfwInit();

    /// <inheritdoc />
    public void Terminate() =>
        glfwTerminate();

    /// <inheritdoc />
    public Version GetVersion() {
        var majorPtr = Marshal.AllocHGlobal(Marshal.SizeOf<int>());
        var minorPtr = Marshal.AllocHGlobal(Marshal.SizeOf<int>());
        var revPtr = Marshal.AllocHGlobal(Marshal.SizeOf<int>());
        Marshal.WriteInt32(majorPtr, -1);
        Marshal.WriteInt32(minorPtr, -1);
        Marshal.WriteInt32(revPtr, -1);
        glfwGetVersion(majorPtr, minorPtr, revPtr);
        var major = Marshal.ReadInt32(majorPtr);
        var minor = Marshal.ReadInt32(minorPtr);
        var rev = Marshal.ReadInt32(revPtr);
        Marshal.FreeHGlobal(majorPtr);
        Marshal.FreeHGlobal(minorPtr);
        Marshal.FreeHGlobal(revPtr);
        return major < 0 || minor < 0 || rev < 0
            ? throw new InvalidOperationException("Failed to retrieve native library version")
            : new(major, minor, rev);
    }

    /// <inheritdoc />
    public string GetVersionString() {
        var str = Marshal.PtrToStringUTF8(glfwGetVersionString());
        return string.IsNullOrWhiteSpace(str)
            ? throw new InvalidOperationException("Failed to retrieve native library version string")
            : str;
    }

    /// <inheritdoc />
    public (ErrorCode Code, string? Description) GetError() {
        var descriptionPtrPtr = Marshal.AllocHGlobal(IntPtr.Size);
        var code = glfwGetError(descriptionPtrPtr);
        var descriptionPtr = Marshal.ReadIntPtr(descriptionPtrPtr);
        var description = Marshal.PtrToStringUTF8(descriptionPtr);
        Marshal.FreeHGlobal(descriptionPtrPtr);
        return (code, description);
    }

    /// <inheritdoc />
    public INativeAPIProvider.ErrorCallback? SetErrorCallback(INativeAPIProvider.ErrorCallback? callback) {
        if (callback == null) {
            glfwSetErrorCallback(IntPtr.Zero);
        } else {
            glfwSetErrorCallback(Marshal.GetFunctionPointerForDelegate(this.HandleNativeErrorCallback));
        }
        var oldCallback = this.ErrorCallback;
        this.ErrorCallback = callback;
        return oldCallback;
    }

    /// <inheritdoc />
    public Platform GetPlatform() =>
        glfwGetPlatform();

    /// <inheritdoc />
    public bool IsPlatformSupported(Platform platform) =>
        glfwPlatformSupported(platform);

    #endregion

    #region Private Methods

    /// <summary>
    /// The internal callback for handling errors being surfaced from the native
    /// library.
    /// </summary>
    /// <param name="code">
    /// The error code which occurred.
    /// </param>
    /// <param name="descriptionPtr">
    /// A pointer to the optional human-readable description of the error that
    /// occurred.
    /// </param>
    private void HandleNativeErrorCallback(ErrorCode code, IntPtr descriptionPtr) =>
        this.ErrorCallback?.Invoke(code, Marshal.PtrToStringUTF8(descriptionPtr));

    #endregion
}
