#!/usr/bin/env bash

SCRIPT_SOURCE=${BASH_SOURCE[0]};
while [ -L "${SCRIPT_SOURCE}" ]; do
  SCRIPT_DIR="$(cd -P "$(dirname "${SCRIPT_SOURCE}")" >/dev/null 2>&1 && pwd)";
  SCRIPT_SOURCE="$(readlink "${SCRIPT_SOURCE}")";
  [[ ${SCRIPT_SOURCE} != /* ]] && SCRIPT_SOURCE="$SCRIPT_DIR/${SCRIPT_SOURCE}";
done
SCRIPT_DIR="$(cd -P "$(dirname "${SCRIPT_SOURCE}")" >/dev/null 2>&1 && pwd)";

# Try to decrypt secret file if required
if [[ "${1}" =~ .gpg$ ]]; then
    ${SCRIPT_DIR}/decrypt-secret.sh "${1}" "${2}";
    # Set file name to decrypted file
    SECRET_FILE="${1##.gpg}";
else
    # Set file name to first argument
    SECRET_FILE="${1}";
fi

# Determine our passphrase variable from our second argument
eval "SECRET_PASSPHRASE=\${$2}";

# Import the gpg key
gpg --quiet --yes --batch --passphrase="${SECRET_PASSPHRASE}" --import "${SECRET_FILE}";

# Store our result
RESULT=$?;

# Unset variables before exiting
unset SECRET_FILE;
unset SECRET_PASSPHRASE;

# Exit with the result code from gpg
exit ${RESULT};
