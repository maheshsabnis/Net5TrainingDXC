using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blaz_Wasm_App
{
	/// <summary>
	/// The App State Container Object
	/// This object will be responsible to maintain the state
	/// of data across components 
	/// This will have a  public property that will be used to store data send by the sender component
	/// This will have an event that will be raised when the sender send data and update its state(?)
	/// The 'STATE' means the value that is explicitely updated by one component so that other subscriber components
	/// can receive it
	/// 
	/// IMP: Make this object as a Singletone object at application level so that
	/// all components can access it.
	/// </summary>
	public class AppStateContainer
	{
		// a public state property
		public int ValueState { get; set; } = 0;
		// an event that will be raised when the state is changed
		public event Action OnStateChange;
		/// <summary>
		/// The methd that will be used by the component(s) to update the state with
		/// new value
		/// </summary>
		/// <param name="v"></param>
		public void SetValueState(int v)
		{
			// overwrite the old value with new value
			ValueState = v;
			// notify that the state is changed
			NotifyStateChanged();
		}

		private void NotifyStateChanged()=> OnStateChange?.Invoke();
	}
}
