using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace ETD.Services
{
    public class LanguageSelector : Observable 
    {
        //List of available languages
        public enum Languages
        {
            English,
            French
        }

        private static readonly Dictionary<Languages, string> AvailableLanguages = new Dictionary<Languages, string>
        {
            { Languages.English, "en-CA" },
            { Languages.French, "fr-CA" }
        };

        private static Languages currentLanguage = Languages.English;//Default language set to english
        private static List<Observer> observers = new List<Observer>();
        

        //Switch betwen language
        public static void switchLanguage(Languages language)
        {
            if (AvailableLanguages.ContainsKey(language) && !language.Equals(currentLanguage))
            {
                currentLanguage = language;
                ETD.Properties.Resources.Culture = new CultureInfo(AvailableLanguages[language]);
                Observable.ClassModifiedNotification(typeof(LanguageSelector));
            }
        }
    }
}
