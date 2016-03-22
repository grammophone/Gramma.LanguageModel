using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Training source of word forms fully tagged and lemmatized.
	/// </summary>
	public abstract class TaggedWordTrainingSource : TrainingSource<TaggedWordForm>
	{
	}
}
