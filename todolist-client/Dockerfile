FROM node:20-slim AS builder
WORKDIR /app

COPY todolist-client/package*.json ./
RUN npm install

COPY todolist-client .
RUN npm run build

FROM node:20-slim AS runtime
WORKDIR /app

COPY --from=builder /app ./
RUN npm install

EXPOSE 4000

CMD ["npm", "start"]
