namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of initialization hints supported by the native library.
/// </summary>
public enum InitHint : int {
    /// <summary>
    /// A hint to the native library about whether to expose joystick hats as
    /// buttons in addition to via the dedicated API.
    /// </summary>
    JoystickHatButtons = 0x00050001,

    /// <summary>
    /// A hint to the native library about which platform underlying ANGLE
    /// platform to use.
    /// </summary>
    AnglePlatformType = 0x00050002,

    /// <summary>
    /// A hint to the native library about which platform underlying platform
    /// to use.
    /// </summary>
    Platform = 0x00050003,

    /// <summary>
    /// A hint to the native library about whether to change into the bundle
    /// resources directory on macOS.
    /// </summary>
    CocoaChdirResources = 0x00051001,

    /// <summary>
    /// A hint to the native library about whether to create the menu bar on
    /// macOS.
    /// </summary>
    CocoaMenubar = 0x00051002,

    /// <summary>
    /// A hint to the native library about whether to prefer the
    /// <c>VK_KHR_xcb_surface</c> on X11.
    /// </summary>
    X11XCBVulkanSurface = 0x00052001,

    /// <summary>
    /// A hint to the native library about whether to prefer the <c>libdecor</c>
    /// library on Wayland.
    /// </summary>
    WaylandLibdecor = 0x00053001,
}
