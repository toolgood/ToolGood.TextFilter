package toolgood.textfilter.api.Grpcs;

import java.util.ArrayList;
import java.util.List;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceRequest;
import toolgood.textfilter.api.Datas.Texts.TextFilterContactItem;
import toolgood.textfilter.api.Datas.Texts.TextFilterDetailItem;
import toolgood.textfilter.api.Datas.Texts.TextFilterResult;
import toolgood.textfilter.api.Datas.Texts.TextReplaceResult;
import toolgood.textfilter.api.GrpcBase.TextFilterGrpcGrpc;
import toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterContactGrpcResult;
import toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterDetailGrpcResult;
import toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply;
import toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply;
import toolgood.textfilter.api.GrpcBase.TextFilterGrpcGrpc.TextFilterGrpcBlockingStub;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

public class TextFilterGrpcProvider implements ITextFilterProvider {
    private final TextFilterGrpcBlockingStub blockingStub;

    public TextFilterGrpcProvider(String host, int port) {
        this(ManagedChannelBuilder.forAddress(host, port).usePlaintext().build());
    }

    public TextFilterGrpcProvider(String url) {
        this(ManagedChannelBuilder.forTarget(url).usePlaintext().build());
    }

    private TextFilterGrpcProvider(ManagedChannel channel) {
        blockingStub = TextFilterGrpcGrpc.newBlockingStub(channel);
    }

    @Override
    public TextFilterResult TextFilter(TextFilterRequest request) {
        TextFindAllGrpcReply response = blockingStub.textFilter(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextReplaceResult TextReplace(TextReplaceRequest request) {
        TextReplaceGrpcReply response = blockingStub.textReplace(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextFilterResult HtmlFilter(TextFilterRequest request) {
        TextFindAllGrpcReply response = blockingStub.htmlFilter(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextReplaceResult HtmlReplace(TextReplaceRequest request) {
        TextReplaceGrpcReply response = blockingStub.htmlReplace(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextFilterResult JsonFilter(TextFilterRequest request) {
        TextFindAllGrpcReply response = blockingStub.jsonFilter(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextReplaceResult JsonReplace(TextReplaceRequest request) {
        TextReplaceGrpcReply response = blockingStub.jsonReplace(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextFilterResult MarkdownFilter(TextFilterRequest request) {
        TextFindAllGrpcReply response = blockingStub.markdownFilter(request.ToGrpcRequest());
        return Create(response);
    }

    @Override
    public TextReplaceResult MarkdownReplace(TextReplaceRequest request) {
        TextReplaceGrpcReply response = blockingStub.markdownReplace(request.ToGrpcRequest());
        return Create(response);
    }

    private TextFilterResult Create(TextFindAllGrpcReply response) {
        TextFilterResult result = new TextFilterResult();
        result.code = response.getCode();
        result.riskCode = response.getRiskCode();
        result.requestId = response.getRequestId();
        result.message = response.getMessage();
        result.riskLevel = response.getRiskLevel();
        result.sentimentScore = response.getSentimentScore();
        List<TextFilterDetailItem> details = new ArrayList<TextFilterDetailItem>();
        int length = response.getDetailsCount();
        for (int i = 0; i < length; i++) {
            TextFilterDetailItem item = new TextFilterDetailItem();
            TextFilterDetailGrpcResult r = response.getDetails(i);
            item.Position = r.getPosition();
            item.RiskCode = r.getRiskCode();
            item.RiskLevel = r.getRiskLevel();
            item.Text = r.getText();
            details.add(item);
        }
        result.details = details;

        List<TextFilterContactItem> contactItems = new ArrayList<TextFilterContactItem>();
        length = response.getContactsCount();
        for (int i = 0; i < length; i++) {
            TextFilterContactItem item = new TextFilterContactItem();
            TextFilterContactGrpcResult r = response.getContacts(i);
            item.contactType = r.getContactType();
            item.contactString = r.getContactString();
            item.position = r.getPosition();
            contactItems.add(item);
        }
        result.contacts = contactItems;
        return result;
    }

    private TextReplaceResult Create(TextReplaceGrpcReply response) {
        TextReplaceResult result = new TextReplaceResult();
        result.code = response.getCode();
        result.requestId = response.getRequestId();
        result.message = response.getMessage();
        result.riskLevel = response.getRiskLevel();

        List<TextFilterDetailItem> details = new ArrayList<TextFilterDetailItem>();
        int length = response.getDetailsCount();
        for (int i = 0; i < length; i++) {
            TextFilterDetailItem item = new TextFilterDetailItem();
            TextFilterDetailGrpcResult r = response.getDetails(i);
            item.Position = r.getPosition();
            item.RiskCode = r.getRiskCode();
            item.RiskLevel = r.getRiskLevel();
            item.Text = r.getText();
            details.add(item);
        }
        result.details = details;

        return result;
    }

}
