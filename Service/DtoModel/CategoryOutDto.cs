using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel
{
    public class CategoryOutDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required()]
        [DisplayName("Nazwa Kategorii")]
        public string Name { get; set; }

        public ICollection<MovieOutDto> Movies { get; set; }
    }
}
