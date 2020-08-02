namespace C64.FrontEnd.Extensions
{
    public static class StringExtensions
    {
        // include digits (0-9), letters(A-Z, a-z), and a few special characters ("-", ".", "_", "~").
        // 48 -57, 65-90, 97-122,
        public static string UrlEncode(this string text)
        {
            text = System.Text.RegularExpressions.Regex.Replace(text, @"[^A-Za-z0-9_\s-]", ""); // Remove all non valid chars
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ").Trim(); // convert multiple spaces into one space
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s", "_"); // //Replace spaces by dashes
            return System.Web.HttpUtility.UrlEncode(text);
        }
    }
}