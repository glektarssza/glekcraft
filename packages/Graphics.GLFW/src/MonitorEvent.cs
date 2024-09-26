namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of events that can occur to a monitor.
/// </summary>
public enum MonitorEvent : int {
    /// <summary>
    /// A monitor was connected to the system.
    /// </summary>
    Connected = 0x00040001,

    /// <summary>
    /// A monitor was disconnected from the system.
    /// </summary>
    Disconnected = 0x00040002
}
