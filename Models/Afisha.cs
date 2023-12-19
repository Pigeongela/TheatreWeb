using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Афиша")]
    public class Afisha
    {
        [Key]
        [Column("id_афиши")]
        public int Id { get; set; }

        [Required]
        [Column("Дата_Время_начала", TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата и Время начала")] public DateTime Date { get; set; }

        [Required]
        [Column("День_недели", TypeName = "varchar(13)")]
        [Display(Name = "День недели")] public string DayOfWeek { get; set; }
        
        [Column("Примечание", TypeName = "varchar(50)")]
        [Display(Name = "Примечание")] public string Note { get; set; }

        [ForeignKey("Performance")]
        [Column("Спектакль")]
        [Display(Name = "Спектакль")] public int? Spectacle { get; set; }

        public Performances Performance { get; set; }

        [Required]
        [ForeignKey("Halls")]
        [Column("Зал")]
        [Display(Name = "Зал")] public int? Hall { get; set; }
        public Halls Halls { get; set; }

        [Column("Базовая_стоимость")]
        [Display(Name = "Базовая стоимость")] public double BaseCost { get; set; }
        
        [Required]
        [Column("Флаг_архивации")]
        [Display(Name = "Флаг архивации")] public bool IsArchived { get; set; }
    }
}


