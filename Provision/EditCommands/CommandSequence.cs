using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// A sequence of <see cref="IndexedOperation"/>, in ascending index order,
	/// which can be applied to an array of syllables.
	/// </summary>
	[Serializable]
	public class CommandSequence : EquatableReadOnlySequence<IndexedOperation>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The collection of items to include in the container.</param>
		public CommandSequence(IEnumerable<IndexedOperation> items)
			: base(items)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The array of items to include in the container.</param>
		public CommandSequence(IndexedOperation[] items)
			: base(items)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Transform an array of syllables using the perscribed commands sequence.
		/// </summary>
		/// <param name="syllables">The array of syllables to transform.</param>
		/// <returns>Returns the transformed array of syllables.</returns>
		public SyllabicWord Execute(SyllabicWord syllables)
		{
			if (syllables == null) throw new ArgumentNullException("syllables");

			var builder = new SyllabicWordBuilder(syllables);

			for (int operationIndex = 0; operationIndex < this.list.Count; operationIndex++)
			{
				var operation = this.list[operationIndex];

				// Imitate the implied NoChangeCommand when there is no command for the current source index.
				while (builder.CurrentSourceIndex < operation.SourceIndex && !builder.IsComplete)
				{
					builder.AdvanceSourceIndex();
					builder.AppendToTarget(builder.GetCurrentSourceSyllable());
				}

				if (!builder.IsComplete)
					operation.Command.Execute(builder);
				else
					break;
			}

			// Complete any residual not handled by commands by emulating a NoChangeCommand.
			while (!builder.IsComplete)
			{
				builder.AdvanceSourceIndex();
				builder.AppendToTarget(builder.GetCurrentSourceSyllable());
			}

			return builder.ToWord();
		}

		#endregion
	}
}
