namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known initialization hints.
/// </summary>
public enum InitHint : int {
    /// <summary>
    /// Whether to expose joystick hats as buttons in addition to though their
    /// dedicate APIs.
    /// </summary>
    JoystickHatButtons = 0x00050001,

    /// <summary>
    /// The type of platform to request when using OpenGL and EGL via ANGLE.
    /// </summary>
    AnglePlatformType = 0x00050002,

    /// <summary>
    /// The type of platform to use.
    /// </summary>
    Platform = 0x00050003,

    /// <summary>
    /// Whether to change into the <c>Contents/Resources</c> directory of
    /// application's bundle, if present, on macOS.
    /// </summary>
    CocoaChdirResources = 0x00051001,

    /// <summary>
    /// Whether to create a menu bar and dock icon on macOS.
    /// </summary>
    CocoaMenubar = 0x00051002,

    /// <summary>
    /// Whether to prefer to <c>VK_KHR_xcb_surface</c> for creating Vulkan
    /// surfaces on X11 as opposed to <c>VK_KHR_xlib_surface</c>.
    /// </summary>
    X11XCBVulkanSurface = 0x00052001,

    /// <summary>
    /// Whether to prefer <c>libdecor</c> for window decoration on Wayland.
    /// </summary>
    WaylandLibdecor = 0x00053001
}
