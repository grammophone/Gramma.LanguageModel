using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel.Grammar
{
	/// <summary>
	/// The inflection of a word.
	/// </summary>
	[Serializable]
	public sealed class Inflection : NamedKeyedChild<InflectionType, string>, IEquatable<Inflection>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The inflection's key.</param>
		/// <param name="name">The inflection's friendly name.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		internal Inflection(string key, string name)
			: base(key, name)
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The <see cref="InflectionType"/> of this inflection.
		/// Redirects to <see cref="IChild{InflectionType}.Parent"/> explicit implementation of this class.
		/// </summary>
		public InflectionType Type
		{
			get { return parent; }
		}

		#endregion

		#region Public methods

		public override bool Equals(object obj)
		{
			return base.Equals(obj as Inflection);
		}

		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		#endregion

		#region IEquatable<Inflection> Members

		public bool Equals(Inflection other)
		{
			if (Object.ReferenceEquals(other, null)) return false;

			if (Object.ReferenceEquals(this, other)) return true;

			return this.Key == other.Key && this.Name == other.Name && this.Type == other.Type;
		}

		public override string ToString()
		{
			return String.Format("Type {0}, {1}", this.Type.ToString(), this.Name);
		}

		#endregion
	}
}
