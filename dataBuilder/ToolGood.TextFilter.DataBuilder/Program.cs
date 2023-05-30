using Microsoft.Data.Sqlite;
using ToolGood.RcxCrypto;
using ToolGood.TextFilter.DataBuilder2;

namespace ToolGood.TextFilter.DataBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0) {
                Console.WriteLine("请输入【文件名】及【输出文件夹】！");
                return;
            }
            //SqliteFactory factory = SqliteFactory.Instance;
            var file = args[0];
            var outFile = "";
            if (args.Length > 1) {
                outFile = args[1];
            }
            build(file, outFile);

            Console.WriteLine("Build complete!");
        }
        private static void build(string file, string outFile)
        {
            if (File.Exists(file) == false) {
                Console.WriteLine("请选择敏感词库！");
                return;
            }
            var filePath = Path.GetFullPath(file);
            var outFilePath = Path.GetFullPath(Path.Combine(outFile, @$"TextFilter-{DateTime.Now.ToString("yyyyMMdd")}.data"));

            var bigFile = false;
            Startup startup = new Startup();

            startup.Init(filePath, "", false, bigFile);
            startup.Build();

            if (bigFile) {
                WriteOutFile("temp/out_big.temp", outFilePath);
            } else {
                WriteOutFile("temp/out_small.temp", outFilePath);
            }
            Console.WriteLine("完成");
        }
        private static void WriteOutFile(string path, string outPath)
        {
            var bytes = File.ReadAllBytes(path);

            string pwd = "012345679";
            bytes = CompressionUtil.GzipCompress(bytes);

            bytes = Rcy.Encrypt(bytes, pwd);

            outPath = Path.GetFullPath(outPath);
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllBytes(outPath, bytes);
        }

    }
}