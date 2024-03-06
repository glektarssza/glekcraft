package logging_tests

import "core:slice"
import "core:testing"

import "app:logging"

@(test)
test_parse_namespace :: proc(t: ^testing.T) {
    //-- Given
    raw_ns := "a:raw:namespace"

    //-- When
    res, err := logging.parse_namespace(raw_ns)

    //-- Then
    testing.expect(t, slice.equal(res, ([]string){"a", "raw", "namespace"}))
}

@(test)
test_namespace_to_string :: proc(t: ^testing.T) {
    //-- Given
    raw_ns := ([]string){"a", "raw", "namespace"}

    //-- When
    res, err := logging.namespace_to_string(raw_ns)

    //-- Then
    testing.expect_value(t, res, "a:raw:namespace")
}
