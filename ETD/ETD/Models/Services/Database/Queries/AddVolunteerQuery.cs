using ETD.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Services.Database.Queries
{
    class AddVolunteerQuery : DBQuery
    {
        public AddVolunteerQuery(string name, Trainings training)
        {
            sql = "INSERT INTO [Volunteers] (Volunteer_ID, Name, Training_Level) VALUES (null, '" + name + "', '" + training.ToString() + "')";
        }
    }
}
