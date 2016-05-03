using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class PageLoader : IPageLoader
    {
        private int _currentPage;

        private int _totalPages;
        private PauseTokenSource _pauseTokenSource;
        private CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        ///     Событие, возникающее при загрузке страницы
        /// </summary>
        public event EventHandler<PageLoadedEventArgs> PageLoaded;

        /// <summary>
        ///     Номер текущей загружаемой страницы
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                if (_currentPage == _totalPages)
                {
                    LoadingEnded?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        ///     Общее количество страниц
        /// </summary>
        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                _totalPages = value;
                if (_currentPage == _totalPages)
                {
                    LoadingEnded?.Invoke(this, new EventArgs());
                }
            }
        }

        public bool IsBusy { get; set; }

        /// <summary>
        ///     Парсер объявлений
        /// </summary>
        public IAnnonceParser Parser { get; set; }
        /// <summary>
        /// Отмена загрузки
        /// </summary>
        public void Cancel()
        {
            _cancellationTokenSource?.Cancel(true);
        }

        /// <summary>
        ///     Метод осуществляет загрузку страниц
        /// </summary>
        /// <param name="parser">Парсер используемый для парсинга объявлений</param>
        /// <param name="pageUrl">Адрес страницы</param>
        public void LoadAnnoncesOnPage(IAnnonceParser parser, string pageUrl)
        {
            Parser = parser;
            LoadAnnoncesOnPage(pageUrl);
        }
        /// <summary>
        /// Пауза загрузки
        /// </summary>
        public void Pause()
        {
            _pauseTokenSource.IsPaused = !_pauseTokenSource.IsPaused;
        }

        /// <summary>
        ///     Событие, возникающее при окончании загрузки страниц
        /// </summary>
        public event EventHandler LoadingEnded;


        /// <summary>
        ///     Найденное количество объявлений
        /// </summary>
        /// <param name="pageHtml">Код страницы</param>
        private int GetAnnonceCount(string pageHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);
            var result = doc.DocumentNode.SelectSingleNode(".//div[@class='add-find']").InnerText;
            return int.Parse(result.Substring(result.LastIndexOf(':') + 1));
        }

        /// <summary>
        ///     Номер текущей страницы выдачи
        /// </summary>
        /// <param name="pageHtml">Код страницы</param>
        private int GetCurrentPage(string pageHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);
            var result =
                doc.DocumentNode.SelectNodes(".//div[@class='objav-pages']/a[@class='current']")
                   .Select(n => n.InnerText)
                   .Min();
            return int.Parse(result);
        }

        /// <summary>
        ///     Получение адреса следующей страницы
        /// </summary>
        /// <param name="pageUrl">Адрес текущей страницы</param>
        /// <returns>Адрес для загрузки следующей страницы</returns>
        /// <remarks>
        ///     Функция проверяет в строке наличие токена "page=НомерСтраницы".
        ///     Если такой токен найден, то определяется номер страницы и вычисляется следующий.
        ///     Если не найден, то страница считается первой.
        /// </remarks>
        private static string GetNextPageUrl(string pageUrl)
        {
            const string pageToken = "page=";
            //Если в адресе нет токена текущей страницы
            if (pageUrl.IndexOf($"&{pageToken}", StringComparison.InvariantCultureIgnoreCase) == -1)
            {
                //то возвращаем адрес на вторую страницу
                return $"{pageUrl}&{pageToken}2";
            }
            //Иначе выполняем поиск токена и замену номера страницы
            string[] tokens = pageUrl.Split('&');
            return tokens.Aggregate((first, second) =>
            {
                int pageNum;
                //if (first.StartsWith(pageToken))
                //{
                //    pageNum = int.Parse(first.Substring(pageToken.Length));
                //    return $"{pageToken}{pageNum + 1}&{second}";
                //}
                if (second.StartsWith(pageToken))
                {
                    pageNum = int.Parse(second.Substring(pageToken.Length));
                    return $"{first}&{pageToken}{pageNum + 1}";
                }
                return $"{first}&{second}";
            });
        }

        /// <summary>
        ///     Количество страниц выдачи поиска
        /// </summary>
        private int GetPagesCount(string pageHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);
            var result = doc.DocumentNode.SelectNodes(".//div[@class='objav-pages']/a").Select(n =>
            {
                int num;
                int.TryParse(n.InnerText, NumberStyles.Integer, CultureInfo.InvariantCulture, out num);
                return num.ToString(CultureInfo.InvariantCulture);
            }).Max(s => int.Parse(s, CultureInfo.InvariantCulture));
            return result;
        }

        private async void LoadAnnoncesOnPage(string pageUrl)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var pageHtml = await client.DownloadStringTaskAsync(pageUrl);
                OnPageLoaded(pageHtml, pageUrl);
                //_pauseTokenSource = new PauseTokenSource();
                //_cancellationTokenSource = new CancellationTokenSource();
                Parser.Parse(pageHtml,_pauseTokenSource, _cancellationTokenSource);
            }
        }

        /// <summary>
        ///     Метод обрабатывающий загрузку страницы
        /// </summary>
        /// <param name="pageHtml">Код загруженной страницы</param>
        /// <param name="pageUrl">Адрес страницы</param>
        private void OnPageLoaded(string pageHtml, string pageUrl)
        {
            TotalPages = GetPagesCount(pageHtml);
            CurrentPage = GetCurrentPage(pageHtml);
            var totalAnnonces = GetAnnonceCount(pageHtml);
            PageLoaded?.Invoke(this, new PageLoadedEventArgs(CurrentPage, TotalPages, totalAnnonces));
            if (CurrentPage < TotalPages)
            {
                LoadAnnoncesOnPage(GetNextPageUrl(pageUrl));
            }
        }
    }
}