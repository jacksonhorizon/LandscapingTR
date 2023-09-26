using System.ComponentModel.DataAnnotations;

namespace LandscapingTR.Core.Entities
{

    public class BaseEntity<TKey>
    {
        [Key]
        [Required]
        public TKey Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
