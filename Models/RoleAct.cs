using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Роли")]
    public class RoleAct
    {
        [Key]
        [Column("id_роли")]
        public int Id { get; set; }

        [Column("Актер")]
        [Required]
        public int ActorId { get; set; }

        [Column("Спектакль")]
        [Required]
        public int PlayId { get; set; }

        [Column("Название_роли", TypeName = "varchar(50)")]
        [Required]
        public string RoleName { get; set; }

        [Column("Флаг_архивации")]
        [Required]
        public bool IsArchived { get; set; }
    }
}
