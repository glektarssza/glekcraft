package glekcraft

//-- Odin Standard Library
import "core:fmt"
import "core:os"

//-- Vendor Libraries
import "vendor:OpenGL"
import "vendor:glfw"

//-- Project Code
import "logging"

test_logger :: proc() {
    logger, _ := logging.create_console_logger("root")
    logging.log_fatal(logger, "Test")
}

/*
The program entry point.
*/
main :: proc() {
    test_logger()

    glfw.InitHint(glfw.JOYSTICK_HAT_BUTTONS, 0)
    glfw.InitHint(glfw.COCOA_CHDIR_RESOURCES, 1)
    glfw.InitHint(glfw.COCOA_MENUBAR, 1)
    // TODO: Wayland `libdecor` hint once it's available
    glfw_initialized := glfw.Init()
    defer if glfw_initialized {
        glfw.Terminate()
    }
    if !glfw_initialized {
        err_msg, err_code := glfw.GetError()
        fmt.eprintln("Failed to initialize GLFW:", err_msg)
        os.exit(1)
    }
    glfw.DefaultWindowHints()
    glfw.WindowHint(glfw.CONTEXT_VERSION_MAJOR, 4)
    glfw.WindowHint(glfw.CONTEXT_VERSION_MINOR, 1)
    glfw.WindowHint(glfw.OPENGL_PROFILE, glfw.OPENGL_CORE_PROFILE)
    glfw.WindowHint(glfw.OPENGL_FORWARD_COMPAT, true)
    glfw.WindowHint(glfw.VISIBLE, false)
    window := glfw.CreateWindow(640, 480, "G'lekcraft", nil, nil)
    defer if window != nil {
        glfw.DestroyWindow(window)
    }
    if window == nil {
        err_msg, err_code := glfw.GetError()
        fmt.eprintln("Failed to create game window:", err_msg)
        os.exit(1)
    }
    glfw.MakeContextCurrent(window)
    glfw.SwapInterval(1)
    OpenGL.load_up_to(4, 5, glfw.gl_set_proc_address)
    glfw.ShowWindow(window)
    defer {
        glfw.HideWindow(window)
    }
    running := true
    for running {
        glfw.PollEvents()
        if glfw.GetKey(window, glfw.KEY_ESCAPE) == glfw.PRESS {
            glfw.SetWindowShouldClose(window, true)
        }
        if glfw.WindowShouldClose(window) {
            glfw.SetWindowShouldClose(window, false)
            running = false
        }
        glfw.MakeContextCurrent(window)
        OpenGL.ClearColor(0, 0, 0, 1)
        OpenGL.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT)
        // TODO
        glfw.SwapBuffers(window)
    }
}
