# shellcheck shell=bash

# Get the directory the script is running from.
# === Outputs ===
# The path to the directory the script is running from.
# === Returns ===
# `0` - the function succeeded.
# `1` - a `cd` call failed.
# `2` - a `popd` call failed.
function get_script_dir() {
    pushd . >/dev/null
    local SCRIPT_PATH="${BASH_SOURCE[0]:-$0}"
    while [[ -L "${SCRIPT_PATH}" ]]; do
        cd "$(dirname -- "${SCRIPT_PATH}")" || return 1
        SCRIPT_PATH="$(readlink -f -- "$SCRIPT_PATH")"
    done
    cd "$(dirname -- "$SCRIPT_PATH")" >/dev/null || return 1
    SCRIPT_PATH="$(pwd)"
    # shellcheck disable=SC2164
    popd >/dev/null 2>&1
    echo "${SCRIPT_PATH}"
    return 0
}

if [[ -z "${SCRIPT_DIR}" ]] && ! SCRIPT_DIR="$(get_script_dir)"; then
    return 1
fi
if [[ -z "${_LIB_PATH}" ]]; then
    _LIB_PATH="${SCRIPT_DIR}"
fi

if [[ -n "${_LIB_OS}" ]]; then
    return 0
fi
declare _LIB_OS="loaded"

# Get the OS distribution information.
# === Outputs ===
# Information about the current distribution.
# === Returns ===
# The result of the operation.
function lib::os::get_distro_info() {
    cat /etc/*-release
    return $?
}

# Get the OS distribution name.
# === Outputs ===
# The OS distribution name.
# === Returns ===
# The result of the operation.
function lib::os::get_distro_name() {
    lib::os::get_distro_info | grep '^NAME=' | sed 's/NAME=//'
    return $?
}

# Get the OS distribution ID.
# === Outputs ===
# The OS distribution ID.
# === Returns ===
# The result of the operation.
function lib::os::get_distro_id() {
    lib::os::get_distro_info | grep '^ID=' | sed 's/ID=//'
    return $?
}

# Get the OS distribution build id.
# === Outputs ===
# The OS distribution build id.
# === Returns ===
# The result of the operation.
function lib::os::get_distro_build_id() {
    lib::os::get_distro_info | grep '^BUILD_ID=' | sed 's/BUILD_ID=//'
    return $?
}
