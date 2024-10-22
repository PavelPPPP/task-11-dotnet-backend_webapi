using System.Text.RegularExpressions;

namespace InfrastructureApi.Extensions
{
    public static class StringExtension
    {
        public static string ReplacePartNameTypeEntity(this string strName, string deletePartStr, string insertStr)
        {
            string tempStr = strName;
            tempStr = Regex.Replace(tempStr, @$"{deletePartStr}$", insertStr);

            return tempStr;
        }

        public static string LowerFirstChar(this string str)
        {
            string tempStr = str;
            char firstChar = str[0];
            tempStr = Regex.Replace(tempStr, @"^\w{1}", firstChar.ToString().ToLower());

            return tempStr;
        }
    }
}
