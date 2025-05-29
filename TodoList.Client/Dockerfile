FROM node:slim as builder
WORKDIR /app
COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

FROM node:slim
WORKDIR /app

COPY --from=builder /app/dist/TodoList.Client /app/dist/TodoList.Client
COPY --from=builder /app/node_modules /app/node_modules

COPY certs /app/certs

EXPOSE 4000

CMD ["node", "dist/TodoList.Client/server/server.mjs"]