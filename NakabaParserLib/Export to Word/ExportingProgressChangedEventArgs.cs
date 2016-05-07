using System;

namespace NakabaParserLib.Export_to_Word
{
    public class ExportingProgressChangedEventArgs : EventArgs
    {
        public int Value { get; set; }
        public string State { get; set; }
    }
}