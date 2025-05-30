FROM node:slim as builder
WORKDIR /app
COPY package*.json ./
RUN npm install

ARG ANGULAR_ENVIRONMENT
COPY . .
RUN npx ng build --configuration=${ANGULAR_ENVIRONMENT}

FROM node:slim
WORKDIR /app

COPY --from=builder /app/dist/TodoList.Client /app/dist/TodoList.Client
COPY --from=builder /app/node_modules /app/node_modules

COPY certs /app/certs

EXPOSE 4000

CMD ["node", "dist/TodoList.Client/server/server.mjs"]