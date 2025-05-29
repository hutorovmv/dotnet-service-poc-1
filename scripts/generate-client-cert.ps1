New-Item -ItemType Directory -Force -Path ./TodoList.Client/certs
winget install FiloSottile.mkcert --accept-package-agreements --accept-source-agreements
mkcert -install
mkcert -key-file ./TodoList.Client/certs/key.key -cert-file ./TodoList.Client/certs/cert.crt localhost