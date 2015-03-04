using System;

namespace ETD.Models.Objects
{
    /* Class containing the multilingual equivalent of a word. One string variable per language.
     Can be expanded by adding more.*/

    public class Word
    {
        private String french; // French version of a word.

        private String english; // English version of a word.
        
        public Word(String fr, String eng)
        {
            this.french = fr;
            this.english = eng;
        }

        //Accessors

        //Returns french words
        public String getFrench()
        {
            return this.french;
        }


        //Returns english words
        public String getEnglish()
        {
            return this.english;
        }

        //Mutators

        //Sets the french word equivalent for the passed string
        public void setFrench(String fr)
        {
            this.french = fr;
        }

        //Sets the english word for the passed string
        public void setEnglish(String eng)
        {
            this.english = eng;
        }

    }
}