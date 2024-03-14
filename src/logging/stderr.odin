package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:strings"
import "core:time"

/*
Format a message destined for standard error output.
*/
format_stderr_message :: proc(
    record: Log_Record,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    year, month, day := time.date(record.timestamp)
    hour, minute, second := time.clock_from_time(record.timestamp)
    log_level := log_level_to_string(record.level)
    namespace := namespace_to_string(record.namespace) or_return

    sb := strings.builder_make()
    fmt.sbprintf(
        &sb,
        "%02d-%02d-%02d %02d:%02d:%02d [%s@%s] %s",
        year,
        month,
        day,
        hour,
        minute,
        second,
        namespace,
        log_level,
        record.message,
    )
    return strings.to_string(sb), nil
}

/*
Write a message to the standard error.
*/
output_stderr_message :: proc(message: string) {
    fmt.eprintln(message)
}

/*
Create a new logger that writes to the standard error.
*/
create_stderr_logger_from_namespace :: proc(
    ns: Namespace,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> Logger {
    return(
        Logger {
            namespace = ns,
            level = level,
            enabled = enabled,
            format = format_stdout_message,
            output = output_stdout_message,
        } \
    )
}

/*
Create a new logger that writes to the stdndard error.
*/
create_stderr_logger_from_namespace_string :: proc(
    ns: string,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    namespace := namespace_from_string(ns) or_return
    return create_stderr_logger_from_namespace(namespace, level, enabled), nil
}

/*
Create a new logger that writes to the standard error.
*/
create_stderr_logger :: proc {
    create_stderr_logger_from_namespace,
    create_stderr_logger_from_namespace_string,
}
