package glekcraft

//-- Odin Standard Library
import "core:fmt"

//-- Project Code
import "./logging"

/*
The program entry point.
*/
main :: proc() {
    fmt.println(logging.get_message())
}
