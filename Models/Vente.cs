using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class Vente
{
    public int IdVente { get; set; }

    public DateTime? DateVente { get; set; }

    public decimal? Total { get; set; }

    public decimal? Remise { get; set; }

    public int? IdClient { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ICollection<LigneVente> LigneVentes { get; set; } = new List<LigneVente>();
}
