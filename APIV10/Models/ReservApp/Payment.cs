using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? ReservId { get; set; }

    public string? Payment1 { get; set; }

    public virtual ReservationDatum? Reserv { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
