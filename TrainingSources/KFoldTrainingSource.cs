using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// A composite training source for K-fold validation: 
	/// Depending on <see cref="Mode"/>, it either includes all samples or
	/// it omits or includes one sample every <see cref="FoldSize"/> samples of the underlying sources,
	/// starting at <see cref="FoldOffset"/>.
	/// </summary>
	/// <typeparam name="T">The type of training samples.</typeparam>
	public class KFoldTrainingSource<T> : CompositeTrainingSource<T>
	{
		#region Private fields

		private int foldSize = 10;

		private int foldOffset = 0;

		private KFoldMode mode = KFoldMode.IncludeAll;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public KFoldTrainingSource(IEnumerable<TrainingSource<T>> trainingSources, LanguageProvider languageProvider)
			: base(trainingSources, languageProvider)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public KFoldTrainingSource(TrainingSource<T>[] trainingSources, LanguageProvider languageProvider)
			: base(trainingSources, languageProvider)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="trainingSources">The training sources being aggregated to a single composite training source.</param>
		/// <param name="languageProvider">The language provider for the composite training source made of all the training sources.</param>
		public KFoldTrainingSource(ICollection<TrainingSource<T>> trainingSources, LanguageProvider languageProvider)
			: base(trainingSources, languageProvider)
		{
		}

		/// <summary>
		/// Create an empty K-fold composite source with no language provider set. Intended for XAML usage.
		/// </summary>
		/// <remarks>
		/// This is intended for XAML instanciation. Otherwise, the properties are read-only and cannot be set programmatically.
		/// </remarks>
		public KFoldTrainingSource()
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The size of the folds. In each fold a sample is omitted.
		/// Default is 10.
		/// </summary>
		public int FoldSize
		{
			get
			{
				return foldSize;
			}
			set
			{
				if (value <= 0) throw new ArgumentException("FoldSize must be positive");
				foldSize = value;
			}
		}

		/// <summary>
		/// The offset of the omitted sample in each fold.
		/// Default is 0.
		/// </summary>
		public int FoldOffset
		{
			get
			{
				return foldOffset;
			}
			set
			{
				if (value < 0) throw new ArgumentException("FoldOffset must not be negative");
				foldOffset = value;
			}
		}

		/// <summary>
		/// Depending on <see cref="Mode"/>, the source either includes all samples or
		/// it omits or includes one sample every <see cref="FoldSize"/> samples of the underlying sources,
		/// starting at <see cref="FoldOffset"/>.
		/// Default is <see cref="KFoldMode.IncludeAll"/>.
		/// </summary>
		public KFoldMode Mode
		{
			get
			{
				return mode;
			}
			set
			{
				mode = value;
			}
		}

		#endregion

		#region Protected methods

		protected override IEnumerable<T> GetDataImplementation()
		{
			switch (this.Mode)
			{
				case KFoldMode.Training:
					return GetTrainingData();
				
				case KFoldMode.Validation:
					return GetValidationData();

				default:
					return base.GetDataImplementation();
			}
		}

		#endregion

		#region Private methods

		private IEnumerable<T> GetTrainingData()
		{
			int index = 0;

			foreach (var sample in base.GetDataImplementation())
			{
				if ((index++ - foldOffset) % foldSize == 0) continue;

				yield return sample;
			}
		}

		private IEnumerable<T> GetValidationData()
		{
			int index = 0;

			foreach (var sample in base.GetDataImplementation())
			{
				if ((index++ - foldOffset) % foldSize != 0) continue;

				yield return sample;
			}
		}

		#endregion
	}
}
