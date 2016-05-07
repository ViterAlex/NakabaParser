using System;

namespace NakabaParserLib.Export_to_Word
{
    public class ExportEventArgs : EventArgs
    {
        public bool Append { get; set; }

        public ExportEventArgs()
            : this(false)
        {

        }

        public ExportEventArgs(bool append)
        {
            Append = append;
        }
    }
}