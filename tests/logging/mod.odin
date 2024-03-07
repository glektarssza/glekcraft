package logging_tests

//-- Odin Standard Library
import "core:slice"
import "core:testing"

//-- Project Code
import "app:logging"

@(test)
test_namespace_from_string :: proc(t: ^testing.T) {
    //-- Given
    raw_str := "a:random:namespace"

    //-- When
    res, err := logging.namespace_from_string(raw_str)

    //-- Then
    testing.expect_value(t, err, nil)
    testing.expect(t, slice.equal(res, ([]string){"a", "random", "namespace"}))
}
