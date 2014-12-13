using System;

namespace ETD.Models
{
    // Class containing the multilingual equivalent of a word. One string variable per language.
    // Can be expanded by adding more.
    public class Word
    {
        private String fr; // French version of a word.

        private String en; // English version of a word.
        
        public Word(String f, String e)
        {
            this.fr = f;
            this.en = e;
        }

        public void setFrench(string f)
        {
            this.fr = f;
        }

        public String getFrench()
        {
            return this.fr;
        }

        public void setEnglish(string e)
        {
            this.en = e;
        }

        public String getEnglish()
        {
            return this.en;
        }
        
    }
}