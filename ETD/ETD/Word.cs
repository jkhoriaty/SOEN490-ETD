using System;

namespace ETD
{
    // Class containing the multilingual equivalent of a word. One string variable per language.
    // Can be expanded by adding more.
    class Word
    {
        private String fr { get; set; } // French version of a word.
        private String en { get; set; } // English version of a word.

        public Word(String f, String e)
        {
            this.fr = f;
            this.en = e;
        }
    }
}