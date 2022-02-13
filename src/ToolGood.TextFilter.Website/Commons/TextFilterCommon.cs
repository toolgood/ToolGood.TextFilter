/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.TextFilter.App.Datas.Results;
using ToolGood.TextFilter.Models;

namespace ToolGood.TextFilter.Website.Commons
{
    public static class TextFilterCommon
    {
        public static void SetTextFilterResult(TextFilterResult result, IllegalWordsFindAllResult temp, ITextRequest request)
        {
            result.SentimentScore = temp.SentimentScore;
            if (temp.RiskLevel == IllegalWordsRiskLevel.Pass) {
                result.RiskLevel = "PASS";
            } else if (temp.RiskLevel == IllegalWordsRiskLevel.Reject) {
                result.RiskLevel = "REJECT";
                result.RiskCode = temp.Code;
                result.Details = new List<TextFilterDetailItem>();
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.RejectSingleItems, postions, IllegalWordsRiskLevel.Reject, request);
                GetTextFilterDetailResult(result.Details, temp.RejectMultiItems, postions, IllegalWordsRiskLevel.Reject, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
                SetContacts(result, temp, request);
            } else {
                result.RiskLevel = "REVIEW";
                result.RiskCode = temp.Code;
                result.Details = new List<TextFilterDetailItem>();
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
                SetContacts(result, temp, request);
            }
        }

        public static void SetTextReplaceResult(TextReplaceResult result, IllegalWordsReplaceResult temp, ITextRequest request)
        {
            if (temp.RiskLevel == IllegalWordsRiskLevel.Pass) {
                result.RiskLevel = "PASS";
            } else if (temp.RiskLevel == IllegalWordsRiskLevel.Reject) {
                result.RiskLevel = "REJECT";
                result.ResultText = temp.Result;
                return;
            } else {
                result.RiskLevel = "REVIEW";
                result.Details = new List<TextFilterDetailItem>();
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
            }
        }

        #region SetContacts
        private static void SetContacts(TextFilterResult result, IllegalWordsFindAllResult temp, ITextRequest request)
        {
            if (temp.ContactItems.Count == 0) { return; }
            result.Contacts = new List<TextFilterContactItem>();
            foreach (var contactItem in temp.ContactItems) {
                TextFilterContactItem item = new TextFilterContactItem();
                item.ContactType = contactItem.ContactType.ToString();
                item.Position = $"{contactItem.Start}-{contactItem.End}";
                if (request.OnlyPosition == false) {
                    item.ContactString = request.Txt.AsSpan().Slice(contactItem.Start, contactItem.End + 1 - contactItem.Start).ToString();
                }
                result.Contacts.Add(item);
            }
        } 
        #endregion
 

        #region private GetTextFilterDetailResult
        private static void GetTextFilterDetailResult(List<TextFilterDetailItem> results, List<SingleWordsResult> singles, HashSet<string> postions
            , IllegalWordsRiskLevel? riskLevel, ITextRequest request)
        {
            foreach (var resultItem in singles) {
                TextFilterDetailItem result = new TextFilterDetailItem();
                result.RiskCode = resultItem.Code;
                if (riskLevel == IllegalWordsRiskLevel.Reject) {
                    result.RiskLevel = "REJECT";
                } else {
                    result.RiskLevel = "REVIEW";
                }

                if (resultItem.Start == resultItem.End) {
                    result.Position = resultItem.Start.ToString();
                } else {
                    result.Position = $"{resultItem.Start}-{resultItem.End}";
                }
                if (request.OnlyPosition == false) {
                    result.Text = request.Txt.AsSpan().Slice(resultItem.Start, resultItem.End + 1 - resultItem.Start).ToString();
                }

                if (postions.Add(result.Position)) {
                    results.Add(result);
                }
            }
        }
        private static void GetTextFilterDetailResult(List<TextFilterDetailItem> results, List<MultiWordsResult> multis, HashSet<string> postions
            , IllegalWordsRiskLevel? riskLevel, ITextRequest request)
        {
            foreach (var resultItem in multis) {
                TextFilterDetailItem result = new TextFilterDetailItem();
                result.RiskCode = resultItem.Code;
                if (riskLevel == IllegalWordsRiskLevel.Reject) {
                    result.RiskLevel = "REJECT";
                } else {
                    result.RiskLevel = "REVIEW";
                }
                TextRange textRange = new TextRange();
                if (request.OnlyPosition == false) {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < resultItem.Items.Length; i++) {
                        var t = resultItem.Items[i];
                        textRange.Add(t.Start, t.End);
                        if (i > 0) { sb.Append(' '); }
                        sb.Append(request.Txt.AsSpan().Slice(t.Start, t.End + 1 - t.Start).ToString());
                    }
                    result.Text = sb.ToString();
                } else {
                    for (int i = 0; i < resultItem.Items.Length; i++) {
                        var t = resultItem.Items[i];
                        textRange.Add(t.Start, t.End);
                    }
                }
                result.Position = textRange.ToString();
                textRange = null;

                if (postions.Add(result.Position)) {
                    results.Add(result);
                }
            }
        }

        #endregion


#if Async

        #region  private SetTextFilterResult SetTextReplaceResult
        public static void SetTextFilterResult(TextFindAllGrpcReply result, IllegalWordsFindAllResult temp, ITextRequest request)
        {
            if (temp.RiskLevel == IllegalWordsRiskLevel.Pass) {
                result.RiskLevel = "PASS";
            } else if (temp.RiskLevel == IllegalWordsRiskLevel.Reject) {
                result.RiskLevel = "REJECT";
                result.RiskCode = temp.Code;
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.RejectSingleItems, postions, IllegalWordsRiskLevel.Reject, request);
                GetTextFilterDetailResult(result.Details, temp.RejectMultiItems, postions, IllegalWordsRiskLevel.Reject, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
                SetContacts(result, temp, request);

            } else {
                result.RiskLevel = "REVIEW";
                result.RiskCode = temp.Code;
                result.SentimentScore = (float)(temp.SentimentScore ?? 0);
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
                SetContacts(result, temp, request);
            }
        }

        public static void SetTextReplaceResult(TextReplaceGrpcReply result, IllegalWordsReplaceResult temp, ITextRequest request)
        {
            if (temp.RiskLevel == IllegalWordsRiskLevel.Pass) {
                result.RiskLevel = "PASS";
            } else if (temp.RiskLevel == IllegalWordsRiskLevel.Reject) {
                result.RiskLevel = "REJECT";
                result.ResultText = temp.Result;
                return;
            } else {
                result.RiskLevel = "REVIEW";
                HashSet<string> postions = new HashSet<string>();
                GetTextFilterDetailResult(result.Details, temp.ReviewSingleItems, postions, IllegalWordsRiskLevel.Review, request);
                GetTextFilterDetailResult(result.Details, temp.ReviewMultiItems, postions, IllegalWordsRiskLevel.Review, request);
                postions = null;
            }
        }

        #region SetContacts
        private static void SetContacts(TextFindAllGrpcReply result, IllegalWordsFindAllResult temp, ITextRequest request)
        {
            if (temp.ContactItems.Count == 0) { return; }
            foreach (var contactItem in temp.ContactItems) {
                TextFilterContactGrpcResult item = new TextFilterContactGrpcResult();
                item.ContactType = contactItem.ContactType.ToString();
                item.Position = $"{contactItem.Start}-{contactItem.End}";
                if (request.OnlyPosition == false) {
                    item.ContactString = request.Txt.AsSpan().Slice(contactItem.Start, contactItem.End + 1 - contactItem.Start).ToString();
                }
                result.Contacts.Add(item);
            }
        } 
        #endregion

        #region private GetTextFilterDetailResult
        private static void GetTextFilterDetailResult(IList<TextFilterDetailGrpcResult> results, List<SingleWordsResult> singles, HashSet<string> postions
            , IllegalWordsRiskLevel? riskLevel, ITextRequest request)
        {
            foreach (var resultItem in singles) {
                TextFilterDetailGrpcResult result = new TextFilterDetailGrpcResult();
                result.RiskCode = resultItem.Code;
                if (riskLevel == IllegalWordsRiskLevel.Reject) {
                    result.RiskLevel = "REJECT";
                } else {
                    result.RiskLevel = "REVIEW";
                }

                if (resultItem.Start == resultItem.End) {
                    result.Position = resultItem.Start.ToString();
                } else {
                    result.Position = $"{resultItem.Start}-{resultItem.End}";
                }
                if (request.OnlyPosition == false) {
                    result.Text = request.Txt.AsSpan().Slice(resultItem.Start, resultItem.End + 1 - resultItem.Start).ToString();
                }
                if (postions.Add(result.Position)) {
                    results.Add(result);
                }
            }
        }
        private static void GetTextFilterDetailResult(IList<TextFilterDetailGrpcResult> results, List<MultiWordsResult> multis, HashSet<string> postions
            , IllegalWordsRiskLevel? riskLevel, ITextRequest request)
        {
            foreach (var resultItem in multis) {
                TextFilterDetailGrpcResult result = new TextFilterDetailGrpcResult();
                result.RiskCode = resultItem.Code;
                if (riskLevel == IllegalWordsRiskLevel.Reject) {
                    result.RiskLevel = "REJECT";
                } else {
                    result.RiskLevel = "REVIEW";
                }

                TextRange textRange = new TextRange();
                if (request.OnlyPosition == false) {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < resultItem.Items.Length; i++) {
                        var t = resultItem.Items[i];
                        textRange.Add(t.Start, t.End);
                        if (i > 0) { sb.Append(' '); }
                        sb.Append(request.Txt.AsSpan().Slice(t.Start, t.End + 1 - t.Start).ToString());
                    }
                    result.Text = sb.ToString();
                } else {
                    for (int i = 0; i < resultItem.Items.Length; i++) {
                        var t = resultItem.Items[i];
                        textRange.Add(t.Start, t.End);
                    }
                }
                result.Position = textRange.ToString();
                textRange = null;

  
                if (postions.Add(result.Position)) {
                    results.Add(result);
                }
            }
        }
        #endregion
        #endregion
#endif



    }
}
