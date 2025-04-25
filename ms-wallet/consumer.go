package main

import (
	"database/sql"
	"encoding/json"
	"log"
	"math/rand"
	"os"
	"strconv"
	"time"

	"github.com/streadway/amqp"
)

type Evento struct {
	Tipo string        `json:"tipo"`
	Data UsuarioCreado `json:"data"`
}

type UsuarioCreado struct {
	UserID int    `json:"user_id"`
	Email  string `json:"email"`
}

func RandomSuffix() string {
	rand.Seed(time.Now().UnixNano())
	return strconv.Itoa(100000 + rand.Intn(900000))
}

func ConsumeMessages(db *sql.DB) {
	conn, err := amqp.Dial(os.Getenv("RABBITMQ_URL"))
	if err != nil {
		log.Fatal("❌ Error conectando a RabbitMQ:", err)
	}
	defer conn.Close()

	ch, err := conn.Channel()
	if err != nil {
		log.Fatal("❌ Error creando canal:", err)
	}
	defer ch.Close()

	queueName := os.Getenv("QUEUE")
	if queueName == "" {
		log.Fatal("❌ QUEUE no está definida en el entorno")
	}

	msgs, err := ch.Consume(queueName, "", true, false, false, false, nil)
	if err != nil {
		log.Fatal("❌ Error consumiendo mensajes:", err)
	}

	log.Printf("📡 Escuchando eventos en la cola '%s'...\n", queueName)

	for msg := range msgs {
		log.Printf("📩 Mensaje recibido: %s\n", msg.Body)

		var evento struct {
			Tipo string `json:"tipo"`
			Data UsuarioCreado `json:"data"`
		}

		if err := json.Unmarshal(msg.Body, &evento); err != nil {
			log.Println("❌ Error al parsear mensaje:", err)
			continue
		}

		u := evento.Data

		_, err := db.Exec(`INSERT INTO account (user_id, alias, cvu, created_at)
			VALUES (?, ?, ?, NOW())`,
			u.UserID,
			"alias"+RandomSuffix(),
			"CVU"+RandomSuffix())

		if err != nil {
			log.Println("❌ Error al insertar cuenta:", err)
		} else {
			log.Println("✅ Cuenta creada para usuario:", u.UserID)
		}
	}
}

