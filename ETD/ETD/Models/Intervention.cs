using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models
{
    public class Intervention
    {
        private DateTime timeOfCall;
        private String callerName;
        private String location;
        private String natureOfCall;
        private Resource[] resources;

        private int code;
        private char gender;
        private int age;

        private String additionalInfo;

        public Intervention(String callerName, String location, String natureOfCall)
        {
            this.timeOfCall = DateTime.Now;
            this.callerName = callerName;
            this.location = location;
            this.natureOfCall = natureOfCall;
        }

        public struct Resource
        {
            String resourceName;
            String indicatif;
            DateTime inDirection;
            DateTime inPosition;

            public Resource(String resourceName, String indicatif, DateTime inDirection, DateTime inPosition)
            {
                this.resourceName = resourceName;
                this.indicatif = indicatif;
                this.inDirection = inDirection;
                this.inPosition = inPosition;
            }
        }

        public void setCode(int code)
        {
            this.code = code;
        }
        public int getCode()
        {
            return this.code;
        }

        public void setGender(char gender)
        {
            this.gender = gender;
        }
        public char getGender()
        {
            return this.gender;
        }

        public void setAge(int age) 
        {
            this.age = age;
        }
        public int getAge()
        {
            return this.age;
        }

        public void setAdditionalInfo(String info)
        {
            this.additionalInfo = info;
        }
        public String getAdditionalInfo()
        {
            return this.additionalInfo;
        }
    }
}
