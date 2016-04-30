using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using SiteParser.Interfaces;

//TODO:Не работает экспорт в асинхронном режиме

namespace SiteParser
{
    public static class WordExporter
    {
        private static readonly string TemplatePath;
        private static dynamic _wdApp;
        private static dynamic _wdDocument;
        public static event EventHandler<ExportingProgressChangedEventArgs> ProgressChanged;
        static WordExporter()
        {
            TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Docs\annonceTemplate.dotm");
        }

        private static void CloseWordInstance()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Закрывание Word", Value = 1 });
            if (_wdApp != null)
            {
                _wdApp.Quit(false);
            }
        }

        private static void CreateNewDocument(string tempPath)
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Создание нового документа", Value = 0 });
            _wdDocument = _wdApp.Documents.Add(tempPath);
        }

        private static void CreateWordInstance()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Открытие Word", Value = 0 });
            _wdApp = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application"));
            _wdApp.Visible = true;
        }

        public static bool ExportAnnonce(IAnnonceContent annonce)
        {
            _wdDocument.AttachedTemplate.BuildingBlockEntries("RowTemplate")
                      .Insert(_wdDocument.Paragraphs.Last.Range, true);
            dynamic wdTable = _wdDocument.Tables[1];
            dynamic firstCellRange = _wdDocument.Tables[1].Rows.Last.Cells[1].Range;
            //Цена
            firstCellRange.Paragraphs[1].Range.Text = string.Format("{0:N2} руб.{1}", annonce.Price, Environment.NewLine).Trim(' ');
            //Заголовок
            firstCellRange.Paragraphs[2].Range.Text = string.Format("{0}{1}", annonce.Title, Environment.NewLine).Trim(' ');
            //Описание
            firstCellRange.Paragraphs[3].Range.Text = string.Format("{0}{1}", annonce.Description, Environment.NewLine).Trim();
            //Изображение
            if (annonce.Image == null)
            {
                wdTable.Rows.Last.Cells[2].Range.Text = "Нет изображения";
                return true;
            }
            wdTable.Rows.Last.Cells[2].Range.Text = "";
            var imgFileName = Path.GetTempFileName() + ".jpg";
            using (var bmp = new Bitmap(annonce.Image))
            {
                bmp.Save(imgFileName, ImageFormat.Jpeg);
            }
            wdTable.Rows.Last.Cells[2].Range.InlineShapes.AddPicture(imgFileName);
            File.Delete(imgFileName);
            return true;
        }

        public static bool ExportAnnonces(IEnumerable<IAnnonceContent> annonces)
        {
            var result = true;
            try
            {
                CreateWordInstance();
                CreateNewDocument(TemplatePath);
                IList<IAnnonceContent> annonceContents = annonces as IList<IAnnonceContent> ?? annonces.ToList();
                foreach (IAnnonceContent annonce in annonceContents)
                {
                    OnProgressChanged(new ExportingProgressChangedEventArgs
                    {
                        State = "Экспорт объявлений...",
                        Value = annonceContents.IndexOf(annonce)*100 / annonceContents.Count
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

        private static void CleanCells()
        {
            OnProgressChanged(new ExportingProgressChangedEventArgs { State = "Открытие Word", Value = 1 });
            //Удаление пустых абзацев в конце ячейки
            foreach (dynamic cell in _wdDocument.Tables[1].Columns[1].Cells)
            {
                string text = cell.Range.Text;
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