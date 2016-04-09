using System;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// The mode of <see cref="KFoldTrainingSource{T}"/>.
	/// </summary>
	[Serializable]
	public enum KFoldMode
	{
		/// <summary>
		/// The <see cref="KFoldTrainingSource{T}"/> does not omit any sample from the underlying source,
		/// being functionally equivalent to <see cref="CompositeTrainingSource{T}"/>.
		/// </summary>
		IncludeAll,

		/// <summary>
		/// The <see cref="KFoldTrainingSource{T}"/> omits one sample every <see cref="KFoldTrainingSource{T}.FoldSize"/> samples
		/// of the underlying sources, starting at <see cref="KFoldTrainingSource{T}.FoldOffset"/>
		/// </summary>
		Training,

		/// <summary>
		/// The <see cref="KFoldTrainingSource{T}"/> includes one sample every <see cref="KFoldTrainingSource{T}.FoldSize"/> samples
		/// of the underlying sources, starting at <see cref="KFoldTrainingSource{T}.FoldOffset"/>
		/// </summary>
		Validation
	}
}