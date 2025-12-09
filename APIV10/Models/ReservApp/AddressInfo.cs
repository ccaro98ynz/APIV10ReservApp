using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class AddressInfo
{
    public int AddressId { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Area { get; set; }

    public string? Country { get; set; }

    public int? PostalCode { get; set; }

    public int? OutsideNumber { get; set; }

    public virtual ICollection<PropertiesInfo> PropertiesInfos { get; set; } = new List<PropertiesInfo>();
}
