using System;
using System.Collections.Generic;
using System.Text;

namespace DayNight12.Desktop.Language
{
    public class LanguageHelper
    {
        public static ILanguage GetLanguageInstance(string language)
        {
            return language switch
            {
                "en" => new EnglishUS(),
                "ar" => new ArabicEgypt(),
                _ => new EnglishUS(),
            };
        }
    }
}
