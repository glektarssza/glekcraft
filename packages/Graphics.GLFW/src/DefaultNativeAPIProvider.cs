namespace Glekcraft.Graphics.GLFW;

using System.Linq;
using System.Runtime.InteropServices;

/// <summary>
/// The default native API provider for the library.
/// </summary>
internal partial class DefaultNativeAPIProvider : INativeAPIProvider {
    #region Public Constants

    /// <summary>
    /// The name of the native library to load entry points from.
    /// </summary>
    public const string DLL_NAME = "glfw3";

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

    [LibraryImport(DLL_NAME)]
    [return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 0)]
    private static partial IntPtr[] glfwGetMonitors(ref int count);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetPrimaryMonitor();

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetMonitorPos(IntPtr monitor, ref int xPos, ref int yPos);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetMonitorWorkarea(IntPtr monitor, ref int xPos, ref int yPos, ref int width, ref int height);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetMonitorPhysicalSize(IntPtr monitor, ref int widthMM, ref int heightMM);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwGetMonitorContentScale(IntPtr monitor, ref float xScale, ref float yScale);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetMonitorName(IntPtr monitor);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwSetMonitorUserPointer(IntPtr monitor, IntPtr userPointer);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetMonitorUserPointer(IntPtr monitor);

    [LibraryImport(DLL_NAME)]
    private static partial INativeAPIProvider.MonitorCallback? glfwSetMonitorCallback(INativeAPIProvider.MonitorCallback? callback);

    [LibraryImport(DLL_NAME)]
    [return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 1)]
    private static partial IntPtr[] glfwGetVideoModes(IntPtr monitor, ref int count);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetVideoMode(IntPtr monitor);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwSetGamma(IntPtr monitor, float gamma);

    [LibraryImport(DLL_NAME)]
    private static partial IntPtr glfwGetGammaRamp(IntPtr monitor);

    [LibraryImport(DLL_NAME)]
    private static partial void glfwSetGammaRamp(IntPtr monitor, IntPtr ramp);

    #endregion

    #region Public Methods - Core

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

    #region Public Methods - Monitors

    /// <inheritdoc />
    public IntPtr[] GetMonitors() {
        var count = 0;
        return glfwGetMonitors(ref count);
    }

    /// <inheritdoc />
    public IntPtr GetPrimaryMonitor() =>
        glfwGetPrimaryMonitor();

    /// <inheritdoc />
    public (int xPos, int yPos) GetMonitorPos(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        int xPos = 0, yPos = 0;
        glfwGetMonitorPos(monitor, ref xPos, ref yPos);
        return (xPos, yPos);
    }

    /// <inheritdoc />
    public (int xPos, int yPos, int width, int height) GetMonitorWorkarea(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        int xPos = 0, yPos = 0, width = 0, height = 0;
        glfwGetMonitorWorkarea(monitor, ref xPos, ref yPos, ref width, ref height);
        return (xPos, yPos, width, height);
    }

    /// <inheritdoc />
    public (int widthMM, int heightMM) GetMonitorPhysicalSize(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        int widthMM = 0, heightMM = 0;
        glfwGetMonitorPhysicalSize(monitor, ref widthMM, ref heightMM);
        return (widthMM, heightMM);
    }

    /// <inheritdoc />
    public (float xScale, float yScale) GetMonitorContentScale(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        float xScale = 0, yScale = 0;
        glfwGetMonitorContentScale(monitor, ref xScale, ref yScale);
        return (xScale, yScale);
    }

    /// <inheritdoc />
    public string? GetMonitorName(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        return Marshal.PtrToStringUTF8(glfwGetMonitorName(monitor));
    }

    /// <inheritdoc />
    public void SetMonitorUserPointer(IntPtr monitor, IntPtr userPointer) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        glfwSetMonitorUserPointer(monitor, userPointer);
    }

    /// <inheritdoc />
    public IntPtr GetMonitorUserPointer(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        return glfwGetMonitorUserPointer(monitor);
    }

    /// <inheritdoc />
    public INativeAPIProvider.MonitorCallback? SetMonitorCallback(INativeAPIProvider.MonitorCallback? callback) =>
        glfwSetMonitorCallback(callback);

    /// <inheritdoc />
    public VideoMode[] GetVideoModes(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        var count = 0;
        var vModes = glfwGetVideoModes(monitor, ref count);
        return vModes.Select(Marshal.PtrToStructure<VideoMode>).ToArray();
    }

    /// <inheritdoc />
    public VideoMode? GetVideoMode(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        var ptr = glfwGetVideoMode(monitor);
        if (ptr == IntPtr.Zero) {
            return null;
        }
        return Marshal.PtrToStructure<VideoMode>(ptr);
    }

    /// <inheritdoc />
    public void SetGamma(IntPtr monitor, float gamma) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        glfwSetGamma(monitor, gamma);
    }

    /// <inheritdoc />
    public GammaRamp? GetGammaRamp(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        var ptr = glfwGetGammaRamp(monitor);
        if (ptr == IntPtr.Zero) {
            return null;
        }
        return Marshal.PtrToStructure<GammaRamp>(ptr);
    }

    /// <inheritdoc />
    public void SetGammaRamp(IntPtr monitor, GammaRamp ramp) {
        if (monitor == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(monitor), "Invalid monitor handle");
        }
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<GammaRamp>());
        Marshal.StructureToPtr(ramp, ptr, false);
        glfwSetGammaRamp(monitor, ptr);
        Marshal.FreeHGlobal(ptr);
    }

    #endregion
}
