using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Gramma.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// Command for adding a syllable next to the current one.
	/// </summary>
	[Serializable]
	public class AddCommand : EditCommand, IEquatable<AddCommand>, IDeserializationCallback
	{
		#region Private fields

		[NonSerialized]
		private int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="addedSyllable">The syllable to be addedSyllable.</param>
		public AddCommand(string addedSyllable)
		{
			if (addedSyllable == null) throw new ArgumentNullException("addedSyllable");

			this.AddedSyllable = addedSyllable;
			this.Cost = 1.0f;

			ComputeHashCode();
		}

		private void ComputeHashCode()
		{
			this.hashCode = this.AddedSyllable.GetHashCode();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The syllable to be addedSyllable.
		/// </summary>
		public string AddedSyllable { get; private set; }

		#endregion

		#region Public methods

		public override void Execute(SyllabicWordBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.AppendToTarget(this.AddedSyllable);
		}

		public override bool Equals(object otherObject)
		{
			var otherCommand = EnsureExactType<AddCommand>(otherObject);

			return Equals(otherCommand);
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		#endregion

		#region IEquatable<AddCommand> Members

		public bool Equals(AddCommand other)
		{
			if (other == null) return false;

			return this.AddedSyllable == other.AddedSyllable;
		}

		#endregion

		#region IDeserializationCallback Members

		void IDeserializationCallback.OnDeserialization(object sender)
		{
			ComputeHashCode();
		}

		#endregion
	}
}
