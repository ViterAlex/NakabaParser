using System.Windows.Forms;
using SiteParser.Interfaces;

namespace SiteParser.UserControls
{
    public partial class AnnoncePresenter : UserControl
    {
        public AnnoncePresenter()
        {
            InitializeComponent();
        }

        public AnnoncePresenter(IAnnonceContent annonce)
            : this()
        {
            pictureBox1.Image = annonce.Image;
            priceLabel.Text = annonce.Price.Equals(0) ? "" : annonce.Price.ToString("##.### руб.");
            descriptionLabel.Text = annonce.Description;
            titleLabel.Text = annonce.Title;
        }
    }
}
