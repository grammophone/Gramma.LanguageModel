using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.Provision
{
	/// <summary>
	/// Read-only collection of <see cref="LanguageProvider"/> elements,
	/// keyed by <see cref="LanguageProvider.LanguageKey"/>,
	/// suitable for XAML.
	/// </summary>
	[Serializable]
	public class LanguageProviders : ReadOnlyMap<string, LanguageProvider>
	{
	}
}
