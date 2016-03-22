using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.Grammar
{
	/// <summary>
	/// A type of inflection annotation. Example: Gender, Number, Case, Mood, Voice etc.
	/// </summary>
	[Serializable]
	public sealed class InflectionType : NamedKeyedChild<GrammarModel, string>, IEquatable<InflectionType>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The inflection type key.</param>
		/// <param name="name">The inflection type friendly name.</param>
		/// <param name="inflections">The set of inflections of this type or null if empty.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> 
		/// are null.
		/// </exception>
		internal InflectionType(string key, string name, IEnumerable<Inflection> inflections)
			: base(key, name)
		{
			this.Inflections = new KeyedReadOnlyChildren<InflectionType, string, Inflection>(this, inflections);

			this.TagTypes = new ReadOnlyMap<string, TagType>();
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The inflection type key.</param>
		/// <param name="name">The inflection type friendly name.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> 
		/// are null.
		/// </exception>
		/// <remarks>
		/// The set of inflections is empty.
		/// </remarks>
		internal InflectionType(string key, string name)
			: this(key, name, null)
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The inflections of this inflection type.
		/// </summary>
		public IKeyedReadOnlyChildren<InflectionType, string, Inflection> Inflections { get; internal set; }

		/// <summary>
		/// The tag types related to this inflection type.
		/// </summary>
		public IReadOnlyMap<string, TagType> TagTypes { get; internal set; }

		/// <summary>
		/// The <see cref="GrammarModel"/> of this inflection type.
		/// Redirects to <see cref="IChild{GrammarModel}.Parent"/> explicit implementation of this class.
		/// </summary>
		public GrammarModel GrammarModel
		{
			get { return parent; }
		}

		#endregion

		#region Public methods

		public override string ToString()
		{
			return this.Name;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as InflectionType);
		}

		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		#endregion

		#region IEquatable<InflectionType> Members

		public bool Equals(InflectionType other)
		{
			if (Object.ReferenceEquals(other, null)) return false;

			if (Object.ReferenceEquals(this, other)) return true;

			if (this.Inflections.Count != other.Inflections.Count) return false;

			foreach (var inflection in this.Inflections)
			{
				if (!other.Inflections.ContainsKey(inflection.Key)) return false;
			}

			return true;
		}

		#endregion
	}
}
