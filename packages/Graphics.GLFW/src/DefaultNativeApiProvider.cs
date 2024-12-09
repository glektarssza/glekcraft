namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A default implementation of the <see cref="INativeApiProvider"/> interface
/// that relies on the <c>Glekcraft.Native.GLFW</c> library.
/// </summary>
internal partial class DefaultNativeApiProvider : INativeApiProvider {
    #region Public Constants

    /// <summary>
    /// The name of the native library to load API entry points from.
    /// </summary>
    public const string LibraryName = "glfw";

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

    #region External APIs - Monitors

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetMonitors(ref int count);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetPrimaryMonitor();

    [LibraryImport(LibraryName)]
    private static partial void glfwGetMonitorPos(IntPtr monitor, ref int xPos, ref int yPos);

    [LibraryImport(LibraryName)]
    private static partial void glfwGetMonitorWorkarea(IntPtr monitor, ref int xPos, ref int yPos, ref int width, ref int height);

    [LibraryImport(LibraryName)]
    private static partial void glfwGetMonitorPhysicalSize(IntPtr monitor, ref int widthMM, ref int heightMM);

    [LibraryImport(LibraryName)]
    private static partial void glfwGetMonitorContentScale(IntPtr monitor, ref float xScale, ref float yScale);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetMonitorName(IntPtr monitor);

    [LibraryImport(LibraryName)]
    private static partial void glfwSetMonitorUserPointer(IntPtr monitor, IntPtr pointer);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetMonitorUserPointer(IntPtr monitor);

    [LibraryImport(LibraryName)]
    private static partial INativeApiProvider.MonitorCallback? glfwSetMonitorCallback(INativeApiProvider.MonitorCallback? callback);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetVideoModes(IntPtr monitor, ref int count);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetVideoMode(IntPtr monitor);

    [LibraryImport(LibraryName)]
    private static partial void glfwSetGamma(IntPtr monitor, float gamma);

    [LibraryImport(LibraryName)]
    private static partial IntPtr glfwGetGammaRamp(IntPtr monitor);

    [LibraryImport(LibraryName)]
    private static partial void glfwSetGammaRamp(IntPtr monitor, IntPtr ramp);

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

    #region Public Methods - Monitors

    /// <inheritdoc />
    public IntPtr[] GetMonitors() {
        var count = 0;
        var monitorsPtr = glfwGetMonitors(ref count);
        if (monitorsPtr == IntPtr.Zero) {
            return [];
        }
        var monitors = new IntPtr[count];
        for (var i = 0; i < count; i++) {
            monitors[i] = Marshal.ReadIntPtr(monitorsPtr, i * IntPtr.Size);
        }
        return monitors;
    }

    /// <inheritdoc />
    public IntPtr GetPrimaryMonitor() => glfwGetPrimaryMonitor();

    /// <inheritdoc />
    public (int xPos, int yPos)? GetMonitorPosition(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        int xPos = -1, yPos = -1;
        glfwGetMonitorPos(monitor, ref xPos, ref yPos);
        return xPos == -1 || yPos == -1 ? null : (xPos, yPos);
    }

    /// <inheritdoc />
    public (int xPos, int yPos, int width, int height)? GetMonitorWorkArea(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        int xPos = -1, yPos = -1, width = -1, height = -1;
        glfwGetMonitorWorkarea(monitor, ref xPos, ref yPos, ref width, ref height);
        return xPos == -1 || yPos == -1 || width == -1 || height == -1 ? null : (xPos, yPos, width, height);
    }

    /// <inheritdoc />
    public (int widthMM, int heightMM)? GetMonitorPhysicalSize(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        int widthMM = -1, heightMM = -1;
        glfwGetMonitorPhysicalSize(monitor, ref widthMM, ref heightMM);
        return widthMM == -1 || heightMM == -1 ? null : (widthMM, heightMM);
    }

    /// <inheritdoc />
    public (float xScale, float yScale)? GetMonitorContentScale(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        float xScale = -1, yScale = -1;
        glfwGetMonitorContentScale(monitor, ref xScale, ref yScale);
        return xScale == -1 || yScale == -1 ? null : (xScale, yScale);
    }

    /// <inheritdoc />
    public string? GetMonitorName(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        return Marshal.PtrToStringUTF8(glfwGetMonitorName(monitor));
    }

    /// <inheritdoc />
    public void SetMonitorUserPointer(IntPtr monitor, IntPtr pointer) {
        if (monitor == IntPtr.Zero) {
            return;
        }
        glfwSetMonitorUserPointer(monitor, pointer);
    }

    /// <inheritdoc />
    public IntPtr GetMonitorUserPointer(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return IntPtr.Zero;
        }
        return glfwGetMonitorUserPointer(monitor);
    }

    /// <inheritdoc />
    public INativeApiProvider.MonitorCallback? SetMonitorCallback(INativeApiProvider.MonitorCallback? callback) =>
        glfwSetMonitorCallback(callback);

    /// <inheritdoc />
    public VideoMode[] GetVideoModes(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return [];
        }
        var count = 0;
        var videoModesPtr = glfwGetVideoModes(monitor, ref count);
        if (videoModesPtr == IntPtr.Zero) {
            return [];
        }
        var videoModes = new VideoMode[count];
        for (var i = 0; i < count; i++) {
            videoModes[i] = Marshal.PtrToStructure<VideoMode>(Marshal.ReadIntPtr(videoModesPtr, i * IntPtr.Size));
        }
        return videoModes;
    }

    /// <inheritdoc />
    public VideoMode? GetVideoMode(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        return Marshal.PtrToStructure<VideoMode>(glfwGetVideoMode(monitor));
    }

    /// <inheritdoc />
    public void SetGamma(IntPtr monitor, float gamma) {
        if (monitor == IntPtr.Zero) {
            return;
        }
        glfwSetGamma(monitor, gamma);
    }

    /// <inheritdoc />
    public GammaRamp? GetGammaRamp(IntPtr monitor) {
        if (monitor == IntPtr.Zero) {
            return null;
        }
        return Marshal.PtrToStructure<GammaRamp>(glfwGetGammaRamp(monitor));
    }

    /// <inheritdoc />
    public void SetGammaRamp(IntPtr monitor, GammaRamp ramp) {
        if (monitor == IntPtr.Zero) {
            return;
        }
        var tempRedArray = new short[ramp.Size];
        Buffer.BlockCopy(ramp.Red, 0, tempRedArray, 0, (int)ramp.Size);
        var tempGreenArray = new short[ramp.Size];
        Buffer.BlockCopy(ramp.Green, 0, tempGreenArray, 0, (int)ramp.Size);
        var tempBlueArray = new short[ramp.Size];
        Buffer.BlockCopy(ramp.Blue, 0, tempBlueArray, 0, (int)ramp.Size);

        var redPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ushort>() * (int)ramp.Size);
        var greenPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ushort>() * (int)ramp.Size);
        var bluePtr = Marshal.AllocHGlobal(Marshal.SizeOf<ushort>() * (int)ramp.Size);
        var rampPtr = Marshal.AllocHGlobal(Marshal.SizeOf<GammaRamp>());

        Marshal.Copy(tempRedArray, 0, redPtr, (int)ramp.Size);
        Marshal.Copy(tempGreenArray, 0, greenPtr, (int)ramp.Size);
        Marshal.Copy(tempBlueArray, 0, bluePtr, (int)ramp.Size);
        Marshal.WriteIntPtr(rampPtr, IntPtr.Size * 0, redPtr);
        Marshal.WriteIntPtr(rampPtr, IntPtr.Size * 1, greenPtr);
        Marshal.WriteIntPtr(rampPtr, IntPtr.Size * 2, bluePtr);
        Marshal.WriteInt64(rampPtr, IntPtr.Size * 3, ramp.Size);

        glfwSetGammaRamp(monitor, rampPtr);

        Marshal.FreeHGlobal(redPtr);
        Marshal.FreeHGlobal(greenPtr);
        Marshal.FreeHGlobal(bluePtr);
        Marshal.FreeHGlobal(rampPtr);
    }

    #endregion
}
