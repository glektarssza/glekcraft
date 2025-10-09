set(CMAKE_SYSTEM_NAME "Windows")
set(CMAKE_SYSTEM_VERSION "11")
set(CMAKE_SYSTEM_PROCESSOR "x86_64")

include("${CMAKE_CURRENT_LIST_DIR}/lib/target-triplet.cmake")

set(CMAKE_C_COMPILER "clang")
set(CMAKE_C_COMPILER_TARGET "${_CLANG_TARGET}")
set(CMAKE_CXX_COMPILER "clang++")
set(CMAKE_CXX_COMPILER_TARGET "${_CLANG_TARGET}")

include("${CMAKE_CURRENT_LIST_DIR}/lib/llvm-windres.cmake")

include("${CMAKE_CURRENT_LIST_DIR}/lib/host-tools.cmake")
