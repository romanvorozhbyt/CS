using System;
using System.IO;
using System.Text;
using Lab1;
using ICSharpCode.SharpZipLib.BZip2;

namespace Lab1_part2_base64
{

    class Program
    {

       static string _alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            Console.WriteLine(DateTime.Now);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ToLower(@"C:\Study\CS\Lab1_part1\Shevchenko.txt");
            ToLower(@"C:\Study\CS\Lab1_part1\Podrevlanskiy.txt");
           // if (File.Exists(@"C:\Study\CS\Lab1_part1\Shevchenko.txt") && File.Exists(@"C:\Study\CS\Lab1\Podrevlanskiy.txt") && File.Exists(@"C:\Study\CS\Lab1\PCI.txt"))
            //{
                Base64Encode(@"C:\Study\CS\Lab1_part1\Shevchenko.txt", @"C:\Study\CS\Lab1_part2_base64\ShevaBase64.txt");
                Base64Encode(@"C:\Study\CS\Lab1_part1\Podrevlanskiy.txt", @"C:\Study\CS\Lab1_part2_base64\PodrevlanBase64.txt");
                Base64Encode(@"C:\Study\CS\Lab1_part1\PCI.txt", @"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt");
                //Base64Encode(@"C:\Study\CS\Lab1_part1\ShevchenkoToLower.txt", @"C:\Study\CS\Lab1_part2_base64\ShevaToLowerBase64.txt");
                //Base64Encode(@"C:\Study\CS\Lab1_part1\PodrevlanskiyToLower.txt", @"C:\Study\CS\Lab1_part2_base64\PodrevlanToLowerBase64.txt");


            //}
            //comparing file using local algoritm vs online encoder https://www.browserling.com/tools/file-to-base64
            // Console.WriteLine(File.ReadAllText(@"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt") == File.ReadAllText(@"C:\Study\CS\Lab1_part2_base64\PCIBase64Online.txt"));
            FileInfo f1Base64Compress = new FileInfo(@"C:\Study\CS\Lab1_part2_base64\ShevaBase64.txt");
            FileInfo f2Base64Compress = new FileInfo(@"C:\Study\CS\Lab1_part2_base64\PodrevlanBase64.txt");
            FileInfo f3Base64Compress = new FileInfo(@"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt");
            f1Base64Compress = CompressBZip2(f1Base64Compress);
            f2Base64Compress = CompressBZip2(f2Base64Compress);
            f3Base64Compress = CompressBZip2(f3Base64Compress);

            DepthFileAnalyser f1Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\ShevaBase64.txt");
            DepthFileAnalyser f2Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PodrevlanBase64.txt");
            DepthFileAnalyser f3Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt");

          //  DepthFileAnalyser f1LowerBase64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\ShevaToLowerBase64.txt");
          //  DepthFileAnalyser f2lowerBase64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PodrevlanToLowerBase64.txt");


            DepthFileAnalyser f1Base64Bz2 = new DepthFileAnalyser(f1Base64Compress.FullName);
            DepthFileAnalyser f2Base64Bz2 = new DepthFileAnalyser(f2Base64Compress.FullName);
            DepthFileAnalyser f3Base64Bz2 = new DepthFileAnalyser(f3Base64Compress.FullName);

            //DepthFileAnalyser f1LowerBase64Bz2 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\ShevaBZ2ToLowerBase64.txt");
            //DepthFileAnalyser f2LowerBase64Bz2 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PodrevlanBZ2ToLowerBase64.txt");

            Console.WriteLine("Кількість інформації в закодованому варіанті\\ закодованому і стисненму\nШевченко:  "+
                f1Base64.InformationCount.ToString()+" \\ "+f1Base64Bz2.InformationCount.ToString()+"\nПодрев'янський: "+
                f2Base64.InformationCount.ToString()+ " \\ " + f2Base64Bz2.InformationCount.ToString()+"\nPCI: "+
                f3Base64.InformationCount.ToString() + " \\ " + f3Base64Bz2.InformationCount.ToString()+ "\n shevaLower");
        }

        public static void ToLower(string path)
        {
            string file = File.ReadAllText(path);
            file = file.ToLower();
            using (StreamWriter sr = new StreamWriter(File.OpenWrite(path.Substring(0, path.Length - 4) + "ToLower.txt")))
            {
                sr.Write(file);
            }
        }
        public static void Base64Encode(string pathFrom, string pathTo)
        {
            int val;
            using (BinaryReader br = new BinaryReader(File.Open(pathFrom, FileMode.Open), Encoding.UTF8))
            {
                using (StreamWriter sr = new StreamWriter(File.OpenWrite(pathTo)))
                {
                    int rem = (int)(br.BaseStream.Length % 3);
                    for (int i = 0; i < br.BaseStream.Length - rem; i += 3)
                    {
                        val = br.ReadByte();
                        val <<= 8 + br.ReadByte();
                        val<<= 8 + br.ReadByte();
                        sr.Write(_alphabet[(val >> 18) & 0x3F]);
                        sr.Write(_alphabet[(val >> 12) & 0x3F]);
                        sr.Write(_alphabet[(val >> 6) & 0x3F]);
                        sr.Write(_alphabet[val & 0x3F]);
                    }
                    if (rem == 2)
                    {
                        val = br.ReadByte();
                        val <<= 8 + br.ReadByte();
                        val <<= 2;
                        sr.Write(_alphabet[(val >> 12) & 0x3F]);
                        sr.Write(_alphabet[(val >> 6) & 0x3F]);
                        sr.Write(_alphabet[val  & 0x3F]+"=");
                    }
                    if (rem == 1)
                    {
                        val = br.ReadByte() << 4;
                        sr.Write(_alphabet[(val >> 6) & 0x3F]);
                        sr.Write(_alphabet[val & 0x3F]+"==");
                    }
                }
            }
        }
        public static FileInfo CompressBZip2(FileInfo fi)
        {
            FileInfo zipFileName = new FileInfo(fi.FullName.Substring(0, fi.FullName.Length - fi.Extension.Length) + ".bz2");
            if (!File.Exists(zipFileName.FullName))
            {
                using (FileStream fileToBeZippedAsStream = fi.OpenRead())
                {

                    using (FileStream zipTargetAsStream = zipFileName.Create())
                    {
                        try
                        {
                            BZip2.Compress(fileToBeZippedAsStream, zipTargetAsStream, true, 4096);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return zipFileName;
        }
    }
}

