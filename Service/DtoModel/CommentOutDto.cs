using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel
{
    public class CommentOutDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [DisplayName("Nazwa użytkownika")]
        public string UserName { get; set; }

        [DisplayName("Ocena")]
        public decimal Rating { get; set; }

        [DisplayName("Ostatnia modyfikacja")]
        public DateTime LastModified { get; set; }

        [Required()]
        [DisplayName("Treść")]
        public string Content { get; set; }

        [Required()]
        public int MovieId { get; set; }
    }
}
