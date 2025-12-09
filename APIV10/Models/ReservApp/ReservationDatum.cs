using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class ReservationDatum
{
    public int ReservId { get; set; }

    public int? CustomerId { get; set; }

    public int? PlaceId { get; set; }

    public int? Travelers { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual PropertiesInfo? Place { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
