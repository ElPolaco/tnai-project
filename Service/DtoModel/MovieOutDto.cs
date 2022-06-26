
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.DtoModel
{
    public class MovieOutDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Nazwa filmu")]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("Czas seasnu")]
        public string MovieTime { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Reżyser")]
        public string Director { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data premiery")]
        public DateTime Premiere { get; set; }

        [Required]
        [MaxLength(2000)]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [DisplayName("Nazwa kategorii")]
        public string CategoryName { get; set; }

        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }

        public ICollection<CommentOutDto> Comments { get; set; }
    }
}
