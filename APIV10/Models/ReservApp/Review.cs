using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? ReservId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? DateReview { get; set; }

    public virtual ReservationDatum? Reserv { get; set; }
}
