using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.Grammar.SerializationSurrogates
{
	/// <summary>
	/// <see cref="InflectionType"/> serialization surrogate for replacing deserialized
	/// instances with the ones obtained through <see cref="GrammarModel"/> of a <see cref="LanguageProvider"/>.
	/// The <see cref="StreamingContext.Context"/> is expected to contain the <see cref="LanguageProvider"/>.
	/// </summary>
	public class InflectionTypeSerializationSurrogate : ISerializationSurrogate
	{
		#region ISerializationSurrogate Members

		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			if (info == null) throw new ArgumentNullException("info");

			var inflectionType = (InflectionType)obj;

			info.AddValue("key", inflectionType.Key);
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			if (info == null) throw new ArgumentNullException("info");

			try
			{
				var languageProvider = (LanguageProvider)context.Context;

				var grammarModel = languageProvider.GrammarModel;

				var inflectionTypeKey = info.GetString("key");

				return grammarModel.InflectionTypes[inflectionTypeKey];
			}
			catch (KeyNotFoundException ex)
			{
				throw new SerializationException("The deserialized grammar model does not match the existing grammar model.", ex);
			}
		}

		#endregion
	}
}
