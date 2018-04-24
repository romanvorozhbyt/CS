using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 7, b = 2;
            var (s, rem, q) = RemShiftDivider.Devide(a, b);
            Console.WriteLine($"{a}/{b} = {q}, remainder = {rem} - my result");
            Console.WriteLine($"{a}/{b} = {a/b}, remainder = {a%b} - compiller");
            Console.WriteLine(s) ;
            Console.ReadLine();
        }
    }
}
