using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// Represents a full training set for a language, containing
	/// <see cref="TrainingSet.UntaggedWordTrainingSources"/>,
	/// <see cref="TrainingSet.TaggedWordTrainingSources"/> and
	/// <see cref="TrainingSet.SentenceTrainingSources"/>.
	/// </summary>
	public class TrainingSet : SourceSet
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public TrainingSet()
		{
			this.UntaggedWordTrainingSources = new UntaggedWordTrainingSources();
			this.TaggedWordTrainingSources = new TaggedWordTrainingSources();
			this.SentenceTrainingSources = new SentenceTrainingSources();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Collection of <see cref="UntaggedWordTrainingSource"/> elements.
		/// </summary>
		public UntaggedWordTrainingSources UntaggedWordTrainingSources { get; private set; }

		/// <summary>
		/// Collection of <see cref="TaggedWordTrainingSource"/> elements.
		/// </summary>
		public TaggedWordTrainingSources TaggedWordTrainingSources { get; private set; }

		/// <summary>
		/// Collection of <see cref="SentenceTrainingSource"/> elements.
		/// </summary>
		public SentenceTrainingSources SentenceTrainingSources { get; private set; }

		#endregion

		#region Public methods

		/// <summary>
		/// Set the language provider to all training sources.
		/// </summary>
		public override void AssignLanguageProviderToSources()
		{
			AssignLanguageProviderToSources<string>(this.UntaggedWordTrainingSources);
			AssignLanguageProviderToSources<TaggedWordForm>(this.TaggedWordTrainingSources);
			AssignLanguageProviderToSources<TaggedSentence>(this.SentenceTrainingSources);
		}

		#endregion
	}
}
