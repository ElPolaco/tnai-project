using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel
{
    public class CommentInDto
    {
        public string UserId { get; set; }
        [Required()]
        public int MovieId { get; set;}

        [Required]
        [DisplayName("Ocena")]
        public decimal Rating { get; set; }

        [MaxLength(2000)]
        [Required()]
        public string Content { get; set; }
    }
}
