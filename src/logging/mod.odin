package logging

//-- Odin Standard Library
import "core:mem"
import "core:strings"
import "core:time"

/*
The string to use to deliniate namespace components.
*/
NAMESPACE_COMPONENT_SEPARATOR :: ":"

/*
A type alias for a parsed namespace.
*/
Namespace :: #type []string

/*
Parse a namespace from a string.
*/
namespace_from_string :: proc(
    raw_str: string,
) -> (
    res: Namespace,
    err: mem.Allocator_Error,
) {
    return strings.split(raw_str, NAMESPACE_COMPONENT_SEPARATOR)
}

/*
Convert a namespace to a string.
*/
namespace_to_string :: proc(
    ns: Namespace,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    return strings.join(ns, NAMESPACE_COMPONENT_SEPARATOR)
}

/*
An enumeration of logging levels.
*/
Log_Level :: enum {
    /*
    No logging output.
    */
    None    = 0,

    /*
    Only fatal error messages or more severe.
    */
    Fatal   = 1,

    /*
    Only error messages or more severe.
    */
    Error   = 2,

    /*
    Only warning messages or more severe.
    */
    Warning = 3,

    /*
    Only informational messages or more severe.
    */
    Info    = 4,

    /*
    Only verbose messages or more severe.
    */
    Verbose = 5,

    /*
    Only debug messages or more severe.
    */
    Debug   = 6,
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
            return "WARNING"
        case Log_Level.Info:
            return "INFO"
        case Log_Level.Verbose:
            return "VERBOSE"
        case Log_Level.Debug:
            return "DEBUG"
    }
    return ""
}

/*
A record of a logging output.
*/
Log_Record :: struct {
    /*
    The namespace the record was created for.
    */
    namespace: Namespace,

    /*
    The logging level the record was created at.
    */
    level:     Log_Level,

    /*
    The timestamp the record was created at.
    */
    timestamp: time.Time,

    /*
    The message the record was created for.
    */
    message:   string,
}

/*
A procecure that can format a logging record.
*/
logger_format_record_proc :: #type proc(record: Log_Record) -> string

/*
A procecure that can output a logging record.
*/
logger_output_proc :: #type proc(msg: string)

/*
A structure which holds data about a namespace to be logged to.
*/
Logger :: struct {
    /*
    The namespace this structure wraps around.
    */
    namespace: Namespace,

    /*
    An override to disable logging output.
    */
    enabled:   bool,

    /*
    The maximum severity of logs to allow to be output.
    */
    level:     Log_Level,

    /*
    The procedure to use to format logging records.
    */
    format:    logger_format_record_proc,

    /*
    The procedure to use to output logs.
    */
    output:    logger_output_proc,
}

log_fatal :: proc(logger: Logger, message: string) {
    if !logger.enabled {
        return
    }
    if logger.level < Log_Level.Fatal {
        return
    }
    record := Log_Record {
        namespace = logger.namespace,
        level     = Log_Level.Fatal,
        timestamp = time.now(),
        message   = message,
    }
    formatted := logger.format(record)
    logger.output(formatted)
}
