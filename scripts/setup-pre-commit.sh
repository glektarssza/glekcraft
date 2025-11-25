#!/usr/bin/env bash

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
    popd >/dev/null || return 2
    echo "${SCRIPT_PATH}"
    return 0
}

# Set up script variables.
function setup_variables() {
    declare SCRIPT_DIR PROJECT_ROOT DISTRO PRE_COMMIT_PATHS
    declare DEFAULT_PRE_COMMIT_INDEX PRE_COMMIT_INDEX
}

# Set up the script.
function setup() {
    setup_variables
}
setup

function cleanup_variables() {
    unset SCRIPT_DIR PROJECT_ROOT DISTRO PRE_COMMIT_PATHS
    unset DEFAULT_PRE_COMMIT_INDEX PRE_COMMIT_INDEX
}

# Clean up as the script exits.
# shellcheck disable=SC2329
function cleanup() {
    cleanup_variables
}

# Run our cleanup routine on exit
trap cleanup EXIT

if [[ -z "${SCRIPT_DIR}" ]] && ! SCRIPT_DIR="$(get_script_dir)"; then
    return 1
fi

_LIB_PATH="$(realpath -e -- "${SCRIPT_DIR}/lib/")"

# shellcheck source=./lib/logging.sh
source "${_LIB_PATH}/logging.sh"
# shellcheck source=./lib/strings.sh
source "${_LIB_PATH}/strings.sh"
# shellcheck source=./lib/os.sh
source "${_LIB_PATH}/os.sh"

# Prompt the user if it's okay to continue.
# === Inputs ===
# `$1` - The prompt to display.
# `$2` - The default response. Defaults to `y`.
# === Returns ===
# `0` - Okay to continue.
# `1` - Not okay to continue.
# `2` - Some other error.
function prompt_to_continue() {
    local PROMPT="$1"
    local DEFAULT_RESP="${2:-y}"
    local RESP=""
    if [[ -z "${PROMPT}" ]]; then
        lib::logging::error "No prompt provided to 'prompt_to_continue'!"
        return 2
    fi
    while true; do
        read -p "${PROMPT}" -r RESP
        case "$(lib::strings::to_lower_case "${RESP:-${DEFAULT_RESP}}")" in
            y) return 0 ;;
            n) return 1 ;;
            *)
                RESP=""
                lib::logging::error "Invalid response \"${RESP}\"! Please try again!"
                ;;
        esac
    done
}

# The path to the project root directory
PROJECT_ROOT="$(realpath -e -- "${SCRIPT_DIR}/..")"

# Determine our distribution
DISTRO="$(lib::os::get_distro_id)"

lib::logging::verbose "Determined OS distro to be \"${DISTRO}\""

if [[ -n "${CI}" ]]; then
    lib::logging::warning "Running in a CI environment, not setting up pre-commit!"
    exit 0
fi

# Locate pre-commit
PRE_COMMIT="$(command -v pre-commit 2>/dev/null)"
if [[ -z "${PRE_COMMIT}" ]]; then
    lib::logging::warning "\"pre-commit\" is not installed, attempting to install!"
    PIPX="$(command -v pipx 2>/dev/null)"
    if [[ -z "${PIPX}" ]]; then
        lib::logging::warning "\"pipx\" is not installed, attempting to install!"
        case "${DISTRO}" in
            arch)
                PACMAN="pacman"
                PACMAN_FLAGS=(-S --noconfirm --needed --asexplicit)
                PACKAGE_NAME="python-pipx"
                ;;
            ubuntu)
                PACMAN="apt"
                PACMAN_FLAGS=(install --assume-yes --no-install-recommends)
                PACKAGE_NAME="python-pipx"
                ;;
            debian)
                PACMAN="apt"
                PACMAN_FLAGS=(install --assume-yes --no-install-recommends)
                PACKAGE_NAME="pipx"
                ;;
        esac
        lib::logging::info "We're going to attempt to install \"pipx\", this will require admin permissions!"
        if [[ ! -x "${PACMAN}" ]]; then
            lib::logging::warning "Unable to execute \"${PACMAN}\" as we are, trying to elevate..."
            if command -v sudo >/dev/null 2>&1; then
                lib::logging::verbose "Trying to elevate via \"sudo\"..."
                if ! prompt_to_continue "We're about to run 'sudo \"${SHELL}\" -i -c \"${PACMAN} ${PACMAN_FLAGS[*]} ${PACKAGE_NAME}\"'. Is this okay? [y/N] " "n"; then
                    lib::logging::error "Aborting!"
                    exit 1
                fi
                lib::logging::info "Proceeding!"
                sudo "${SHELL}" -i -c "${PACMAN} ${PACMAN_FLAGS[*]} ${PACKAGE_NAME}"
                STATUS_CODE=$?
            elif command -v su >/dev/null 2>&1; then
                lib::logging::verbose "Trying to elevate via \"su\"..."
                if ! prompt_to_continue "We're about to run 'su --login --command=\n\"${PACMAN} ${PACMAN_FLAGS[*]} ${PACKAGE_NAME}\"'.\nIs this okay? [y/N] " "n"; then
                    lib::logging::error "Aborting!"
                    exit 1
                fi
                lib::logging::info "Proceeding!"
                su --login --command="${PACMAN} ${PACMAN_FLAGS[*]} ${PACKAGE_NAME}"
                STATUS_CODE=$?
            else
                lib::logging::error "Failed to elevate!"
                exit 1
            fi
        else
            # shellcheck disable=SC2048,SC2086
            "${PACMAN}" ${PACMAN_FLAGS[*]} ${PACKAGE_NAME}
            STATUS_CODE=$?
        fi
        if [[ "${STATUS_CODE}" != "0" ]]; then
            lib::logging::verbose "\"${PACMAN}\" exited with code \"${STATUS_CODE}\"!"
            lib::logging::error "Failed to install \"pipx\"!"
            exit $STATUS_CODE
        fi
        lib::logging::info "\"pipx\" was installed successfully!"
        PIPX="$(command -v pipx 2>/dev/null)"
    fi
    "${PIPX}" install pre-commit
    STATUS_CODE=$?
    if [[ "${STATUS_CODE}" != "0" ]]; then
        lib::logging::verbose "\"pipx\" exited with code \"${STATUS_CODE}\"!"
        lib::logging::error "Failed to install \"pre-commit\"!"
        exit $STATUS_CODE
    fi
    lib::logging::info "\"pre-commit\" was installed successfully!"
    PRE_COMMIT="$(command -v pre-commit 2>/dev/null)"
    if [[ -z "${PRE_COMMIT}" ]]; then
        lib::logging::warning "Still cannot find \"pre-commit\", trying some well-known locations..."
        mapfile -t PRE_COMMIT_PATHS < <(find ~ -maxdepth 4 \( -type f -or -type l \) -name pre-commit -printf '%p\n')
        if [[ "${#PRE_COMMIT_PATHS[@]}" -le 0 ]]; then
            lib::logging::error "Failed to locate \"pre-commit\"!"
            exit 1
        fi
        DEFAULT_PRE_COMMIT_INDEX="${#PRE_COMMIT_PATHS[*]}"
        echo -e "Found the following \"pre-commit\" executables:\n$(echo "${PRE_COMMIT_PATHS[*]}" | awk -F' ' '{for(i=1;i<=NF;i+=1){print i": "$i;}}')"
        if [[ -t 0 ]]; then
            read -rep "Which one do you want to use? [index (default (${DEFAULT_PRE_COMMIT_INDEX})] " PRE_COMMIT_INDEX
            if [[ -z "${PRE_COMMIT_INDEX}" ]]; then
                PRE_COMMIT_INDEX="$((DEFAULT_PRE_COMMIT_INDEX - 1))"
            fi
            while [[ ! "${PRE_COMMIT_INDEX}" =~ [[:digit:]]+ || "${PRE_COMMIT_INDEX}" -lt 0 || "${PRE_COMMIT_INDEX}" -ge "${#PRE_COMMIT_PATHS[@]}" ]]; do
                lib::logging::error "Value ${PRE_COMMIT_INDEX} is not valid, try again!"
                echo -e "Found the following \"pre-commit\" executables:\n$(echo "${PRE_COMMIT_PATHS[*]}" | awk -F' ' '{for(i=1;i<=NF;i+=1){print i": "$i;}}')"
                read -rep "Which one do you want to use? [index (default ${DEFAULT_PRE_COMMIT_INDEX})]" PRE_COMMIT_INDEX
            done
        else
            PRE_COMMIT_INDEX="${DEFAULT_PRE_COMMIT_INDEX}"
            lib::logging::warning "Standard input not available, pre-selecting index $((PRE_COMMIT_INDEX + 1))!"
        fi
        lib::logging::verbose "\"pre-commit\" option \"$((PRE_COMMIT_INDEX + 1))\" picked"
        PRE_COMMIT="${PRE_COMMIT_PATHS[PRE_COMMIT_INDEX]}"
        lib::logging::verbose "Selected \"pre-commit\" executable \"${PRE_COMMIT}\""
    fi
fi

pushd "${PROJECT_ROOT}" >/dev/null 2>&1 || (lib::logging::error "Failed to enter project root directory!" && exit 1) || exit 1

lib::logging::info "Installing pre-commit hooks..."
"${PRE_COMMIT}" install --overwrite --hook-type pre-commit --hook-type pre-push
STATUS_CODE=$?
if [[ "${STATUS_CODE}" != "0" ]]; then
    lib::logging::verbose "\"pre-commit\" exited with code \"${STATUS_CODE}\"!"
    lib::logging::error "Failed to install pre-commit hooks!"
    exit $STATUS_CODE
fi

popd >/dev/null 2>&1 || (lib::logging::error "Failed to exit project root directory!" && exit 1) || exit 1

# On success, exit
exit 0
