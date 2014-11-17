using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;


namespace ETD
{
    public class LanguageSelector
    {
        const String French = "French";
        const String English = "English";
        static private String currentLang = LanguageSelector.English;
        static private Vocabulary dictionary = new Vocabulary();
        static private List<IObserver> observers = new List<IObserver>();

        public static void attach(IObserver ob)
        {
            observers.Add(ob);
        }
        public static String getString(String n)
        {
            String value = dictionary.findWord(n, currentLang);
            return value;
        }

        public static void switchLanguage(String lang)
        {
            if (lang.Equals(LanguageSelector.English))
                currentLang = LanguageSelector.English;
            else if (lang.Equals(LanguageSelector.French))
                currentLang = LanguageSelector.French;
            update();
        }

        private static void update()
        {
            foreach(IObserver ob in observers)
            {
                ob.update();
            }
        }


    }
}
