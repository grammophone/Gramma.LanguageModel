using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;
using Grammophone.LanguageModel.Provision;
using System.Windows.Markup;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// A composite training source consisting of many training sources.
	/// </summary>
	/// <typeparam name="T">the type of data supplied by the training source.</typeparam>
	[ContentProperty("TrainingSources")]
	public class CompositeTrainingSource<T> : TrainingSource<T>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public CompositeTrainingSource(IEnumerable<TrainingSource<T>> trainingSources, LanguageProvider languageProvider)
		{
			if (trainingSources == null) throw new ArgumentNullException("trainingSources");
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");

			foreach (var trainingSource in trainingSources)
			{
				if (trainingSource.LanguageProvider != languageProvider)
				{
					throw new ArgumentException("At least one training source has not the same LanguageProvider as the given one.", "trainingSources");
				}
			}

			this.TrainingSources = new ReadOnlySequence<TrainingSource<T>>(trainingSources);
			this.LanguageProvider = languageProvider;
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public CompositeTrainingSource(TrainingSource<T>[] trainingSources, LanguageProvider languageProvider)
		{
			if (trainingSources == null) throw new ArgumentNullException("trainingSources");
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");

			foreach (var trainingSource in trainingSources)
			{
				if (trainingSource.LanguageProvider != languageProvider)
				{
					throw new ArgumentException("At least one training source has not the same LanguageProvider as the given one.", "trainingSources");
				}
			}

			this.TrainingSources = new ReadOnlySequence<TrainingSource<T>>(trainingSources);
			this.LanguageProvider = languageProvider;
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public CompositeTrainingSource(ICollection<TrainingSource<T>> trainingSources, LanguageProvider languageProvider)
		{
			if (trainingSources == null) throw new ArgumentNullException("trainingSources");
			if (languageProvider == null) throw new ArgumentNullException("languageProvider");

			foreach (var trainingSource in trainingSources)
			{
				if (trainingSource.LanguageProvider != languageProvider)
				{
					throw new ArgumentException("At least one training source has not the same LanguageProvider as the given one.", "trainingSources");
				}
			}

			this.TrainingSources = new ReadOnlySequence<TrainingSource<T>>(trainingSources);
			this.LanguageProvider = languageProvider;
		}

		/// <summary>
		/// Create an empty composite source with no language provider set. Intended for XAML usage.
		/// </summary>
		/// <remarks>
		/// This is intended for XAML instanciation. Otherwise, the properties are read-only and cannot be set programmatically.
		/// </remarks>
		public CompositeTrainingSource()
		{
			this.TrainingSources = new ReadOnlySequence<TrainingSource<T>>(new TrainingSource<T>[0]);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The training sources being aggregated to a single composite training source.
		/// </summary>
		public ReadOnlySequence<TrainingSource<T>> TrainingSources { get; private set; }

		#endregion

		#region Protected methods

		protected override void OpenImplementation()
		{
			foreach (var trainingSource in this.TrainingSources)
			{
				trainingSource.Open();
			}
		}

		protected override void CloseImplementation()
		{
			foreach (var trainingSource in this.TrainingSources)
			{
				trainingSource.Close();
			}
		}

		protected override IEnumerable<T> GetDataImplementation()
		{
			var dataCollections = from trainingSource in this.TrainingSources
														select trainingSource.GetData();

			IEnumerable<T> emptyCollection = new List<T>(0);

			var aggregateDataCollection = dataCollections.Aggregate(emptyCollection, (collection1, collection2) => collection1.Concat(collection2));

			return aggregateDataCollection;
		}

		#endregion
	}
}
