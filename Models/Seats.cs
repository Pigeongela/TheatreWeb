using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Места")]
    public class Seats
    {
        [Key]
        [Column("id_места")]
        public int Id { get; set; }

        [ForeignKey("Halls")]
        [Column("Зал")]
        [Required]
        [Display(Name = "Зал")] public int? HallId { get; set; }

        public Halls Halls { get; set; }

        [Column("Уровень", TypeName = "varchar(30)")]
        [Required]
        [Display(Name = "Уровень")] public string Level { get; set; }

        [Column("Ряд")]
        [Required]
        [Display(Name = "Ряд")] public int Row { get; set; }

        [Column("Номер")]
        [Required]
        [Display(Name = "Номер")] public int Number { get; set; }

        [Column("Занято")]
        [Required]
        [Display(Name = "Забронировать")] public bool Status { get; set; }

        [Column("Цена")]
        [Required]
        [Display(Name = "Цена")] public double CostCoefficient { get; set; }
    }
}
