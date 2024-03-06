package logging

import "core:mem"
import "core:strings"

/*
The character to use to join/split namespace components.
*/
NAMESPACE_COMPONENT_SEPARATOR :: ":"

/*
A type alias for a namespace.
*/
Namespace :: []string

/*
A structure which provides a container for various information about how to log
to a particular namespace.
*/
Logger :: struct {}

/*
Parse a namespace from a string.
*/
parse_namespace :: proc(
    raw_namespace: string,
) -> (
    res: Namespace,
    err: mem.Allocator_Error,
) {
    return strings.split(
        raw_namespace,
        auto_cast NAMESPACE_COMPONENT_SEPARATOR,
    )
}

/*
Convert a namespace to a string.
*/
namespace_to_string :: proc(
    namespace: Namespace,
) -> (
    res: string,
    err: mem.Allocator_Error,
) {
    return strings.join(namespace, NAMESPACE_COMPONENT_SEPARATOR)
}
