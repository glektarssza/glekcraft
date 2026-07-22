cmake_minimum_required(VERSION 4.4)
project(glekcraft
    VERSION 0.1.0
    DESCRIPTION "A simple Minecraft clone written from scratch in Odin using OpenGL."
    HOMEPAGE_URL "https://github.com/glektarssza/glekcraft"
    LANGUAGES CXX)

set(GLEKCRAFT_SOURCES "src/main.cpp")

add_executable(glekcraft ${GLEKCRAFT_SOURCES})
install(TARGETS glekcraft RUNTIME)
include(CPack)
