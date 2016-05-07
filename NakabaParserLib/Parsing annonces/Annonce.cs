using System.Drawing;
using NakabaParserLib.Interfaces;

namespace NakabaParserLib
{
    public class Annonce : IAnnonceContent
    {
        public Image Image { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
