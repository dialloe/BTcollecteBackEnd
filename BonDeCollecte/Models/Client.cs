using System.ComponentModel.DataAnnotations;

namespace BonDeCollecte.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Ville { get; set; }
        public string? Photo { get; set; }
        public byte[]? PhotoData { get; set; }
        public string? Adresse { get; set; }
    }
}
