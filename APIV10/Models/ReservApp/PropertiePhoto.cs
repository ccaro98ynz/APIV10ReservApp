using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class PropertiePhoto
{
    public int PhotoId { get; set; }

    public int? PlaceId { get; set; }

    public string? PhotoUrl { get; set; }

    public bool? IsCoverPhoto { get; set; }

    public virtual PropertiesInfo? Place { get; set; }
}
