using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using SiteParser.Interfaces;

//TODO:Не работает экспорт в асинхронном режиме

namespace SiteParser
{
    public static class WordExporter
    {
        private static readonly string TemplatePath;
        private static dynamic _wdApp;
        private static dynamic _wdDocument;

        static WordExporter()
        {
            TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Docs\annonceTemplate.dotm");
        }

        private static void CloseWordInstance()
        {
            _wdApp.Quit(false);
        }

        private static void CreateNewDocument(string tempPath)
        {
            _wdDocument = _wdApp.Documents.Add(tempPath);
        }

        private static void CreateWordInstance()
        {
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
            firstCellRange.Paragraphs[1].Range.Text = string.Format("{0:N2} руб.{1}", annonce.Price, Environment.NewLine);
            //Заголовок
            firstCellRange.Paragraphs[2].Range.Text = string.Format("{0}{1}", annonce.Title, Environment.NewLine);
            //Описание
            firstCellRange.Paragraphs[3].Range.Text = string.Format("{0}{1}", annonce.Description, Environment.NewLine);
            //Изображение
            if (annonce.Image == null)
            {
                wdTable.Rows.Last.Cells[2].Range.Text = "";
                return true;
            }
            var imgFileName = Path.GetTempFileName() + ".jpg";
            using (var bmp = new Bitmap(annonce.Image))
            {
                bmp.Save(imgFileName, ImageFormat.Jpeg);
            }
            wdTable.Rows.Last.Cells[2].Range.InlineShapes.AddPicture(imgFileName);
            File.Delete(imgFileName);
            return true;
        }

        public static void ExportAnnonces(IEnumerable<IAnnonceContent> annonces)
        {
            CreateWordInstance();
            CreateNewDocument(TemplatePath);
            foreach (IAnnonceContent annonce in annonces)
            {
                //Task t = new Task(() => ExportAnnonce(annonce));
                //await t;
                //if (t.Exception != null)
                //{
                //    break;
                //}
                ExportAnnonce(annonce);
            }
            SaveDocument();
            CloseWordInstance();
        }

        private static void SaveDocument()
        {
            float appVersion = float.Parse(_wdApp.Version.ToString(), CultureInfo.InvariantCulture);
            var docFullPath =
                $"{Path.GetDirectoryName(TemplatePath)}{Path.DirectorySeparatorChar}{DateTime.Now.ToLongDateString()}.docx";
            if (appVersion < 14)
            {
                _wdDocument.SaveAs(docFullPath);
            }
            else
            {
                _wdDocument.SaveAs2(docFullPath);
            }
        }
    }
}