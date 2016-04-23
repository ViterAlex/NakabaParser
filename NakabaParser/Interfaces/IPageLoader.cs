using System;

namespace SiteParser.Interfaces
{
    public interface IPageLoader
    {
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        IAnnonceParser Parser { get; set; }
        event EventHandler<PageLoadedEventArgs> PageLoaded;
        void LoadPage(IAnnonceParser parser, string pageUrl);
    }
}