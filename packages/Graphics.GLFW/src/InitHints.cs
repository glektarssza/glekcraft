namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A structure that represents initialization hints for the native library.
/// </summary>
public struct InitHints {
    #region Public Properties

    /// <summary>
    /// Whether to expose joystick hats as buttons in addition to through their
    /// own dedicated APIs.
    /// </summary>
    /// <remarks>
    /// Mainly for backwards compatibility with older versions of the native
    /// library.
    /// </remarks>
    public bool JoystickHatButtons {
        get;
        set;
    }

    /// <summary>
    /// The platform type to request when requesting an OpenGL or EGL context
    /// via ANGLE.
    /// </summary>
    public AnglePlatform AnglePlatformType {
        get;
        set;
    }

    /// <summary>
    /// What platform to select.
    /// </summary>
    public Platform Platform {
        get;
        set;
    }

    /// <summary>
    /// Whether to change into the <c>Contents/Resources</c> subdirectory of the
    /// application's bundle when the native library is initialized.
    /// </summary>
    /// <remarks>
    /// Only used on macOS.
    /// </remarks>
    public bool CocoaChdirResources {
        get;
        set;
    }

    /// <summary>
    /// Whether to create a menu bar and dock icon when the native library is
    /// initialized.
    /// </summary>
    /// <remarks>
    /// This applies whether the menu bar is created via a nib or manually by
    /// the native library. Only used on macOS.
    /// </remarks>
    public bool CocoaMenubar {
        get;
        set;
    }

    /// <summary>
    /// Whether to prefer the <c>VK_KHR_xcb_surface</c> extension as opposed to
    /// the <c>VK_KHR_xlib_surface</c> extension for creating Vulkan surfaces.
    /// </summary>
    /// <remarks>
    /// Only applies on X11-based Linux systems.
    /// </remarks>
    public bool X11XcbVulkanSurface {
        get;
        set;
    }

    /// <summary>
    /// Whether to prefer <c>libdecor</c> for window decoration.
    /// </summary>
    /// <remarks>
    /// Only applies on Wayland-based Linux systems.
    /// </remarks>
    public bool WaylandLibdecor {
        get;
        set;
    }

    #endregion
}
