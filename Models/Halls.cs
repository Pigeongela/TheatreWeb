using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Залы")]
    public class Halls
    {
        [Key]
        [Column("id_зала")]
        public int Id { get; set; }

        [Column("Название_зала", TypeName = "varchar(30)")]
        [Required]
        [Display(Name = "Название зала")] public string HallName { get; set; }

        [Column("Схема", TypeName = "varbinary(MAX)")]
        [Display(Name = "Схема")] public byte[] Schema { get; set; }

        [Column("Коэффициент_стоимости_зал")]
        [Required]
        [Display(Name = "Коэффициент стоимости")] public double CostCoefficient { get; set; }

        public virtual ICollection<Seats> Seats { get; set; }
        public virtual ICollection<Afisha> Afisha { get; set; }
    }
}
