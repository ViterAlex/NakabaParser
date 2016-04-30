using System;

namespace SiteParser
{
    public class ExportingProgressChangedEventArgs : EventArgs
    {
        public int Value { get; set; }
        public string State { get; set; }
    }
}