using System;
using System.Globalization;

namespace JobPortal.Shared
{
    public static class Utilities
    {
        // -------------------------------------------------------
        // SAFE GUID ID CREATOR
        // -------------------------------------------------------
        public static string CreateId(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "ID";

            return prefix + "_" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }


        // -------------------------------------------------------
        // SAFE DATE FORMATTER
        // Prevents: format exception, invalid date crashes, null
        // -------------------------------------------------------
        public static string FormatDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return "";

            // Try normal parse
            if (DateTime.TryParse(date, out DateTime d))
                return d.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);

            // If it’s already clean (e.g. "December 15, 2025")
            return date;
        }


        // -------------------------------------------------------
        // TEXT FORMATTER (newline → <br>)
        // Prevents null reference + handles multiline safely
        // -------------------------------------------------------
        public static string FormatText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "Not specified";

            return text.Replace("\r\n", "<br/>")
                       .Replace("\n", "<br/>");
        }


        // -------------------------------------------------------
        // INPUT CLEANING (null-proof)
        // -------------------------------------------------------
        public static string Clean(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "" : input.Trim();
        }
    }
}
