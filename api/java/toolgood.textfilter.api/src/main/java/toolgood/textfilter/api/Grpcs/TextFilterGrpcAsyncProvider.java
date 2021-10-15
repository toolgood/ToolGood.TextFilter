package toolgood.textfilter.api.Grpcs;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Requests.TextFilterAsyncRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceAsyncRequest;
import toolgood.textfilter.api.GrpcBase.TextFilterGrpcGrpc;
import toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply;
import toolgood.textfilter.api.GrpcBase.TextFilterGrpcGrpc.TextFilterGrpcBlockingStub;
import toolgood.textfilter.api.Interfaces.ITextFilterAsyncProvider;

public class TextFilterGrpcAsyncProvider implements ITextFilterAsyncProvider {
    private final TextFilterGrpcBlockingStub blockingStub;

    public TextFilterGrpcAsyncProvider(String host, int port) {
        this(ManagedChannelBuilder.forAddress(host, port).usePlaintext().build());
    }

    public TextFilterGrpcAsyncProvider(String url) {
        this(ManagedChannelBuilder.forTarget(url).usePlaintext().build());
    }

    private TextFilterGrpcAsyncProvider(ManagedChannel channel) {
        blockingStub = TextFilterGrpcGrpc.newBlockingStub(channel);
    }

    @Override
    public CommonResult TextFilter(TextFilterAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.textFilterAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult TextReplace(TextReplaceAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.textReplaceAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult HtmlFilter(TextFilterAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.htmlFilterAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult HtmlReplace(TextReplaceAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.htmlReplaceAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult JsonFilter(TextFilterAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.jsonFilterAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult JsonReplace(TextReplaceAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.jsonReplaceAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult MarkdownFilter(TextFilterAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.markdownFilterAsync(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public CommonResult MarkdownReplace(TextReplaceAsyncRequest request) {
        TextFilterRequestIdGrpcReply response = blockingStub.markdownReplaceAsync(request.ToGrpcRequest());
        return Create(response);
    }

    private CommonResult Create(TextFilterRequestIdGrpcReply response) {
        CommonResult result = new CommonResult();
        result.code = response.getCode();
        result.message = response.getMessage();
        result.requestId = response.getRequestId();
        return result;
    }

}
