using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Спектакли")]
    public class Performances
    {
        [Key]
        [Column("id_спектакля")]
        public int Id { get; set; }

        [Column("Название_спектакля", TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "Название")] public string Title { get; set; }

        [Column("Описание", TypeName = "varchar(MAX)")]
        [Required]
        [Display(Name = "Описание")] public string Description { get; set; }

        [Column("Продолжительность", TypeName = "time(7)")]
        [Required]
        [Display(Name = "Продолжительность")] public TimeSpan Duration { get; set; }
        
        [Column("Картинка", TypeName = "varbinary(MAX)")]
        [Display(Name = "Картинка")] public byte[] Image { get; set; }

        [Required]
        [Column("Флаг_архивации")]
        [Display(Name = "Флаг архивации")] public bool IsArchived { get; set; }

        public virtual ICollection<Afisha> Afisha { get; set; }
    }
}
