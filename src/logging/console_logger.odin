package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:strings"

console_logger_format_proc :: proc(record: Log_Record) -> string {
    return record.message
}

console_logger_output_proc :: proc(msg: string) {
    fmt.println(msg)
}

create_console_logger_from_namespace :: proc(
    ns: Namespace,
    level := Log_Level.Info,
    enabled := true,
) -> Logger {
    res := Logger {
        namespace = ns,
        enabled   = enabled,
        level     = level,
        format    = console_logger_format_proc,
        output    = console_logger_output_proc,
    }
    return res
}

create_console_logger_from_namespace_string :: proc(
    raw_str: string,
    level := Log_Level.Info,
    enabled := true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    ns := namespace_from_string(raw_str) or_return
    return create_console_logger_from_namespace(ns, level, enabled), nil
}

create_console_logger :: proc {
    create_console_logger_from_namespace,
    create_console_logger_from_namespace_string,
}
