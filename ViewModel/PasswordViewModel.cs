using Sanator;

using Sanator.View;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sanator.ViewModel
{
    class PasswordViewModel : INotifyPropertyChanged, IRequireViewIdentification
    {
        public DbOperations db = new DbOperations();
        public ObservableCollection<Worker> workers { get; set; }
        private Worker worker;
        private IDialogService ds = new DefaultDialogService();
        public PasswordViewModel()
        {
            ViewID = Guid.NewGuid();
            workers = new ObservableCollection<Worker>(db.GetAllWorker());
            worker = new Worker();
            if (User.ID_worker > 0) Login = db.FindWorker(User.ID_worker).Login;
        }
        public Guid ViewID { get; }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        private RelayCommand password;
        public RelayCommand Password
        {
            get
            {
                return password ??
                    (password = new RelayCommand(obj =>
                    {
                        var passwordBox = obj as PasswordBox;
                        if (passwordBox != null)
                        {
                            var selectedUsers = workers.Where(u => u.Login.TrimEnd() == Login && u.Parol.Trim() == passwordBox.Password);
                            if (selectedUsers.Count() > 0) // если такая запись существует
                            {
                                MainWindow1 main = new MainWindow1();
                                ds.ShowMessage("Добро пожаловать, " + selectedUsers.FirstOrDefault().FIO.TrimEnd() + " <3"); // говорим, что авторизовался
                                if (selectedUsers.FirstOrDefault().position.TrimEnd() == "Администратор")
                                {
                                    User.ID_worker = selectedUsers.FirstOrDefault().ID_worker;
                                    User.FIO = selectedUsers.FirstOrDefault().Fio;
                                    WindowManager.CloseWindow(ViewID);
                                    main.DataContext = new MainViewModel1(db,ds);
                                    main.Show();

                                }
                                else
                                {
                                  
                                    User.ID_worker = selectedUsers.FirstOrDefault().ID_worker;
                                    User.FIO = selectedUsers.FirstOrDefault().Fio;
                                    main.DataContext =new MainViewModel1(db,ds);
                                    main.Show();
                                    WindowManager.CloseWindow(ViewID);                                   
                                }
                            }
                        
                            else ds.ShowMessage("Пожалуйста, повторите ввод логина и пароля!"); // выводим ошибку
                        }
                    }, obj=> (Login!=null)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
