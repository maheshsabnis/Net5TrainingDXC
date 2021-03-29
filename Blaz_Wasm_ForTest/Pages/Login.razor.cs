using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blaz_Wasm_ForTest.Pages
{
	/// <summary>
	/// Login for The Login Component Goes Here
	/// The class is partial class so that it will be linked with component as Code Behind
	/// Note: The Name of the Code file will be match with the Component file name
	/// e.g. Login.razor will have code behind as login.razor.cs
	/// </summary>
	public partial class Login
	{
		// C# 9.0 feature of creating instance of the class mentioned in LHS
		private LoginModel login = new();

		// inject then LoginLogic Service, this isn same as @inject directive
		[Inject]
		private ILoginLogic loginLogic { get; set; }

		private string errorMessage = string.Empty;

		private bool IsEmailValid()
		{
			return !string.IsNullOrEmpty(login.Email);
		}

		private bool IsPasswordValid()
		{
			return !string.IsNullOrEmpty(login.Password);
		}

		public void SignIn()
		{
			if (IsEmailValid() && IsPasswordValid())
			{
				if (!loginLogic.SignIn(login.Email, login.Password))
				{
					errorMessage = "Invalid Object";
				}
			}
			else
			{
				errorMessage = "Email/Password Invalid";
			}
		}

	}
}
