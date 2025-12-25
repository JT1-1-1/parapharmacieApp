using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class LigneCommande
{
    public int IdLigneCommande { get; set; }

    public int IdCommande { get; set; }

    public int IdProduit { get; set; }

    public int Quantite { get; set; }

    public virtual CommandeFournisseur IdCommandeNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
