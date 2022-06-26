using Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public decimal Rating { get; set; }

        public DateTime LastModified { get; set; }

        public virtual ApplicationUser User {get; set;}
        public string UserId { get; set; }

        public virtual Movie Movie { get; set; }
        public int MovieId { get; set; }

        
    }
}
