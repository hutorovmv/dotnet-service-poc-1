New-Item -ItemType Directory -Force -Path ./TodoList.Client/certs
winget install FiloSottile.mkcert --accept-package-agreements --accept-source-agreements
mkcert -install
mkcert -key-file certs/key.key -cert-file certs/cert.crt localhost