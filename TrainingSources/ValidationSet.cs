using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Contains sentence training sources used for overall system validation.
	/// </summary>
	public class ValidationSet : SourceSet
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public ValidationSet()
		{
			this.TaggedWordTrainingSources = new TaggedWordTrainingSources();
			this.SentenceValidationSources = new SentenceTrainingSources();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Collection of <see cref="TaggedWordTrainingSource"/> elements.
		/// </summary>
		public TaggedWordTrainingSources TaggedWordTrainingSources { get; private set; }

		/// <summary>
		/// Collection of <see cref="SentenceTrainingSource"/> elements for validation testing.
		/// </summary>
		public SentenceTrainingSources SentenceValidationSources { get; private set; }

		#endregion

		#region Public methods

		/// <summary>
		/// Set the language provider to all training sources.
		/// </summary>
		public override void AssignLanguageProviderToSources()
		{
			AssignLanguageProviderToSources<TaggedWordForm>(this.TaggedWordTrainingSources);
			AssignLanguageProviderToSources<TaggedSentence>(this.SentenceValidationSources);
		}

		#endregion
	}
}
