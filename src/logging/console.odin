package logging

//-- Odin Standard Library
import "core:fmt"
import "core:mem"
import "core:strings"
import "core:time"

/*
Format a message destined for console output.
*/
format_console_message :: proc(
    record: Log_Record,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    year, month, day := time.date(record.timestamp)
    hour, minute, second := time.clock_from_time(record.timestamp)

    sb := strings.builder_make()
    strings.write_int(&sb, year)
    strings.write_rune(&sb, '-')
    strings.write_string(&sb, fmt.aprintf("%02d", month))
    strings.write_rune(&sb, '-')
    strings.write_string(&sb, fmt.aprintf("%02d", day))
    strings.write_rune(&sb, ' ')
    strings.write_string(&sb, fmt.aprintf("%02d", hour))
    strings.write_rune(&sb, ':')
    strings.write_string(&sb, fmt.aprintf("%02d", minute))
    strings.write_rune(&sb, ':')
    strings.write_string(&sb, fmt.aprintf("%02d", second))
    strings.write_rune(&sb, ' ')
    strings.write_rune(&sb, '[')
    strings.write_string(&sb, log_level_to_string(record.level))
    strings.write_rune(&sb, ']')
    strings.write_rune(&sb, ' ')
    strings.write_string(&sb, record.message)
    return strings.to_string(sb), nil
}

/*
Write a message to the console.
*/
output_console_message :: proc(message: string) {
    fmt.println(message)
}

/*
Create a new logger that writes to the console.
*/
create_console_logger_from_namespace :: proc(
    ns: Namespace,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> Logger {
    return(
        Logger {
            namespace = ns,
            level = level,
            enabled = enabled,
            format = format_console_message,
            output = output_console_message,
        } \
    )
}

/*
Create a new logger that writes to the console.
*/
create_console_logger_from_namespace_string :: proc(
    ns: string,
    level: Log_Level = Log_Level.Info,
    enabled: bool = true,
) -> (
    res: Logger,
    err: mem.Allocator_Error,
) {
    namespace := namespace_from_string(ns) or_return
    return create_console_logger_from_namespace(namespace, level, enabled), nil
}

/*
Create a new logger that writes to the console.
*/
create_console_logger :: proc {
    create_console_logger_from_namespace,
    create_console_logger_from_namespace_string,
}
