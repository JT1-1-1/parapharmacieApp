using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class Produit
{
    public int IdProduit { get; set; }

    public string Nom { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public string? Categorie { get; set; }

    public decimal? PrixAchat { get; set; }

    public decimal PrixVente { get; set; }

    public int QuantiteStock { get; set; }

    public int? SeuilAlerte { get; set; }

    public DateOnly? DatePeremption { get; set; }

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual ICollection<LigneVente> LigneVentes { get; set; } = new List<LigneVente>();
}
