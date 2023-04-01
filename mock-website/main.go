package main

import (
	"bytes"
	"fmt"
	"image/jpeg"
	"io/ioutil"
	"net/http"
	"os"
	"path/filepath"
	"time"

	"github.com/boombuler/barcode"
	"github.com/boombuler/barcode/qr"
)

func main() {
	http.Handle("/", http.FileServer(http.Dir("./")))
	http.HandleFunc("/register", registerHandler)
	http.HandleFunc("/login", loginHandler)
	http.ListenAndServe(":3000", nil)
}

var state = 0

func registerHandler(w http.ResponseWriter, r *http.Request) {
	filename := ""
	if state%3 == 0 {
		filename = "myimms_pre.html"
		// http.ServeFile(w, r, "myimms_pre.html")
	} else if state%3 == 1 {
		filename = "myimms_qr.html"
		// http.ServeFile(w, r, "myimms_qr.html")
	} else {
		filename = "myimms_post.html"
		// http.ServeFile(w, r, "myimms_post.html")
	}
	// fmt.Println(filename)
	file, _ := os.Open(filename)
	defer file.Close()
	data, _ := ioutil.ReadAll(file)
	var content []byte = data
	http.ServeContent(w, r, filepath.Base(r.URL.Path), time.Time{}, bytes.NewReader(content))
	state += 1
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	generateQr()
	http.ServeFile(w, r, "login.html")
}

func generateQr() {
	token := getLoginToken()
	qrcode, _ := qr.Encode(token, qr.L, qr.Auto)
	size := 512
	qrcode, _ = barcode.Scale(qrcode, size, size)
	f, _ := os.Create("qrcode.jpg")
	defer f.Close()
	jpeg.Encode(f, qrcode, nil)
}

func getLoginToken() string {
	resp, err := http.Get("https://kyjqqy73i2.execute-api.ap-southeast-1.amazonaws.com/default/login")
	if err != nil {
		fmt.Println(err)
	}
	defer resp.Body.Close()
	body, _ := ioutil.ReadAll(resp.Body)
	return string(body)
}
