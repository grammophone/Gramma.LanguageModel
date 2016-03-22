using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.Grammar
{
	/// <summary>
	/// Represents a Part-Of-Speech, wich also specifies the possible <see cref="InflectionType"/>s
	/// that may appear for this Part-Of-Speech.
	/// </summary>
	[Serializable]
	public sealed class TagType : NamedKeyedChild<GrammarModel, string>, IEquatable<TagType>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The child's key.</param>
		/// <param name="name">The child's friendly name.</param>
		/// <param name="areTagsUnrelated">
		/// If true, every <see cref="Tag"/> of this <see cref="TagType"/> is unrelated against each other.
		/// Example, when the <see cref="TagType"/> signals a 'Conjunction' and the <see cref="Tag"/>s of it represent the specific conjunctions.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		internal TagType(string key, string name, bool areTagsUnrelated)
			: base(key, name)
		{
			this.InflectionTypes = new ReadOnlyMap<string, InflectionType>();
			this.AreTagsUnrelated = areTagsUnrelated;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The inflection types associated with this tag type.
		/// </summary>
		public IReadOnlyMap<string, InflectionType> InflectionTypes { get; internal set; }

		/// <summary>
		/// If true, every <see cref="Tag"/> of this <see cref="TagType"/> is unrelated against each other.
		/// Example, when the <see cref="TagType"/> signals a 'Conjunction' and the <see cref="Tag"/>s of it represent the specific conjunctions.
		/// </summary>
		public bool AreTagsUnrelated { get; internal set; }

		/// <summary>
		/// The <see cref="GrammarModel"/> of this tag type.
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
			return this.Equals(obj as TagType);
		}

		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		#endregion

		#region IEquatable<TagType> Members

		public bool Equals(TagType other)
		{
			if (Object.ReferenceEquals(other, null)) return false;

			if (Object.ReferenceEquals(other, this)) return true;

			return this.Key == other.Key;
		}

		#endregion

	}
}
