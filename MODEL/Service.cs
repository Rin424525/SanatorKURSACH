namespace Sanator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.CompilerServices;

    [Table("Service")]
    public partial class Service: INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Log = new HashSet<Log>();
        }

        [Key]
        public int ID_service { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }
        [NotMapped]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int pay { get; set; }
        [NotMapped]
        public int Pay
        {
            get { return pay; }
            set
            {
                pay = value;
                OnPropertyChanged("Pay");
            }
        }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        [NotMapped]
        public string Description
        {
            get
            {
                if (description == null) return description;
                return description.TrimEnd();
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
