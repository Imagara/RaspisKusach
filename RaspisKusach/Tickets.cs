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
    
    public partial class Tickets
    {
        public int IdTicket { get; set; }
        public int IdUser { get; set; }
        public int IdTrip { get; set; }
        public int IdCarriage { get; set; }
        public int PlaceNumber { get; set; }
        public System.DateTime BuyDate { get; set; }
    
        public virtual Carriages Carriages { get; set; }
        public virtual Trips Trips { get; set; }
        public virtual Users Users { get; set; }
    }
}
