using System;
using System.Collections.Generic;
using System.Xml;

namespace ETD
{
    // Class containing the set of words in a vocabulary associated with a unique identifier.
    class Vocabulary
    {
        private Dictionary<String, Word> vocabulary; // <identifier, word>

        public Vocabulary()
        {
            this.vocabulary = new Dictionary<String, Word>();
            loadVocabulary();
        }

        //Load words for the vocabulary from an XML file.
        private void loadVocabulary()
        {
            using (XmlReader reader = XmlReader.Create("..\\..\\Vocabulary.xml"))
            {
                String name = "", fr = "", en = "";
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "control": name = reader.GetAttribute("name");
                                break;
                            case "fr": fr = reader.ReadElementContentAsString().Trim();
                                break;
                            case "en": en = reader.ReadElementContentAsString().Trim();
                                vocabulary.Add(name, new Word(fr, en));
                                break;
                        }
                    }
                }
            }
        }

        public string findWord(String id, String lang)
        {
            string value = "";
            if (vocabulary.ContainsKey(id))
            {
                if(lang.Equals("French"))
                    value = vocabulary[id].getFrench();
                else if(lang.Equals("English"))
                    value = vocabulary[id].getEnglish();
            }
            return value;
        }
    }
}