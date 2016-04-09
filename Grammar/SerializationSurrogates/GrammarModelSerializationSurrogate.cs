using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.Grammar.SerializationSurrogates
{
	/// <summary>
	/// Serialization surrogate for <see cref="GrammarModel"/> of a <see cref="LanguageProvider"/>.
	/// The <see cref="StreamingContext.Context"/> is expected to contain the <see cref="LanguageProvider"/>.
	/// </summary>
	public class GrammarModelSerializationSurrogate : ISerializationSurrogate
	{
		#region ISerializationSurrogate Members

		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj != ((LanguageProvider)context.Context).GrammarModel)
				throw new SerializationException("The serialized GrammarModel does not belong to the LanguageProvider passed in StreamingContext.");
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return ((LanguageProvider)context.Context).GrammarModel;
		}

		#endregion
	}
}
