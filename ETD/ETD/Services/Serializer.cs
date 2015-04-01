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
using ETD.Services.Database;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD.Services
{
    class Serializer : Observer
    {
        //Singleton variable
        private static Serializer instance;

        //Settings variables
        private const string outputDirectory = ".\\Temp\\";
        private const int backupRate = 15000; //interval of backups, in milliseconds

        //Objects to backup
        private List<Team> teams;
        private List<Intervention> activeInterventions;
        private List<Intervention> completedInterventions;
        private List<Equipment> equipments;

        //Objects needed for serialization
        private Timer timer;
        private FileStream fileStream;
        private BinaryFormatter serializer;
        

        private Serializer()
        {
            teams = new List<Team>();
            Observable.RegisterClassObserver(typeof(Team), this);

            activeInterventions = new List<Intervention>();
            completedInterventions = new List<Intervention>();
            Observable.RegisterClassObserver(typeof(Intervention), this);

            equipments = new List<Equipment>();
            Observable.RegisterClassObserver(typeof(Equipment), this);

            serializer = new BinaryFormatter();
            timer = new Timer(backupRate);
            timer.Elapsed += new ElapsedEventHandler(BackUpEvent);
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
            CleanUp();
            
            if(!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            BackUpOperation();
            BackUpTeams();
            BackUpActiveInterventions();
            BackUpCompletedInterventions();
            BackUpEquipments();
            BackUpRequests();
            BackUpPinPositions();

            StartBackUp();
        }
        private void BackUpOperation()
        {
            Operation operation = Operation.currentOperation;
            if (operation != null && !File.Exists(outputDirectory + "Operation" + operation.getID()))
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "Operation" + operation.getID() + ".tmp");
                    serializer.Serialize(fileStream, operation);
                    fileStream.Close();
                }
                catch(Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpTeams()
        {
            foreach (Team t in teams)
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "Team" + t.getID() + ".tmp");
                    serializer.Serialize(fileStream, t);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpActiveInterventions()
        {
            foreach (Intervention i in activeInterventions)
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "ActiveIntervention" + i.getID() + ".tmp");
                    serializer.Serialize(fileStream, i);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpCompletedInterventions()
        {
            foreach (Intervention i in completedInterventions)
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "CompletedIntervention" + i.getID() + ".tmp");
                    serializer.Serialize(fileStream, i);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpEquipments()
        {
            foreach (Equipment e in equipments)
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "Equipment" + e.getID() + ".tmp");
                    serializer.Serialize(fileStream, e);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpRequests()
        {
            List<Request> requests = new List<Request>(Request.getRequestList());
            int count = 1;
            foreach (Request r in requests)
            {
                try
                {
                    fileStream = File.Create(outputDirectory + "Request" + count++ + ".tmp");
                    serializer.Serialize(fileStream, r);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    StreamWriter log = new StreamWriter(@"..\log\error.log");
                    log.WriteLine("An Exception was caught.");
                    log.WriteLine("\nException Message:\n{0}", ex.Message);
                    log.WriteLine("\nException Source:\n{0}", ex.Source);
                    log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                    log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                }
            }
        }
        private void BackUpPinPositions()
        {
            try
            {
                List<Pin> pins = new List<Pin>(Pin.getPinList());
                StreamWriter fileStream = new StreamWriter(outputDirectory + "PinsInfo.tmp");
                foreach (Pin p in pins)
                {
                    fileStream.Write(p.GetType() + ";");
                    if (p.GetType().Equals(typeof(TeamPin)))
                    {
                        fileStream.Write(((TeamPin)p).getTeam().getID() + ";");
                    }
                    else if (p.GetType().Equals(typeof(InterventionPin)))
                    {
                        fileStream.Write(((InterventionPin)p).getIntervention().getID() + ";");
                    }
                    else if (p.GetType().Equals(typeof(EquipmentPin)))
                    {
                        fileStream.Write(((EquipmentPin)p).getEquipment().getID() + ";");
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        fileStream.Write(p.getX() + ";");
                        fileStream.WriteLine(p.getY());
                    }));

                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                StreamWriter log = new StreamWriter(@"..\log\error.log");
                log.WriteLine("An Exception was caught.");
                log.WriteLine("\nException Message:\n{0}", ex.Message);
                log.WriteLine("\nException Source:\n{0}", ex.Source);
                log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
            }
        }
        
        public void CleanUp()
        {
            StopBackUp();
            if(Directory.Exists(outputDirectory))
            {
                try
                {
                    Directory.Delete(outputDirectory, true);
                }
                catch(Exception e)
                {
                    Console.Write(e.Message);
                }
            }
        }

        public bool Recoverable()
        {
            return Directory.Exists(outputDirectory);
        }

        public void PerformRecovery()
        {
            RecoverOperation();
            RecoverTeams();
            RecoverActiveInterventions();
            RecoverCompletedInterventions();
            RecoverEquipments();
            RecoverRequests();
            RecoverPinPositions();
        }

        private void RecoverOperation()
        {
            if (Recoverable())
            {
                String[] filename = Directory.GetFiles(outputDirectory, "Operation*.tmp");
                if(filename.Length > 0)
                {
                    try
                    {
                        fileStream = File.OpenRead(filename[0]);
                        Operation.currentOperation = (Operation)serializer.Deserialize(fileStream);
                        fileStream.Close();
                    }
                    catch (Exception ex)
                    {
                        StreamWriter log = new StreamWriter(@"..\log\error.log");
                        log.WriteLine("An Exception was caught.");
                        log.WriteLine("\nException Message:\n{0}", ex.Message);
                        log.WriteLine("\nException Source:\n{0}", ex.Source);
                        log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                        log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                    }
                }
            }
        }
        private void RecoverTeams()
        {
            if (Recoverable())
            {
                String[] filenames = Directory.GetFiles(outputDirectory, "Team*.tmp");
                if (filenames.Length > 0)
                {
                    foreach (string fn in filenames)
                    {
                        try
                        {
                            fileStream = File.OpenRead(fn);
                            Team.InsertTeam((Team)serializer.Deserialize(fileStream));
                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            StreamWriter log = new StreamWriter(@"..\log\error.log");
                            log.WriteLine("An Exception was caught.");
                            log.WriteLine("\nException Message:\n{0}", ex.Message);
                            log.WriteLine("\nException Source:\n{0}", ex.Source);
                            log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                            log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                        }
                    }
                }
            }
        }
        private void RecoverActiveInterventions()
        {
            if (Recoverable())
            {
                String[] filenames = Directory.GetFiles(outputDirectory, "ActiveIntervention*.tmp");
                if (filenames.Length > 0)
                {
                    foreach (string fn in filenames)
                    {
                        try
                        {
                            fileStream = File.OpenRead(fn);
                            Intervention.AddActiveIntervention((Intervention)serializer.Deserialize(fileStream));
                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            StreamWriter log = new StreamWriter(@"..\log\error.log");
                            log.WriteLine("An Exception was caught.");
                            log.WriteLine("\nException Message:\n{0}", ex.Message);
                            log.WriteLine("\nException Source:\n{0}", ex.Source);
                            log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                            log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                        }
                    }
                }
            }
        }
        private void RecoverCompletedInterventions()
        {
            if (Recoverable())
            {
                String[] filenames = Directory.GetFiles(outputDirectory, "CompletedIntervention*.tmp");
                if (filenames.Length > 0)
                {
                    foreach (string fn in filenames)
                    {
                        try
                        {
                            fileStream = File.OpenRead(fn);
                            Intervention.AddCompletedIntervention((Intervention)serializer.Deserialize(fileStream));
                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            StreamWriter log = new StreamWriter(@"..\log\error.log");
                            log.WriteLine("An Exception was caught.");
                            log.WriteLine("\nException Message:\n{0}", ex.Message);
                            log.WriteLine("\nException Source:\n{0}", ex.Source);
                            log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                            log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                        }
                    }
                }
            }
        }
        private void RecoverEquipments()
        {
            if (Recoverable())
            {
                String[] filenames = Directory.GetFiles(outputDirectory, "Equipment*.tmp");
                if (filenames.Length > 0)
                {
                    foreach (string fn in filenames)
                    {
                        try
                        {
                            fileStream = File.OpenRead(fn);
                            Equipment.AddEquipment((Equipment)serializer.Deserialize(fileStream));
                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            StreamWriter log = new StreamWriter(@"..\log\error.log");
                            log.WriteLine("An Exception was caught.");
                            log.WriteLine("\nException Message:\n{0}", ex.Message);
                            log.WriteLine("\nException Source:\n{0}", ex.Source);
                            log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                            log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                        }
                    }
                }
            }
        }
        private void RecoverRequests()
        {
            if (Recoverable())
            {
                String[] filenames = Directory.GetFiles(outputDirectory, "Request*.tmp");
                if (filenames.Length > 0)
                {
                    foreach (string fn in filenames)
                    {
                        try
                        {
                            fileStream = File.OpenRead(fn);
                            Request.getRequestList().Add(((Request)serializer.Deserialize(fileStream)));
                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            StreamWriter log = new StreamWriter(@"..\log\error.log");
                            log.WriteLine("An Exception was caught.");
                            log.WriteLine("\nException Message:\n{0}", ex.Message);
                            log.WriteLine("\nException Source:\n{0}", ex.Source);
                            log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                            log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                        }
                    }
                }
            }
            
        }
        private void RecoverPinPositions()
        {
            if (Recoverable())
            {
                if (File.Exists(outputDirectory + "PinsInfo.tmp"))
                {
                    try
                    {
                        StreamReader reader = new StreamReader(outputDirectory + "PinsInfo.tmp");
                        string line = "";
                        string[] lineInfo;

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            lineInfo = line.Split(new char[] { ';' });

                            Pin p = Pin.MatchPin(Type.GetType(lineInfo[0]), int.Parse(lineInfo[1]));
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                p.setPinPosition(double.Parse(lineInfo[2]), double.Parse(lineInfo[3]));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        StreamWriter log = new StreamWriter(@"..\log\error.log");
                        log.WriteLine("An Exception was caught.");
                        log.WriteLine("\nException Message:\n{0}", ex.Message);
                        log.WriteLine("\nException Source:\n{0}", ex.Source);
                        log.WriteLine("\nException Target:\n{0}", ex.TargetSite);
                        log.WriteLine("\nException Stack Trace:\n{0}", ex.StackTrace);
                    }
                }
            }
        }

        public void Update()
        {
            teams = Team.getTeamList();
            activeInterventions = Intervention.getActiveInterventionList();
            completedInterventions = Intervention.getCompletedInterventionList();
            equipments = Equipment.getEquipmentList();
        }

        //Mutators

        public void StartBackUp()
        {
            timer.Start();
        }
        public void StopBackUp()
        {
            timer.Stop();
        } 
    }
}
