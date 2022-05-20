namespace Sanator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.CompilerServices;
    public partial class Status: INotifyPropertyChanged
    {
       

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status()
        {
            Number = new HashSet<Number>();
        }

        [Key]
        public int id_status { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        [NotMapped]
        public string Name
        {
            get { return name.TrimEnd(); }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Number> Number { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
