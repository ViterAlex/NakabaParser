using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Interfaces
{
    public interface IView : INotifyPropertyChanged
    {
        /// <summary>
        /// Отображает состояние загрузчика.
        /// </summary>
        bool IsParsing { get; set; }
        /// <summary>
        /// Отображает состояние экспорта.
        /// </summary>
        bool IsExporting { get; set; }
        /// <summary>
        /// Номер обрабатываемой страницы
        /// </summary>
        int CurrentPage { get; set; }
        /// <summary>
        /// Общее количество страниц
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// Обрабатываемое объявление
        /// </summary>
        int CurrentAnnonce { get; set; }
        /// <summary>
        /// Общее количество объявлений
        /// </summary>
        int TotalAnnonces { get; set; }
        /// <summary>
        /// Прогресс выполнения задания
        /// </summary>
        int Progress { get; set; }
        string ProgressMessage { get; set; }
        /// <summary>
        /// Добавление объявления
        /// </summary>
        void AddAnnonce(IAnnonceContent content);
        /// <summary>
        /// Событие, возникающее, когда форма запрашивает объявления
        /// </summary>
        event EventHandler LoadAnnonces;
        /// <summary>
        /// Событие, когда форма запрашивает приостановку загрузки
        /// </summary>
        event EventHandler Pause;
        /// <summary>
        /// Событие, когда форма запрашивает остановку загрузки
        /// </summary>
        event EventHandler Stop;
        /// <summary>
        /// Событие, когда форма запрашивает экспорт в Word
        /// </summary>
        event EventHandler ExportToWord;
        /// <summary>
        /// Получение введённого Url страницы
        /// </summary>
        string GetUrl();
    }
}
