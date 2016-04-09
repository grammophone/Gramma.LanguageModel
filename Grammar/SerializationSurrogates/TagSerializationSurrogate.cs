using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Grammophone.GenericContentModel;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.Grammar.SerializationSurrogates
{
	/// <summary>
	/// Serialization surrogate for <see cref="Tag"/>
	/// belonging to a specified <see cref="GrammarModel"/> of a <see cref="LanguageProvider"/>.
	/// The <see cref="StreamingContext.Context"/> is expected to contain the <see cref="LanguageProvider"/>.
	/// </summary>
	public class TagSerializationSurrogate : ISerializationSurrogate
	{
		#region ISerializationSurrogate Members

		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var tag = (Tag)obj;

			info.AddValue("type", tag.Type);
			info.AddValue("inflections", tag.Inflections.ToArray());
			info.AddValue("discriminant", tag.Discriminant);
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			var type = (TagType)info.GetValue("type", typeof(TagType));
			var inflections = (Inflection[])info.GetValue("inflections", typeof(Inflection[]));
			var discriminant = info.GetString("discriminant");

			var grammarModel = ((LanguageProvider)context.Context).GrammarModel;

			return grammarModel.GetTag(type, inflections, discriminant);
		}

		#endregion
	}
}
