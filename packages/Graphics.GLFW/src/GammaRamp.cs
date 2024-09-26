namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A structure which represents a monitor gamma ramp.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GammaRamp {
    #region Public Properties

    /// <summary>
    /// The response of the red channel.
    /// </summary>
    public ushort[] Red {
        get;
        set;
    }

    /// <summary>
    /// The response of the green channel.
    /// </summary>
    public ushort[] Green {
        get;
        set;
    }

    /// <summary>
    /// The response of the blue channel.
    /// </summary>
    public ushort[] Blue {
        get;
        set;
    }

    /// <summary>
    /// The number of elements in the red, green, and blue arrays.
    /// </summary>
    public uint Size {
        get;
        set;
    }

    #endregion
}
