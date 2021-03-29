using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blaz_Wasm_ForTest.Pages
{
	public interface ILoginLogic
	{
		bool SignIn(string email, string password);
	}
}
