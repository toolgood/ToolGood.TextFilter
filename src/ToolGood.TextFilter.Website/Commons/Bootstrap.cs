/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ToolGood.TextFilter.App.Datas.Results;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;


namespace ToolGood.TextFilter.Website.Commons
{
    class Bootstrap
    {

        public static void Init(IServiceProvider serviceProvider)
        {
            SysApplication.Init();
            GC.Collect();

#if Async
            _asyncFilterUtil = new Bootstrap();
            _asyncFilterUtil.httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            FilterQueueApplication.SetFindAllAction(_asyncFilterUtil.TextFindAllNotice);
            FilterQueueApplication.SetReplaceAction(_asyncFilterUtil.TextReplaceNotice);
#endif

        }

#if Async
        private static Bootstrap _asyncFilterUtil;

        private IHttpClientFactory httpClientFactory;
        private async void TextFindAllNotice(string requestId, string url, IllegalWordsFindAllResult temp,string text,bool onlyPosition)
        {
            TextFilterResult result = new TextFilterResult();
            TextFilterCommon.SetTextFilterResult(result, temp,new TextRequestBase(text,onlyPosition));
            result.Message = "SUCCESS";
            result.RequestId = requestId;
            var json = JsonConvert.SerializeObject(result);
            result = null;

            await PostContent(url, json);
        }

        private async void TextReplaceNotice(string requestId, string url, IllegalWordsReplaceResult temp, string text, bool onlyPosition)
        {
            TextReplaceResult result = new TextReplaceResult();
            TextFilterCommon.SetTextReplaceResult(result, temp, new TextRequestBase(text, onlyPosition));
            result.Message = "SUCCESS";
            result.RequestId = requestId;
            var json = JsonConvert.SerializeObject(result);
            result = null;

            await PostContent(url, json);
        }


        private async Task PostContent(string url, string json)
        {
            var httpClient = httpClientFactory.CreateClient("Polly");
            try {
                var message = new HttpRequestMessage() {
                    RequestUri = new Uri(url),
                    Method = new HttpMethod("POST"),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                message.Headers.Add("Accept", "*/*");
                message.Headers.Add("User-Agent", "Mozilla/5.0 ToolGood.TextFilter/1.0");

                await httpClient.SendAsync(message);
            } catch {
            } finally {
                httpClient.Dispose();
            }
        }
#endif


    }
}

