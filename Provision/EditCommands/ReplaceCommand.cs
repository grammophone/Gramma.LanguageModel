using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision.EditCommands
{
	/// <summary>
	/// Abstract base for commands which replace a syllable with another syllable.
	/// </summary>
	[Serializable]
	public abstract class ReplaceCommand : EditCommand
	{
		#region Public methods

		/// <summary>
		/// Attempt to replace the <paramref name="baseSyllable"/>
		/// with another syllable.
		/// </summary>
		/// <param name="baseSyllable">The syllable to replace.</param>
		/// <returns>Returns the output string or null if replacement is not possible.</returns>
		public abstract string Replace(string baseSyllable);

		/// <summary>
		/// Try to execute the command. This implementation relies on the <see cref="Replace"/> method.
		/// </summary>
		public override void Execute(SyllabicWordBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.AdvanceSourceIndex();

			var baseSyllable = builder.GetCurrentSourceSyllable();

			// Base syllable is null if we have completed scanning past the end of the source word.
			if (baseSyllable == null) return;

			builder.AppendToTarget(this.Replace(baseSyllable));
		}

		#endregion
	}
}
