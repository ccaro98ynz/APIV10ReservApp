using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class PropertiesInfo
{
    public int PlaceId { get; set; }

    public int? HostId { get; set; }

    public int? AddressId { get; set; }

    public bool? Status { get; set; }

    public string? Type { get; set; }

    public int? ExpectedGuests { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual AddressInfo? Address { get; set; }

    public virtual Host? Host { get; set; }

    public virtual ICollection<PropertiePhoto> PropertiePhotos { get; set; } = new List<PropertiePhoto>();

    public virtual ICollection<ReservationDatum> ReservationData { get; set; } = new List<ReservationDatum>();
}
