package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:strings"
import "core:time"

/*
Write a message to the standard output.
*/
output_stdout_message :: proc(message: string, proc_data: rawptr) {
    fmt.println(message)
}

/*
Create a new logger that writes to the standard output.
*/
create_stdout_logger_from_namespace :: proc(
    ns: Namespace,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> Logger {
    return(
        Logger {
            namespace = ns,
            level = level,
            enabled = enabled,
            format = default_format_message,
            output = output_stdout_message,
        } \
    )
}

/*
Create a new logger that writes to the standard output.
*/
create_stdout_logger_from_namespace_string :: proc(
    ns: string,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    namespace := namespace_from_string(ns) or_return
    return create_stdout_logger_from_namespace(namespace, level, enabled), nil
}

/*
Create a new logger that writes to the standard output.
*/
create_stdout_logger :: proc {
    create_stdout_logger_from_namespace,
    create_stdout_logger_from_namespace_string,
}
