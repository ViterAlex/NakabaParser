using System;
using System.Windows.Forms;
using SiteParser.Interfaces;
using SiteParser.UserControls;

//TODO: Удалять старые объявления при смене адреса
//TODO: Разобраться с окончанием загрузки

namespace SiteParser
{
    public partial class MainForm : Form
    {
        private readonly PageLoader _pageLoader;
        private AnnonceLoadStateEnum _annonceLoadState;
        private NakabaParser _nakabaParser;
        //private int _annoncesLoaded;
        //private int _totalAnnoces;
        //private int _currPageNum;
        //private int _totalPages;

        public MainForm()
        {
            InitializeComponent();
            AnnonceLoadState = AnnonceLoadStateEnum.NotLoaded;
            _pageLoader = new PageLoader();
            SetControlsState(AnnonceLoadState);
        }

        private AnnonceLoadStateEnum AnnonceLoadState
        {
            get { return _annonceLoadState; }
            set
            {
                _annonceLoadState = value;
                SetControlsState(_annonceLoadState);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            WordExporter.ExportAnnonces(_nakabaParser.Annonces);
        }

        private void getAnnoncesButton_Click(object sender, EventArgs e)
        {
            _nakabaParser = new NakabaParser();
            SetEvents();
            _pageLoader.LoadPage(_nakabaParser, urlTextBox.Text);
            flowLayoutPanel1.Controls.Clear();
            messageStatusLabel.Text = "Загрузка страницы...";
            AnnonceLoadState = AnnonceLoadStateEnum.Loading;
        }

        /// <summary>
        ///     Устанавливает состояние элементов управления
        /// </summary>
        /// <param name="state">Состояние программы.</param>
        private void SetControlsState(AnnonceLoadStateEnum state)
        {
            getAnnoncesButton.Enabled = state == AnnonceLoadStateEnum.NotLoaded || state == AnnonceLoadStateEnum.Loaded;
            exportButton.Enabled = state == AnnonceLoadStateEnum.Loaded;
            statusStrip1.Visible = state == AnnonceLoadStateEnum.Loading;
        }

        private void SetEvents()
        {
            //Добавление объявления в контейнер FlowLayoutPanel
            Action<IAnnonceContent> addAnnonce = content =>
            {
                flowLayoutPanel1.Controls.Add(new AnnoncePresenter(content));
                AnnonceLoadState = AnnonceLoadStateEnum.Loading;
            };
            //Прогресс
            Action<string, int, int> setProgress = (m, v, max) =>
            {
                ProgressBar1.Maximum = _nakabaParser.TotalAnnonces;
                ProgressBar1.Value = _nakabaParser.AnnoncesParced;
                messageStatusLabel.Text = $"Страница {_pageLoader.CurrentPage} из {_pageLoader.TotalPages}. Загружено объявлений {_nakabaParser.AnnoncesParced} из {_nakabaParser.TotalAnnonces}";
            };

            Action<AnnonceLoadStateEnum, PageLoadedEventArgs> setState = (s, args) =>
            {
                AnnonceLoadState = s;
            };

            _nakabaParser.AnnonceParsed += (s, args) =>
            {
                this.InvokeEx(addAnnonce, args.Content);
            };

            _nakabaParser.ParcingProgressChanged += (o, args) =>
            {
                this.InvokeEx(setProgress, args.Message, (int)args.Parced, (int)args.TotalToParce);
            };
            _nakabaParser.ParsingEnded += (sender, args) =>
            {
                this.InvokeEx(new Action(() =>
                {
                    AnnonceLoadState = AnnonceLoadStateEnum.Loaded;
                }), null);
            };
            _pageLoader.PageLoaded += (sender, args) =>
            {
                this.InvokeEx(setState, AnnonceLoadStateEnum.Loading, args);
            };
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            getAnnoncesButton.Enabled = urlTextBox.Text.Length > 0;
        }

        [Flags]
        private enum AnnonceLoadStateEnum
        {
            Loading,
            NotLoaded,
            Loaded
        }


    }
    public static class ControlHelper
    {
        public static void InvokeEx(this Control control, Delegate method, params object[] param)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(method, param);
            }
            else
            {
                method.DynamicInvoke(param);
            }
        }
    }
}