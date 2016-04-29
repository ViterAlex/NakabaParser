using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using SiteParser.Annotations;
using SiteParser.Interfaces;

namespace SiteParser.UserControls
{
    public partial class AnnoncePresenter : UserControl, INotifyPropertyChanged
    {
        private static int Number { get; set; }
        public AnnoncePresenter()
        {
            InitializeComponent();
            label1.DataBindings.Add(new Binding("Text", this, "Id", true, DataSourceUpdateMode.OnPropertyChanged));
            pictureBox1.DataBindings.Add("Image", this, "Image", true, DataSourceUpdateMode.OnPropertyChanged);
            Binding binding = new Binding("Text", this, "Price", true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += (sender, args) =>
            {
                if (args.DesiredType != typeof(string))
                {
                    return;
                }
                decimal val = (decimal)args.Value;
                args.Value = val.Equals(0) ? "" : val.ToString("##.### руб.");
            };

            priceLabel.DataBindings.Add(binding);
            //priceLabel.Text = annonce.Price.Equals(0) ? "" : annonce.Price.ToString("##.### руб.");
            descriptionLabel.DataBindings.Add("Text", this, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
            titleLabel.DataBindings.Add("Text", this, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
            Id = ++Number;
        }

        public int Id { get; set; }
        private Image _image;

        public Image Image
        {
            get { return _image; }
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public AnnoncePresenter(IAnnonceContent annonce)
            : this()
        {
            Image = annonce.Image;
            Title = annonce.Title;
            Description = annonce.Description;
            Price = annonce.Price;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
