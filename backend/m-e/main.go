package main

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type profile struct {
	IC              string `json:"ic"`
	Name            string `json:"name"`
	LicenseValidity string `json:"licenseValidity"`
}

var profiles = []profile{
	{IC: "010101-01-0001", Name: "Merdeka bin Jumaat", LicenseValidity: "04/01/2022 - 03/01/2024"},
	{IC: "020202-02-0002", Name: "Tan Wei Chun", LicenseValidity: "04/02/2022 - 03/02/2024"},
	{IC: "030303-03-0003", Name: "Dernice Lee ", LicenseValidity: "04/03/2022 - 03/03/2024"},
}

func main() {
	router := gin.Default()
	router.GET("/login", getLoginToken)

	router.Run("localhost:8080")
}

func getLoginToken(c *gin.Context) {
	c.IndentedJSON(http.StatusOK, profiles)
}
