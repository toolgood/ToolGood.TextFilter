package toolgood.textfilter.api.ConsulDiscovery;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import com.orbitz.consul.Consul;
import com.orbitz.consul.HealthClient;
import com.orbitz.consul.model.health.Service;
import com.orbitz.consul.model.health.ServiceHealth;

public class ConsulServiceDiscovery {
    private static Random random = new Random();

    public static List<String> Discovery(String registryAddress, String serviceName) {
        Consul consul = Consul.builder().withUrl(registryAddress).build();
        HealthClient healthClient = consul.healthClient();// 获取所有健康的服务
        List<ServiceHealth> serviceHealths = healthClient.getHealthyServiceInstances(serviceName).getResponse();// 寻找passing状态的节点

        List<String> result = new ArrayList<String>();
        for (ServiceHealth health : serviceHealths) {
            Service s = health.getService();
            result.add("http://" + s.getAddress() + ":" + s.getPort());
        }
        return result;
    }

    /// <summary>
    /// 随机负载均衡
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    public static String DiscoveryOne(String registryAddress, String serviceName) {
        List<String> result = Discovery(registryAddress, serviceName);
        if (result.size() == 0) {
            return null;
        }
        if (result.size() == 1) {
            return result.get(0);
        }
        int next = random.nextInt(result.size());
        return result.get(next);
    }

}
