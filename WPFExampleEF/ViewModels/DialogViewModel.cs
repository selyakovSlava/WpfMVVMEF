using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFExampleEF.Classes;
using WPFExampleEF.Models;

namespace WPFExampleEF.ViewModels
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        private BondModel _selectedBond;
        private RelayCommand _saveCommand;
        private ApplicationContext _dbContext;


        /// <summary>
        /// Модель выбранной ОФЗ.
        /// </summary>
        public BondModel SelectedBond
        {
            get { return _selectedBond; }
            set
            {
                _selectedBond = value;
                OnPropertyChanged("SelectedBond");
            }
        }

        public DialogViewModel()
        {
            _dbContext = new ApplicationContext();
            SelectedBond = new BondModel();
        }

        /// <summary>
        /// Добавление записи.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(obj =>
                  {
                      SelectedBond.Update();
                      _dbContext.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
