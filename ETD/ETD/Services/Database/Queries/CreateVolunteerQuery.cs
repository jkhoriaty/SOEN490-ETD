using ETD.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateVolunteerQuery : DBQuery
    {
        public CreateVolunteerQuery(string name, Trainings training)
        {
            sql = "INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + name + "', " + training.ToString() + ")";
        }
    }
}
