using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    public class ABC
    {
        private String consciousness;
        private bool disoriented;
        private String airways;
        private String breathing;
        private int breathingFrequency;
        private String circulation;
        private int circulationFrequency;

        public ABC(String consciousness, bool disoriented, String airways, String breathing, int breathingFrequency, String circulation, int circulationFrequency)
        {
            this.consciousness = consciousness;
            this.disoriented = disoriented;
            this.airways = airways;
            this.breathing = breathing;
            this.breathingFrequency = breathingFrequency;
            this.circulation = circulation;
            this.circulationFrequency = circulationFrequency;
        }
    }
}
