using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Актеры")]
    public class Actors
    {
        [Key]
        [Column("id_актера")]
        public int Id { get; set; }

        [Column("ФИО", TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "ФИО")] public string FullName { get; set; }

        [Column("Фото", TypeName = "varbinary(MAX)")]
        [Display(Name = "Фото")] public byte[] Photo { get; set; }

        [Column("Биография", TypeName = "varchar(MAX)")]
        [Display(Name = "Биография")] public string Biography { get; set; }

        [Column("Флаг_удаления")]
        [Required]
        [Display(Name = "Флаг удаления")] public bool IsDeleted { get; set; }
    }
}
