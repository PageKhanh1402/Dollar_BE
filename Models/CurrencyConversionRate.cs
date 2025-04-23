using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class CurrencyConversionRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateID { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal VNDtoXuRate { get; set; }

        public DateTime EffectiveFrom { get; set; } = DateTime.Now;

        public int SetByUserID { get; set; }

        // Navigation property
        [ForeignKey("SetByUserID")]
        public virtual User SetByUser { get; set; }
    }
}
