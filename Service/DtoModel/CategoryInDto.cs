using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.DtoModel
{
    public class CategoryInDto
    {
        [MaxLength(50)]
        [Required()]
        [DisplayName("Nazwa nowej kategorii")]
        public string Name { get; set; }
    }
}
