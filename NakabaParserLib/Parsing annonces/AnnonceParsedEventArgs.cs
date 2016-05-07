using System;
using NakabaParserLib.Interfaces;

namespace NakabaParserLib
{
    public class AnnonceParsedEventArgs : EventArgs
    {
        public IAnnonceContent Content { get; private set; }
        public int Number { get; private set; }
        public int TotalAnnonces { get; private set; }
        public int Progress => Number * 100 / TotalAnnonces;
        public AnnonceParsedEventArgs(IAnnonceContent content, int number, int total)
        {
            Content = content;
            Number = number;
            TotalAnnonces = total;
        }
    }
}