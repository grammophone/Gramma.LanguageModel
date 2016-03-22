using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.GenericContentModel;
using Gramma.LanguageModel.Provision;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// Read-only collection of <see cref="TrainingSet"/> elements, 
	/// keyed by <see cref="LanguageProvider"/>,
	/// suitable for XAML.
	/// </summary>
	public class TrainingSets : ReadOnlyMap<LanguageProvider, TrainingSet>
	{
	}
}
