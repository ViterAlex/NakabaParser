using System;
using System.ComponentModel;
using NakabaParserLib.Export_to_Word;

namespace NakabaParserLib.Interfaces
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
        event EventHandler<ExportEventArgs> ExportToWord;
        /// <summary>
        /// Получение введённого Url страницы
        /// </summary>
        string GetUrl();
        /// <summary>
        /// Окончание загрузки
        /// </summary>
        void Finish();
    }
}
