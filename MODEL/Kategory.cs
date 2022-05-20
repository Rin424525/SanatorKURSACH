namespace Sanator
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    [Table("Kategory")]
    public partial class Kategory : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategory()
        {
            Number = new HashSet<Number>();
        }

        [Key]
        public int ID_type { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public int capacity { get; set; }

        public int cost { get; set; }

        public string photo { get; set; }
        [NotMapped]
        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
            get { return name.TrimEnd(); }
        }
        [NotMapped]
        public string Description
        {
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
            get { return description.TrimEnd(); }
        }
        [NotMapped]
        public int Capacity
        {
            set
            {
                capacity = value;
                OnPropertyChanged("Capacity");
            }
            get { return capacity; }
        }
        [NotMapped]
        public int Cost
        {
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
            get { return cost; }
        }

        [NotMapped]
        public string Photo
        {
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
            get { return photo; }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Number> Number { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
