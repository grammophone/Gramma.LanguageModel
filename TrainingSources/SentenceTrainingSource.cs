using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Training source of fully tagged sentences.
	/// </summary>
	public abstract class SentenceTrainingSource : TrainingSource<TaggedSentence>
	{
	}
}
