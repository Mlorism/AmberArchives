using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class BookQuery
    {
		public string SearchPhraze { get; set; }
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
		public string SortBy { get; set; }
		public SortDirectionEnum SortDirection { get; set; }
	}
}
