using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// Training source of word forms with no tagging.
	/// </summary>
	public abstract class UntaggedWordTrainingSource : TrainingSource<string>
	{
	}
}
