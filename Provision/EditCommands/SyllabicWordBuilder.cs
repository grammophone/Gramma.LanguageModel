using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// <see cref="SyllabicWord"/> building facility during execution 
	/// of <see cref="EditCommand"/>s against a source word.
	/// It holds the state of transforming a source word into a target word.
	/// </summary>
	public class SyllabicWordBuilder
	{
		#region Private fields

		private List<string> target;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="input">The input word to be transfrormed.</param>
		public SyllabicWordBuilder(SyllabicWord input)
		{
			if (input == null) throw new ArgumentNullException("input");

			this.Source = input;
			this.target = new List<string>(input.Count * 2);
			this.CurrentSourceIndex = -1;
		}

		#endregion

		#region Internal properties

		/// <summary>
		/// The index in the <see cref="Source"/> word, before execution of a command.
		/// </summary>
		internal int CurrentSourceIndex { get; private set; }

		/// <summary>
		/// The input word.
		/// </summary>
		internal SyllabicWord Source { get; private set; }

		/// <summary>
		/// Returns true if the source index is advanced past the end of the source.
		/// </summary>
		internal bool IsComplete
		{
			get
			{
				return this.CurrentSourceIndex >= this.Source.Count;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get the <see cref="SyllabicWord"/> defined so far.
		/// </summary>
		public SyllabicWord ToWord()
		{
			return new SyllabicWord(target);
		}

		/// <summary>
		/// Get the syllable at the current source index or null if out of bounds.
		/// </summary>
		public string GetCurrentSourceSyllable()
		{
			if (this.CurrentSourceIndex < 0 || this.CurrentSourceIndex >= this.Source.Count) 
				return null;

			return this.Source[this.CurrentSourceIndex];
		}

		/// <summary>
		/// Advance the index in the source word.
		/// </summary>
		public void AdvanceSourceIndex()
		{
			if (this.CurrentSourceIndex < this.Source.Count) this.CurrentSourceIndex++;
		}

		/// <summary>
		/// Append a syllable to the target word. This is what effectively builds the target word.
		/// </summary>
		/// <param name="syllable">The syllable to add. If null, nothing is added.</param>
		public void AppendToTarget(string syllable)
		{
			if (syllable == null) return;

			this.target.Add(syllable);
		}

		#endregion
	}
}
