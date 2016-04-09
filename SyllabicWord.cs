using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel
{
	/// <summary>
	/// A word represented with syllables.
	/// </summary>
	/// <remarks>
	/// Supports fast equality checks and it is suitable for hashtables.
	/// </remarks>
	[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	[Serializable]
	public class SyllabicWord : EquatableReadOnlySequence<string>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="syllables">The syllables of the word.</param>
		public SyllabicWord(string[] syllables)
			: base(syllables)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="syllables">The syllables of the word.</param>
		public SyllabicWord(ICollection<string> syllables)
			: base(syllables)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="syllables">The syllables of the word.</param>
		public SyllabicWord(IEnumerable<string> syllables)
			: base(syllables)
		{

		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns a representation of the syllables, in reverse order.
		/// </summary>
		/// <remarks>
		/// Reversed syllable order is more common among language providers,
		/// because most languages have heavier inflection towards the suffix.
		/// </remarks>
		public override string ToString()
		{
			var builder = new StringBuilder();

			for (int i = this.Count - 1; i >= 0; i--)
			{
				var syllable = this[i];

				builder.Append(syllable);
				builder.Append(' ');
			}

			return builder.ToString();
		}

		#endregion
	}
}
