using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SiteParser.Interfaces;
using SiteParser.UserControls;

//TODO: Удалять старые объявления при смене адреса
//TODO: Разобраться с окончанием загрузки
//TODO: Дописывать документ, а не создавать заново
//TODO: Возможность приостановки и отмены скачивания
//TODO: Задание количества объявлений для скачивания
//TODO: Фильтрование объявлений по критериям
//TODO: Парсинг цены из текста объявления, если цена не указана
namespace SiteParser
{
    public partial class MainForm : Form
    {
        private readonly PageLoader _pageLoader;
        private AnnonceLoadStateEnum _annonceLoadState;
        private NakabaParser _nakabaParser;

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

        private  void exportButton_Click(object sender, EventArgs e)
        {
            ExportToWord();
        }

        private async void ExportToWord()
        {
            exportButton.Enabled = false;
            await Task.Factory.StartNew(()=> WordExporter.ExportAnnonces(_nakabaParser.Annonces));
            exportButton.Enabled = true;
        }

        private void getAnnoncesButton_Click(object sender, EventArgs e)
        {
            _nakabaParser = new NakabaParser();
            SetEvents();
            flowLayoutPanel1.Controls.Clear();
            messageStatusLabel.Text = "Загрузка страницы...";
            _pageLoader.LoadPage(_nakabaParser, urlTextBox.Text);
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
            //Отображение процесса загрузки в StatusBar
            Action<int, int> setProgress = (v, max) =>
            {
                ProgressBar1.Maximum = _nakabaParser.TotalAnnonces;
                ProgressBar1.Value = _nakabaParser.AnnoncesParced;
                messageStatusLabel.Text = $"Страница {_pageLoader.CurrentPage} из {_pageLoader.TotalPages}. Загружено объявлений {v} из {max}";
            };
            //Изменение состояния загрузки
            Action<AnnonceLoadStateEnum, PageLoadedEventArgs> setState = (s, args) =>
            {
                AnnonceLoadState = s;
            };
            //Событие обработанного объявления.
            _nakabaParser.AnnonceParsed += (s, args) =>
            {
                this.InvokeEx(addAnnonce, args.Content);
                this.InvokeEx(setProgress, args.Number, args.TotalAnnonces);
            };
            //Событие изменение прогресса парсинга
            _nakabaParser.ParcingProgressChanged += (o, args) =>
            {
            };
            //Событие окончания парсинга
            _nakabaParser.ParsingEnded += (sender, args) =>
            {
                this.InvokeEx(new Action(() =>
                {
                    AnnonceLoadState = AnnonceLoadStateEnum.Loaded;
                }), null);
            };
            //Событие окончания загрузки страницы
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