//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaspisKusach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Routes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Routes()
        {
            this.Tickets = new HashSet<Tickets>();
        }
    
        public int IdRoute { get; set; }
        public int IdTrain { get; set; }
        public string Departure_Station { get; set; }
        public string Arrival_Station { get; set; }
        public System.DateTime Departure_Date { get; set; }
        public System.DateTime Arrival_Date { get; set; }
    
        public virtual Trains Trains { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
