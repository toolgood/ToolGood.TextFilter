/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using CommonMark;
using CommonMark.Syntax;
using ToolGood.HtmlExtract.HtmlAgilityPack;

namespace ToolGood.TextFilter
{
    public class MarkdownStream : ReadStreamBase
    {
        public MarkdownStream(string source) : base(source)
        {
            Parse(source);
        }

 
        #region Parse

        private void Parse(string src)
        {
            var document = CommonMarkConverter.Parse(src, new CommonMarkSettings() {
                AdditionalFeatures = CommonMarkAdditionalFeatures.All,
                TrackSourcePosition = true,
                RenderSoftLineBreaksAsLineBreaks = true,
            });

            StringBuilder sb1 = new StringBuilder();
            var starts1 = new List<int>();
            var ends1 = new List<int>();
            //StringBuilder sb2 = new StringBuilder();
            //var starts2 = new List<int>();
            //var ends2 = new List<int>();


            foreach (var node in document.AsEnumerable()) {
                #region Block
                if (node.Block != null) {
                    var block = node.Block;
                    if (block.Tag == BlockTag.FencedCode) {
                        AddEnter(sb1, starts1, ends1);
                        var str = block.StringContent.ToString();
                        AddString(str, block.FencedCodeData.CodePosition, str.Length, sb1, starts1, ends1);
                        AddEnter(sb1, starts1, ends1);
                    } else if (block.Tag == BlockTag.Paragraph || block.Tag == BlockTag.AtxHeading || block.Tag == BlockTag.SetextHeading) {
                        AddEnter(sb1, starts1, ends1);
                    } else if (block.Tag == BlockTag.HtmlBlock) {
                        AddEnter(sb1, starts1, ends1);
                        var str = Source.AsSpan().Slice(block.SourcePosition, block.SourceLength).ToString();
                        AnalysisHtml(str, block.SourcePosition, sb1, starts1, ends1);
                        AddEnter(sb1, starts1, ends1);
                    } else if (block.Tag == BlockTag.IndentedCode) {
                        AddEnter(sb1, starts1, ends1);
                        AddString(Source.AsSpan().Slice(block.SourcePosition, block.SourceLength).ToString(), block.SourcePosition, block.SourceLength, sb1, starts1, ends1);
                        AddEnter(sb1, starts1, ends1);
                    }
                }
                #endregion
                #region Inline
                if (node.Inline == null) { continue; }
                var inline = node.Inline;

                switch (inline.Tag) {
                    case InlineTag.String:
                    case InlineTag.Code:
                    case InlineTag.RawHtml:
                        AddString(inline.LiteralContent, inline.SourcePosition, inline.SourceLength, sb1, starts1, ends1);
                        break;
                    case InlineTag.Placeholder:
                        AddString(inline.TargetUrl, inline.SourcePosition, inline.TargetUrl.Length, sb1, starts1, ends1);
                        break;
                    case InlineTag.Link:
                    case InlineTag.Image:
                        //AddZore(sb2, starts2, ends2);
                        //var start = src.IndexOf(inline.TargetUrl, inline.SourcePosition);
                        //AddString(inline.TargetUrl, start, inline.TargetUrl.Length, sb2, starts2, ends2);
                        break;
                    case InlineTag.SoftBreak: AddEnter(sb1, starts1, ends1); break;
                    case InlineTag.LineBreak: AddEnter(sb1, starts1, ends1); break;
                    case InlineTag.Emphasis: break;//强调
                    case InlineTag.Strong: break;//加粗
                    case InlineTag.Strikethrough: break;//删除线
                    default: break;
                }
                #endregion
            }
            document = null;

            TestingText = sb1.ToString().ToCharArray();
            Start = starts1.ToArray();
            End = ends1.ToArray();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddEnter(StringBuilder sb, List<int> starts, List<int> ends)
        {
            if (starts.Count == 0) { return; }
            if (starts[starts.Count - 1] == -1) {
                sb.Append('\n');
                starts.Add(-1);
                ends.Add(-1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddZore(StringBuilder sb, List<int> starts, List<int> ends)
        {
            if (starts.Count == 0) { return; }
            if (starts[starts.Count - 1] == -1) {
                sb.Append((char)0);
                starts.Add(-1);
                ends.Add(-1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddString(string txt, int position, int length, StringBuilder sb, List<int> starts, List<int> ends)
        {
            var len = txt.Length;
            if (len == 1) {
                sb.Append(txt);
                starts.Add(position);
                ends.Add(position + length - 1);
                return;
            }
            sb.Append(txt);
            for (int i = 0; i < length; i++) {
                starts.Add(position + i);
                ends.Add(position + i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AnalysisHtml(string txt, int position, StringBuilder sb1, List<int> starts1, List<int> ends1)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(txt);

            HtmlUtil.BuildWordsInfo(doc.DocumentNode, position, sb1, starts1, ends1);
            doc = null;
        }

        #endregion


    }
}
