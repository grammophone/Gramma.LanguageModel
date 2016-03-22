using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Gramma.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// Command that performs full, unconditional replacement of 
	/// any base syllable with a specified target syllable.
	/// </summary>
	[Serializable]
	public class ReplaceAllCommand : ReplaceCommand, IEquatable<ReplaceAllCommand>, IDeserializationCallback
	{
		#region Private fields

		[NonSerialized]
		private int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="targetSyllable">The replacement syllable.</param>
		public ReplaceAllCommand(string targetSyllable)
		{
			if (targetSyllable == null) throw new ArgumentNullException("targetSyllable");

			this.TargetSyllable = targetSyllable;

			ComputeHashCode();
		}

		private void ComputeHashCode()
		{
			this.hashCode = 23 * this.TargetSyllable.GetHashCode();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The replacement syllable.
		/// </summary>
		public string TargetSyllable { get; protected set; }

		#endregion

		#region Public methods

		public override string Replace(string baseSyllable)
		{
			return this.TargetSyllable;
		}

		public override bool Equals(object otherObject)
		{
			var otherCommand = this.EnsureExactType<ReplaceAllCommand>(otherObject);

			return Equals(otherCommand);
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		#endregion

		#region IEquatable<ReplaceAllCommand> Members

		public bool Equals(ReplaceAllCommand other)
		{
			if (other == null) return false;

			return this.TargetSyllable.Equals(other.TargetSyllable);
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
