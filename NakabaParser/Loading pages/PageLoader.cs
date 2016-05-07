using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using SiteParser.Interfaces;

namespace SiteParser
{
    public partial class PageLoader : IPageLoader
    {
        public PageLoader()
        {
            LoadingEnded += (sender, args) => { WebClient.Dispose(); };
        }

        /// <summary>
        ///     Событие, возникающее при загрузке страницы
        /// </summary>
        public event EventHandler<PageLoadedEventArgs> PageLoaded;

        /// <summary>
        ///     Отмена загрузки
        /// </summary>
        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            Parser.Stop();
            LoadingEnded?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Метод осуществляет загрузку страниц
        /// </summary>
        /// <param name="parser">Парсер используемый для парсинга объявлений</param>
        /// <param name="pageUrl">Адрес страницы</param>
        public void LoadAnnoncesOnPage(IAnnonceParser parser, string pageUrl)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _pauseTokenSource = new PauseTokenSource();
            Parser = parser;
            LoadAnnoncesOnPage(pageUrl);
        }

        /// <summary>
        ///     Пауза загрузки
        /// </summary>
        public void Pause()
        {
            _pauseTokenSource.IsPaused = !_pauseTokenSource.IsPaused;
            Parser.Pause();
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

        /// <summary>
        ///     Метод вызывающий загрузку объявлений с определённой страницы
        /// </summary>
        /// <param name="pageUrl">Адрес страницы, с которой нужно загрузить объявления</param>
        private async void LoadAnnoncesOnPage(string pageUrl)
        {
            try
            {
                WebClient.Encoding = Encoding.UTF8;
                //Исключение, если было запрошено прерывание загрузки
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                await _pauseTokenSource.WaitWhilePausedAsync();
                var pageHtml = await WebClient.DownloadStringTaskAsync(pageUrl);
                OnPageLoaded(pageHtml, pageUrl);
                Parser.Parse(pageHtml);
            }
            catch (OperationCanceledException)
            {
            }
        }

        /// <summary>
        ///     Метод обрабатывающий загрузку страницы
        /// </summary>
        /// <param name="pageHtml">Код загруженной страницы</param>
        /// <param name="pageUrl">Адрес страницы</param>
        private void OnPageLoaded(string pageHtml, string pageUrl)
        {
            try
            {
                // исключение, если было запрошено прерывание загрузки
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                TotalPages = GetPagesCount(pageHtml);
                CurrentPage = GetCurrentPage(pageHtml);
                var totalAnnonces = GetAnnonceCount(pageHtml);
                PageLoaded?.Invoke(this, new PageLoadedEventArgs(CurrentPage, TotalPages, totalAnnonces));
                if (CurrentPage < TotalPages)
                {
                    LoadAnnoncesOnPage(GetNextPageUrl(pageUrl));
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}