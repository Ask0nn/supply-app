using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class FilesViewModel
    {
        private readonly FilesModel _filesModel;
        private readonly long _requestId;

        public ObservableCollection<Document> Documents { get; set; }
        public Document SelectedDocument { get; set; }
        public Visibility AddVisibility { get; set; }
        public SnackbarMessageQueue SnackBarMessageQueue { get; set; }

        public string FilePath { get; set; }

        public FilesViewModel(long requestId)
        {
            _requestId = requestId;
            _filesModel = new FilesModel();
            Documents = _filesModel.GetDocumentsByRequestId(_requestId);
            Documents.CollectionChanged += Collection_Changed;
            SelectedDocument = new Document();
            AddVisibility = Visibility.Collapsed;
            SnackBarMessageQueue = new SnackbarMessageQueue();
            FilePath = string.Empty;
        }

        private void Collection_Changed(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (Document doc in e.NewItems)
                            SnackBarMessageQueue.Enqueue(_filesModel.Add(doc)
                                ? "Файл добавлен"
                                : "Файл с таким именем уже существует"); break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (Document doc in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_filesModel.Remove(doc)
                                ? "Файл удален"
                                : "Ошибка при удалении файла");
                    break;
            }
        }

        public ICommand ShowAddCommand => new RelayCommand((obj) =>
        {
            AddVisibility = AddVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            SelectedDocument = new Document();
            FilePath = string.Empty;
        });

        public ICommand AddCommand => new RelayCommand((obj) =>
        {
            Document doc = new Document
            {
                Name = Path.GetFileName(FilePath),
                Path = FilePath,
                RequestId = _requestId
            };
            Documents.Add(doc);
            Documents = _filesModel.GetDocumentsByRequestId(_requestId);
            Documents.CollectionChanged += Collection_Changed;
            AddVisibility = Visibility.Collapsed;
        });

        public ICommand SelectFileCommand => new RelayCommand((obj) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF/EXCEL files |*.pdf;*.xlsx;*.xlsm;*.xls;*.xlw|All files (*.*)|*.*",
                Title = "Выбор документа"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                if (string.IsNullOrEmpty(FilePath))
                {
                    SnackBarMessageQueue.Enqueue("Вы не выбрали файл базы данных");
                }
            }
        });

        public ICommand RemoveCommand => new RelayCommand((obj) =>
        {
            Documents.Remove(SelectedDocument);
            SelectedDocument = new Document();
            Documents = _filesModel.GetDocumentsByRequestId(_requestId);
            Documents.CollectionChanged += Collection_Changed;
        }, (obj) => SelectedDocument != null! && SelectedDocument.Id > 0);

        public ICommand OpenFileCommand => new RelayCommand((obj) =>
        {
            if (!File.Exists(SelectedDocument.Path))
            {
                SnackBarMessageQueue.Enqueue("Файл не найден");
                return;
            }
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(SelectedDocument.Path)
            {
                UseShellExecute = true
            };
            p.Start();
        }, (obj) => SelectedDocument != null! && SelectedDocument.Id > 0);
    }
}
