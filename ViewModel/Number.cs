

using Sanator.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows;

namespace Sanator
{
    class NumbersViewModel : INotifyPropertyChanged
    {
        public DbOperations db;
        public ObservableCollection<Kategory> kategory { get; set; }


        public ObservableCollection<Number> numbers { get; set; }//all numbers
        private Number selectedNumber;

        public ObservableCollection<Status> status { get; set; }

        private IDialogService ds;
        public Number SelectedNumber
        {
            set
            {
                selectedNumber = value;
                OnPropertyChanged("SelectedNumber");
            }
            get { return selectedNumber; }
        }

        public NumbersViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            numbers = new ObservableCollection<Number>(db.GetAllNumber().ToList());
            numbers.OrderBy(u => u.ID);
            kategory = new ObservableCollection<Kategory>(db.GetAllKategory());
            status = new ObservableCollection<Status>(db.GetAllStatus());
            SelectedNumber = db.GetAllNumber().FirstOrDefault();
        }
        private RelayCommand addNumber;
        public RelayCommand AddNumber
        {
            get
            {
                return addNumber ??
                    (addNumber = new RelayCommand(obj =>
                    {
                        Number number = new Number();
                        numbers.Insert(0, number);
                        SelectedNumber = number;
                        ds.ShowMessage("Новая номер добавлен!");
                    }));
            }
        }
        private RelayCommand removeNumber;
        public RelayCommand RemoveNumber
        {
            get
            {
                return removeNumber ??
                    (removeNumber = new RelayCommand(obj =>
                    {

                        if (db.FindNumber(SelectedNumber.ID) != null)
                        {
                            db.RemoveNumber(SelectedNumber);
                            numbers.Remove(SelectedNumber);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else numbers.Remove(SelectedNumber);
                    },
                    (obj) => SelectedNumber != null));
            }
        }

        private RelayCommand saveNumber;
        public RelayCommand SaveNumber
        {
            get
            {
                return saveNumber ??
                    (saveNumber = new RelayCommand(obj =>
                    {
                        Number number = new Number();
                        try
                        {
                            if (SelectedNumber != null)
                            {
                                number = (Number)db.FindNumber(SelectedNumber.ID);
                                if (number == null)
                                {
                                    db.AddNumber(SelectedNumber);
                                }
                                db.Save();
                                ds.ShowMessage("Изменения сохранены!");
                            }
                        }
                        catch
                        {

                            ds.ShowMessage("Невозможно изменить объект!");
                        }

                    }, (obj) => (SelectedNumber != null) && SelectedNumber.Status1 != null && SelectedNumber.Kategory1 != null));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
