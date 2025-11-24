using System.ComponentModel.DataAnnotations;

namespace BonDeCollecte.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public string Password { get; set; }

        /*
         string? password = Console.ReadLine();

        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = "test45484454545454545".GetBytes(); // divide by 8 to convert bits to bytes
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password!,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8)); */ 


    }
}
