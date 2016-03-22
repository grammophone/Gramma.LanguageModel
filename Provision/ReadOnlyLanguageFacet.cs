using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.Provision
{
	/// <summary>
	/// An object that is associated to a <see cref="LanguageProvider"/>.
	/// </summary>
	public abstract class ReadOnlyLanguageFacet : IKeyedElement<LanguageProvider>
	{
		#region Private fields

		private LanguageProvider languageProvider;

		#endregion

		#region Construction

		/// <summary>
		/// Create
		/// </summary>
		/// <param name="languageProvider">The provider associated with this object.</param>
		public ReadOnlyLanguageFacet(LanguageProvider languageProvider)
		{
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");
			this.languageProvider = languageProvider;
		}

		/// <summary>
		/// Internal create. The propeprty <see cref="ReadOnlyLanguageFacet.LanguageProvider"/>
		/// remains null until its private setter is invoked.
		/// </summary>
		internal ReadOnlyLanguageFacet()
		{

		}

		#endregion

		#region Public properties

		/// <summary>
		/// The language provider associated with this object.
		/// </summary>
		public LanguageProvider LanguageProvider 
		{
			get
			{
				return languageProvider;
			}
			internal set
			{
				if (value == null) throw new ArgumentNullException("value");
				this.languageProvider = value;
			}
		}

		#endregion

		#region IKeyedElement<LanguageProvider> Members

		/// <summary>
		/// Returns the <see cref="ReadOnlyLanguageFacet.LanguageProvider"/> property.
		/// </summary>
		LanguageProvider IKeyedElement<LanguageProvider>.Key
		{
			get { return this.LanguageProvider; }
		}

		#endregion
	}
}
