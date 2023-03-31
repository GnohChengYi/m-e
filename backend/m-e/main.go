package main

import (
	"crypto/rand"
	"encoding/hex"
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

const IC = "ic"
const NAME = "name"
const LICENSE_VALIDITY = "licenseValidity"

type profile struct {
	IC              string `json:"ic"`
	Name            string `json:"name"`
	LicenseValidity string `json:"licenseValidity"`
}

type session struct {
	Type      string `json:"type"`
	StartTime string `json:"time"`
	Token     string `json:"token"`
}

var profiles = []profile{
	{IC: "010101-01-0001", Name: "Merdeka bin Jumaat", LicenseValidity: "04/01/2022 - 03/01/2024"},
	{IC: "020202-02-0002", Name: "Tan Wei Chun", LicenseValidity: "04/02/2022 - 03/02/2024"},
	{IC: "030303-03-0003", Name: "Dernice Lee ", LicenseValidity: "04/03/2022 - 03/03/2024"},
}

// var sessions = []session{}

func main() {
	router := gin.Default()
	router.GET("/login", getLoginToken)

	router.Run("localhost:8080")
}

func getLoginToken(c *gin.Context) {
	// newSession = session{"login", time.now()}
	// append(sessions, newSession)
	c.IndentedJSON(http.StatusOK, generateToken())
	// fmt.Println(sessions)
}

func generateToken() string {
	length := 64
	b := make([]byte, length)
	_, err := rand.Read(b)
	if err != nil {
		fmt.Println(err)
	}
	return hex.EncodeToString(b)
}
