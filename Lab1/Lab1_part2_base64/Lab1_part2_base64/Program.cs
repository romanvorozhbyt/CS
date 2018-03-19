using System;
using System.IO;
using System.Text;
using Lab1;

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

            if (File.Exists(@"C:\Study\CS\Lab1\Shevchenko.txt") && File.Exists(@"C:\Study\CS\Lab1\Podrevlanskiy.txt") && File.Exists(@"C:\Study\CS\Lab1\PCI.txt"))
            {
                Base64Encode(@"C:\Study\CS\Lab1\Shevchenko.txt", @"C:\Study\CS\Lab1_part2_base64\ShevaBase64.txt");
                Base64Encode(@"C:\Study\CS\Lab1\Podrevlanskiy.txt", @"C:\Study\CS\Lab1_part2_base64\PodrevlanBase64.txt");
                Base64Encode(@"C:\Study\CS\Lab1\PCI.txt", @"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt");

            }
            //comparing file using local algoritm vs online encoder https://www.browserling.com/tools/file-to-base64
            Console.WriteLine(File.ReadAllText(@"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt") == File.ReadAllText(@"C:\Study\CS\Lab1_part2_base64\PCIBase64Online.txt"));
            Base64Encode(@"C:\Study\CS\Lab1\Shevchenko.bz2", @"C:\Study\CS\Lab1_part2_base64\ShevaBZ2Base64.txt");
            Base64Encode(@"C:\Study\CS\Lab1\Podrevlanskiy.bz2", @"C:\Study\CS\Lab1_part2_base64\PodrevlanBZ2Base64.txt");
            Base64Encode(@"C:\Study\CS\Lab1\PCI.bz2", @"C:\Study\CS\Lab1_part2_base64\PCIBZ2Base64.txt");

            DepthFileAnalyser f1Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\ShevaBase64.txt");
            DepthFileAnalyser f2Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PodrevlanBase64.txt");
            DepthFileAnalyser f3Base64 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PCIBase64.txt");

            DepthFileAnalyser f1Base64Bz2 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\ShevaBZ2Base64.txt");
            DepthFileAnalyser f2Base64Bz2 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PodrevlanBZ2Base64.txt");
            DepthFileAnalyser f3Base64Bz2 = new DepthFileAnalyser(@"C:\Study\CS\Lab1_part2_base64\PCIBZ2Base64.txt");
            Console.WriteLine("Кількість інформації в закодованому варіанті\\ закодованому і стисненму\nШевченко:  "+
                f1Base64.InformationCount.ToString()+" \\ "+f1Base64Bz2.InformationCount.ToString()+"\nПодрев'янський: "+
                f2Base64.InformationCount.ToString()+ " \\ " + f2Base64Bz2.InformationCount.ToString()+"\nPCI: "+
                f3Base64.InformationCount.ToString() + " \\ " + f3Base64Bz2.InformationCount.ToString());
        }

        public static void Base64Encode(string pathFrom, string pathTo)
        {
            int val;
            using (BinaryReader br = new BinaryReader(File.Open(pathFrom, FileMode.Open), Encoding.UTF8))
            {
                using (StreamWriter sr = new StreamWriter(File.OpenWrite(pathTo)))
                {
                    int mod = (int)(br.BaseStream.Length % 3);
                    for (int i = 0; i < br.BaseStream.Length - mod; i += 3)
                    {
                        val = (((br.ReadByte() << 8) + br.ReadByte() << 8) + br.ReadByte());
                        sr.Write(String.Format($"{ _alphabet[(val >> 18) & 0x3F]}{_alphabet[(val >> 12) & 0x3F]}{_alphabet[(val >> 6) & 0x3F]}{_alphabet[val & 0x3F]}"));
                    }
                    if (mod == 2)
                    {
                        val = ((br.ReadByte() << 8) + br.ReadByte() << 2);
                        sr.Write(String.Format($"{_alphabet[(val >> 12) & 0x3F]}{_alphabet[(val >> 6) & 0x3F]}{_alphabet[val & 0x3F]}="));
                    }
                    if (mod == 1)
                    {
                        val = br.ReadByte() << 4;
                        sr.Write(String.Format($"{_alphabet[(val >> 6) & 0x3F]}{_alphabet[val & 0x3F]}=="));
                    }
                }
            }
        }
    }
}

