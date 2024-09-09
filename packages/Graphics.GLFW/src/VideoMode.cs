namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A structure describing a video mode for a monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct VideoMode {
    #region Public Properties

    /// <summary>
    /// The width of the video mode, in pixels.
    /// </summary>
    public int Width {
        get;
        set;
    }

    /// <summary>
    /// The height of the video mode, in pixels.
    /// </summary>
    public int Height {
        get;
        set;
    }

    /// <summary>
    /// The number of the red bits in the video mode.
    /// </summary>
    public int RedBits {
        get;
        set;
    }

    /// <summary>
    /// The number of the green bits in the video mode.
    /// </summary>
    public int GreenBits {
        get;
        set;
    }

    /// <summary>
    /// The number of the blue bits in the video mode.
    /// </summary>
    public int BlueBits {
        get;
        set;
    }

    /// <summary>
    /// The refresh rate of the video mode, in Hz.
    /// </summary>
    public int RefreshRate {
        get;
        set;
    }

    #endregion
}
