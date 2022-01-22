using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class Edition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }       
        public string Publisher { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
