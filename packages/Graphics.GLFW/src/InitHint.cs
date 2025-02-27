namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of supported initialization hints for the native library.
/// </summary>
public enum InitHint : int {
    /// <summary>
    /// Whether to expose joystick hats as buttons.
    /// </summary>
    /// <remarks>
    /// Provided mainly for backwards compatibility with older versions of GLFW.
    /// </remarks>
    JoystickHatButtons = 0x00050001,

    /// <summary>
    /// Specifies what type of rendering back end to request when requesting an
    /// OpenGL ES and EGL rendering context via
    /// <see href="https://chromium.googlesource.com/angle/angle/">ANGLE</see>.
    /// </summary>
    ANGLEPlatformType = 0x00050002,

    /// <summary>
    /// The platform to use for windowing and input.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="Platform.Any" /> which will select an
    /// appropriate platform from the available, supported platforms excluding
    /// the <see cref="Platform.Null">Null</see> platform.
    /// </remarks>
    Platform = 0x00050003,

    /// <summary>
    /// Whether to change into the <c>Contents/Resources</c> sub-directory of
    /// the application bundle.
    /// </summary>
    /// <remarks>
    /// Only used on macOS.
    /// </remarks>
    CocoaChdirResources = 0x00051001,

    /// <summary>
    /// Whether to create a menu bar and dock icon.
    /// </summary>
    /// <remarks>
    /// This applies whether the menu bar is created from a nib or manually by
    /// the native library.
    ///
    /// Only used on macOS.
    /// </remarks>
    CocoaMenubar = 0x00051002,

    /// <summary>
    /// Whether to prefer the <c>VK_KHR_xcb_surface</c> for creating Vulkan
    /// surfaces.
    /// </summary>
    /// <remarks>
    /// Only used on X11-supported platforms.
    /// </remarks>
    X11XcbVulkanSurface = 0x00052001,

    /// <summary>
    /// Whether to prefer <c>libdecor</c> for creating window decorations.
    /// </summary>
    /// <remarks>
    /// Only used on platforms where <c>libdecor</c> is available.
    /// </remarks>
    WaylandLibdecor = 0x00053001
}
