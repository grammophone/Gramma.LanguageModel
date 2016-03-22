using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using Gramma.LanguageModel.Provision;

namespace Gramma.LanguageModel.TrainingSources
{
	/// <summary>
	/// A collection of training data sets under a <see cref="LanguageProvider"/>.
	/// </summary>
	public abstract class SourceSet : LanguageFacet
	{
		#region Public methods

		/// <summary>
		/// Set the language provider to all training sources.
		/// </summary>
		public abstract void AssignLanguageProviderToSources();

		/// <summary>
		/// Load a source set from XAML.
		/// </summary>
		/// <typeparam name="S">The concrete descentant type from <see cref="SourceSet"/>.</typeparam>
		/// <param name="xamlFilename">The name of the XAML file.</param>
		/// <param name="languageProvider">The language provider to associate with the source set.</param>
		/// <returns>
		/// Returns the source set defined in the XAML, else an <see cref="ApplicationException"/> if the object of the file
		/// is not of the required source set type.
		/// </returns>
		/// <exception cref="ApplicationException">
		/// When the object defined in the XAML file is not of type <typeparamref name="S"/>.
		/// </exception>
		public static S LoadFromXAML<S>(string xamlFilename, LanguageProvider languageProvider)
			where S : SourceSet
		{
			if (xamlFilename == null) throw new ArgumentNullException("xamlFilename");
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");

			using (XamlXmlReader reader = new XamlXmlReader(xamlFilename))
			{
				return LoadFromXAML<S>(reader, languageProvider);
			}
		}

		/// <summary>
		/// Load a source set from XAML.
		/// </summary>
		/// <typeparam name="S">The concrete descentant type from <see cref="SourceSet"/>.</typeparam>
		/// <param name="reader">The XAML reader. Disposing is left to the caller.</param>
		/// <param name="languageProvider">The language provider to associate with the source set.</param>
		/// <returns>
		/// Returns the source set defined in the XAML, else an <see cref="ApplicationException"/> if the object of the file
		/// is not of the required source set type.
		/// </returns>
		/// <exception cref="ApplicationException">
		/// When the object defined in the XAML file is not of type <typeparamref name="S"/>.
		/// </exception>
		public static S LoadFromXAML<S>(XamlXmlReader reader, LanguageProvider languageProvider)
			where S : SourceSet
		{
			if (reader == null) throw new ArgumentNullException("reader");
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");

			var validationSet = XamlServices.Load(reader) as S;

			if (validationSet == null) 
				throw new ApplicationException(String.Format("The object in the XAML file is not of type '{0}'.", typeof(S).FullName));

			validationSet.LanguageProvider = languageProvider;
			validationSet.AssignLanguageProviderToSources();

			return validationSet;
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Assign the current <see cref="LanguageFacet.LanguageProvider"/> to a collection of training sources.
		/// </summary>
		/// <typeparam name="T">The type of data in the training source.</typeparam>
		/// <param name="trainingSources">The training source.</param>
		protected void AssignLanguageProviderToSources<T>(IEnumerable<TrainingSource<T>> trainingSources)
		{
			var languageProvider = this.LanguageProvider;

			if (trainingSources == null) throw new ArgumentNullException("trainingSources");

			foreach (var source in trainingSources)
			{
				source.LanguageProvider = languageProvider;

				var compositeSource = source as CompositeTrainingSource<T>;

				if (compositeSource != null) AssignLanguageProviderToSources(compositeSource.TrainingSources);
			}
		}

		#endregion
	}
}
