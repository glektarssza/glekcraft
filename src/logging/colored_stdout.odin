package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:strings"
import "core:time"

/*
Format a message destined for standard output output.
*/
format_colored_stdout_message :: proc(
    record: Log_Record,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    year, month, day := time.date(record.timestamp)
    hour, minute, second := time.clock_from_time(record.timestamp)
    log_level := log_level_to_string(record.level)
    namespace := namespace_to_string(record.namespace) or_return

    color := ""
    #partial switch record.level {
        case Log_Level.Fatal:
            fallthrough
        case Log_Level.Error:
            color = "\x1B[91m"
        case Log_Level.Warning:
            color = "\x1B[33m"
        case Log_Level.Info:
            color = "\x1B[37m"
        case Log_Level.Verbose:
            color = "\x1B[35m"
        case Log_Level.Debug:
            fallthrough
        case Log_Level.Trace:
            color = "\x1B[96m"
    }

    sb := strings.builder_make()
    fmt.sbprintf(
        &sb,
        "%02d-%02d-%02d %02d:%02d:%02d %s[%s@%s]\x1B[0m %s",
        year,
        month,
        day,
        hour,
        minute,
        second,
        color,
        namespace,
        log_level,
        record.message,
    )
    return strings.to_string(sb), nil
}

/*
Write a message to the standard output.
*/
output_colored_stdout_message :: proc(message: string) {
    fmt.println(message)
}

/*
Create a new logger that writes to the standard output.
*/
create_colored_stdout_logger_from_namespace :: proc(
    ns: Namespace,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> Logger {
    return(
        Logger {
            namespace = ns,
            level = level,
            enabled = enabled,
            format = format_colored_stdout_message,
            output = output_colored_stdout_message,
        } \
    )
}

/*
Create a new logger that writes to the standard output.
*/
create_colored_stdout_logger_from_namespace_string :: proc(
    ns: string,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    namespace := namespace_from_string(ns) or_return
    return create_colored_stdout_logger_from_namespace(
            namespace,
            level,
            enabled,
        ),
        nil
}

/*
Create a new logger that writes to the standard output.
*/
create_colored_stdout_logger :: proc {
    create_colored_stdout_logger_from_namespace,
    create_colored_stdout_logger_from_namespace_string,
}
