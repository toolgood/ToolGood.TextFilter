#if Async
#if GRPC
using System.Threading.Tasks;
using Grpc.Net.Client;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Requests;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Grpcs
{
    public class TextFilterGrpcAsyncProvider : ITextFilterAsyncProvider
    {
        private readonly TextFilterGrpc.TextFilterGrpcClient _client;

        public TextFilterGrpcAsyncProvider(string grpcHost)
        {
            var channel = GrpcChannel.ForAddress(grpcHost);
            _client = new TextFilterGrpc.TextFilterGrpcClient(channel);
        }

 
        public CommonResult HtmlFilter(TextFilterAsyncRequest request)
        {
            var reply = _client.HtmlFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }

 
        public async Task<CommonResult> HtmlFilterAsync(TextFilterAsyncRequest request)
        {
            var reply = await _client.HtmlFilterAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public CommonResult HtmlReplace(TextReplaceAsyncRequest request)
        {
            var reply = _client.HtmlReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }


        public async Task<CommonResult> HtmlReplaceAsync(TextReplaceAsyncRequest request)
        {
            var reply = await _client.HtmlReplaceAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }
 
        public CommonResult JsonFilter(TextFilterAsyncRequest request)
        {
            var reply = _client.JsonFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }
        public async Task<CommonResult> JsonFilterAsync(TextFilterAsyncRequest request)
        {
            var reply = await _client.JsonFilterAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public CommonResult JsonReplace(TextReplaceAsyncRequest request)
        {
            var reply = _client.JsonReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<CommonResult> JsonReplaceAsync(TextReplaceAsyncRequest request)
        {
            var reply = await _client.JsonReplaceAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }
 
        public CommonResult MarkdownFilter(TextFilterAsyncRequest request)
        {
            var reply = _client.MarkdownFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<CommonResult> MarkdownFilterAsync(TextFilterAsyncRequest request)
        {
            var reply = await _client.MarkdownFilterAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }
 
        public CommonResult MarkdownReplace(TextReplaceAsyncRequest request)
        {
            var reply = _client.MarkdownReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<CommonResult> MarkdownReplaceAsync(TextReplaceAsyncRequest request)
        {
            var reply = await _client.MarkdownReplaceAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public CommonResult TextFilter(TextFilterAsyncRequest request)
        {
            var reply = _client.TextFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }
 
        public async Task<CommonResult> TextFilterAsync(TextFilterAsyncRequest request)
        {
            var reply = await _client.TextFilterAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public CommonResult TextReplace(TextReplaceAsyncRequest request)
        {
            var reply = _client.TextReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<CommonResult> TextReplaceAsync(TextReplaceAsyncRequest request)
        {
            var reply = await _client.TextReplaceAsyncAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        private CommonResult Create(TextFilterRequestIdGrpcReply reply)
        {
            CommonResult result = new CommonResult() {
                Code = reply.Code,
                Message = reply.Message,
                RequestId = reply.RequestId
            };
            return result;
        }
    }
}

#endif  
#endif