#!/bin/zsh

if ! dotnet dev-certs https --check --quiet; then
    dotnet dev-certs https --clean
    dotnet dev-certs https --trust -ep ".devcontainer/dev-certs.crt" --format Pem

    echo "download the certificate from the container (/workspaces/project-name/dev-certs.crt ) and trust on your host"
    echo "e.g. sudo security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain <certificate>.crt"
fi

dotnet dev-certs https --check --trust --verbose
