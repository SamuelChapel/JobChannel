using System.Globalization;
using System.Text;

namespace JobChannel.DAL.ObjectExtensions
{
    public static class StringExtension
    {
        public static string NormalizeAndRemoveDiacriticsAndToLower(this string value)
            => string.IsNullOrEmpty(value) ? value : NormalizeAndRemoveDiacritics(value).ToLower();

        private static string NormalizeAndRemoveDiacritics(string value)
        {
            var valueWithoutDiacritics = RemoveDiacritics(value);

            return valueWithoutDiacritics.Normalize(NormalizationForm.FormC);
        }

        public static string RemoveDiacritics(this string value)
        {
            // We need to normalize the value with decompose form to suppress diacritics later
            var decomposedFormOfValue = value.Normalize(NormalizationForm.FormD);

            var builder = new StringBuilder();

            foreach (var character in decomposedFormOfValue)
            {
                if (IsDiacritics(character))
                {
                    builder.Append(character);
                }
            }

            return builder.ToString();
        }

        private static bool IsDiacritics(char c)
            => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark;
    }
}