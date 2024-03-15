package glekcraft

//-- Odin Standard Library
import "core:fmt"
import "core:os"

//-- Vendor Libraries
import "vendor:OpenGL"
import "vendor:glfw"

//-- Project Code
import "logging"

/*
The program main procedure.
*/
app_main :: proc() -> int {
    logger_file_handle, _ := os.open(
        "./output.log",
        os.O_RDWR | os.O_CREATE | os.O_TRUNC,
    )
    logger, _ := logging.create_file_logger(
        &logger_file_handle,
        "root",
        logging.Log_Level.All,
    )
    logging.info(logger, "Starting G'lekcraft")

    glfw.InitHint(glfw.JOYSTICK_HAT_BUTTONS, 0)
    glfw.InitHint(glfw.COCOA_CHDIR_RESOURCES, 1)
    glfw.InitHint(glfw.COCOA_MENUBAR, 1)
    // TODO: Wayland `libdecor` hint once it's available
    logging.debug(logger, "Initializing GLFW")
    glfw_initialized := glfw.Init()
    defer if glfw_initialized {
        logging.debug(logger, "Shutting down GLFW")
        glfw.Terminate()
    }
    if !glfw_initialized {
        err_msg, err_code := glfw.GetError()
        msg := fmt.aprintf(
            "Failed to initialize GLFW: %d - %s",
            err_code,
            err_msg,
        )
        logging.fatal(logger, msg)
        return 1
    }
    glfw.DefaultWindowHints()
    glfw.WindowHint(glfw.CONTEXT_VERSION_MAJOR, 4)
    glfw.WindowHint(glfw.CONTEXT_VERSION_MINOR, 1)
    glfw.WindowHint(glfw.OPENGL_PROFILE, glfw.OPENGL_CORE_PROFILE)
    glfw.WindowHint(glfw.OPENGL_FORWARD_COMPAT, true)
    glfw.WindowHint(glfw.VISIBLE, false)
    logging.debug(logger, "Creating main window")
    window := glfw.CreateWindow(640, 480, "G'lekcraft", nil, nil)
    defer if window != nil {
        logging.debug(logger, "Destroying main window")
        glfw.DestroyWindow(window)
    }
    if window == nil {
        err_msg, err_code := glfw.GetError()
        msg := fmt.aprintf(
            "Failed to create main game window: %d - %s",
            err_code,
            err_msg,
        )
        logging.fatal(logger, msg)
        return 1
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
    logging.info(logger, "Shutting down G'lekcraft")
    return 0
}

/*
The program entry point.
*/
main :: proc() {
    status := app_main()
    os.exit(status)
}
