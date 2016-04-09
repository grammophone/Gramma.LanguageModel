using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Grammophone.GenericContentModel;
using Grammophone.LanguageModel.Grammar;

namespace Grammophone.LanguageModel.Provision
{
	/// <summary>
	/// A contract for providing resources for processing text of a language.
	/// </summary>
	public abstract class LanguageProvider : IKeyedElement<string>
	{
		#region Private fields

		private Lazy<GrammarModel> lazyGrammarModel;

		private Lazy<Tag> lazyStartTag;

		private Lazy<Tag> lazyEndTag;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public LanguageProvider()
		{
			lazyGrammarModel = 
				new Lazy<GrammarModel>(CreateGrammarModel, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

			lazyStartTag =
				new Lazy<Tag>(GetStartTag, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

			lazyEndTag =
				new Lazy<Tag>(GetEndTag, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The grammar model of the language.
		/// </summary>
		public GrammarModel GrammarModel
		{
			get
			{
				return lazyGrammarModel.Value;
			}
		}

		/// <summary>
		/// The special start-of-sentence tag.
		/// </summary>
		public Tag StartTag
		{
			get
			{
				return lazyStartTag.Value;
			}
		}

		/// <summary>
		/// The special end-of-sentence tag.
		/// </summary>
		public Tag EndTag
		{
			get
			{
				return lazyEndTag.Value;
			}
		}

		/// <summary>
		/// The friendly name of the language.
		/// </summary>
		public abstract string LanguageName { get; }

		/// <summary>
		/// The identifier of the language.
		/// </summary>
		public abstract string LanguageKey { get; }

		/// <summary>
		/// Processor for breaking, reassembling and comparing word syllables according to the language.
		/// </summary>
		/// <remarks>
		/// Syllable breaking doesn't necessarily follow the official grammar rules,
		/// but may follow a machine-friendly strategy instead.
		/// </remarks>
		public abstract Syllabizer Syllabizer { get; }

		/// <summary>
		/// Processor for finding delimiters of sentences and breaking into words,
		/// appropriate to the language.
		/// </summary>
		public abstract SentenceBreaker SentenceBreaker { get; }

		#endregion

		#region Public methods

		/// <summary>
		/// Normalize a word.
		/// </summary>
		/// <param name="word">The word to normalize.</param>
		/// <returns>Returns the normalized word.</returns>
		public abstract string NormalizeWord(string word);

		#endregion

		#region Protected methods

		/// <summary>
		/// Create a fresh, non-cached <see cref="InflectionType"/>.
		/// </summary>
		/// <param name="key">The inflection type key.</param>
		/// <param name="name">The inflection type friendly name.</param>
		/// <param name="inflections">The set of inflections of this type or null if empty.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> 
		/// are null.
		/// </exception>
		/// <remarks>
		/// This is intended to be used for the definitions provided by <see cref="CreateGrammarModel"/>.
		/// </remarks>
		protected InflectionType CreateInflectionType(string key, string name, IEnumerable<Inflection> inflections)
		{
			return new InflectionType(key, name, inflections);
		}

		/// <summary>
		/// Create a fresh, non-cached <see cref="TagType"/>.
		/// </summary>
		/// <param name="key">The tag type key.</param>
		/// <param name="name">The tag type friendly name.</param>
		/// <param name="areTagsUnrelated">
		/// If true, every <see cref="Tag"/> of this <see cref="TagType"/> is unrelated against each other.
		/// Example, when the <see cref="TagType"/> signals a 'Conjunction' and the <see cref="Tag"/>s of it represent the specific conjunctions.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		/// <remarks>
		/// This is intended to be used for the definitions provided by <see cref="CreateGrammarModel"/>.
		/// </remarks>
		protected TagType CreateTagType(string key, string name, bool areTagsUnrelated)
		{
			return new TagType(key, name, areTagsUnrelated);
		}

		/// <summary>
		/// Create a fresh, non-cached <see cref="Inflection"/>.
		/// </summary>
		/// <param name="key">The inflection's key.</param>
		/// <param name="name">The inflection's friendly name.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		/// <remarks>
		/// This is intended to be used for the definitions provided by <see cref="CreateGrammarModel"/>.
		/// </remarks>
		protected Inflection CreateInflection(string key, string name)
		{
			return new Inflection(key, name);
		}

		/// <summary>
		/// This method is called just-in-time upon the first invokation of
		/// <see cref="LanguageProvider.GrammarModel"/> property.
		/// </summary>
		/// <returns>Returns a newly created object graph of he language's grammar model.</returns>
		protected abstract GrammarModel CreateGrammarModel();

		/// <summary>
		/// Return the start-of-sentence special marker tag, excluding the start and end tag.
		/// </summary>
		/// <remarks>
		/// Must not call <see cref="CreateGrammarModel"/> to reference the grammar model.
		/// Use the property <see cref="GrammarModel"/> instead.
		/// </remarks>
		protected abstract Tag GetStartTag();

		/// <summary>
		/// Return the end-of-sentence special marker tag.
		/// </summary>
		/// <remarks>
		/// Must not call <see cref="CreateGrammarModel"/> to reference the grammar model.
		/// Use the property <see cref="GrammarModel"/> instead.
		/// </remarks>
		protected abstract Tag GetEndTag();

		/// <summary>
		/// Get an array of combinations of inflections.
		/// </summary>
		/// <param name="inflectionTypes">The inflection types which provide the inflections to combine.</param>
		/// <returns>
		/// Returns an array of combinations of <see cref="Inflection"/>s.
		/// </returns>
		protected Inflection[][] GetInflectionCombinations(IEnumerable<InflectionType> inflectionTypes)
		{
			if (inflectionTypes == null) throw new ArgumentNullException("inflectionTypes");

			// Base condition of recursion.
			if (!inflectionTypes.Any())
			{
				return new Inflection[][] { new Inflection[0] };
			}

			// Recurse.
			var restInflectionsCombinations = GetInflectionCombinations(inflectionTypes.Skip(1));

			var firstInflectionType = inflectionTypes.First();

			Inflection[][] inflectionCombinations = new Inflection[firstInflectionType.Inflections.Count * restInflectionsCombinations.Length][];

			int j = 0;

			foreach (var firstInflection in firstInflectionType.Inflections)
			{
				for (int i = 0; i < restInflectionsCombinations.Length; i++)
				{
					var inflectionsCombinationSuffix = restInflectionsCombinations[i];

					var inflectionsCombination = new Inflection[inflectionsCombinationSuffix.Length + 1];

					inflectionsCombination[0] = firstInflection;

					inflectionsCombinationSuffix.CopyTo(inflectionsCombination, 1);

					inflectionCombinations[j++] = inflectionsCombination;
				}
			}

			return inflectionCombinations;
		}

		#endregion

		#region IKeyedElement<string> Members

		/// <summary>
		/// Reverts to <see cref="LanguageKey"/> property.
		/// </summary>
		string IKeyedElement<string>.Key
		{
			get { return this.LanguageKey; }
		}

		#endregion
	}
}
