namespace Sanator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.CompilerServices;

    [Table("Client")]
    public partial class Client : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Log = new HashSet<Log>();
            Pay = new HashSet<Pay>();
            Uchet = new HashSet<Uchet>();
        }

        [Key]
        public int ID_client { get; set; }

        [Required]
        [StringLength(50)]
        public string FIO { get; set; }

        [Required]
        [StringLength(20)]
        public string passport { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; }


        [Column(TypeName = "date")]
        public DateTime birth { get; set; }

        [NotMapped]
        public string Fio
        {
            get { return FIO; }
            set
            {
                FIO = value;
                OnPropertyChanged("Fio");
            }
        }

       
        [NotMapped]
        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        
        [NotMapped]
        public DateTime Birth
        {
            get { return birth; }
            set
            {
                birth = value;
                OnPropertyChanged("Birth");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pay> Pay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uchet> Uchet { get; set; }
        [NotMapped]
        private ObservableCollection<Uchet> Uchets;
        [NotMapped]
        public ObservableCollection<Uchet> Uchet1
        {
            get
            {
                Uchets = new ObservableCollection<Uchet>();
                foreach (var item in Uchet)
                {
                    if (item.Pay == null) Uchets.Add(item);
                }
                return Uchets;
            }
            
        }
        [NotMapped]
        private ObservableCollection<Log> Logs;
        [NotMapped]
        public ObservableCollection<Log> Log1
        {
            get
            {
                Logs = new ObservableCollection<Log>();
                foreach (var it in Log)
                {
                    if (it.Pay == null) Logs.Add(it);
                }
                return Logs;
            }

        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        
    }
}
