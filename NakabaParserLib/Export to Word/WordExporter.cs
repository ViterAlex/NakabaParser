using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using NakabaParserLib.Interfaces;

namespace NakabaParserLib.Export_to_Word
{
    public static class WordExporter
    {
        private static readonly string TemplatePath;
        private static dynamic _wdApp;
        private static dynamic _wdDocument;
        public static event EventHandler<ExportingProgressChangedEventArgs> ProgressChanged;
        /// <summary>
        /// Инициализация статического класса.
        /// </summary>
        static WordExporter()
        {
            TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Docs\annonceTemplate.dotm");
        }

        /// <summary>
        /// Закрытие приложения Word.
        /// </summary>
        private static void CloseWordInstance()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Закрывание Word", Value = 1 });
            if (_wdApp != null)
            {
                _wdApp.Quit(false);
            }
        }
        /// <summary>
        /// Создание нового документа на основе заданного шаблона.
        /// </summary>
        /// <param name="tempPath">Путь к шаблону документа</param>
        private static void CreateNewDocument(string tempPath)
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Создание нового документа", Value = 0 });
            _wdDocument = _wdApp.Documents.Add(tempPath);
        }
        /// <summary>
        /// Создание экземпляра приложения Word.
        /// </summary>
        private static void CreateWordInstance()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Открытие Word", Value = 0 });
            _wdApp = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application"));
            _wdApp.Visible = true;
        }
        /// <summary>
        /// Экспорт одного объявления в документ.
        /// </summary>
        /// <param name="annonce">Содержимое объявления</param>
        private static bool ExportAnnonce(IAnnonceContent annonce)
        {
            _wdDocument.AttachedTemplate.BuildingBlockEntries("RowTemplate")
                      .Insert(_wdDocument.Paragraphs.Last.Range, true);
            dynamic wdTable = _wdDocument.Tables[1];
            dynamic firstCellRange = _wdDocument.Tables[1].Rows.Last.Cells[1].Range;
            //Цена {PRICE}
            firstCellRange.Paragraphs[1].Range.Text = string.Format("{0:N2} руб.{1}", annonce.Price, Environment.NewLine).Trim(' ');
            //Заголовок {TITLE}
            firstCellRange.Paragraphs[2].Range.Text = string.Format("{0}{1}", annonce.Title, Environment.NewLine).Trim(' ');
            //Описание {DESCRIPTION}
            firstCellRange.Paragraphs[3].Range.Text = string.Format("{0}{1}", annonce.Description, Environment.NewLine).Trim();
            //Изображение {IMAGE}
            if (annonce.Image == null)
            {
                wdTable.Rows.Last.Cells[2].Range.Text = "Нет изображения";
                return true;
            }
            //Очистка ячейки с полем {IMAGE}
            wdTable.Rows.Last.Cells[2].Range.Text = "";
            //Путь ко временному файлу для сохранения в него изображения
            var imgFileName = Path.GetTempFileName() + ".jpg";
            //Сохраняем изображение во временный файл
            using (var bmp = new Bitmap(annonce.Image))
            {
                bmp.Save(imgFileName, ImageFormat.Jpeg);
            }
            //Вставка изображения в документ
            wdTable.Rows.Last.Cells[2].Range.InlineShapes.AddPicture(imgFileName);
            //Удаление временного файла.
            File.Delete(imgFileName);

            return true;
        }
        /// <summary>
        /// Экспорт всех объявлений из коллекции
        /// </summary>
        /// <param name="annonces">Коллекция с объявлениями</param>
        /// <param name="append">Дописывать ли документ?</param>
        public static bool ExportAnnonces(IEnumerable<IAnnonceContent> annonces, bool append)
        {
            var result = true;
            try
            {
                CreateWordInstance();
                if (append)
                {
                    OpenDocument();
                }
                else
                {
                    CreateNewDocument(TemplatePath);
                }
                IList<IAnnonceContent> annonceContents = annonces as IList<IAnnonceContent> ?? annonces.ToList();
                foreach (IAnnonceContent annonce in annonceContents)
                {
                    OnProgressChanged(new ExportingProgressChangedEventArgs
                    {
                        State = "Экспорт объявлений...",
                        Value = annonceContents.IndexOf(annonce) * 100 / annonceContents.Count
                    });
                    ExportAnnonce(annonce);
                }
                CleanCells();
                SaveDocument();
            }
            catch (Exception)
            {
                result = false;
                Debug.WriteLine("Ошибка при экспорте в Word");
            }
            finally
            {
                SaveDocument();
                CloseWordInstance();
            }
            return result;
        }

        private static void OpenDocument()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Открытие документа", Value = 0 });
            var docFullPath =
                $"{Path.GetDirectoryName(TemplatePath)}{Path.DirectorySeparatorChar}market.nakaba.docx";
            _wdDocument = _wdApp.Documents.Open(docFullPath, true, false, false);
        }
        /// <summary>
        /// Очистка ячеек таблицы от артефактов.
        /// </summary>
        private static void CleanCells()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Открытие Word", Value = 1 });
            //Удаление пустых абзацев в конце ячейки
            foreach (dynamic cell in _wdDocument.Tables[1].Columns[1].Cells)
            {
                string text = cell.Range.Text;
                //Удаление пустых абзацев в конце ячейки
                while (text.Substring(text.Length - 3) == "\r\r\a")
                {
                    cell.Range.Characters.Last.Previous.Delete();
                    if (text.Length == cell.Range.Text.Length)
                    {
                        break;
                    }
                    text = cell.Range.Text;
                }
            }
        }
        /// <summary>
        /// Сохранение документа
        /// </summary>
        private static void SaveDocument()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Сохранение документа", Value = 1 });
            float appVersion = float.Parse(_wdApp.Version.ToString(), CultureInfo.InvariantCulture);
            var docFullPath =
                $"{Path.GetDirectoryName(TemplatePath)}{Path.DirectorySeparatorChar}market.nakaba.docx";
            if (appVersion < 14)
            {
                _wdDocument.SaveAs(docFullPath);
            }
            else
            {
                _wdDocument.SaveAs2(docFullPath);
            }
        }

        private static void OnProgressChanged(ExportingProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(null, e);
        }
    }
}