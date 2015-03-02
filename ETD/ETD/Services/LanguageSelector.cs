using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;


namespace ETD.Services
{
    public class LanguageSelector : Observable 
    {
        public enum Languages
        {
            English,
            French
        }
        
        static private Languages currentLang = Languages.English;
        static private Vocabulary dictionary = new Vocabulary();
        static private List<Observer> observers = new List<Observer>();

        public static String getString(String n)
        {
            String value = dictionary.findWord(n, currentLang.ToString());
            return value;
        }

        public static void switchLanguage(Languages lang)
        {
            if (!lang.Equals(currentLang))
            {
                currentLang = lang;
                Observable.ClassModifiedNotification(typeof(LanguageSelector));
            }
        }
    }
}
