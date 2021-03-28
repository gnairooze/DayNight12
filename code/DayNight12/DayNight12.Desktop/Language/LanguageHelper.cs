using System;
using System.Collections.Generic;
using System.Text;

namespace DayNight12.Desktop.Language
{
    public class LanguageHelper
    {
        public static ILanguage GetLanguageInstance(string language)
        {
            switch (language)
            {
                case "en":
                    return new EnglishUS();
                case "ar":
                    return new ArabicEgypt();
                default:
                    return new EnglishUS();
            }
        }
    }
}
