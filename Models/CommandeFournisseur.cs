using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class CommandeFournisseur
{
    public int IdCommande { get; set; }

    public DateOnly? DateCommande { get; set; }

    public string? Statut { get; set; }

    public int IdFournisseur { get; set; }

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();
}
