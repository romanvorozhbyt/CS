using System;
using System.Text;


namespace Part3
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            float a = 10.5F, b = -555.25F;
            var (def, binAnsw, answ) = SumFloatingPointNumbers.Sum(a, b);
            Console.WriteLine("{0} + {1} = {2} is that right?",a,b,answ);
            Console.WriteLine("How we get this? here :\n" + def);
        }
    }
}
