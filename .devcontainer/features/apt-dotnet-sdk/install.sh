#!/usr/bin/env sh

# Import our function library.
. ./functions.sh;

write_debug "Installing dotnet SDK ${VERSION}...";

# Install or upgrade dotnet-sdk.
apt_install_or_upgrade_multiple dotnet-sdk-${VERSION};

if [ $? -ne 0 ]; then
    write_error "Failed to install dotnet SDK ${VERSION}!";
    exit 1;
fi
