using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Льготы")]
    public class Privilege
    {
        [Key]
        [Column("id_льготы")]
        public int Id { get; set; }

        [Column("Тип_льготы", TypeName = "varchar(50)")]
        [Required]
        public string PrivilegeType { get; set; }

        [Column("Размер_скидки")]
        [Required]
        public int DiscountSize { get; set; }

        [Column("Коэффициент_скидки")]
        [Required]
        public float DiscountCoefficient { get; set; }

        [Column("Флаг_архивации")]
        [Required]
        public bool IsArchived { get; set; }
    }
}
