using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestTask200125.Models;

namespace TestTask200125.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainModel _model;

        private ObservableCollection<ObjectViewModel> _objects;
        private ObjectViewModel _currentObject;

        public ObservableCollection<ObjectViewModel> Objects
        {
            get { return _objects; }
        }

        public ObjectViewModel CurrentObject {
            get { return _currentObject; }
            set
            {
                if (_currentObject != value)
                {
                    _currentObject = value;
                    RaisePropertyChanged(nameof(CurrentObject));
                }
            }
        }

        public Visibility LoadingThrobberVisibility { get; private set; } = Visibility.Collapsed;

        public ICommand ImportCommand { get; private set; }

        public MainViewModel(MainModel model)
        {
            _model = model;
            InitCommands();
        }

        public MainViewModel() : this(new MainModel()) { }

        private void InitCommands()
        {
            ImportCommand = new RelayCommand(Import);
        }

        private async void Import()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog(Application.Current.MainWindow);
            ChangeLoadingThrobber(shouldBeVisible: true);
            await _model.ImportXlsAsync(dlg.FileName);
            Refresh();
            ChangeLoadingThrobber(shouldBeVisible: false);
        }

        private void Refresh()
        {
            var objects = _model.Objects;
            if (objects != null)
                _objects = new ObservableCollection<ObjectViewModel>(objects.Select(model => new ObjectViewModel(model)));
            else
                _objects = null;
            RaisePropertyChanged(nameof(Objects));
        }

        private void ChangeLoadingThrobber(bool shouldBeVisible)
        {
            LoadingThrobberVisibility = shouldBeVisible ? Visibility.Visible : Visibility.Collapsed;
            RaisePropertyChanged(nameof(LoadingThrobberVisibility));
        }
    }
}