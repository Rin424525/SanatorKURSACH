namespace Sanator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 
    using System.Runtime.CompilerServices;

    [Table("Number")]
    public partial class Number: INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Number()
        {
            Uchet = new HashSet<Uchet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_number { get; set; }

        public int ID_type_FK { get; set; }

        public int ID_status_FK { get; set; }
        [NotMapped]
        public int ID
        {
            get { return ID_number; }
            set
            {
                ID_number = value;
                OnPropertyChanged("ID");
            }
        }
        [NotMapped]
        public int Id_type_FK
        {
            get { return ID_type_FK; }
            set
            {
                ID_type_FK = value;
                OnPropertyChanged("Id_type_FK");
            }
        }
        [NotMapped]
        public int Id_status_FK
        {
            get { return ID_status_FK; }
            set
            {
                ID_status_FK = value;
                OnPropertyChanged("Id_status_FK");
            }
        }
    
        public virtual Kategory Kategory { get; set; }
        [NotMapped]
        public Kategory Kategory1
        {
            get { return Kategory; }
            set
            {
                Kategory = value;
                OnPropertyChanged("Kategory1");
            }
        }
        public virtual Status Status { get; set; }
        [NotMapped]
        public Status Status1
        {
            get { return Status; }
            set
            {
                Status = value;
                OnPropertyChanged("Status1");
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uchet> Uchet { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
