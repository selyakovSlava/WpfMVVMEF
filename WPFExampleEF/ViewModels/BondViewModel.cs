using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFExampleEF.Classes;
using WPFExampleEF.Models;
using WPFExampleEF.Views;

namespace WPFExampleEF.ViewModels
{
    public class BondViewModel : INotifyPropertyChanged
    {
        #region Privates.

        private BondModel _selectedBond;
        private RelayCommand _saveCommand;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _deleteCommand;

        private ApplicationContext _dbContext;

        #endregion

        #region Properties.

        /// <summary>
        /// Коллекция ОФЗ.
        /// </summary>
        public ObservableCollection<BondModel> Bonds { get; set; }

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

        #endregion

        public BondViewModel()
        {
            _dbContext = new ApplicationContext();

            _dbContext.Bonds.Load<BondModel>();

            Bonds = _dbContext.Bonds.Local.ToObservableCollection();

            Update();
        }

        #region Commands.

        /// <summary>
        /// Сохранение изменений.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(obj =>
                  {
                      _dbContext.SaveChanges();
                  }));
            }
        }

        /// <summary>
        /// Добавление записи.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      BondWindow bondWindow = new BondWindow(new BondModel());

                      if (bondWindow.ShowDialog() == true)
                      {
                          _dbContext.Bonds.Add(bondWindow.Bond);
                          _dbContext.SaveChanges();
                      }
                  }));
            }
        }

        /// <summary>
        /// Изменение записи.
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                  (_editCommand = new RelayCommand(obj =>
                  {
                      BondWindow bondWindow = new BondWindow(SelectedBond);

                      if (bondWindow.ShowDialog() == true)
                      {
                          SelectedBond = bondWindow.Bond;
                          _dbContext.SaveChanges();
                      }
                  }));
            }
        }


        /// <summary>
        /// Удаление записи.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(obj =>
                    {
                        BondModel? bond = obj as BondModel;

                        if (bond != null)
                        {
                            _dbContext.Bonds.Remove(bond);
                            _dbContext.SaveChanges();
                        }
                    },
                    (obj) => Bonds.Count > 0));
            }
        }

        /// <summary>
        /// Обновление данных.
        /// </summary>
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ??
                  (_updateCommand = new RelayCommand(obj =>
                  {
                      _dbContext.SaveChanges();
                  }));
            }
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Обновление данных с Мосбиржи.
        /// </summary>
        private void Update()
        {
            foreach (BondModel bond in Bonds)
            {
                bond.Update();
            }

            _dbContext.SaveChanges();
        }

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
