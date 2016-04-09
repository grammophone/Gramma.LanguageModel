using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// Command for deleting the current syllable.
	/// </summary>
	[Serializable]
	public class DeleteCommand : EditCommand, IEquatable<DeleteCommand>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public DeleteCommand()
		{
			this.Cost = 1.0f;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The syllable to be matched for deletion.
		/// </summary>
		public string DeletedSyllable { get; private set; }

		#endregion

		#region Public methods

		public override void Execute(SyllabicWordBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.AdvanceSourceIndex();
		}

		public override bool Equals(object otherObject)
		{
			return Equals(this.EnsureExactType<DeleteCommand>(otherObject));
		}

		public override int GetHashCode()
		{
			return 42; // The answer to life, the universe and everything.
		}

		#endregion

		#region IEquatable<DeleteCommand> Members

		public bool Equals(DeleteCommand other)
		{
			return other != null;
		}

		#endregion
	}
}
