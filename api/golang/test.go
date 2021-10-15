package main

import (
	"fmt"

	. "./Toolgood/TextFilter/Api"
	. "./Toolgood/TextFilter/Api/Datas/Requests"
)

func main() {
	test_TextFilterProvider()

	test_TextFilterGrpcClient()

	test_GetServiceUrls_Http()
	test_GetServiceUrls_Grpc()
}
func test_TextFilterProvider() {
	fmt.Println("test_TextFilterProvider is Start.")

	config := NewTextFilterConfig()
	provider := config.CreateTextFilterProvider()
	request := NewTextFilterRequest()
	request.Txt = "你妈的"

	result := provider.TextFilter(request)
	if result.RiskLevel != "REJECT" {
		fmt.Println("TextFilter is Error.")
	}

	result = provider.HtmlFilter(request)
	if result.RiskLevel != "REJECT" {
		fmt.Println("HtmlFilter is Error.")
	}

	result = provider.JsonFilter(request)
	if result.RiskLevel != "REJECT" {
		fmt.Println("HtmlFilter is Error.")
	}
	result = provider.MarkdownFilter(request)
	if result.RiskLevel != "REJECT" {
		fmt.Println("HtmlFilter is Error.")
	}
}

func test_TextFilterGrpcClient() {
	fmt.Println("test_TextFilterGrpcClient is Start.")
	config := NewTextFilterConfig()
	provider := config.CreateTextFilterGrpcClient()
	request := NewTextFilterRequest()
	request.Txt = "你妈的"

	result := provider.TextFilter(request)
	if result.RiskLevel != "REJECT" {
		fmt.Println("TextFilter is Error.")
	}
}

func test_GetServiceUrls_Http() {
	fmt.Println("test_GetServiceUrls_Http is Start.")
	config := NewTextFilterConfig()

	result := config.GetServiceUrls_Http()
	for _, sever := range result {
		fmt.Println(sever)
	}
}

func test_GetServiceUrls_Grpc() {
	fmt.Println("test_GetServiceUrls_Grpc is Start.")
	config := NewTextFilterConfig()

	result := config.GetServiceUrls_Grpc()
	for _, sever := range result {
		fmt.Println(sever)
	}
}
