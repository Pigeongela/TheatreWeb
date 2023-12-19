using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreWeb.Models
{
    [Table("Заказы")]
    public class Order
    {
        [Key]
        [Column("id_заказа")]
        public int Id { get; set; }

        [Column("Номер_заказа", TypeName = "char(30)")]
        [Required]
        public string OrderNumber { get; set; }

        [Column("Данные_покупателя")]
        public int CustomerData { get; set; }

        [Column("Место")]
        [Required]
        public int Seat { get; set; }

        [Column("id_афиши")]
        [Required]
        public int AfishaId { get; set; }

        [Column("Статус", TypeName = "varchar(20)")]
        [Required]
        public string Status { get; set; }

        [Column("Льгота")]
        public int? Privilege { get; set; }

        [Column("Размер_скидки")]
        public int? DiscountSize { get; set; }

        [Column("Квитанция_об_оплате", TypeName = "char(18)")]
        public string PaymentReceipt { get; set; }

        [Column("Электронный_билет", TypeName = "image")]
        public byte[] ElectronicTicket { get; set; }

        [Column("Флаг_архивации")]
        [Required]
        public bool IsArchived { get; set; }

        [Column("Заказ_создан")]
        public DateTime? OrderCreated { get; set; }
    }
}
