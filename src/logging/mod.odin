package logging

//-- Odin Standard Library
import "core:mem"
import "core:strings"

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
Log_Level :: enum {}
