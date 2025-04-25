package main

import (
	"log"
	"net/http"
	"github.com/gorilla/mux"
	"github.com/joho/godotenv"
	
)

func LoadEnv() {
	err := godotenv.Load()
	if err != nil {
		log.Println("⚠️  No se pudo cargar .env (usando variables del entorno si existen)")
	}
}

func main() {
	LoadEnv()
	db := InitDB()
	defer db.Close()

	go ConsumeMessages(db)

	r := mux.NewRouter()
	r.HandleFunc("/accounts", GetAllAccounts(db)).Methods("GET")
	r.HandleFunc("/accounts/{id}", GetAccountByID(db)).Methods("GET")

	log.Println("🟢 Servidor escuchando en :4000")
	log.Fatal(http.ListenAndServe(":4000", r))
}
