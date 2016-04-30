using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteParser.Interfaces;

namespace SiteParser
{
    public class Presenter
    {
        private IView _view;
        private IPageLoader _pageLoader;
        private IAnnonceParser _annonceParser;

        public Presenter(IView view)
        {
            _view = view;
            _view.ExportToWord += _view_ExportToWord;
            _view.LoadAnnonces += _view_LoadAnnonces;
            _view.Pause += _view_Pause;
            _view.Stop += _view_Stop;
            _pageLoader = new PageLoader();
            _pageLoader.PageLoaded += _pageLoader_PageLoaded;
            _annonceParser = new NakabaParser();
            _annonceParser.AnnonceParsed += _annonceParser_AnnonceParsed;
            _annonceParser.ParsingEnded += _annonceParser_ParsingEnded;
        }

        private void _annonceParser_ParsingEnded(object sender, EventArgs e)
        {
            _view.IsParsing = false;
        }

        private void _annonceParser_AnnonceParsed(object sender, AnnonceParsedEventArgs e)
        {
            _view.AddAnnonce(e.Content);
            _view.TotalAnnonces = e.TotalAnnonces;
            _view.CurrentAnnonce = e.Number;
            _view.Progress = e.Number * 100 / e.TotalAnnonces;
        }

        private void _pageLoader_PageLoaded(object sender, PageLoadedEventArgs e)
        {
            _view.CurrentPage = e.CurrentPageNumber;
            _view.TotalPages = e.TotalPages;
            _view.TotalAnnonces = e.TotalAnnonces;
        }

        private void _view_Stop(object sender, EventArgs e)
        {
            _pageLoader.Cancel();
        }

        private void _view_Pause(object sender, EventArgs e)
        {
            _pageLoader.Pause();
        }

        private void _view_LoadAnnonces(object sender, EventArgs e)
        {
            _view.IsParsing = true;
            _pageLoader.LoadPage(_annonceParser, _view.GetUrl());
        }

        private async void _view_ExportToWord(object sender, EventArgs e)
        {
            _view.IsExporting = true;
            WordExporter.ProgressChanged += (o, args) =>
            {
                _view.ProgressMessage = args.State;
                _view.Progress = args.Value;
            };
            await Task.Factory.StartNew(() => WordExporter.ExportAnnonces(_annonceParser.Annonces));
            _view.IsExporting = false;
        }
    }
}
