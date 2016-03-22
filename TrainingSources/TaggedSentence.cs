using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Represents a sentence of fully tagged and lemmatized words,
	/// not including the special START and END markers
	/// </summary>
	[Serializable]
	public class TaggedSentence
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="wordForms">The words in the sentence, not including the special START and END markers.</param>
		public TaggedSentence(TaggedWordForm[] wordForms)
		{
			if (wordForms == null) throw new ArgumentNullException("wordForms");

			this.WordForms = wordForms;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The words in the sentence, not including the special START and END markers.
		/// </summary>
		public TaggedWordForm[] WordForms { get; private set; }

		#endregion

		#region Public methods

		public override string ToString()
		{
			var builder = new StringBuilder(this.WordForms.Length * 80);

			foreach (var wordForm in this.WordForms)
			{
				builder.AppendLine(
					String.Format("{0} ({1}) {2} ", 
						wordForm.Text, 
						wordForm.Lemma, 
						wordForm.Tag.ToString().Replace('\r', ' ').Replace("\n", String.Empty)
					)
				);
			}

			return builder.ToString();
		}

		#endregion
	}
}
