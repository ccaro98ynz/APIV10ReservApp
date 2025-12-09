using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class PropertieAmenity
{
    public int? PlaceId { get; set; }

    public int? AmenityId { get; set; }

    public virtual Amenity? Amenity { get; set; }

    public virtual PropertiesInfo? Place { get; set; }
}
