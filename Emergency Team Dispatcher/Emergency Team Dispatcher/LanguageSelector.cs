using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Emergency_Team_Dispatcher
{
    class LanguageSelector
    {
        static List<Word> vocabulary = new List<Word>();

        public static void loadVocabulary()
        {
            if (vocabulary.Count() == 0)
            {
                using (XmlReader reader = XmlReader.Create("..\\..\\Vocabulary.xml"))
                {
                    String name = "", type = "", fr = "", en = "";
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "control": name = reader.GetAttribute("name");
                                    type = reader.GetAttribute("type");
                                    break;
                                case "fr": fr = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "en": en = reader.ReadElementContentAsString().Trim();
                                    vocabulary.Add(new Word(name, type, fr, en));
                                    break;
                            }
                        }
                    }
                }
            }

        }
        public static void changeLanguage(MainWindow win, String lang)
        {
            foreach(Word w in vocabulary)
            {
                if (w.type == "MenuItem")
                    renameMenuItem(w, win, lang);
            }
        }

        private static void renameMenuItem(Word w,MainWindow win, String lang)
        {
            MenuItem item = win.FindName(w.name) as MenuItem;
            switch(lang)
            {
                case "fr": item.Header = w.fr;
                    break;
                case "en": item.Header = w.en;
                    break;
            }
        }
    }

    class Word
    {
        public String name;
        public String fr;
        public String en;
        public String type;

        public Word(String n, String t, String f, String e)
        {
            this.name = n;
            this.type = t;
            this.fr = f;
            this.en = e;
        }
    }


}
