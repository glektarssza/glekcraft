namespace Glekcraft.Graphics.GLFW;

using System.Runtime.InteropServices;

/// <summary>
/// A structure describing a gamma ramp for a monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GammaRamp(uint size) {
    #region Public Properties

    /// <summary>
    /// The red response of the gamma ramp.
    /// </summary>
    public ushort[] Red {
        get;
        set;
    } = new ushort[size];

    /// <summary>
    /// The green response of the gamma ramp.
    /// </summary>
    public ushort[] Green {
        get;
        set;
    } = new ushort[size];

    /// <summary>
    /// The blue response of the gamma ramp.
    /// </summary>
    public ushort[] Blue {
        get;
        set;
    } = new ushort[size];

    /// <summary>
    /// The size of the components of the gamma ramp.
    /// </summary>
    public uint Size {
        get;
        set;
    } = size;

    #endregion
}
