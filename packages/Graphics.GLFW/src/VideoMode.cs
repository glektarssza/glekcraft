namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A structure which represents a monitor video mode.
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
    /// The heights of the video mode, in pixels.
    /// </summary>
    public int Height {
        get;
        set;
    }

    /// <summary>
    /// The number of bits, per pixel, dedicated to the red channel.
    /// </summary>
    public int RedBits {
        get;
        set;
    }

    /// <summary>
    /// The number of bits, per pixel, dedicated to the green channel.
    /// </summary>
    public int GreenBits {
        get;
        set;
    }

    /// <summary>
    /// The number of bits, per pixel, dedicated to the blue channel.
    /// </summary>
    public int BlueBits {
        get;
        set;
    }

    /// <summary>
    /// The refresh rate of the video mode, in hertz.
    /// </summary>
    public int RefreshRate {
        get;
        set;
    }

    #endregion
}
