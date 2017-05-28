using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace TestDemo
{
    public class CFuncChoices
    {
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public enum FuncStatus { equal = 1, more = 2, lesseven = 3, lessodd = 4, error = 5 };
        public static FuncStatus ShowChoicesResult(string strInput)
        {
            try
            {
                if (IsInt(strInput))
                {
                    int num = int.Parse(strInput);
                    if (num > 10)
                    {
                        return FuncStatus.more;
                    }
                    else if (num == 10)
                    {
                        return FuncStatus.equal;
                    }
                    else if (num % 2 == 0)
                    {
                        return FuncStatus.lesseven;
                    }
                    else
                    {
                        return FuncStatus.lessodd;
                    }
                }
                return FuncStatus.error;
            }
            catch (Exception ex)
            {
                return FuncStatus.error;
            }
        }
    }
}
