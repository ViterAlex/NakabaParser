using System;

namespace SiteParser
{
    public class AnnonceParsingProgressEventArgs : EventArgs
    {
        public double Percent => Parced / TotalToParce;
        public double Parced { get; set; }
        public double TotalToParce { get; set; }
        public string Message { get; set; }
        public AnnonceParsingProgressEventArgs(double parced, double totalToParce, string message)
        {
            Parced = parced;
            TotalToParce = totalToParce;
            Message = message;
        }
    }
}