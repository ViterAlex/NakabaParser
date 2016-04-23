using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class NakabaParser : IAnnonceParser
    {
        private HtmlDocument _document;
        private double counter;
        /// <summary>
        /// Событие, возникающее по окончании парсинга одного сообщения
        /// </summary>
        public event EventHandler<AnnonceParsedEventArgs> AnnonceParsed;
        /// <summary>
        /// Событие окончания парсинга объявлений
        /// </summary>
        public event EventHandler ParsingEnded;
        /// <summary>
        /// Событие, возникающее, если часть сообщений или все были обработаны
        /// </summary>
        public event EventHandler<AnnonceParsingProgressEventArgs> ParcingProgressChanged;
        /// <summary>
        /// Событие, возникающее при очистке списка объявлений
        /// </summary>
        public event EventHandler Cleared;

        private HtmlNodeCollection AnnonceNodes { get; set; }

        private int _annoncesParced;

        public int AnnoncesParced
        {
            get { return _annoncesParced; }
            set
            {
                _annoncesParced = value;
                if (_annoncesParced == TotalAnnonces)
                {
                    ParsingEnded?.Invoke(this, new EventArgs());
                }
            }
        }

        private Queue<HtmlNodeCollection> _pagesQueue;

        private Queue<HtmlNodeCollection> PagesQueue => _pagesQueue ?? (_pagesQueue = new Queue<HtmlNodeCollection>());

        public int TotalAnnonces { get; set; }
        public HtmlNode AnnonceNode { get; set; }

        private List<IAnnonceContent> _annonces;

        public List<IAnnonceContent> Annonces => _annonces ?? (_annonces = new List<IAnnonceContent>());

        public async void LoadAnnonces()
        {
            counter = 0;
            while (PagesQueue.Count != 0)
            {
                foreach (HtmlNode annonceNode in PagesQueue.Dequeue())
                {
                    AnnonceNode = annonceNode;
                    IAnnonceContent content = Task<IAnnonceContent>.Factory.StartNew(GetContent).Result;
                    ParcingProgressChanged?.Invoke(this, new AnnonceParsingProgressEventArgs(++AnnoncesParced, TotalAnnonces, "Обработка сообщений"));
                    AnnonceParsed?.Invoke(null, new AnnonceParsedEventArgs(content));
                    Annonces.Add(content);
                }
            }
        }

        public Image GetImage()
        {
            var imgurl = AnnonceNode.SelectSingleNode(".//a//img")?.GetAttributeValue("src", "");
            if (string.IsNullOrEmpty(imgurl)) return null;
            Image result;
            using (var client = new WebClient())
            {
                using (var ms = new MemoryStream((client.DownloadDataTaskAsync(imgurl)).Result))
                {
                    result = Image.FromStream(ms);
                }
            }
            return result;
        }

        public string GetTitle()
        {
            return AnnonceNode.SelectSingleNode(".//h3//a").GetAttributeValue("title", "");
        }

        public string GetDescription()
        {
            return AnnonceNode.SelectSingleNode(".//p[@class='describe']").InnerText;
        }

        public decimal GetPrice()
        {
            var value = AnnonceNode.SelectSingleNode(".//div[@class='price']//strong")?.InnerText;

            return string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        public async void Parse(string url)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(url);
            AnnonceNodes = _document.DocumentNode.SelectNodes("//div[starts-with(@class,'objav-lister-item')]");
            TotalAnnonces += AnnonceNodes.Count;
            if (PagesQueue.Count == 0)
            {
                PagesQueue.Enqueue(AnnonceNodes);
                await new Task(LoadAnnonces);
                //LoadAnnonces();
            }
            else PagesQueue.Enqueue(AnnonceNodes);
        }

        private int _num;
        public IAnnonceContent GetContent()
        {
            Debug.WriteLine($"{++_num}:\t{nameof(AnnoncesParced)}:{AnnoncesParced},{nameof(TotalAnnonces)}:{TotalAnnonces}");
            return new Annonce()
            {
                Description = GetDescription(),
                Title = GetTitle(),
                Image = GetImage(),
                Price = GetPrice()
            };
        }

        public void ClearAnnonces()
        {
            Annonces.Clear();
            TotalAnnonces = 0;
            Cleared?.Invoke(this, new EventArgs());
        }
    }
}
