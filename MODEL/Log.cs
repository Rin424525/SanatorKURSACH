namespace Sanator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.Runtime.CompilerServices;

    [Table("Log")]
    public partial class Log: INotifyPropertyChanged
    {
        [Key]
        public int ID_line { get; set; }

        public DateTime date { get; set; }

        //public int sum { get; set; }
        public int? ID_pay_FK { get; set; }
        public int ID_service_FK { get; set; }

        public int ID_client_FK { get; set; }

        public virtual Client Client { get; set; }
        [NotMapped]
        public Client Client1
        {
            get
            {
                return Client;
            }
            set
            {
                Client = value;
                OnPropertyChanged("Client1");
            }
        }
        public virtual Service Service { get; set; }
        [NotMapped]
        public Service Service1
        {
            get
            {
                return Service;
            }
            set
            {
                Service = value;
                OnPropertyChanged("Service1");
            }
        }
        [NotMapped]
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public virtual Pay Pay { get; set; }
        [NotMapped]
        public Pay Pay1
        {
            get
            {
                return Pay;
            }
            set
            {
                Pay = value;
                OnPropertyChanged("Pay1");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
