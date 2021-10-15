#if GRPC
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ToolGood.TextFilter.Api.Datas.Requests;
using ToolGood.TextFilter.Api.Datas.Texts;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Grpcs
{
    public class TextFilterGrpcProvider : ITextFilterProvider
    {
        private readonly TextFilterGrpc.TextFilterGrpcClient _client;

        public TextFilterGrpcProvider(string grpcHost)
        {
            var channel = GrpcChannel.ForAddress(grpcHost);
            _client = new TextFilterGrpc.TextFilterGrpcClient(channel);
        }

        public TextFilterResult HtmlFilter(TextFilterRequest request)
        {
            var reply = _client.HtmlFilter(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<TextFilterResult> HtmlFilterAsync(TextFilterRequest request)
        {
            var reply = await _client.HtmlFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public TextReplaceResult HtmlReplace(TextReplaceRequest request)
        {
            var reply = _client.HtmlReplace(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<TextReplaceResult> HtmlReplaceAsync(TextReplaceRequest request)
        {
            var reply = await _client.HtmlReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public TextFilterResult JsonFilter(TextFilterRequest request)
        {
            var reply = _client.JsonFilter(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<TextFilterResult> JsonFilterAsync(TextFilterRequest request)
        {
            var reply = await _client.JsonFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public TextReplaceResult JsonReplace(TextReplaceRequest request)
        {
            var reply = _client.JsonReplace(request.ToGrpcRequest());
            return Create(reply);
        }


        public async Task<TextReplaceResult> JsonReplaceAsync(TextReplaceRequest request)
        {
            var reply = await _client.JsonReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public TextFilterResult MarkdownFilter(TextFilterRequest request)
        {
            var reply = _client.MarkdownFilter(request.ToGrpcRequest());
            return Create(reply);
        }
        public async Task<TextFilterResult> MarkdownFilterAsync(TextFilterRequest request)
        {
            var reply = await _client.MarkdownFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        public TextReplaceResult MarkdownReplace(TextReplaceRequest request)
        {
            var reply = _client.MarkdownReplace(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<TextReplaceResult> MarkdownReplaceAsync(TextReplaceRequest request)
        {
            var reply = await _client.MarkdownReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }
        public TextFilterResult TextFilter(TextFilterRequest request)
        {
            var reply = _client.TextFilter(request.ToGrpcRequest());
            return Create(reply);
        }

        public async Task<TextFilterResult> TextFilterAsync(TextFilterRequest request)
        {
            var reply = await _client.TextFilterAsync(request.ToGrpcRequest());
            return Create(reply);
        }
        public TextReplaceResult TextReplace(TextReplaceRequest request)
        {
            var reply = _client.MarkdownReplace(request.ToGrpcRequest());
            return Create(reply);

        }

        public async Task<TextReplaceResult> TextReplaceAsync(TextReplaceRequest request)
        {
            var reply = await _client.TextReplaceAsync(request.ToGrpcRequest());
            return Create(reply);
        }

        private TextReplaceResult Create(TextReplaceGrpcReply reply)
        {
            TextReplaceResult result = new TextReplaceResult() {
                Code = reply.Code,
                RiskLevel = reply.RiskLevel,
                Message = reply.Message,
                RequestId = reply.RequestId,
                ResultText = reply.ResultText
            };
            result.Details = new List<TextFilterDetailItem>();
            foreach (var detail in reply.Details) {
                TextFilterDetailItem item = new TextFilterDetailItem() {
                    RiskCode = detail.RiskCode,
                    RiskLevel = detail.RiskLevel,
                    Position = detail.Position,
                    Text = detail.Text,
                };
                result.Details.Add(item);
            }
            return result;
        }

        private TextFilterResult Create(TextFindAllGrpcReply reply)
        {
            TextFilterResult result = new TextFilterResult() {
                Code = reply.Code,
                RiskCode = reply.RiskCode,
                RiskLevel = reply.RiskLevel,
                Message = reply.Message,
                RequestId = reply.RequestId,
                SentimentScore = reply.SentimentScore
            };
            result.Details = new List<TextFilterDetailItem>();
            foreach (var detail in reply.Details) {
                TextFilterDetailItem item = new TextFilterDetailItem() {
                    RiskCode = detail.RiskCode,
                    RiskLevel = detail.RiskLevel,
                    Position = detail.Position,
                    Text = detail.Text,
                };
                result.Details.Add(item);
            }
            result.Contacts = new List<TextFilterContactItem>();
            foreach (var contact in reply.Contacts) {
                TextFilterContactItem item = new TextFilterContactItem() {
                    ContactType = contact.ContactType,
                    ContactString = contact.ContactString,
                    Position = contact.Position
                };
                result.Contacts.Add(item);
            }
            return result;
        }

    }
}

#endif