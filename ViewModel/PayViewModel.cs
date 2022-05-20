

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sanator.ViewModel
{
    class PayViewModel: INotifyPropertyChanged
    {
        public DbOperations db;      
        public ObservableCollection<Client> clients { get; set; }
        private int costUchet { get; set; }
        private int costLog { get; set; }
        private int sum;
        IDialogService ds;
        public PayViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            clients = new ObservableCollection<Client>(db.GetAllClient());
            Date = DateTime.Now;      
        }
        private bool chekUchet;
        public bool ChekUchet
        {
            get { return chekUchet; }
            set
            {
                chekUchet = value;
                SetCostUchet();
                SetSum();
                OnPropertyChanged("ChekUchet");
            }
        }
        private bool radioBut;
        public bool RadioBut
        {
            get { return radioBut; }
            set
            {
                radioBut = value;
                SetCostLog();
                SetSum();
                OnPropertyChanged("RadioButUchet");
            }
        }
        private bool chekService;
        public bool ChekService
        {
            get { return chekService; }
            set
            {
                chekService = value;
                SetCostLog();
                SetSum();
                OnPropertyChanged("ChekService");
            }
        }
        
        public int Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }
        public int CostUchet
        {
            get { return costUchet; }
            set
            {
                costUchet = value;
                OnPropertyChanged("CostUchet");
            }
        }
        public int CostLog
        {
            get { return costLog; }
            set
            {
                costLog = value;
                OnPropertyChanged("CostLog");
            }
        }
        private Client selectedClient;
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        
        private Log selectedLog;
        public Log SelectedLog
        {
            get { return selectedLog; }
            set
            {
                selectedLog = value;

                SetCostLog();
    
                SetSum();
                OnPropertyChanged("SelectedLog");
            }
        }
        private void SetSum()
        {
            Sum = CostLog + CostUchet;
        }
        private void SetCostLog()
        {
            CostLog = 0;
            if (ChekService)
            {
                if (RadioBut)
                {
                    foreach (var item in SelectedClient.Log)
                    {
                        CostLog += item.Service1.Pay;
                    }
                }
                else CostLog = selectedLog.Service1.Pay;
            }
        }
        private void SetCostUchet()
        {
            CostUchet = 0;
            if (ChekUchet)
            {

                CostUchet = selectedUchet.Number1.Kategory1.Cost * (selectedUchet.date_finish - selectedUchet.date_start).Days;
                if (CostUchet == 0) CostUchet = selectedUchet.Number1.Kategory1.Cost;
            }
          
        }
        public void SaveChek()
        {
            if (ds.SaveFileDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(ds.FilePath, true))
                {
                    sw.WriteLine("Клиент:{0}\r\nПаспорт: {1}\r\nДата оплаты: {2}\r\n|----------------------------|\r\n Стоимость проживания: {3} ₽\r\n|----------------------------|\r\n Стоимость услуг: {4} ₽\r\n|----------------------------|\r\n Сумма оплаты: {5} ₽\r\n|----------------------------|\r\n", SelectedClient.Fio, SelectedClient.Passport,SelectedLog.Date, CostUchet, CostLog, Sum);
                    sw.Close();
                    ds.ShowMessage("Чек сохранен!");
                }
            }
        }
        private Uchet selectedUchet;
        public Uchet SelectedUchet
        {
            get { return selectedUchet; }
            set
            {
                selectedUchet = value;
                SetCostUchet();
                SetSum();            
                OnPropertyChanged("SelectedUchet");
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private RelayCommand payAccommodation;
        public RelayCommand PayAccommodation
        {
            get
            {
                return payAccommodation ??
                    (payAccommodation = new RelayCommand(obj =>
                    {
                        if (ChekUchet)
                        {
                 
                              Pay pay = new Pay();
                              pay.Date = Date;
                              pay.Client1 = SelectedClient;
                              pay.Sum = this.Sum;
                              db.AddPay(pay);
                              SelectedUchet.Pay1=pay;
                              db.Save();
                             
        
                        }
                        if (ChekService)
                        {
                            if (RadioBut)
                            {
                                foreach(var item in SelectedClient.Log)
                                {
                                    Pay pay = new Pay();
                                    pay.Date = Date;
                                    pay.Client1 = SelectedClient;
                                    pay.Sum = item.Service1.Pay;
                                    db.AddPay(pay);
                                    item.Pay = pay;
                                }
                                db.Save();
                              
                            }
                            else
                            {
                                Pay pay = new Pay();
                                pay.Date = Date;
                                pay.Client1 = SelectedClient;
                                pay.Sum = SelectedLog.Service1.Pay;
                                db.AddPay(pay);
                                SelectedLog.Pay = pay;
                                db.Save();
                              
                            }
                        }
                        if (ds.ShowMessageOKCancel("Оплачено! Сохранить чек?")) SaveChek();

                    },obj=>CanExecute()));
            }
        }
        private bool CanExecute()
        {
            if (ChekService && ChekUchet) return (SelectedUchet != null && SelectedLog != null);
            else if (ChekUchet) return (SelectedUchet != null);
            else if (ChekService) return(SelectedLog != null);
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
