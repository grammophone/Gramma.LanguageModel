using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision
{
	/// <summary>
	/// Exception for the system of the language providers.
	/// </summary>
	[Serializable]
	public class ProvisionException : Exception
	{
		public ProvisionException(string message) : base(message) { }
		public ProvisionException(string message, Exception inner) : base(message, inner) { }
		protected ProvisionException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
