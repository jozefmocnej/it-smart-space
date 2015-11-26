using System;
using System.Timers;

namespace SmartSpaceWeb
{
    public static class DatabasePolling
    {
        private static System.Timers.Timer aTimer;

        public static void RegisterPolling()
        {

            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 5 seconds (5000 milliseconds).
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            // If the timer is declared in a long-running method, use 
            // KeepAlive to prevent garbage collection from occurring 
            // before the method ends. 
            GC.KeepAlive(aTimer);

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}