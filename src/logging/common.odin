package logging

//-- Odin Standard Library
import "core:mem"
import "core:strings"

/*
The separator used to join namespace components.
*/
NAMESPACE_SEPARATOR :: ":"

/*
An array of namespace components.
*/
Namespace :: #type []string

/*
Convert an array of namespace components to a string.
*/
namespace_to_string :: proc(
    namespace: Namespace,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    return strings.join(namespace, NAMESPACE_SEPARATOR)
}

/*
Convert a string to an array of namespace components.
*/
namespace_from_string :: proc(
    namespace: string,
) -> (
    res: Namespace,
    err: mem.Allocator_Error,
) {
    return strings.split(namespace, NAMESPACE_SEPARATOR)
}

/*
An enumeration of the levels of logging.
*/
Log_Level :: enum {
    /*
    The logging level for no messages.
    */
    None    = 0,

    /*
    The logging level for fatal errors.
    */
    Fatal   = 1,

    /*
    The logging level for errors.
    */
    Error   = 2,

    /*
    The logging level for warnings.
    */
    Warning = 3,

    /*
    The logging level for informational messages.
    */
    Info    = 4,

    /*
    The logging level for debugging messages.
    */
    Debug   = 5,

    /*
    The logging level for tracing messages.
    */
    Trace   = 6,

    /*
    The logging level for all messages.
    */
    All     = 7,
}

/*
Convert a logging level to a string.
*/
log_level_to_string :: proc(
    level: Log_Level,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    switch level {
        case Log_Level.None:
            return "None", nil
        case Log_Level.Fatal:
            return "Fatal", nil
        case Log_Level.Error:
            return "Error", nil
        case Log_Level.Warning:
            return "Warning", nil
        case Log_Level.Info:
            return "Info", nil
        case Log_Level.Debug:
            return "Debug", nil
        case Log_Level.Trace:
            return "Trace", nil
        case Log_Level.All:
            return "All", nil
    }
}

/*
A structure which represents a namespace to be logged to.
*/
Logger :: struct {
    /*
    The namespace to log to.
    */
    namespace: Namespace,

    /*
    Whether the namespace is enabled for logging.
    */
    enabled:   bool,

    /*
    The maximum level of logging to perform.
    */
    level:     Log_Level,

    /*
    The user data to pass to the logging procedures.
    */
    user_data: rawptr,
}
