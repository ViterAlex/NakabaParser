using System.Windows.Forms;
using SiteParser.Interfaces;

namespace SiteParser.UserControls
{
    public partial class AnnoncePresenter : UserControl
    {
        private static int Number { get; set; }
        public AnnoncePresenter()
        {
            InitializeComponent();
        }

        public int Id { get; set; }
        public AnnoncePresenter(IAnnonceContent annonce)
            : this()
        {
            label1.DataBindings.Add(new Binding("Text", this, "Id", true, DataSourceUpdateMode.OnPropertyChanged));
            pictureBox1.Image = annonce.Image;
            priceLabel.Text = annonce.Price.Equals(0) ? "" : annonce.Price.ToString("##.### руб.");
            descriptionLabel.Text = annonce.Description;
            titleLabel.Text = annonce.Title;
            Id = ++Number;
        }
    }
}
