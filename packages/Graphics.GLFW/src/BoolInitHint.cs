namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An subset of initialization hints for the native library that accept boolean
/// values.
/// </summary>
/// <seealso cref="InitHint"/>
public enum BoolInitHint : int {
    /// <summary>
    /// Whether to expose joystick hats as buttons.
    /// </summary>
    /// <remarks>
    /// Provided mainly for backwards compatibility with older versions of GLFW.
    /// </remarks>
    JoystickHatButtons = InitHint.JoystickHatButtons,

    /// <summary>
    /// Whether to change into the <c>Contents/Resources</c> sub-directory of
    /// the application bundle.
    /// </summary>
    /// <remarks>
    /// Only used on macOS.
    /// </remarks>
    CocoaChdirResources = InitHint.CocoaChdirResources,

    /// <summary>
    /// Whether to create a menu bar and dock icon.
    /// </summary>
    /// <remarks>
    /// This applies whether the menu bar is created from a nib or manually by
    /// the native library.
    ///
    /// Only used on macOS.
    /// </remarks>
    CocoaMenubar = InitHint.CocoaMenubar,

    /// <summary>
    /// Whether to prefer the <c>VK_KHR_xcb_surface</c> for creating Vulkan
    /// surfaces.
    /// </summary>
    /// <remarks>
    /// Only used on X11-supported platforms.
    /// </remarks>
    X11XcbVulkanSurface = InitHint.X11XcbVulkanSurface,

    /// <summary>
    /// Whether to prefer <c>libdecor</c> for creating window decorations.
    /// </summary>
    /// <remarks>
    /// Technically this hint does <b>not</b> accept a boolean but it is
    /// effectively a switch so it is treated as a boolean in code.
    ///
    /// Only used on platforms where <c>libdecor</c> is available.
    /// </remarks>
    WaylandLibdecor = InitHint.WaylandLibdecor
}
