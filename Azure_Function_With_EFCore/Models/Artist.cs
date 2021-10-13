using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azure_Function_With_EFCore.Models
{
    [Table("Artista")]
    public class Artist
    {
        [Key]
        public int ArtistaID { get; set; }
        public string Nombre { get; set; }
    }
}
