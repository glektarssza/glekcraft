#!/usr/bin/env bash

# Ensure output directory exists before using it
mkdir -p ${HOME}/.secrets;

# Determine our passphrase variable from our second argument
eval "SECRET_PASSPHRASE=\${$2}";

# Decrypt the secret given in the first argument
gpg --quiet --yes --batch --decrypt --passphrase="${SECRET_PASSPHRASE}" \
    --output ${HOME}/.secrets/${1##.gpg} ${1};

# Store our result
RESULT=$?

# Clear our secret passphrase
unset SECRET_PASSPHRASE;

# Exit with the result code from gpg
exit ${RESULT};
