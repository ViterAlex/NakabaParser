namespace SiteParser
{
    public class PageLoadedEventArgs
    {
        /// <summary>
        /// Номер текущей загруженной страницы
        /// </summary>
        public int CurrentPageNumber { get; set; }
        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Общее количество объявлений в выдаче
        /// </summary>
        public int TotalAnnonces { get; set; }

        public PageLoadedEventArgs(int currentPageNumber, int totalPages, int totalAnnonces)
        {
            CurrentPageNumber = currentPageNumber;
            TotalPages = totalPages;
            TotalAnnonces = totalAnnonces;
        }
    }
}