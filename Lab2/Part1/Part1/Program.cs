using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 3, b = -4;
            var (def, binary, answ) = BoothMultiplicator.Multiply(a, b);
            Console.WriteLine($"{a} * {b} = {answ}");
            Console.Read();
            Console.WriteLine(def);
        }
    }
}
