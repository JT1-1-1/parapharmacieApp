using System;
using System.Collections.Generic;

namespace ParapharmacieApp.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    public string Nom { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public string? Adresse { get; set; }

    public virtual ICollection<CommandeFournisseur> CommandeFournisseurs { get; set; } = new List<CommandeFournisseur>();
}
