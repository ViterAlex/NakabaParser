using System;
using System.Collections.Generic;
using System.Threading;
using HtmlAgilityPack;
using NakabaParserLib.Interfaces;

namespace NakabaParserLib
{
    partial class NakabaParser
    {
        private List<IAnnonceContent> _annonces;
        private int _annoncesParced;
        private CancellationTokenSource _cancellationTokenSource;
        private HtmlDocument _document;
        private PauseTokenSource _pauseTokenSource;
        private Semaphore _semaphore;
        private HtmlNodeCollection AnnonceNodes { get; set; }
        private Semaphore Semaphore => _semaphore ?? ( _semaphore = new Semaphore(1, 1) );
        private PauseTokenSource PauseToken => _pauseTokenSource ?? ( _pauseTokenSource = new PauseTokenSource() );

        private CancellationTokenSource CancelToken
            => _cancellationTokenSource ?? ( _cancellationTokenSource = new CancellationTokenSource() );

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

        public int TotalAnnonces { get; set; }
        public HtmlNode AnnonceNode { get; set; }
        public IEnumerable<IAnnonceContent> Annonces => _annonces ?? ( _annonces = new List<IAnnonceContent>() );
    }
}
