using System.ComponentModel.DataAnnotations;

namespace BonDeCollecte.Models
{
    public class BTCollecte
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public DateTime DateCreation { get; set; }
        public virtual Client? Client { get; set; }
        public double NbreFuts20L { get; set; }
        public double NbreFuts60L { get; set; }
        public double CapaciteEnLitre { get; set; }
        public double PoidsTotal { get; set; }
        public double Montant { get; set; }
        public bool Paye { get; set; }
        public bool Validation { get; set; }
        public string? Signature { get; set; }
        public string? Commentaire { get; set; }


    }
}
