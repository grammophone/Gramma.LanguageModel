using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.LanguageModel.Grammar;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Represents a possibly inflected form of a word, annotated
	/// with its lemma and its part-of-speech-and-inflection tag.
	/// </summary>
	[Serializable]
	public class TaggedWordForm
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="text">The word's form text.</param>
		/// <param name="lemma">The lemma of this word form.</param>
		/// <param name="tag">The tag of this word form.</param>
		public TaggedWordForm(string text, string lemma, Tag tag)
		{
			if (text == null) throw new ArgumentNullException("text");
			if (lemma == null) throw new ArgumentNullException("lemma");
			if (tag == null) throw new ArgumentNullException("tag");

			this.Text = text;
			this.Lemma = lemma;
			this.Tag = tag;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The word's form text.
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// The lemma of this word form.
		/// </summary>
		public string Lemma { get; private set; }

		/// <summary>
		/// The tag of this word form.
		/// </summary>
		public Tag Tag { get; private set; }

		#endregion
	}
}
