namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known initialization hints that the native library
/// supports.
/// </summary>
public enum InitHint : int {
    /// <summary>
    /// Whether to expose joystick hats as buttons in addition to through their
    /// own dedicated APIs.
    /// </summary>
    /// <remarks>
    /// Mainly for backwards compatibility with older versions of the native
    /// library.
    /// </remarks>
    JoystickHatButtons = 0x00050001,

    /// <summary>
    /// The platform type to request when requesting an OpenGL or EGL context
    /// via ANGLE.
    /// </summary>
    AnglePlatformType = 0x00050002,

    /// <summary>
    /// What platform to select.
    /// </summary>
    Platform = 0x00050003,

    /// <summary>
    /// Whether to change into the <c>Contents/Resources</c> subdirectory of the
    /// application's bundle when the native library is initialized.
    /// </summary>
    /// <remarks>
    /// Only used on macOS.
    /// </remarks>
    CocoaChdirResources = 0x00051001,

    /// <summary>
    /// Whether to create a menu bar and dock icon when the native library is
    /// initialized.
    /// </summary>
    /// <remarks>
    /// This applies whether the menu bar is created via a nib or manually by
    /// the native library. Only used on macOS.
    /// </remarks>
    CocoaMenubar = 0x00051002,

    /// <summary>
    /// Whether to prefer the <c>VK_KHR_xcb_surface</c> extension as opposed to
    /// the <c>VK_KHR_xlib_surface</c> extension for creating Vulkan surfaces.
    /// </summary>
    /// <remarks>
    /// Only applies on X11-based Linux systems.
    /// </remarks>
    X11XcbVulkanSurface = 0x00052001,

    /// <summary>
    /// Whether to prefer <c>libdecor</c> for window decoration.
    /// </summary>
    /// <remarks>
    /// Only applies on Wayland-based Linux systems.
    /// </remarks>
    WaylandLibdecor = 0x00053001
}
