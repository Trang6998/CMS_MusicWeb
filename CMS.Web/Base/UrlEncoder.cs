using System;
using System.Text;

namespace CMS.Web.Base
{
    public static class UrlEncoder
    {
        public static string ToFriendlyUrl(string title)
        {
            if (title == null) return "x";
            const int maxlen = 150;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1) == "" ? "x" : sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString() == "" ? "x" : sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = Char.ToString(c).ToLowerInvariant();
            if ("àåáâäãåąạậấầẫẩắằẵẳảăặ".Contains(s))
            {
                return "a";
            }
            else if ("èéêëęệếềệẹẻể".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïıịỉĩ".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőðơờớợốộồỗổởọỏỡ".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭůưứừựụủửữũ".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿỹýỳỵ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if ("đđ".Contains(s))
            {
                return "d";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
        public static string UnToFriendlyUrl(string title)
        {
            if (title == null) return "x";
            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append(' ');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(c);
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1) == "" ? "x" : sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString() == "" ? "x" : sb.ToString();
        }

        public static bool checkSpecialChar(char c)
        {
            string s = Char.ToString(c).ToLowerInvariant();
            if ("àåáâäãåąạậấầẫẩắằẵẳảăặèéêëęệếềệẹẻểìíîïıịĩỉòóôõöøőðơờớợốộồỗổởọỏỡùúûüŭůưứừựụủửữũçćčĉżźžśşšŝñńýÿỹýỳỵğĝđđřłßÞĥĵ".Contains(s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isFriendlyUrl(string title)
        {
            if (title == null) return false;
            //const int maxlen = 80;
            int len = title.Length;
            //bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if (checkSpecialChar(c))
                    return false;
            }
            return true;
        }
    }
}