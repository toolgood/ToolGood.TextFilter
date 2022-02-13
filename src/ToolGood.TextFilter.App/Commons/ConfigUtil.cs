/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Diagnostics;
using System.IO;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Commons
{
    public static class ConfigUtil
    {
        private const string _dataPath = "data.save";

        internal static SqlHelper GetSqlHelper()
        {
            var path = Path.Combine(AppContext.BaseDirectory, _dataPath);

            if (System.IO.File.Exists(path) == false) {
                System.IO.File.Create(path).Close();
                var helper = SqlHelperFactory.OpenSqliteFile(path);

                if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.Other) {
                    executeLinuxCmd("chmod", "777 " + path);
                }

                helper._TableHelper.CreateTable(typeof(DbKeywordType));
                helper._TableHelper.CreateTable(typeof(DbSetting));

                string skipword = @"\x00-\x1F\x7f\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF \t\r\n~!@#$%^&*()_+-=【】、[]{}|;':""，。、《》？αβγδεζηθικλμνξοπρστυφχψωΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ。，、；：？！…—·ˉ¨‘’“”々～‖∶＂＇｀｜〃〔〕〈〉《》「」『』．〖〗【】（）［］｛｝ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅪⅫ⒈⒉⒊⒋⒌⒍⒎⒏⒐⒑⒒⒓⒔⒕⒖⒗⒘⒙⒚⒛㈠㈡㈢㈣㈤㈥㈦㈧㈨㈩①②③④⑤⑥⑦⑧⑨⑩⑴⑵⑶⑷⑸⑹⑺⑻⑼⑽⑾⑿⒀⒁⒂⒃⒄⒅⒆⒇≈≡≠＝≤≥＜＞≮≯∷±＋－×÷／∫∮∝∞∧∨∑∏∪∩∈∵∴⊥∥∠⌒⊙≌∽√§№☆★○●◎◇◆□℃‰€■△▲※→←↑↓〓¤°＃＆＠＼︿＿￣―♂♀┌┍┎┐┑┒┓─┄┈├┝┞┟┠┡┢┣│┆┊┬┭┮┯┰┱┲┳┼┽┾┿╀╁╂╃└┕┖┗┘┙┚┛━┅┉┤┥┦┧┨┩┪┫┃┇┋┴┵┶┷┸┹┺┻╋╊╉╈╇╆╅╄";

                helper.Insert(new DbSetting() { Key = "skipword", Value = skipword, Comment = "自定义敏感词的跳词", ModifyTime = DateTime.Now, });
                helper.Insert(new DbSetting() { Key = "TextFindAllNoticeUrl", Value = "", Comment = "查询操作-异步通知网址", ModifyTime = DateTime.Now, });
                helper.Insert(new DbSetting() { Key = "TextReplaceNoticeUrl", Value = "", Comment = "替换操作-异步通知网址", ModifyTime = DateTime.Now, });


                helper.Dispose();
            }
            return SqlHelperFactory.OpenSqliteFile(path);
        }

        private static void executeLinuxCmd(string cmd, string args)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(cmd, args) {
                RedirectStandardOutput = false,
                RedirectStandardInput = false,
                RedirectStandardError = false,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();

        }


    }
}
