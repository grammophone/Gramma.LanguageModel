using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Read-only collection of <see cref="TaggedWordTrainingSource"/> elements,
	/// suitable for XAML.
	/// </summary>
	public class TaggedWordTrainingSources : ReadOnlySequence<TrainingSource<TaggedWordForm>>
	{
	}
}
