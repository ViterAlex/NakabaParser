using System.Drawing;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class Annonce : IAnnonceContent
    {
        public Image Image { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
