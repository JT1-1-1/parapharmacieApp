using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class LigneVente
{
    public int IdLigneVente { get; set; }

    public int IdVente { get; set; }

    public int IdProduit { get; set; }

    public int Quantite { get; set; }

    public decimal? PrixUnitaire { get; set; }

    public virtual Produit IdProduitNavigation { get; set; } = null!;

    public virtual Vente IdVenteNavigation { get; set; } = null!;
}
