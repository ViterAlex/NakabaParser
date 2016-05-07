﻿using System.Drawing;

namespace NakabaParserLib.Interfaces
{
    public interface IAnnonceContent
    {
        Image Image { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
    }
}
