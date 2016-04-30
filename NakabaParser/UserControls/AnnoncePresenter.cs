using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using SiteParser.Annotations;
using SiteParser.Interfaces;

namespace SiteParser.UserControls
{
    //TODO:Добавить иконку для контрола
    [ToolboxBitmap(typeof (AnnoncePresenter), "AnnoncePresenter.bmp")]
    public partial class AnnoncePresenter : UserControl, INotifyPropertyChanged
    {
        private string _description;
        private Image _image;
        private decimal _price;
        private string _title;
        public AnnoncePresenter()
        {
            InitializeComponent();
            idLabel.DataBindings.Add(new Binding("Text", this, "Id", false, DataSourceUpdateMode.OnPropertyChanged));
            pictureBox1.DataBindings.Add("Image", this, "Image", false, DataSourceUpdateMode.OnPropertyChanged);
            var binding = new Binding("Text", this, "Price", true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += (sender, args) =>
            {
                if (args.DesiredType != typeof (string)) return;
                var val = (decimal) args.Value;
                args.Value = val.Equals(0) ? "" : val.ToString("##.### руб.");
            };

            priceLabel.DataBindings.Add(binding);
            descriptionLabel.DataBindings.Add("Text", this, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
            titleLabel.DataBindings.Add("Text", this, "Title", false, DataSourceUpdateMode.OnPropertyChanged);
            Id = ++Number;
            SetDefaultValues();
        }

        public AnnoncePresenter(IAnnonceContent annonce)
            : this()
        {
            Image = annonce.Image;
            Title = annonce.Title;
            Description = annonce.Description;
            Price = annonce.Price;
        }

        private static int Number { get; set; }

        public int Id { get; set; }

        /// <summary>
        ///     Картинка для объявления
        /// </summary>
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

        /// <summary>
        ///     Цена указанная в объявлении
        /// </summary>
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

        /// <summary>
        ///     Описание товара
        /// </summary>
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

        /// <summary>
        ///     Заголовок объявления
        /// </summary>
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetDefaultValues()
        {
            Image = SystemIcons.WinLogo.ToBitmap();
            Title = "Заголовок объявления";
            Description = "Описание товара";
            Price = 1100m;
        }
    }
}