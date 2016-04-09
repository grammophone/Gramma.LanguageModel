using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// A NOP edit action. Its cost is zero.
	/// </summary>
	[Serializable]
	public class NoChangeCommand : ReplaceCommand, IEquatable<NoChangeCommand>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public NoChangeCommand()
		{
			this.Cost = 0.0f;
		}

		#endregion

		#region Public methods

		public override string Replace(string baseSyllable)
		{
			return baseSyllable;
		}

		public override bool Equals(object otherObject)
		{
			return Equals(this.EnsureExactType<NoChangeCommand>(otherObject));
		}

		public override int GetHashCode()
		{
			return 17;
		}

		#endregion

		#region IEquatable<NoChangeCommand> Members

		public bool Equals(NoChangeCommand other)
		{
			return other != null;
		}

		#endregion
	}
}
