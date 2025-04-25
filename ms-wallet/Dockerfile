FROM golang:1.21-alpine

RUN apk add --no-cache git

WORKDIR /app

# Copiar todos los archivos primero
COPY . .

# Ejecutar tidy con todos los archivos presentes (importantes los .go)
RUN go mod tidy

# Compilar la app
RUN go build -o app

EXPOSE 4000

CMD ["./app"]
