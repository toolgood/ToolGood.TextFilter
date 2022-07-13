using System;
using System.IO;
using ToolGood.ReadyGo3;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        private const string _tempOutFilePath_Big = "temp/out_big.temp";
        private const string _tempOutFilePath_Small = "temp/out_small.temp";
        private const string _version = "2021.07.18";
        public void BuildMergeFile(SqlHelper helper, TxtCache txtCache)
        {
            if (BigFile) {
                BuildMergeFile_Big(helper, txtCache);
            } else {
                BuildMergeFile_Small(helper, txtCache);
            }
        }
        private void BuildMergeFile_Big(SqlHelper helper, TxtCache txtCache)
        {
            var fs = File.Create(_tempOutFilePath_Big);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(Phone); bw.Write(_version);
            bw.Write(DateTime.Now.Year); bw.Write(DateTime.Now.Month); bw.Write(DateTime.Now.Day);
            var bytes = File.ReadAllBytes(_tempFenciKeywordPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeywordTypePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeywordPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempTranslateDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempTxtEndChar); bw.Write(bytes);
            bw.Write(txtCache.Keyword34_Start);

            bytes = File.ReadAllBytes(_tempTranslatePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempFenciPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeyword_012); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsTypePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_acRegexSearchDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_acRegexSearchPath); bw.Write(bytes);

            bw.Write(true);
            bytes = File.ReadAllBytes(_bigACTextFilterSearchDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_bigACTextFilterSearchPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_bigAcRegexSearchDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_bigAcRegexSearchPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempMultiwordSrearchPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempContactPath); bw.Write(bytes);

            bw.Close();
        }
        private void BuildMergeFile_Small(SqlHelper helper, TxtCache txtCache)
        {
            var fs = File.Create(_tempOutFilePath_Small);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(Phone); bw.Write(_version);
            bw.Write(DateTime.Now.Year); bw.Write(DateTime.Now.Month); bw.Write(DateTime.Now.Day);
            var bytes = File.ReadAllBytes(_tempFenciKeywordPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeywordTypePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeywordPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempTxtEndChar); bw.Write(bytes);
            bw.Write(txtCache.Keyword34_Start);

            bytes = File.ReadAllBytes(_tempTranslatePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempFenciPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempKeyword_012); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsTypePath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempSkipwordsPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_acRegexSearchDictPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_acRegexSearchPath); bw.Write(bytes);
            bw.Write(false);

            bytes = File.ReadAllBytes(_tempMultiwordSrearchPath); bw.Write(bytes);
            bytes = File.ReadAllBytes(_tempContactPath); bw.Write(bytes);
            bw.Close();
        }

    }
}
