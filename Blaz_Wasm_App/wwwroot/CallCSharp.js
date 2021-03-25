// Creat an object that is scopped insode the 'window' object
// DotNet.invokeMethodAsync("Assembly-Name", "MethodName");

window.callCSharpMethods = {
	displayMessage: function () {
		DotNet.invokeMethodAsync("Blaz_Wasm_App", "Display")
			.then((message) => {
				alert(`Message Received from CSharp ${message}`);
			});
	},

	displayMessageCustomName: function () {
		DotNet.invokeMethodAsync("Blaz_Wasm_App", "showMessage")
			.then((message) => {
				alert(`Message Received from CSharp ${message}`);
			});
	},

	addValues: function () {
		DotNet.invokeMethodAsync("Blaz_Wasm_App", "add", 90,76)
			.then((message) => {
				alert(`Data Received from CSharp after add = ${message}`);
			});
	},

	getDataFromAJAX: function () {
		DotNet.invokeMethodAsync("Blaz_Wasm_App", "getProducts")
			.then((message) => {
				alert(`Data Received from CSharp after AJAX Call= ${JSON.stringify(message)}`);
			});
	}

};