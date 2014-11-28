using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ETD.Models
{
    public class Timer
    {
        public static Dictionary<int, Stopwatch> timers = new Dictionary<int, Stopwatch>();
        public static Dictionary<int, TimeSpan> interventionTime = new Dictionary<int, TimeSpan>();
        public static int currentIntervention = 0;
        public static int currentTeam = 0; //Used in CreateTeamForm as index

        public static void StartTimer()
        {
            Stopwatch interventionTimer = new Stopwatch();
            interventionTimer.Start();

            timers.Add(currentIntervention, interventionTimer);

            currentIntervention += 1;
        }

        public static void SetTimer(int interventionNumber, Stopwatch timer)
        {
            if (timers.ContainsKey(interventionNumber))
            {
                timers.Add(interventionNumber, timer);
            }
            else
            {
                timers.Add(currentIntervention, timer);
                currentIntervention += 1;
            }
        }

        public static void RemoveTimer(int interventionNumber)
        {
            if (timers.ContainsKey(interventionNumber))
            {
                timers.Remove(interventionNumber);
            }
        }

		//POC - TO DELETE
        public static void TestTimer(ETD.ViewsPresenters.MainWindow caller)
        {
            //This Method currently returns the timer of the last queue'd timer.
            //Once the intervention creation interface is completed, it will be a trivial
            //matter to select which intervention from the dictionary you would like to retrieve the time from.
            //It is important to note, that in this current function, the timer keeps going even after you poll
            //it for its value. I left this in intentionally to prove that my function for retrieving didn't break
            //the stopwatch's functionality. This could be extremely useful if we wanted to keep a running display of
            //the time taken for each portion of each intervention.
            if (timers.ContainsKey(currentIntervention - 1))
            {
                TimeSpan elapsed = timers[currentIntervention - 1].Elapsed;
                System.Windows.Controls.TextBlock TimerText = caller.getTimer();
                TimerText.Text = elapsed.TotalSeconds.ToString();                               //Converts the total time on the stopwatch into an amount of seconds.

                interventionTime.Add(currentIntervention - 1, elapsed);     //Saves it to a second dictionary, this one only stores the elapsed times, this will be pushed to DB.
            }
        }

    }
}
