using AmberArchives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public interface IAccountService
	{
		bool Register(RegisterUserDto dto);
		string GenerateJwt(LoginDto dto);
		bool Update(ModifyUserDto dto);
	}
}
