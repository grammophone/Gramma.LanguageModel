using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.LanguageModel.Provision;

namespace Grammophone.LanguageModel.TrainingSources
{
	/// <summary>
	/// An abstract source of training data.
	/// </summary>
	/// <typeparam name="T">The type of training data.</typeparam>
	public abstract class TrainingSource<T> : ReadOnlyLanguageFacet, IDisposable
	{
		#region Construction and Finalization

		/// <summary>
		/// Create.
		/// </summary>
		public TrainingSource()
		{
			this.IsOpen = false;

			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the underlying resources, in case where
		/// the source was open and
		/// <see cref="Dispose"/> or <see cref="Close"/> were not invoked.
		/// </summary>
		~TrainingSource()
		{
			if (this.IsOpen) this.CloseImplementation();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// True if method <see cref="Open"/> was invoked successfully and 
		/// method <see cref="Dispose"/> or <see cref="Close"/> isn't yet called.
		/// </summary>
		public bool IsOpen { get; private set; }

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Closes the source and releases the underlying resources, if currently open,
		/// else does nothing.
		/// </summary>
		public void Dispose()
		{
			if (!this.IsOpen) return;

			Close();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Opens the source and reserves the underlying resources.
		/// </summary>
		/// <exception cref="ProvisionException">
		/// When the source is already open
		/// or when <see cref="ReadOnlyLanguageFacet.LanguageProvider"/> is null.
		/// </exception>
		/// <remarks>
		/// Relies on <see cref="OpenImplementation"/> method implementation.
		/// </remarks>
		public void Open()
		{
			if (this.IsOpen)
				throw new ProvisionException("Training source is already open.");

			EnsureLanguageProviderIsSet();

			OpenImplementation();

			this.IsOpen = true;

			GC.ReRegisterForFinalize(this);
		}

		/// <summary>
		/// Obtain the data from this source. Must be called after
		/// successfully invoking the <see cref="Open"/> method.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ProvisionException">
		/// When the source is not open.
		/// </exception>
		/// <remarks>
		/// Relies on <see cref="GetDataImplementation"/> method implementation.
		/// </remarks>
		public IEnumerable<T> GetData()
		{
			if (!this.IsOpen)
				throw new ProvisionException("The training source is not open.");

			return GetDataImplementation();
		}

		/// <summary>
		/// Closes the source and releases the underlying resources.
		/// </summary>
		/// <exception cref="ProvisionException">
		/// When the source has been already closed.
		/// </exception>
		/// <remarks>
		/// Relies on <see cref="CloseImplementation"/> method implementation.
		/// </remarks>
		public void Close()
		{
			if (!this.IsOpen)
				throw new ProvisionException("The training source is not open.");

			CloseImplementation();

			this.IsOpen = false;

			GC.SuppressFinalize(this);
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Implementation for acquisition of resources required to provide data.
		/// </summary>
		/// <remarks>
		/// It doesn't necessarily imply seeking to start position, as this is
		/// delegated to <see cref="GetDataImplementation"/> method.
		/// </remarks>
		protected abstract void OpenImplementation();

		/// <summary>
		/// Implementation for releasing the underlying resources.
		/// </summary>
		protected abstract void CloseImplementation();

		/// <summary>
		/// Implementation for providing the data from a source
		/// which has been opened successfully.
		/// </summary>
		/// <returns>
		/// Returns an enumeration of the data.
		/// </returns>
		/// <remarks>
		/// This call implies seeking to the start position.
		/// </remarks>
		protected abstract IEnumerable<T> GetDataImplementation();

		/// <summary>
		/// Checks if <see cref="ReadOnlyLanguageFacet.LanguageProvider"/> has been set.
		/// </summary>
		/// <exception cref="ProvisionException">
		/// When the <see cref="ReadOnlyLanguageFacet.LanguageProvider"/> hasn't been set.
		/// </exception>
		protected void EnsureLanguageProviderIsSet()
		{
			if (this.LanguageProvider == null)
				throw new ProvisionException(
					"The property LanguageProvider has not been set. " +
					"Possible causes: either the LanguageProvider property in the parent TrainingSet has not been set " +
					"or the system has not been yet initialized and preprocessed.");
		}

		#endregion
	}
}
