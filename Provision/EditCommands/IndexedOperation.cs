using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// Perscribes an operation of an <see cref="EditCommand"/> at a syllable index.
	/// </summary>
	[Serializable]
	public sealed class IndexedOperation : IEquatable<IndexedOperation>, IComparable<IndexedOperation>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="index">The syllable index where the command will execute.</param>
		public IndexedOperation(EditCommand command, int index)
		{
			if (command == null) throw new ArgumentNullException("command");
			if (index < -1) throw new ArgumentException("Index must be non negative", "index");

			this.Command = command;
			this.SourceIndex = index;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The command to execute.
		/// </summary>
		public EditCommand Command { get; private set; }

		/// <summary>
		/// The syllable index in the source where the command will execute.
		/// </summary>
		public int SourceIndex { get; private set; }

		#endregion

		#region Public methods

		public override int GetHashCode()
		{
			return this.SourceIndex.GetHashCode() ^ this.Command.GetHashCode();
		}

		#endregion

		#region IEquatable<IndexedOperation> Members

		public bool Equals(IndexedOperation other)
		{
			if (other == null) throw new ArgumentNullException("other");

			return this.SourceIndex == other.SourceIndex && this.Command.Equals(other.Command);
		}

		#endregion

		#region IComparable<IndexedOperation> Members

		public int CompareTo(IndexedOperation other)
		{
			if (other == null) throw new ArgumentNullException("other");

			return this.SourceIndex.CompareTo(other.SourceIndex);
		}

		#endregion
	}
}
