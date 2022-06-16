using System;

namespace NiftyFramework.Utils
{
    public static class NumberUtil
    {
        public static string Humanize(this double number)
        {
            string[] suffix = {"f", "a", "p", "n", "μ", "m", string.Empty, "k", "M", "G", "T", "P", "E"};

            if (number == 0)
            {
                return "0";
            }

            var absnum = Math.Abs(number);

            int mag;
            if (absnum < 1)
            {
                mag = (int) Math.Floor(Math.Floor(Math.Log10(absnum))/3);
            }
            else
            {
                mag = (int) (Math.Floor(Math.Log10(absnum))/3);
            }

            var shortNumber = number/Math.Pow(10, mag*3);

            return $"{shortNumber:0.###}{suffix[mag + 6]}";
        }

        public static string GetSign(this double number)
        {
            if (number > 0)
            {
                return "+";
            }
            if (number < 0)
            {
                return "-";
            }
            return "";
        }
    }
}