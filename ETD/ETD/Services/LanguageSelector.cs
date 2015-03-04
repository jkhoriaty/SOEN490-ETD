using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;


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
        
        static private Languages currentLanguage = Languages.English;//Default language set to english
        static private Vocabulary dictionary = new Vocabulary();
        static private List<Observer> observers = new List<Observer>();

        //Returns the current language in use
        public static String getString(String n)
        {
            String value = dictionary.findWord(n, currentLanguage.ToString());
            return value;
        }

        //Switch betwen language
        public static void switchLanguage(Languages language)
        {
            if (!language.Equals(currentLanguage))
            {
                currentLanguage = language;
                Observable.ClassModifiedNotification(typeof(LanguageSelector));
            }
        }
    }
}
