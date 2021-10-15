
#pip install python-consul
import consul

class ConsulServiceDiscovery():

    _consul = None

    def __init__(self, host: str, port: int, token: str = None):
        self.host = host
        self.port = port
        self.token = token
        self._consul = consul.Consul(host, port, token=token)

    def get_services(self) -> list:
        return self._consul.catalog.services()[1].keys()

    def Discovery(self, service_id: str) -> list:
        origin_instances = self._consul.catalog.service(service_id)[1]
        result = []
        for oi in origin_instances:
            result.append({
                'ServiceName':oi.get('ServiceName'),
                'ServiceAddress':oi.get('ServiceAddress'),
                'ServicePort':oi.get('ServicePort'),
                'ServiceTags':oi.get('ServiceTags'),
                'ServiceMeta':oi.get('ServiceMeta'),
                'ServiceID':oi.get('ServiceID'),
            })
        return result
 
 
 