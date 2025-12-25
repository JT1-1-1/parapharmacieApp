using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public int? PointsFidelite { get; set; }

    public virtual ICollection<Vente> Ventes { get; set; } = new List<Vente>();
}
