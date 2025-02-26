#!/usr/bin/env bash

# Ensure output directory exists before using it
mkdir -p "${HOME}/.secrets";

# Set file name to first argument
SECRET_FILE="${1}";

# Determine our passphrase variable from our second argument
eval "SECRET_PASSPHRASE=\${$2}";

# Decrypt the secret given in the first argument
gpg --quiet --yes --batch --decrypt --passphrase="${SECRET_PASSPHRASE}" \
    --output "${HOME}/.secrets/${SECRET_FILE##.gpg}" "${SECRET_FILE}";

# Store our result
RESULT=$?;

# Unset variables before exiting
unset SECRET_FILE;
unset SECRET_PASSPHRASE;

# Exit with the result code from gpg
exit ${RESULT};
