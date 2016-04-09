using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Grammophone.LanguageModel.Grammar;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// A correspondence of a command sequence to a grammar tag.
	/// </summary>
	/// <remarks>
	/// This is the type of class which the multitude of SVM classifiers recognize.
	/// </remarks>
	[Serializable]
	public class CommandSequenceClass : IEquatable<CommandSequenceClass>, IDeserializationCallback
	{
		#region Private fields

		[NonSerialized]
		private int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="sequence">The edit command sequence.</param>
		/// <param name="tag">The part-of speech tag corresponding to the edit command <paramref name="sequence"/>.</param>
		public CommandSequenceClass(CommandSequence sequence, Tag tag)
		{
			if (sequence == null) throw new ArgumentNullException("sequence");
			if (tag == null) throw new ArgumentNullException("tag");

			this.Sequence = sequence;
			this.Tag = tag;

			ComputeHashCode();
		}

		private void ComputeHashCode()
		{
			hashCode = 17;

			hashCode = hashCode * 23 + this.Sequence.GetHashCode();
			hashCode = hashCode * 23 + this.Tag.GetHashCode();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The part-of speech tag corresponding to the edit command <see cref="Sequence"/>.
		/// </summary>
		public Tag Tag { get; internal set; }

		/// <summary>
		/// The edit command sequence.
		/// </summary>
		public CommandSequence Sequence { get; internal set; }

		#endregion

		#region Public methods

		public override bool Equals(object obj)
		{
			return this.Equals(obj as CommandSequenceClass);
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		#endregion

		#region IEquatable<CommandClass> Members

		public bool Equals(CommandSequenceClass other)
		{
			if (other == null) throw new ArgumentNullException("other");

			if (this.hashCode != other.hashCode) return false;

			return this.Tag.Equals(other.Tag) && this.Sequence.Equals(other.Sequence);
		}

		#endregion

		#region IDeserializationCallback Members

		void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender)
		{
			ComputeHashCode();
		}

		#endregion
	}
}
