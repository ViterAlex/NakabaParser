using System;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class AnnonceParsedEventArgs : EventArgs
    {
        public IAnnonceContent Content { get; set; }
        public int Number { get; set; }
        public int TotalAnnonces { get; set; }

        public AnnonceParsedEventArgs(IAnnonceContent content,int number,int total)
        {
            Content = content;
            Number = number;
            TotalAnnonces = total;
        }
    }
}