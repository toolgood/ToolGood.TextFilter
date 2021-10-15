package Api

import (
	"strconv"

	"./ConsulDiscovery"
	. "./Grpcs"
	. "./Impl"
)

type TextFilterConfig struct {
	textFilterHost string
	grpcHost       string
	consulAddress  string
}

func NewTextFilterConfig() *TextFilterConfig {
	result := &TextFilterConfig{}
	result.textFilterHost = "http://localhost:9191"
	result.grpcHost = "localhost:9192"
	result.consulAddress = "http://localhost:8500"

	return result
}

func (this *TextFilterConfig) GetTextFilterHost() string {
	return this.textFilterHost
}

func (this *TextFilterConfig) SetTextFilterHost(host string) {
	this.textFilterHost = host
}
func (this *TextFilterConfig) GetGrpcHost() string {
	return this.grpcHost
}

func (this *TextFilterConfig) SetGrpcHost(host string) {
	this.grpcHost = host
}

func (this *TextFilterConfig) GetConsulAddress() string {
	return this.consulAddress
}

func (this *TextFilterConfig) SetConsulAddress(host string) {
	this.consulAddress = host
}


func (this *TextFilterConfig) CreateKeywordProvider() *KeywordProvider {
	provider := NewKeywordProvider(this.textFilterHost)
	return provider
}
func (this *TextFilterConfig) CreateKeywordTypeProvider() *KeywordTypeProvider {
	provider := NewKeywordTypeProvider(this.textFilterHost)
	return provider
}
func (this *TextFilterConfig) CreateSysProvider() *SysProvider {
	provider := NewSysProvider(this.textFilterHost)
	return provider
}
func (this *TextFilterConfig) CreateTextFilterAsyncProvider() *TextFilterAsyncProvider {
	provider := NewTextFilterAsyncProvider(this.textFilterHost)
	return provider
}
func (this *TextFilterConfig) CreateTextFilterProvider() *TextFilterProvider {
	provider := NewTextFilterProvider(this.textFilterHost)
	return provider
}
func (this *TextFilterConfig) CreateTextFilterGrpcClient() *TextFilterGrpcProvider {
	provider := NewTextFilterGrpcProvider(this.grpcHost)
	return provider
}
func (this *TextFilterConfig) CreateTextFilterGrpcAnsycClient() *TextFilterGrpcAsyncProvider {
	provider := NewTextFilterGrpcAsyncProvider(this.grpcHost)
	return provider
}

func (this *TextFilterConfig) GetServiceUrls_Http() []string {
	service, _ := ConsulDiscovery.NewConsulServiceRegistry(this.consulAddress)
	rs, _ := service.GetInstances("ToolGood.TextFilter")
	result := make([]string, len(rs))
	for index, sever := range rs {
		result[index] = "http://" + sever.Host + ":" + strconv.Itoa(sever.Port)
	}

	return result
}
func (this *TextFilterConfig) GetServiceUrls_Grpc() []string {
	service, _ := ConsulDiscovery.NewConsulServiceRegistry(this.consulAddress)
	rs, _ := service.GetInstances("ToolGood.TextFilter.Grpc")
	result := make([]string, len(rs))
	for index, sever := range rs {
		result[index] = sever.Host + ":" + strconv.Itoa(sever.Port)
	}
	return result
}
