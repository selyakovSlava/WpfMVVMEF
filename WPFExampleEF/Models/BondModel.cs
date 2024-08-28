using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using WPFExampleEF.Parsing;

namespace WPFExampleEF.Models
{
    public class BondModel : INotifyPropertyChanged
    {
        #region Privates
        
        private int _id;
        private string _secId;
        private string _name;
        private int _count;
        private double _price;
        private double _accumulatedCouponIncome = 0;
        private double _couponValue = 0;
        private DateTime? _dateNextCoupon;
        private DateTime? _dateClose;

        private double _grossSum = 0;
        private double _taxIncomeSum = 0;
        private double _netSum = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Id записи.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        
        /// <summary>
        /// Id бумаги.
        /// </summary>
        public string SecId 
        { 
            get { return _secId; } 
            set 
            { 
                _secId = value;
                OnPropertyChanged("SecId"); 
            } 
        }

        /// <summary>
        /// Наименование бумаги.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        /// <summary>
        /// Текущая цена бумаги.
        /// </summary>
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        /// <summary>
        /// НКД бумаги.
        /// </summary>
        public double AccumulatedCouponIncome
        {
            get { return _accumulatedCouponIncome; }
            set
            {
                _accumulatedCouponIncome = value;
                OnPropertyChanged("AccumulatedCouponIncome");
            }
        }

        /// <summary>
        /// Величина купона.
        /// </summary>
        public double CouponValue
        {
            get { return _couponValue; }
            set
            {
                _couponValue = value;
                OnPropertyChanged("CouponValue");
            }
        }

        /// <summary>
        /// Дата выплаты следующего купона.
        /// </summary>
        public DateTime? DateNextCoupon
        {
            get { return _dateNextCoupon; }
            set
            {
                _dateNextCoupon = value;
                OnPropertyChanged("DateNextCoupon");
            }
        }

        /// <summary>
        /// Дата закрытия бумаги.
        /// </summary>
        public DateTime? DateClose
        {
            get { return _dateClose; }
            set
            {
                _dateClose = value;
                OnPropertyChanged("DateClose");
            }
        }

        /// <summary>
        /// Сумма выплаты до вычета НДФЛ.
        /// </summary>
        [NotMapped]
        public double GrossSum
        {
            get { return _grossSum; }
            set
            {
                _grossSum = value;
                OnPropertyChanged("GrossSum");
            }
        }

        /// <summary>
        /// НДФЛ.
        /// </summary>
        [NotMapped]
        public double TaxIncomeSum
        {
            get { return _taxIncomeSum; }
            set
            {
                _taxIncomeSum = value;
                OnPropertyChanged("TaxIncomeSum");
            }
        }

        /// <summary>
        /// Сумма выплаты после вычета НДФЛ.
        /// </summary>
        [NotMapped]
        public double NetSum
        {
            get { return _netSum; }
            set
            {
                _netSum = value;
                OnPropertyChanged("NetSum");
            }
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Загрузка данных с МосБиржи.
        /// </summary>
        public void Update()
        {
            BondParsing bondParsing = new BondParsing(this.SecId);

            this.Name = bondParsing.GetName();
            this.CouponValue = bondParsing.GetCouponValue();
            this.AccumulatedCouponIncome = bondParsing.GetAccumulatedCouponIncome();
            this.DateNextCoupon = bondParsing.GetDateNextCoupon();
            this.DateClose = bondParsing.GetDateClose();
            this.Price = bondParsing.GetCurrentPrice();

            this.GrossSum = this.Count * this.CouponValue;
            this.TaxIncomeSum = this.GrossSum * 0.13;
            this.NetSum = this.GrossSum - this.TaxIncomeSum;
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
