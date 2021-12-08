using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2021;

public static class Extensions
{
    public static double FromBinaryStringToDecimal(this string source)
    {
        if (String.IsNullOrEmpty(source) == false)
        {
            var lineLen = source.Length;
            double result = 0;
            for (var i = 0; i < lineLen; i++)
            {
                var thisChar = source[i];

                var exponent = lineLen - i - 1;

                if(thisChar == '1')
                {
                    result = result + Math.Pow(2, exponent);
                }
            }
            return result;
        }
        else
        {
            return 0;
        }
    }
}

