using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class Edition : ModUserEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
