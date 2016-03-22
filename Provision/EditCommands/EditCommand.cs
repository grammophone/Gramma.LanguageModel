using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// A generalized edit action in Levenshtein edit distance sense.
	/// </summary>
	[Serializable]
	public abstract class EditCommand
	{
		#region Private fields

		private static NoChangeCommand noChangeCommand = new NoChangeCommand();

		private static DeleteCommand deleteCommand = new DeleteCommand();

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public EditCommand()
		{
			this.Cost = 1.0f;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Cost of the edit action.
		/// </summary>
		public float Cost { get; protected set; }

		/// <summary>
		/// Return a singleton immutable NOP command.
		/// </summary>
		public static NoChangeCommand NoChange
		{
			get
			{
				return noChangeCommand;
			}
		}

		/// <summary>
		/// Return a singleton immutable delete command.
		/// </summary>
		public static DeleteCommand Delete
		{
			get
			{
				return deleteCommand;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Try to execute the command against a <see cref="SyllabicWordBuilder"/>,
		/// which holds the state of transforming a source word into a target word.
		/// </summary>
		/// <param name="builder"></param>
		public abstract void Execute(SyllabicWordBuilder builder);

		/// <summary>
		/// Equality test is forced to be implememented on descendants.
		/// </summary>
		public abstract override bool Equals(object otherObject);

		/// <summary>
		/// Hash code computation is forced to be implememented on descendants.
		/// </summary>
		public abstract override int GetHashCode();

		#endregion

		#region Protected methods

		/// <summary>
		/// Tests whether an object is exactly of type <typeparamref name="T"/>
		/// (not a descendant) and, if it is, casts it to <typeparamref name="T"/>, else returns null.
		/// </summary>
		/// <typeparam name="T">The type of a class.</typeparam>
		/// <param name="otherObject">The object to test. Can be null.</param>
		/// <returns>
		/// If the <paramref name="otherObject"/>'s type is exactly <typeparamref name="T"/>, returns
		/// the object casted to <typeparamref name="T"/>, else returns null.
		/// If the <paramref name="otherObject"/> is null, it returns null.
		/// </returns>
		protected T EnsureExactType<T>(object otherObject)
			where T : class
		{
			var otherCommand = otherObject as T;

			if (otherCommand == null) return null;

			if (otherCommand.GetType() == typeof(T))
			{
				return otherCommand;
			}
			else
			{
				return null;
			}
		}

		#endregion
	}
}
