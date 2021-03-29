using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blaz_Wasm_ForTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bunit;
using Moq;
using NuGet.Frameworks;
using Microsoft.Extensions.DependencyInjection;

namespace Blaz_Wasm_ForTest.Pages.Tests
{
	[TestClass()]
	public class LoginTests
	{
		// define the pre-requisites

		private Bunit.TestContext testContext;
		private Moq.Mock<ILoginLogic> mockLoginLogic; 
		public LoginTests()
		{
			testContext = new Bunit.TestContext();
			mockLoginLogic = new Mock<ILoginLogic>();
		}


		[TestMethod()]
		public void SignInTest_No_Email_Password()
		{
			// mock the DI Service by creating a FakeObject
			testContext.Services.AddScoped(m=> mockLoginLogic.Object);
			// render the component in the Memory
			// this will also provide an access to the DOM RenderTree in Memoty
			var component = testContext.RenderComponent<Login>();
			// Assert the HTML
			NUnit.Framework.Assert.IsTrue(component.Markup.Contains("<h1>Hello, world!</h1>"));
			// Check if there exist the button
			// Query to DOM to check if buttons are exists and retriev all buttons
			var buttons = component.FindAll("button");
			// check if only one button is present
			NUnit.Framework.Assert.AreEqual(1, buttons.Count);
			// check if the button is submit button
			var submit = buttons.FirstOrDefault(btn=>btn.OuterHtml.Contains("Submit"));
			// assert
			NUnit.Framework.Assert.IsNotNull(submit);
			// Raise the event
			submit.Click();
			// check for the not valid email and password passed to the method
			mockLoginLogic.Verify(logic=>logic.SignIn(It.IsAny<string>(),  It.IsAny<string>()), Times.Never);

			// serach with div tag with class as alert
			// use te class selector i.e. '.' (dot)
			var dvalert = component.Find("div.alert");
			// check if the div contains 'Email/Password Invalid' message
			NUnit.Framework.Assert.AreEqual("Email/Password Invalid", dvalert.InnerHtml);
		}


		[TestMethod()]
		public void SignInTest_With_Email_Password()
		{
			// mock the DI Service by creating a FakeObject
			testContext.Services.AddScoped(m => mockLoginLogic.Object);
			// render the component in the Memory
			// this will also provide an access to the DOM RenderTree in Memoty
			var component = testContext.RenderComponent<Login>();
			 
			// Check if there exist the button
			// Query to DOM to check if buttons are exists and retriev all buttons
			var buttons = component.FindAll("button");
			// check if only one button is present
			NUnit.Framework.Assert.AreEqual(1, buttons.Count);
			// check if the button is submit button
			var submit = buttons.FirstOrDefault(btn => btn.OuterHtml.Contains("Submit"));

			// accessing the TextBoxes and set values for them

			var emailInput = component.Find("#agentEmail");
			// set the value for it by raising the change event
			emailInput.Change("mahesh");
			var passwordInput = component.Find("#agentPassword");
			// set the value for it by raising the change event
			passwordInput.Change("mahesh");
			// access the signin for returning the false for Invalid Email and passwordf

			mockLoginLogic.Setup(logic => logic.SignIn(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

			// Raise the event
			submit.Click();
			// verify the condition for setting Email and Password at once
			// this will access IsEmailValid() and IsPasswordValid() with a string data at once
			mockLoginLogic.Verify(logic => logic.SignIn(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			// serach with div tag with class as alert
			// use te class selector i.e. '.' (dot)
			var dvalert = component.Find("div.alert");
			// check if the div contains 'Email/Password Invalid' message
			NUnit.Framework.Assert.AreEqual("Invalid Object", dvalert.InnerHtml);
		}
	}
}