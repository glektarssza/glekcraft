#!/usr/bin/env sh

# Import our function library.
. ./functions.sh;

write_debug "Downloading Microsoft GPG key...";

# Download GPG key.
mkdir -p /usr/share/keyrings/ && \
wget -O - https://packages.microsoft.com/keys/microsoft.asc | \
gpg --dearmor | tee /usr/share/keyrings/microsoft-apt.gpg;

if [ $? -ne 0 ]; then
    write_error "Failed to download Microsoft GPG key!";
    exit 1;
fi

write_debug "Installing Microsoft APT source list...";

wget https://packages.microsoft.com/config/debian/12/prod.list -O - | \
sed 's/signed-by=\([^]]\+\)/signed-by=\/usr\/share\/keyrings\/microsoft-apt.gpg/' |
tee /etc/apt/sources.list.d/microsoft.list;

if [ $? -ne 0 ]; then
    write_error "Failed to install Microsoft APT source list!";
    exit 1;
fi
