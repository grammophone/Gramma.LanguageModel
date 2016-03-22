using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.LanguageModel.Provision.EditCommands;

namespace Gramma.LanguageModel.Provision
{
	/// <summary>
	/// Contract for breaking, reassembling and compring word syllables.
	/// </summary>
	public abstract class Syllabizer : ReadOnlyLanguageFacet
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public Syllabizer(LanguageProvider languageProvider)
			: base(languageProvider)
		{

		}

		#endregion

		#region Public methods

		/// <summary>
		/// Normalize and break a word into its syllables.
		/// </summary>
		/// <param name="word">The word to be segmented.</param>
		/// <returns>
		/// Returns the word as a sequence of its syllables. The last syllable must always be a 'sentinel'
		/// syllable that never occurs in any word.
		/// The order of the syllables may be inverse, and it is advised to be inverse when
		/// the inflection happens mostly at the suffix of a word for the language.
		/// </returns>
		/// <remarks>
		/// The rules for segmentation do not respect the official rules of the grammar
		/// but follow the strategy for better machine understanding instead.
		/// The order of the syllables may be inverse, and it is advised to be inverse when
		/// the inflection happens mostly at the suffix of a word for the language.
		/// </remarks>
		public abstract SyllabicWord Segment(string word);

		/// <summary>
		/// Reassemble a word's syllables into a single string.
		/// </summary>
		/// <param name="word">The syllabic word to reassemble.</param>
		/// <returns>Returns the word as a character string.</returns>
		public abstract string Reassemble(SyllabicWord word);

		/// <summary>
		/// Get the generalized edit distance between two syllables.
		/// </summary>
		/// <param name="baseSyllable">The base syllable.</param>
		/// <param name="targetSyllable">The compared syllable.</param>
		/// <returns>
		/// Returns a command which, when executed upon the <paramref name="baseSyllable"/>,
		/// yields the <paramref name="targetSyllable"/> as a result.
		/// </returns>
		public abstract ReplaceCommand GetDistance(
			string baseSyllable,
			string targetSyllable);

		#endregion
	}
}
