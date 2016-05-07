using System;

namespace NakabaParserLib.Interfaces
{
    public interface IPageLoader
    {
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        bool IsBusy { get; set; }
        IAnnonceParser Parser { get; set; }
        event EventHandler<PageLoadedEventArgs> PageLoaded;
        void Cancel();
        void LoadAnnoncesOnPage(IAnnonceParser parser, string pageUrl);
        void Pause();
    }
}