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

@(test)
test_namespace_to_string :: proc(t: ^testing.T) {
    //-- Given
    ns := ([]string){"a", "random", "namespace"}

    //-- When
    res, err := logging.namespace_to_string(ns)

    //-- Then
    testing.expect_value(t, err, nil)
    testing.expect_value(t, res, "a:random:namespace")
}

@(test)
test_log_level_to_string :: proc(t: ^testing.T) {
    //-- Given
    level := logging.Log_Level.Fatal

    //-- When
    res := logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "FATAL")

    //-- Given
    level = logging.Log_Level.Error

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "ERROR")

    //-- Given
    level = logging.Log_Level.Warning

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "WARNING")

    //-- Given
    level = logging.Log_Level.Info

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "INFO")

    //-- Given
    level = logging.Log_Level.Verbose

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "VERBOSE")

    //-- Given
    level = logging.Log_Level.Debug

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "DEBUG")

    //-- Given
    level = logging.Log_Level.None

    //-- When
    res = logging.log_level_to_string(level)

    //-- Then
    testing.expect_value(t, res, "")
}
