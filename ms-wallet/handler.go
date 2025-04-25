package main

import (
	"database/sql"
	"encoding/json"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

type Account struct {
	AccountID int    `json:"account_id"`
	UserID    int    `json:"user_id"`
	Alias     string `json:"alias"`
	CVU       string `json:"cvu"`
	CreatedAt string `json:"created_at"`
}

func GetAllAccounts(db *sql.DB) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		rows, err := db.Query("SELECT * FROM account")
		if err != nil {
			http.Error(w, err.Error(), 500)
			return
		}
		defer rows.Close()

		var accounts []Account
		for rows.Next() {
			var acc Account
			rows.Scan(&acc.AccountID, &acc.UserID, &acc.Alias, &acc.CVU, &acc.CreatedAt)
			accounts = append(accounts, acc)
		}
		json.NewEncoder(w).Encode(accounts)
	}
}

func GetAccountByID(db *sql.DB) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		id, _ := strconv.Atoi(mux.Vars(r)["id"])
		var acc Account
		err := db.QueryRow("SELECT * FROM account WHERE account_id = ?", id).
			Scan(&acc.AccountID, &acc.UserID, &acc.Alias, &acc.CVU, &acc.CreatedAt)

		if err != nil {
			http.Error(w, "Cuenta no encontrada", 404)
			return
		}
		json.NewEncoder(w).Encode(acc)
	}
}
