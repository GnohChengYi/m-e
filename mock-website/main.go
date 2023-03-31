package main

import (
	"net/http"
)

func main() {
	http.Handle("/", http.FileServer(http.Dir("./")))
	http.HandleFunc("/login", createLoginPage)
	http.ListenAndServe(":3000", nil)
}

func createLoginPage(w http.ResponseWriter, r *http.Request) {
	http.ServeFile(w, r, "./login.html")
}
