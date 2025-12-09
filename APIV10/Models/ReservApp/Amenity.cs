using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class Amenity
{
    public int AmenityId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
