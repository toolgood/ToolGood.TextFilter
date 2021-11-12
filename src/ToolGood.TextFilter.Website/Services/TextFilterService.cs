/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if GRPC
using System;
using System.Threading.Tasks;
using Grpc.Core;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Services
{
    public class TextFilterService : TextFilterGrpc.TextFilterGrpcBase
    {
        #region TextFilter 
        public override Task<TextFindAllGrpcReply> TextFilter(TextFindAllGrpcRequest request, ServerCallContext context)
        {
            TextFindAllGrpcReply result = new TextFindAllGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion

                var temp = TextFilterApplication.FindAll(request.Txt, request.SkipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp,new TextRequestBase(request.Txt,request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFindAllGrpcReply> HtmlFilter(TextFindAllGrpcRequest request, ServerCallContext context)
        {
            TextFindAllGrpcReply result = new TextFindAllGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion


                var temp = HtmlFilterApplication.FindAll(request.Txt, request.SkipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFindAllGrpcReply> JsonFilter(TextFindAllGrpcRequest request, ServerCallContext context)
        {
            TextFindAllGrpcReply result = new TextFindAllGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion


                var temp = JsonFilterApplication.FindAll(request.Txt, request.SkipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFindAllGrpcReply> MarkdownFilter(TextFindAllGrpcRequest request, ServerCallContext context)
        {
            TextFindAllGrpcReply result = new TextFindAllGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion

                var temp = MarkdownFilterApplication.FindAll(request.Txt, request.SkipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        #endregion

        #region TextReplace HtmlReplace JsonReplace MarkdownReplace
        public override Task<TextReplaceGrpcReply> TextReplace(TextReplaceGrpcRequest request, ServerCallContext context)
        {
            TextReplaceGrpcReply result = new TextReplaceGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion

                var temp = TextFilterApplication.Replace(request.Txt, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextReplaceGrpcReply> HtmlReplace(TextReplaceGrpcRequest request, ServerCallContext context)
        {
            TextReplaceGrpcReply result = new TextReplaceGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion


                var temp = HtmlFilterApplication.Replace(request.Txt, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextReplaceGrpcReply> JsonReplace(TextReplaceGrpcRequest request, ServerCallContext context)
        {
            TextReplaceGrpcReply result = new TextReplaceGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion

                var temp = JsonFilterApplication.Replace(request.Txt, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextReplaceGrpcReply> MarkdownReplace(TextReplaceGrpcRequest request, ServerCallContext context)
        {
            TextReplaceGrpcReply result = new TextReplaceGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                #endregion


                var temp = MarkdownFilterApplication.Replace(request.Txt, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, new TextRequestBase(request.Txt, request.OnlyPosition));
                temp = null;
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        #endregion

        #region TextFilterAsync HtmlFilterAsync JsonFilterAsync MarkdownFilterAsync
        public override Task<TextFilterRequestIdGrpcReply> TextFilterAsync(TextFindAllAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
                #endregion

                FilterQueueApplication.TextFilter(request.RequestId, request.Url, request.Txt, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> HtmlFilterAsync(TextFindAllAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
                #endregion


                FilterQueueApplication.HtmlFilter(request.RequestId, request.Url, request.Txt, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> JsonFilterAsync(TextFindAllAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
                #endregion


                FilterQueueApplication.JsonFilter(request.RequestId, request.Url, request.Txt, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> MarkdownFilterAsync(TextFindAllAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
                #endregion

                FilterQueueApplication.MarkdownFilter(request.RequestId, request.Url, request.Txt, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        #endregion

        #region TextReplaceAsync HtmlReplaceAsync JsonReplaceAsync MarkdownReplaceAsync
        public override Task<TextFilterRequestIdGrpcReply> TextReplaceAsync(TextReplaceAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                #endregion

                FilterQueueApplication.TextReplace(request.RequestId, request.Url, request.Txt, (char)request.ReplaceChar, request.ReviewReplace
                    , request.ContactReplace, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> HtmlReplaceAsync(TextReplaceAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                #endregion

                FilterQueueApplication.HtmlReplace(request.RequestId, request.Url, request.Txt, (char)request.ReplaceChar, request.ReviewReplace
                    , request.ContactReplace, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> JsonReplaceAsync(TextReplaceAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                #endregion

                FilterQueueApplication.JsonReplace(request.RequestId, request.Url, request.Txt, (char)request.ReplaceChar, request.ReviewReplace
                    , request.ContactReplace, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }
        public override Task<TextFilterRequestIdGrpcReply> MarkdownReplaceAsync(TextReplaceAsyncGrpcRequest request, ServerCallContext context)
        {
            TextFilterRequestIdGrpcReply result = new TextFilterRequestIdGrpcReply();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Task.FromResult(result);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Task.FromResult(result);
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                #endregion

                FilterQueueApplication.MarkdownReplace(request.RequestId, request.Url, request.Txt, (char)request.ReplaceChar, request.ReviewReplace
                    , request.ContactReplace, request.SkipBidi, request.OnlyPosition);
                result.RequestId = request.RequestId;
                result.Message = "SUCCESS";
            } catch (Exception ex) {
                result.Code = 1;
                result.RequestId = request.RequestId;
                result.Message = ex.Message;
            }
            return Task.FromResult(result);
        }

        #endregion


    }
}

#endif