using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class NakabaParser : IAnnonceParser
    {
        private List<IAnnonceContent> _annonces;

        private int _annoncesParced;
        private HtmlDocument _document;
        private Semaphore _semaphore;
        private HtmlNodeCollection AnnonceNodes { get; set; }
        private bool ParcingFinished { get; set; }
        private Semaphore Semaphore => _semaphore ?? (_semaphore = new Semaphore(1, 1));
        private PauseToken _pauseToken;
        /// <summary>
        ///     Событие, возникающее по окончании парсинга одного сообщения
        /// </summary>
        public event EventHandler<AnnonceParsedEventArgs> AnnonceParsed;

        /// <summary>
        ///     Событие окончания парсинга объявлений
        /// </summary>
        public event EventHandler ParsingEnded;

        /// <summary>
        ///     Событие, возникающее при очистке списка объявлений
        /// </summary>
        public event EventHandler Cleared;

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
                ParcingFinished = _annoncesParced != TotalAnnonces;
            }
        }

        public int TotalAnnonces { get; set; }
        public HtmlNode AnnonceNode { get; set; }

        public IEnumerable<IAnnonceContent> Annonces => _annonces ?? (_annonces = new List<IAnnonceContent>());

        public Image GetImage()
        {
            var imgurl = AnnonceNode.SelectSingleNode(".//a//img")?.GetAttributeValue("src", "");
            if (string.IsNullOrEmpty(imgurl)) return null;
            Image result;
            using (var client = new WebClient())
            {
                using (var ms = new MemoryStream(client.DownloadDataTaskAsync(imgurl).Result))
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
            return AnnonceNode.SelectSingleNode(".//p[@class='describe']").InnerHtml;
        }

        public decimal GetPrice()
        {
            var value = AnnonceNode.SelectSingleNode(".//div[@class='price']//strong")?.InnerText;

            return string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        public void Parse(string url, PauseTokenSource pauseTokenSource,CancellationTokenSource cancellationTokenSource)
        {
            _pauseToken = pauseTokenSource.Token;
            Parse(url);
        }

        public void Parse(string url)
        {
            Semaphore.WaitOne();
            _document = new HtmlDocument();
            _document.LoadHtml(url);
            AnnonceNodes = _document.DocumentNode.SelectNodes("//div[starts-with(@class,'objav-lister-item')]");
            TotalAnnonces += AnnonceNodes.Count;
            LoadAnnonces();
            Semaphore.Release();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public IAnnonceContent GetContent()
        {
            return new Annonce
            {
                Description = GetDescription(),
                Title = GetTitle(),
                Image = GetImage(),
                Price = GetPrice()
            };
        }

        public void ClearAnnonces()
        {
            _annonces.Clear();
            TotalAnnonces = 0;
            Cleared?.Invoke(this, new EventArgs());
        }

        private async void LoadAnnonces()
        {
            if (_annonces == null)
            {
                _annonces = new List<IAnnonceContent>();
            }
            foreach (HtmlNode annonceNode in AnnonceNodes)
            {
                AnnonceNode = annonceNode;
                await _pauseToken.WaitWhilePausedAsync();
                IAnnonceContent content = await Task<IAnnonceContent>.Factory.StartNew(GetContent);

                _annonces.Add(content);
                AnnonceParsed?.Invoke(null, new AnnonceParsedEventArgs(content, AnnoncesParced + 1, TotalAnnonces));
                AnnoncesParced++;
            }
        }

    }
}