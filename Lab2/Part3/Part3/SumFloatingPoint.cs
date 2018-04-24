using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    static class SumFloatingPointNumbers
    {
        public static (string desc, string binResult, float result) Sum(float x, float y)
        {
            uint a1 = BitConverter.ToUInt32(BitConverter.GetBytes(x), 0);
            uint a2 = BitConverter.ToUInt32(BitConverter.GetBytes(y), 0);
            
            uint s1 = a1 >> 31;
            uint s2 = a2 >> 31;

            uint e1 = (a1 >> 23) & 0xFF;
            uint e2 = (a2 >> 23) & 0xFF;

            uint m1 = (a1 & 0x7FFFFF) + 8388608;
            uint m2 = (a2 & 0x7FFFFF) + 8388608;

           
            string  description = $"a1 = {Convert.ToString(a1, 2)}\n";
            description += $"a2 = {Convert.ToString(a2, 2)}\n\n";
            description += "s - знаковий біт, e - експонента, m - мантиса\n";
            description += $"s1 = {s1} e1 = {Convert.ToString(e1, 2)} m1 = {Convert.ToString(m1, 2)}\n";
            description += $"s2 = {s2} e2 = {Convert.ToString(e2, 2)} m2 = {Convert.ToString(m2, 2)}\n";
            
            if ((((e2 << 23) + m2) > (e1<<23) + m1) )//if second value is greater swap them 
            {
                uint temp = e1;
                e1 = e2;
                e2 = temp;

                temp = s1;
                s1 = s2;
                s2 = temp;

                temp = m1;
                m1 = m2;
                m2 = temp;

                temp = a1;
                a1 = a2;
                a2 = temp;

                description += "\nміняємо значення місцями, бо |a2| > |a1|)\n";
                description += $"s1 = {s1} e1 = {Convert.ToString(e1, 2)} m1 = {Convert.ToString(m1, 2)}\n";
                description += $"s2 = {s2} e2 = {Convert.ToString(e2, 2)} m2 = {Convert.ToString(m2, 2)}\n";
            }
            
            uint er = e1;//exponent part of the result value
            
            m2 >>= (int)(e1 - e2);
            description += $"\nздвигаємо m2 вправо на різницю експонент {e1 - e2}\n";
            description += $" отримали число: \ns2 = {s2} e2 = {Convert.ToString(e2, 2)} m2 = {Convert.ToString(m2, 2)}\n";
            
            uint mr; //result mantissa

            if (s1 == s2)
                mr = m1 + m2;
            else
                mr = m1 - m2;

            description += $"\nВираховуємо {(s1==s2?"cумму":"різницю")} мантис\n";
            description += "Отримали результат: \n";
            description+=  $"s3 = {s1} e3 = {Convert.ToString(er, 2)} m3 = {Convert.ToString(mr, 2)}\n";
            

            if (mr >> 23 != 1)
            {
                int lenght;
                uint i = mr;
                for (lenght = 0; i != 0; i >>= 1, lenght++) ;

                er += (uint)(lenght - 24);
                if (lenght > 24)
                    mr >>= (lenght - 24);
                else
                    mr <<= (24 - lenght);

                description += "\nНормалізований вигляд\n";
                description += $"s3 = {s1} e3 = {Convert.ToString(er, 2)} m3 = {Convert.ToString(mr, 2)}\n";
            }

            uint result = (((s1 << 8) + er)<<23) + (mr & 0x7FFFFF);
            float floatresult = BitConverter.ToSingle(BitConverter.GetBytes(result), 0);

            return (description, Convert.ToString(result, 2), floatresult);
        }
    }
}