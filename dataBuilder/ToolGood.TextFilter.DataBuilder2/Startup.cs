using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;

namespace ToolGood.TextFilter.DataBuilder2
{
    public partial class Startup
    {
        public string SqliteDataFile;
        public string Phone;
        public bool IsDebug;
        public bool BigFile;

        public void Init(string sqliteDataFile, string phone, bool isDebug = false, bool bigFile = false)
        {
            SqliteDataFile = sqliteDataFile;
            Phone = phone;
            IsDebug = isDebug;
            BigFile = bigFile;
        }

        public void Build()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> temp file clear...");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Directory.CreateDirectory("temp");
            foreach (var file in Directory.GetFiles("temp")) { File.Delete(file); }

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> Startup Build Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            var helper = SqlHelperFactory.OpenSqliteFile(SqliteDataFile);


            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildTxtEndChar Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildTxtEndChar(helper);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildTranslate Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            var translateSearch = BuildTranslate(helper);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildFenci Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildFenci(helper, translateSearch);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildKeywordType Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildKeywordType(helper);


            TxtCache txtCache = new TxtCache();
            TxtCommonCache txtCommonCache = new TxtCommonCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> LoadTxtCommon Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            LoadTxtCommon(helper, txtCommonCache);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildKeyword_012 Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildKeyword_012(helper, txtCache, txtCommonCache);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildKeyword_34 Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            var (multiKeywords, contactInfos) = BuildKeyword_34(helper, txtCache, txtCommonCache);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildMergeKeyword Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildMultiKeyword(helper, txtCache, multiKeywords);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildContact Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildContactKeyword(helper, txtCache, contactInfos);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildMergeKeyword Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildKeywordInfoFile(helper, txtCache);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildSkipwords Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildSkipwords(helper);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> BuildMergeFile Start");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
            BuildMergeFile(helper, txtCache);


        }




    }

}
