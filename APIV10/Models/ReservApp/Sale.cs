using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class Sale
{
    public int TicketId { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? SaleDate { get; set; }

    public virtual Payment? Payment { get; set; }
}
