package logging

//-- Odin Standard Library
import "core:mem"
import "core:strings"
import "core:time"

/*
The separator used to separate namespace components.
*/
NAMESPACE_SEPARATOR :: ":"

/*
An alias type for a slice of strings, used to represent a namespace.
*/
Namespace :: #type []string

/*
Convert a namespace to a string.
*/
namespace_to_string :: proc(
    ns: Namespace,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    return strings.join(ns, NAMESPACE_SEPARATOR)
}

/*
Convert a string to a namespace.
*/
namespace_from_string :: proc(
    ns: string,
) -> (
    res: Namespace,
    err: mem.Allocator_Error,
) {
    return strings.split(ns, NAMESPACE_SEPARATOR)
}

/*
An enumeration of known logging levels.
*/
Log_Level :: enum {
    /*
    No logging.
    */
    None    = 0,

    /*
    Fatal errors.
    */
    Fatal   = 1,

    /*
    Errors.
    */
    Error   = 2,

    /*
    Warnings.
    */
    Warning = 3,

    /*
    Informational messages.
    */
    Info    = 4,

    /*
    Verbose messages.
    */
    Verbose = 5,

    /*
    Debug messages.
    */
    Debug   = 6,

    /*
    Tracing messages.
    */
    Trace   = 7,

    /*
    All logging.
    */
    All     = 0xFFFF,
}

/*
Convert a logging level to a string.
*/
log_level_to_string :: proc(level: Log_Level) -> string {
    #partial switch level {
        case Log_Level.Fatal:
            return "FATAL"
        case Log_Level.Error:
            return "ERROR"
        case Log_Level.Warning:
            return "WARN"
        case Log_Level.Info:
            return "INFO"
        case Log_Level.Verbose:
            return "VERBOSE"
        case Log_Level.Debug:
            return "DEBUG"
        case Log_Level.Trace:
            return "TRACE"
    }
    return "UNKNOWN"
}

/*
A record of a request to log some output.
*/
Log_Record :: struct {
    /*
    The namespace the output was being sent to.
    */
    namespace: Namespace,

    /*
    The level the output was logged at.
    */
    level:     Log_Level,

    /*
    The timestamp the output was logged at.
    */
    timestamp: time.Time,

    /*
    The message to log.
    */
    message:   string,
}

/*
A procedure that can be used to format a log record into a string.
*/
Format_Log_Record_Proc :: #type proc(
    record: Log_Record,
) -> (
    res: string,
    err: mem.Allocator_Error,
)

/*
A procedure that can be used to output a log message.
*/
Output_Message_Proc :: #type proc(message: string)

/*
A destination for logging output.
*/
Logger :: struct {
    /*
    The namespace this logger is for.
    */
    namespace: Namespace,

    /*
    Whether this logger is enabled.
    */
    enabled:   bool,

    /*
    The minimum level of log messages to output.
    */
    level:     Log_Level,

    /*
    The procedure to use to format log records.
    */
    format:    Format_Log_Record_Proc,

    /*
    The procedure to use to output log messages.
    */
    output:    Output_Message_Proc,
}

/*
Log a message.
*/
log :: proc(logger: Logger, level: Log_Level, message: string) {
    if !logger.enabled || level > logger.level {
        return
    }
    record := Log_Record {
        namespace = logger.namespace,
        level     = level,
        timestamp = time.now(),
        message   = message,
    }
    formatted := logger.format(record) or_else ""
    if formatted == "" {
        return
    }
    logger.output(formatted)
}

/*
Log a fatal error message.
*/
fatal :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Fatal, message)
}

/*
Log an error message.
*/
error :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Error, message)
}

/*
Log a warning message.
*/
warn :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Warning, message)
}

/*
Log an informational message.
*/
info :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Info, message)
}

/*
Log a verbose message.
*/
verbose :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Verbose, message)
}

/*
Log a debug message.
*/
debug :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Debug, message)
}

/*
Log a tracing message.
*/
trace :: proc(logger: Logger, message: string) {
    log(logger, Log_Level.Trace, message)
}
