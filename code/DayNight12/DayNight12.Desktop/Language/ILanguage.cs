using System;
using System.Collections.Generic;
using System.Text;

namespace DayNight12.Desktop.Language
{
    public interface ILanguage
    {
        public string Day { get; }
        public string Night { get; }
        public string Culture { get; }
    }
}
