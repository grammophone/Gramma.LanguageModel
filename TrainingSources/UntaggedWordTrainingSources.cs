using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// Read-only collection of <see cref="UntaggedWordTrainingSource"/> elements,
	/// suitable for XAML.
	/// </summary>
	public class UntaggedWordTrainingSources : ReadOnlySequence<TrainingSource<string>>
	{
	}
}
