using System;
using System.Collections.Generic;

namespace APIV10.Models.ReservApp;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public byte[]? PasswordHash { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ReservationDatum> ReservationData { get; set; } = new List<ReservationDatum>();
}
