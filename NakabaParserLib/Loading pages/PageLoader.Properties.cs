using System;
using System.Net;
using System.Threading;
using NakabaParserLib.Interfaces;

namespace NakabaParserLib
{
    partial class PageLoader
    {
        private CancellationTokenSource _cancellationTokenSource;
        private int _currentPage;
        private PauseTokenSource _pauseTokenSource;
        private int _totalPages;

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

        public bool IsBusy { get; set; }

        /// <summary>
        ///     Парсер объявлений
        /// </summary>
        public IAnnonceParser Parser { get; set; }

        private WebClient _webClient;
        /// <summary>
        /// Клиент, выполняющий загрузку страницы
        /// </summary>
        private WebClient WebClient
        {
            get { return _webClient ?? (_webClient = new WebClient()); }
            set { _webClient = value; }
        }
    }
}