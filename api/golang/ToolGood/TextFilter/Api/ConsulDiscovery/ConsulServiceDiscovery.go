package ConsulDiscovery

import (
	"errors"

	"github.com/hashicorp/consul/api"
)

type ConsulServiceDiscovery struct {
	client api.Client
}
type ServiceInstance struct {
	InstanceId string
	ServiceId  string
	Host       string
	Port       int
}

func (c ConsulServiceDiscovery) GetInstances(serviceId string) ([]ServiceInstance, error) {
	catalogService, _, _ := c.client.Catalog().Service(serviceId, "", nil)
	if len(catalogService) > 0 {
		result := make([]ServiceInstance, len(catalogService))
		for index, sever := range catalogService {
			s := &ServiceInstance{}
			s.InstanceId = sever.ServiceID
			s.InstanceId = sever.ServiceID
			s.ServiceId = sever.ServiceName
			s.Host = sever.Address
			s.Port = sever.ServicePort

			result[index] = *s
		}
		return result, nil
	}
	return nil, nil
}

// new a consulServiceRegistry instance
// token is optional
func NewConsulServiceRegistry(address string) (*ConsulServiceDiscovery, error) {
	if len(address) < 3 {
		return nil, errors.New("check address")
	}

	config := api.DefaultConfig()
	config.Address = address
	client, err := api.NewClient(config)
	if err != nil {
		return nil, err
	}

	return &ConsulServiceDiscovery{client: *client}, nil
}
