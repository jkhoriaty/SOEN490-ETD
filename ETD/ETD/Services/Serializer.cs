using ETD.Models.ArchitecturalObjects;
using ETD.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ETD.Services
{
    class Serializer
    {
        //Singleton variable
        private static Serializer instance;

        //Settings variables
        private const string outputDirectory = ".\\Temp\\";
        private const int backupRate = 5000; //interval of backups, in milliseconds

        //Objects to backup
        private List<Team> savedTeams;
        private List<Intervention> savedInterventions;
        private Operation savedOperation;

        //Objects needed for serialization
        private Timer timer;
        private FileStream fileStream;
        private BinaryFormatter serializer;
        

        private Serializer()
        {
            savedTeams = new List<Team>();
            savedInterventions = new List<Intervention>();

            serializer = new BinaryFormatter();
            timer = new Timer(backupRate);
            timer.Elapsed += new ElapsedEventHandler(BackUpEvent);
            timer.Enabled = true;
        }

        public static Serializer Instance
        {
            get
            {
                if (instance == null)
                {
                    Serializer.instance = new Serializer();
                }
                return instance;
            }
        }

        private void BackUpEvent(object sender, ElapsedEventArgs e)
        {
            BackUp();
        }
        private void BackUp()
        {
            if(!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            if (savedOperation != null && !File.Exists(outputDirectory + "Operation" + savedOperation.getID()))
            {
                fileStream = File.Create(outputDirectory + "Operation" + savedOperation.getID() + ".tmp");
                serializer.Serialize(fileStream, savedOperation);
                fileStream.Close();
            }
            foreach(Team t in savedTeams)
            {
                fileStream = File.Create(outputDirectory + "Team" + t.getID() + ".tmp");
                serializer.Serialize(fileStream, t);
                fileStream.Close();
            }
            foreach (Intervention i in savedInterventions)
            {
                fileStream = File.Create(outputDirectory + "Intervention" + i.getID() + ".tmp");
                serializer.Serialize(fileStream, i);
                fileStream.Close();
            }
        }

        public void CleanUp()
        {
            if(Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }
        }

        public bool Recoverable()
        {
            return Directory.Exists(outputDirectory);
        }

        public Operation RecoverOperation()
        {
            Operation recovered = null;
            if (Recoverable())
            {
                String[] filename = Directory.GetFiles(outputDirectory, "Operation*.tmp");
                if(filename.Length > 0)
                {
                    fileStream = File.OpenRead(filename[0]);
                    recovered = (Operation)serializer.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            //TODO: Recovery Code
            return recovered;
        }

        public List<Team> RecoverTeams()
        {
            List<Team> recovered = new List<Team>();
            //TODO: Recovery Code
            return recovered;
        }

        public bool AddTeam(Team team)
        {
            if (!savedTeams.Contains(team))
            {
                savedTeams.Add(team);
                return true;
            }
            return false;
        }

        public bool RemoveTeam(Team team)
        {
            if (savedTeams.Contains(team))
            {
                savedTeams.Remove(team);
                return true;
            }
            return false;
        }

        public bool AddIntervention(Intervention intervention)
        {
            if (!savedInterventions.Contains(intervention))
            {
                savedInterventions.Add(intervention);
                return true;
            }
            return false;
        }

        public bool RemoveIntervention(Intervention intervention)
        {
            if (savedInterventions.Contains(intervention))
            {
                savedInterventions.Remove(intervention);
                return true;
            }
            return false;
        }

        public bool SetOperation(Operation operation)
        {
            this.savedOperation = operation;
            return true;
        }
        
    }
}
