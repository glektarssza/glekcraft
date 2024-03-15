package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:os"

/*
Write a message to a file.
*/
output_file_message :: proc(message: string, proc_data: rawptr) {
    fmt.fprintln((cast(^os.Handle)proc_data)^, message)
}

/*
Create a new logger that writes to the standard error.
*/
create_file_logger_from_namespace :: proc(
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
            output = output_file_message,
        } \
    )
}

/*
Create a new logger that writes to the stdndard error.
*/
create_file_logger_from_namespace_string :: proc(
    ns: string,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    namespace := namespace_from_string(ns) or_return
    return create_file_logger_from_namespace(namespace, level, enabled), nil
}

/*
Create a new logger that writes to the standard error.
*/
create_file_logger :: proc {
    create_file_logger_from_namespace,
    create_file_logger_from_namespace_string,
}
