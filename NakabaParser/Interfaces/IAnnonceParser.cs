﻿using System;
using System.Collections.Generic;
using System.Drawing;
using HtmlAgilityPack;

namespace SiteParser.Interfaces
{
    /// <summary>
    ///     Интерфейс парсера объявлений
    /// </summary>
    public interface IAnnonceParser
    {
        int AnnoncesParced { get; set; }
        int TotalAnnonces { get; set; }
        HtmlNode AnnonceNode { get; set; }

        List<IAnnonceContent> Annonces { get; }
        event EventHandler<AnnonceParsedEventArgs> AnnonceParsed;
        event EventHandler Cleared;
        event EventHandler ParsingEnded; 
        event EventHandler<AnnonceParsingProgressEventArgs> ParcingProgressChanged;
        void ClearAnnonces();
        IAnnonceContent GetContent();

        /// <summary>
        ///     Метод для получения описания объявления
        /// </summary>
        string GetDescription();

        /// <summary>
        ///     Метод для получения изображения
        /// </summary>
        Image GetImage();

        /// <summary>
        ///     Метод для получения цены, указанной в объявлении
        /// </summary>
        decimal GetPrice();

        /// <summary>
        ///     Метод для получения заголовка объявления
        /// </summary>
        string GetTitle();

        void Parse(string url);
    }
}