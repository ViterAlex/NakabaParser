using System;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class AnnonceParsedEventArgs : EventArgs
    {
        public IAnnonceContent Content { get; set; }

        public AnnonceParsedEventArgs(IAnnonceContent content)
        {
            Content = content;
        }
    }
}