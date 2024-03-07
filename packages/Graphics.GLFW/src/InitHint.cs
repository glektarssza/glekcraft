namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known initialization hints for the native library.
/// </summary>
public enum InitHint {
    /// <summary>
    /// An initialization hint indicating whether to expose joystick hats as
    /// buttons in addition to through their dedicated APIs.
    /// </summary>
    JoystickHatButtons = 0x00050001,

    /// <summary>
    /// An initialization hint indicating whether to change in the application's
    /// <c>Content/Resources</c> directory upon application startup.
    /// </summary>
    CocoaChDir = 0x00051001,

    /// <summary>
    /// An initialization hint indicating whether to create a basic menubar when
    /// the <c>AppKit</c> starts up.
    /// </summary>
    CocoaMenubar = 0x00051002,

    /// <summary>
    /// An initialization hint indicating whether to use <c>libdecor</c>.
    /// </summary>
    WaylandLibdecor = 0x00053001
}
