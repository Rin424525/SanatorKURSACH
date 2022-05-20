using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace Sanator
{


    [Table("Uchet")]
    public partial class Uchet : INotifyPropertyChanged
    {
        [Key]
        public int ID_ychet { get; set; }

        public DateTime Date_start { get; set; }

        public DateTime Date_finish { get; set; }

        public int ID_number_FK { get; set; }

        public int ID_worker_FK { get; set; }

        public int? ID_pay_FK { get; set; }

        public int ID_client_FK { get; set; }

        public virtual Client Client { get; set; }

        [NotMapped]
        public Client Client1
        {
            get { return Client; }
            set
            {
                Client = value;
                OnPropertyChanged("Client1");
            }
        }

        public virtual Number Number { get; set; }
        [NotMapped]
        public Number Number1
        {
            get { return Number; }
            set
            {
                Number = value;
                OnPropertyChanged("Number1");
            }
        }


        public virtual Pay Pay { get; set; }
        [NotMapped]


        public Pay Pay1
        {
            get { return Pay; }
            set
            {
                Pay = value;
                OnPropertyChanged("Pay1");
            }
        }
    
        public virtual Worker Worker { get; set; }
        [NotMapped]
        public Worker Worker1
        {
            get { return Worker; }
            set
            {
                Worker = value;
                OnPropertyChanged("Worker1");
            }
        }
        [NotMapped]
        public DateTime date_start
        {
            get { return Date_start; }
            set
            {
                Date_start = value;
                OnPropertyChanged("date_start");
            }
        }
        [NotMapped]
        public DateTime date_finish
        {
            get { return Date_finish; }
            set
            {
                Date_finish = value;
                OnPropertyChanged("date_finish");
            }
        }
        [NotMapped]
        public string Period
        {
            get
            {
                return "C " +date_start.ToString() +" по "+date_finish.ToString();
            }

        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

