using System.Globalization;
using System.Text;

namespace Phoneword;

public static class PhonewordTranslator
{
    public static string ToNumber(string raw)
    {
        if (string.IsNullOrEmpty(raw))
            return null;
        raw = raw.ToUpperInvariant();
        var newNumber = new StringBuilder();
        foreach (var i in raw)
        {
            if (" -0123456789".Contains(i))
            {
                newNumber.Append(i);
            }
            else
            {
                var result = TranslateNumber(i);
                if (result != null)
                {
                    newNumber.Append(result);
                }
                else
                {
                    return null;
                }
                
            }
        }
        return newNumber.ToString();
    }
    
    static bool Contains(this string keyString, char c)
    {
        return keyString.IndexOf(c) >= 0;
    }

    private static readonly string[] Digits =
    {
        "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    static int? TranslateNumber(char number)
    {
        for (int i = 0; i < Digits.Length; i++)
        {
            if (Digits[i].Contains(number))
                return 2 + i;   
        }

        return null;
    }
}