using System;

namespace SiteParser.Interfaces
{
    public interface IPageLoader
    {
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        bool IsBusy { get; set; }
        IAnnonceParser Parser { get; set; }
        event EventHandler<PageLoadedEventArgs> PageLoaded;
        void Cancel();
        void LoadPage(IAnnonceParser parser, string pageUrl);
        void Pause();
    }
}