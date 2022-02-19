using AmberArchives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services.Interfaces
{
    public interface IShelfService
    {
        List<ShelfDto> GetShelfs();
    }
}
