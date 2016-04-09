using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// Read-only collection of <see cref="ValidationSet"/> elements, 
	/// keyed by <see cref="LanguageProvider"/>,
	/// suitable for XAML.
	/// </summary>
	public class ValidationSets : ReadOnlyMap<LanguageProvider, ValidationSet>
	{
	}
}
