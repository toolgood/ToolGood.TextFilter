package toolgood.textfilter.api;

import java.util.List;

import toolgood.textfilter.api.ConsulDiscovery.ConsulServiceDiscovery;
import toolgood.textfilter.api.Grpcs.TextFilterGrpcAsyncProvider;
import toolgood.textfilter.api.Grpcs.TextFilterGrpcProvider;
import toolgood.textfilter.api.Impl.KeywordProvider;
import toolgood.textfilter.api.Impl.KeywordTypeProvider;
import toolgood.textfilter.api.Impl.SysProvider;
import toolgood.textfilter.api.Impl.TextFilterAsyncProvider;
import toolgood.textfilter.api.Impl.TextFilterProvider;
import toolgood.textfilter.api.Interfaces.IKeywordProvider;
import toolgood.textfilter.api.Interfaces.IKeywordTypeProvider;
import toolgood.textfilter.api.Interfaces.ISysProvider;
import toolgood.textfilter.api.Interfaces.ITextFilterAsyncProvider;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

public class TextFilterConfig {
    private static TextFilterConfig _textFilterConfig;

    public static TextFilterConfig Instance() {
        if (_textFilterConfig == null) {
            _textFilterConfig = new TextFilterConfig();
        }
        return _textFilterConfig;
    }

    private String _grpcHost;
    private String _textFilterHost;
    private String _registryAddress = "http://localhost:8500";

    public void SetConsulAddress(String value) {
        _registryAddress = value;
    }

    public void SetTextFilterHost(String value) {
        _textFilterHost = value;
    }

    public String GetTextFilterHost() {
        if (_textFilterHost == null) {
            return ConsulServiceDiscovery.DiscoveryOne(_registryAddress, "ToolGood.TextFilter");
        }
        return _textFilterHost;
    }

    public void SetGrpcHost(String value) {
        _grpcHost = value;
    }

    public String GetGrpcHost() {
        if (_grpcHost == null) {
            return ConsulServiceDiscovery.DiscoveryOne(_registryAddress, "ToolGood.TextFilter.Grpc");
        }
        return _grpcHost;
    }

    public IKeywordProvider CreateKeywordProvider() {
        return new KeywordProvider(this);
    }

    public IKeywordTypeProvider CreateKeywordTypeProvider() {
        return new KeywordTypeProvider(this);
    }

    public ISysProvider CreateSysProvider() {
        return new SysProvider(this);
    }

    public ITextFilterProvider CreateTextFilterProvider() {
        return new TextFilterProvider(this);
    }

    public ITextFilterAsyncProvider CreateTextFilterAsyncProvider() {
        return new TextFilterAsyncProvider(this);
    }

    public TextFilterGrpcProvider CreateTextFilterGrpcClient() {
        return new TextFilterGrpcProvider(GetGrpcHost().replace("http://", ""));
    }

    public TextFilterGrpcAsyncProvider CreateTextFilterGrpcAnsycClient() {
        return new TextFilterGrpcAsyncProvider(GetGrpcHost().replace("http://", ""));
    }

    public List<String> GetServiceUrls(ServiceUrlType urlType) {
        if (urlType == ServiceUrlType.Grpc) {
            return ConsulServiceDiscovery.Discovery(_registryAddress, "ToolGood.TextFilter.Grpc");
        }
        return ConsulServiceDiscovery.Discovery(_registryAddress, "ToolGood.TextFilter");
    }

}
