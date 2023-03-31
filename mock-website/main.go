package main

import (
	"fmt"
	"image/jpeg"
	"io/ioutil"
	"net/http"
	"os"

	"github.com/boombuler/barcode"
	"github.com/boombuler/barcode/qr"
)

func main() {
	http.Handle("/", http.FileServer(http.Dir("./")))
	http.HandleFunc("/login", loginHandler)
	http.ListenAndServe(":3000", nil)
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	// TODO get token from API
	token := getLoginToken()
	qrcode, _ := qr.Encode(token, qr.L, qr.Auto)
	size := 512
	qrcode, _ = barcode.Scale(qrcode, size, size)
	f, _ := os.Create("qrcode.jpg")
	defer f.Close()
	jpeg.Encode(f, qrcode, nil)
	http.ServeFile(w, r, "login.html")
}

func getLoginToken() string {
	// TODO change to cloud server path
	resp, err := http.Get("http://127.0.0.1:8080/login")
	if err != nil {
		fmt.Println(err)
	}
	defer resp.Body.Close()
	body, _ := ioutil.ReadAll(resp.Body)
	return string(body)
}
