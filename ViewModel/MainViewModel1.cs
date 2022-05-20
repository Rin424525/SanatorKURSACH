
using Sanator.View;
using Sanator.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;


namespace Sanator
{
    class MainViewModel1 : INotifyPropertyChanged, IRequireViewIdentification
    {
        public DbOperations db;
        public ObservableCollection<Uchet> uchets { get; set; }
        private bool[] flag;
        private DispatcherTimer timer;
        private DateTime date;
        public Guid ViewID { get; }
        private IDialogService dialog;
        public MainViewModel1(DbOperations db, IDialogService ds)
        {
            this.db = db;
            dialog = ds;
            date = DateTime.Now;
            ViewID = Guid.NewGuid();
            uchets = new ObservableCollection<Uchet>(db.GetAllUchet());
            flag = new bool[db.GetAllNumber().Count()];
            foreach (var obj in uchets)
            {
                if (DateTime.Today >= obj.Date_start && DateTime.Today <= obj.Date_finish && !flag[obj.Number.ID - 1]) { obj.Number.Status1 = db.FindStatus(2); flag[obj.Number.ID - 1] = true; }
                else if (obj.Number.Status1 != db.FindStatus(4)) obj.Number.Status1 = db.FindStatus(1);
            }            
            timer = new DispatcherTimer(); 
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {
            if (DateTime.Now.Day != date.Day)
            {        
                for (int i=0;i< db.GetAllNumber().Count(); i++)
                {
                    flag[i] = false;
                }
                foreach (var obj in uchets)
                {
                   if (DateTime.Today >= obj.Date_start && DateTime.Today <= obj.Date_finish && !flag[obj.Number.ID - 1]) { obj.Number.Status1 = db.FindStatus(2); flag[obj.Number.ID - 1] = true; }
                   else if (obj.Number.Status1 != db.FindStatus(4)) obj.Number.Status1 = db.FindStatus(1);
                }
            }
  
        }                        
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand openServiceView;
        public RelayCommand OpenServiceView
        {
            get
            {
                return openServiceView ??
                    (openServiceView = new RelayCommand(obj =>
                    {
                        ServiceView l = new ServiceView(new ServiceViewModel(db, dialog));
                        l.DataContext=new ServiceViewModel(db, dialog);
                        l.ShowDialog();
                    }));
            }
        }
        private RelayCommand openWorkerView;
        public RelayCommand OpenWorkerView
        {
            get
            {
                return openWorkerView ??
                    (openWorkerView = new RelayCommand(obj =>
                    {
                        WorkerView l = new WorkerView(new WorkerViewModel(db, dialog));
                        l.DataContext = new WorkerViewModel(db, dialog);
                        l.ShowDialog();
                    }));
            }
        }


        private RelayCommand openUchetView;
        public RelayCommand OpenUchetView
        {
            get
            {
                return openUchetView ??
                    (openUchetView = new RelayCommand(obj =>
                    {
                        UchetView l = new UchetView();
                        l.DataContext = new UchetViewModel(db, dialog);
                        l.ShowDialog();
                    }));
            }
        }

        private RelayCommand openPasswordView;
        public RelayCommand OpenPasswordView
        {
            get
            {
                return openPasswordView ??
                    (openPasswordView = new RelayCommand(obj =>
                    {
                        Password vm = new Password();                                   
                        vm.Show();
                        WindowManager.CloseWindow(ViewID);  
                    }));
            }
        }

      
        private RelayCommand openPayView;
        public RelayCommand OpenPayView
        {
            get
            {
                return openPayView ??
                    (openPayView = new RelayCommand(obj =>
                    {
                        PayView l = new PayView();
                        l.DataContext = new PayViewModel(db, dialog);
                        l.ShowDialog();
                    }));
            }
        }
        private RelayCommand openLogView;
        public RelayCommand OpenLogView
        {
            get
            {
                return openLogView ??
                    (openLogView = new RelayCommand(obj =>
                    {
                        LogView l = new LogView();
                        l.DataContext = new LogViewModel(db, dialog);
                        l.ShowDialog();
                    }));
            }
        }

        private RelayCommand openClientView;
        public RelayCommand OpenClientView
        {
            get
            {
                return openClientView ??
                    (openClientView = new RelayCommand(obj =>
                    {
                        ClientView f = new ClientView(new ClientViewModel(db, dialog)) ;
                    
                        f.DataContext = new ClientViewModel(db,dialog);
                        f.ShowDialog();
                    }));
            }
        }

     

        private RelayCommand openReserveView;
        public RelayCommand OpenReserveView
        {
            get
            {
                return openReserveView ??
                    (openReserveView = new RelayCommand(obj =>
                    {
                        ReserveView vm = new ReserveView();
                        vm.DataContext = new ReserveViewModel(db,dialog);
                        vm.ShowDialog();
                    }));
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
