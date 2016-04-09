using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.Grammar.SerializationSurrogates
{
	/// <summary>
	/// <see cref="Inflection"/> serialization surrogate for replacing deserialized
	/// instances with the ones obtained through <see cref="GrammarModel"/> of a <see cref="LanguageProvider"/>.
	/// The <see cref="StreamingContext.Context"/> is expected to contain the <see cref="LanguageProvider"/>.
	/// </summary>
	public class InflectionSerializationSurrogate : ISerializationSurrogate
	{
		#region ISerializationSurrogate Members

		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			if (info == null) throw new ArgumentNullException("info");

			var inflection = (Inflection)obj;

			info.AddValue("typeKey", inflection.Type.Key);
			info.AddValue("key", inflection.Key);
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			if (info == null) throw new ArgumentNullException("info");

			try
			{
				var languageProvider = (LanguageProvider)context.Context;

				var grammarModel = languageProvider.GrammarModel;

				var inflectionTypeKey = info.GetString("typeKey");
				var inflectionKey = info.GetString("key");

				var inflectionType = grammarModel.InflectionTypes[inflectionTypeKey];

				return inflectionType.Inflections[inflectionKey];
			}
			catch (KeyNotFoundException ex)
			{
				throw new SerializationException("The deserialized grammar model does not match the existing grammar model.", ex);
			}
		}

		#endregion
	}
}
