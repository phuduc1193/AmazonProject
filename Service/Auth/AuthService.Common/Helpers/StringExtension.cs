using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthService.Common.Helpers
{
    public static class StringExtension
    {
        public static List<string> SplitString(this string input, string splitBy = " ")
        {
            if (string.IsNullOrEmpty(input)) return null;
            return input.Split(new string[] { splitBy }, StringSplitOptions.None).ToList();
        }
    }
}
