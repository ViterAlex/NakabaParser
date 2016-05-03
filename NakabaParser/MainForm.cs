using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using SiteParser.Annotations;
using SiteParser.Interfaces;
using SiteParser.UserControls;

//TODO: Удалять старые объявления при смене адреса
//TODO: Дописывать документ, а не создавать заново
//TODO: Возможность приостановки и отмены скачивания
//TODO: Задание количества объявлений для скачивания
//TODO: Фильтрование объявлений по критериям
//TODO: Парсинг цены из текста объявления, если цена не указана в отдельном блоке

namespace SiteParser
{
    public sealed partial class MainForm : Form, IView
    {
        private int _currentAnnonce;
        private int _currentPage;
        private int _progress;
        private int _totalAnnonces;
        private int _totalPages;
        private bool _isParsing;
        private bool _isExporting;
        private string _progressMessage;

        public MainForm()
        {
            InitializeComponent();
            //SetBindings();
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(bindingSource, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void exportButton_Click(object sender, EventArgs e)
        {
            ExportToWord?.Invoke(this, new EventArgs());
        }

        private void loadAnnoncesButton_Click(object sender, EventArgs e)
        {
            LoadAnnonces?.Invoke(this, new EventArgs());
        }
        
        private void pauseButton_Click(object sender, EventArgs e)
        {
            Pause?.Invoke(this, new EventArgs());
        }
        
        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop?.Invoke(this, new EventArgs());
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            loadAnnoncesButton.Enabled = urlTextBox.Text.Length > 0;
        }

        private void SetControlsState()
        {
            loadAnnoncesButton.Enabled = !_isParsing;
            parsingStatusStrip.Visible = _isParsing;
            pauseButton.Enabled = _isParsing;
            stopButton.Enabled = _isParsing;
            exportButton.Enabled = !_isExporting;
            exportStatusStrip.Visible = _isExporting;
        }

        #region Implementation of IView

        #region Свойства
        public bool IsExporting
        {
            get { return _isExporting; }
            set
            {
                if (value == _isExporting) return;
                _isExporting = value;
                SetControlsState();
                OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (value == _currentPage) return;
                _currentPage = value;
                currentPageNumStatusLabel.Text = _currentPage.ToString();
                OnPropertyChanged();
            }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                if (value == _totalPages) return;
                _totalPages = value;
                totalPagesStatusLabel.Text = _totalPages.ToString();
                OnPropertyChanged();
            }
        }

        public int CurrentAnnonce
        {
            get { return _currentAnnonce; }
            set
            {
                if (value == _currentAnnonce) return;
                _currentAnnonce = value;
                currentAnnonceNumStatusLabel.Text = _currentAnnonce.ToString();
                OnPropertyChanged();
            }
        }

        public int TotalAnnonces
        {
            get { return _totalAnnonces; }
            set
            {
                if (value == _totalAnnonces) return;
                _totalAnnonces = value;
                totalAnnoncesStatusLabel.Text = _totalAnnonces.ToString();
                OnPropertyChanged();
            }
        }

        public int Progress
        {
            get { return _progress; }
            set
            {
                if (value == _progress) return;
                _progress = value;
                exportProgressBar.Value = _progress;
                parsingProgressBar.Value = _progress;
                OnPropertyChanged();
            }
        }

        public string ProgressMessage
        {
            get { return _progressMessage; }
            set
            {
                if (value == _progressMessage) return;
                _progressMessage = value;
                exportMessageStatusLabel.Text = _progressMessage;
                OnPropertyChanged();
            }
        }


        public bool IsParsing
        {
            get { return _isParsing; }

            set
            {
                if (value == _isParsing) return;
                _isParsing = value;
                SetControlsState();
                OnPropertyChanged();
            }
        }
        #endregion


        #region События
        public event EventHandler LoadAnnonces;
        public event EventHandler Pause;
        public event EventHandler Stop;
        public event EventHandler ExportToWord;
        #endregion

        #region Методы
        public string GetUrl()
        {
            return urlTextBox.Text;
        }

        public void AddAnnonce(IAnnonceContent content)
        {
            flowLayoutPanel1.Controls.Add(new AnnoncePresenter(content));
        }
        #endregion

        #endregion
    }
}