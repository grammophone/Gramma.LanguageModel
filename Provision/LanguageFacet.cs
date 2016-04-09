using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.LanguageModel.Provision
{
	/// <summary>
	/// An object that is associated to a <see cref="LanguageProvider"/>,
	/// allowing read-write access to the property.
	/// </summary>
	public class LanguageFacet : ReadOnlyLanguageFacet
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="languageProvider">The provider associated with this object.</param>
		public LanguageFacet(LanguageProvider languageProvider)
			: base(languageProvider)
		{
		}

		/// <summary>
		/// Create. The propeprty <see cref="LanguageFacet.LanguageProvider"/>
		/// remains null until its setter is invoked.
		/// </summary>
		public LanguageFacet()
		{
		}

		/// <summary>
		/// The language provider associated with this object.
		/// </summary>
		public new LanguageProvider LanguageProvider
		{
			get
			{
				return base.LanguageProvider;
			}
			set
			{
				base.LanguageProvider = value;
			}
		}

	}
}
