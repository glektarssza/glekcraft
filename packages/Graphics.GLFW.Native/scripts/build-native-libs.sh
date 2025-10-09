#!/usr/bin/env bash

declare SCRIPT_PATH PROJECT_ROOT TOOLCHAINS_ROOT RUNTIMES_ROOT TEMP_ROOT;

# Our cleanup function
# shellcheck disable=SC2329
function cleanup() {
    rm -rf "${TEMP_ROOT}";
    unset SCRIPT_PATH PROJECT_ROOT TOOLCHAINS_ROOT RUNTIMES_ROOT TEMP_ROOT;
}

# Run our cleanup routine on exit
trap cleanup EXIT;

# Reset the terminal graphics.
function sgr_reset() {
    printf "\x1b[0m";
}

# Output a foreground color set code.
function sgr_fg_8bit() {
    printf "\x1b[38;5;%sm" "$1";
}

# Log an error message to the standard error stream.
function log_error() {
  printf "$(sgr_fg_8bit 196)[ERROR]$(sgr_reset) %s\n" "$*" >&2;
}

# Log a warning message to the standard output stream.
function log_warning() {
  printf "$(sgr_fg_8bit 214)[WARN]$(sgr_reset) %s\n" "$*";
}

# Log an information message to the standard output stream.
function log_info() {
  printf "$(sgr_fg_8bit 111)[INFO]$(sgr_reset) %s\n" "$*";
}

# Log a verbose message to the standard output stream.
function log_verbose() {
  if [[ ! -n "${VERBOSE}" || ! "${VERBOSE,,}" =~ 1|true ]]; then
    return 0;
  fi
  printf "$(sgr_fg_8bit 171)[VERBOSE]$(sgr_reset) %s\n" "$*";
}

# Get the directory this script is running from.
function get_script_dir() {
    local SOURCE_PATH="${BASH_SOURCE[0]}";
    local SYMLINK_DIR;
    local SCRIPT_DIR;
    while [ -L "${SOURCE_PATH}" ]; do
        SYMLINK_DIR="$(cd -P "$(dirname "${SOURCE_PATH}")" > /dev/null 2>&1 && pwd)";
        SOURCE_PATH="$(readlink "${SOURCE_PATH}")";
        if [[ "${SOURCE_PATH}" != /* ]]; then
            SOURCE_PATH="${SYMLINK_DIR}/${SOURCE_PATH}";
        fi
    done
    SCRIPT_DIR="$(cd -P "$(dirname "${SOURCE_PATH}")" > /dev/null 2>&1 && pwd)";
    echo "${SCRIPT_DIR}";
}

# The path to the directory this script is living in
SCRIPT_PATH="$(get_script_dir)";

# The path to the project root directory
PROJECT_ROOT="$(readlink -f "${SCRIPT_PATH}/..")";

# The path to the project root directory
TOOLCHAINS_ROOT="$(readlink -f "${SCRIPT_PATH}/toolchains")";

# The path to the runtimes root directory
RUNTIMES_ROOT="$(readlink -f "${PROJECT_ROOT}/runtimes")";

# The temporary directory
TEMP_ROOT="$(readlink -f "${PROJECT_ROOT}/.temp")";

for TOOLCHAIN in "${TOOLCHAINS_ROOT}"/*; do
    RUNTIME_NAME="$(basename -s ".cmake" "${TOOLCHAIN}")";
    if [[ "${RUNTIME_NAME}" == "lib" ]]; then
        continue;
    fi
    if [[ "$(uname)" != "Darwin" && "${RUNTIME_NAME}" =~ osx ]]; then
        continue;
    fi
    log_info "Building GLFW for runtime \"${RUNTIME_NAME}\"...";
    log_info "Running CMake...";
    cmake --toolchain "${TOOLCHAINS_ROOT}/${RUNTIME_NAME}.cmake" \
        -S "${PROJECT_ROOT}/deps/glfw/" \
        -B "${TEMP_ROOT}/lib-build/glfw/${RUNTIME_NAME}/" \
        -DGLFW_BUILD_TESTS=OFF -DGLFW_BUILD_DOCS=OFF -DGLFW_BUILD_EXAMPLES=OFF \
        -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON \
        -DCMAKE_INSTALL_PREFIX="${TEMP_ROOT}/dist/glfw/${RUNTIME_NAME}/";
    STATUS_CODE=$?;
    if [[ $STATUS_CODE -ne 0 ]]; then
        log_error "$(sgr_fg_8bit 9)✕$(sgr_reset) Failed to configure CMake build!";
        exit 1;
    fi
    cmake --build "${TEMP_ROOT}/lib-build/glfw/${RUNTIME_NAME}/" --config release;
    STATUS_CODE=$?;
    if [[ $STATUS_CODE -ne 0 ]]; then
        log_error "$(sgr_fg_8bit 9)✕$(sgr_reset) Failed to build with CMake!";
        exit 1;
    fi
    cmake --install "${TEMP_ROOT}/lib-build/glfw/${RUNTIME_NAME}/" --config release;
    STATUS_CODE=$?;
    if [[ $STATUS_CODE -ne 0 ]]; then
        log_error "$(sgr_fg_8bit 9)✕$(sgr_reset) Failed to install with CMake!";
        exit 1;
    fi
    log_info "$(sgr_fg_8bit 10)✓$(sgr_reset) Built GLFW for runtime \"${RUNTIME_NAME}\"!";
    log_info "Copying updated GLFW binaries for runtime \"${RUNTIME_NAME}\"...";
    if [[ "${RUNTIME_NAME}" =~ ^win ]]; then
        cp "${TEMP_ROOT}"/dist/glfw/"${RUNTIME_NAME}"/bin/glfw3.dll \
            "${RUNTIMES_ROOT}/${RUNTIME_NAME}/glfw3.dll";
    elif [[ "${RUNTIME_NAME}" =~ ^linux ]]; then
        cp "${TEMP_ROOT}"/dist/glfw/"${RUNTIME_NAME}"/lib/libglfw.so \
            "${RUNTIMES_ROOT}/${RUNTIME_NAME}/libglfw3.so";
    elif [[ "${RUNTIME_NAME}" =~ ^osx ]]; then
        cp "${TEMP_ROOT}"/dist/glfw/"${RUNTIME_NAME}"/lib/libglfw.dylib \
            "${RUNTIMES_ROOT}/${RUNTIME_NAME}/libglfw3.dylib";
    fi
    STATUS_CODE=$?;
    if [[ $STATUS_CODE -ne 0 ]]; then
        log_error "$(sgr_fg_8bit 9)✕$(sgr_reset) Failed to copy updated GLFW binaries for runtime \"${RUNTIME_NAME}\"!";
        exit 1;
    fi
    log_info "$(sgr_fg_8bit 10)✓$(sgr_reset) Copied updated GLFW binaries for runtime \"${RUNTIME_NAME}\"!";
done
