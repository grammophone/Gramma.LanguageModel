using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Grammophone.LanguageModel.Provision
{
	/// <summary>
	/// Specifies delimiters of sentences and breaks them into words or punctuation.
	/// </summary>
	public abstract class SentenceBreaker : ReadOnlyLanguageFacet
	{
		#region Private fields

		private ISet<char> sentenceDelimiters;

		private ISet<char> punctuationCharacters;

		private Regex wordBreakingRegex;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public SentenceBreaker(LanguageProvider languageProvider)
			: base(languageProvider)
		{
			var sentenceDelimiters = this.GetSentenceDelimiters();

			if (sentenceDelimiters == null) throw new ProvisionException("GetSentenceDelimiters should not return null.");
			if (sentenceDelimiters.Length == 0) throw new ProvisionException("GetSentenceDelimiters should not return rempty array.");

			this.sentenceDelimiters = new HashSet<char>(sentenceDelimiters);
			
			var wordDelimiters = this.GetWordDelimiters();

			if (wordDelimiters == null) throw new ProvisionException("GetWordDelimiters should not return null.");
			if (wordDelimiters.Length == 0) throw new ProvisionException("GetWordDelimiters should not return empty array.");

			var punctuationCharacters = this.GetPunctuationCharacters();
			if (punctuationCharacters == null) throw new ProvisionException("GetPunctuationCharacters should not return null.");
			this.punctuationCharacters = new HashSet<char>(punctuationCharacters);

			string wordBreakExpression = String.Format(
				@"[ \r\n]*([{0}]+)[ \r\n]*|[{1}]+", 
				punctuationCharacters.Replace("-", "\\-").Replace("\r", @"\r").Replace("\n", @"\n"),
				wordDelimiters.Replace("-", "\\-").Replace("\r", @"\r").Replace("\n", @"\n"));

			this.wordBreakingRegex = new Regex(wordBreakExpression, RegexOptions.Compiled | RegexOptions.Singleline);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Break a sentence to its words and punctuation marks.
		/// </summary>
		/// <param name="sentence">The sentence to break.</param>
		/// <returns>Returns the words and the punctuation marks of the sentence.</returns>
		/// <remarks>
		/// The default implementation uses the characters specified in 
		/// <see cref="GetWordDelimiters"/> and <see cref="GetPunctuationCharacters"/> methods.
		/// </remarks>
		public virtual string[] Break(string sentence)
		{
			if (sentence == null) throw new ArgumentNullException("sentence");

			var words = wordBreakingRegex.Split(sentence);

			var filteredWords = new List<string>(words.Length);

			for (int i = 0; i < words.Length; i++)
			{
				var word = words[i].Trim(' ', '\r', '\n');

				if (word.Length == 0) continue;

				// Translate three consecutive stops.
				if (word.Contains("...")) word = "…";

				filteredWords.Add(word);
			}

			return filteredWords.ToArray();
		}

		/// <summary>
		/// Tests whether a character is a sentence delimiter.
		/// </summary>
		/// <param name="c">The character to test.</param>
		/// <returns>Returns true if the character is a sentence delimiter.</returns>
		/// <remarks>
		/// The default implementation uses the characters specified in <see cref="GetSentenceDelimiters"/> method.
		/// </remarks>
		public virtual bool IsSentenceDelimiter(char c)
		{
			return sentenceDelimiters.Contains(c);
		}

		/// <summary>
		/// Tests whether a word consists entirely of punctuation characters.
		/// </summary>
		/// <param name="word">The word to test.</param>
		/// <returns>Returns true if the word consists entirely of punctuation characters.</returns>
		/// <remarks>
		/// The default implementation uses the <see cref="IsPunctuation(char)"/> method.
		/// </remarks>
		public virtual bool IsPunctuation(string word)
		{
			return word.All(c => IsPunctuation(c));
		}

		/// <summary>
		/// Tests whether a character is a punctuation character.
		/// </summary>
		/// <param name="c">The word to test.</param>
		/// <returns>Returns true if the character is a punctuation character.</returns>
		/// <remarks>
		/// The default implementation uses the characters specified in <see cref="GetPunctuationCharacters"/> method.
		/// </remarks>
		public virtual bool IsPunctuation(char c)
		{
			return punctuationCharacters.Contains(c);
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Specify the end of sentence punctuatuion characters.
		/// </summary>
		/// <returns></returns>
		protected abstract string GetSentenceDelimiters();

		/// <summary>
		/// Specify the word delimiter characters. When spliting a sentence, 
		/// these characters will be omitted from the resulting array.
		/// </summary>
		protected abstract string GetWordDelimiters();

		/// <summary>
		/// Specify the word punctuation characters. When spliting a sentence, 
		/// these characters will be included in the resulting array.
		/// </summary>
		protected abstract string GetPunctuationCharacters();

		#endregion
	}
}
