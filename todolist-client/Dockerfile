FROM node:20-slim AS builder
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

FROM node:20-slim AS runtime
WORKDIR /app

COPY --from=builder /app ./
COPY certs/https /app/certs/https

RUN npm install --global ts-node

EXPOSE 4000

CMD ["npm", "run", "start"]
