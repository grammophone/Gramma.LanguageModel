using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel.Grammar
{
	/// <summary>
	/// Represents a Part-Of-Speech annotation with an attached collection of <see cref="Inflection"/> annotations.
	/// Suitable for fast equality checks and hashtables.
	/// </summary>
	[Serializable]
	public class Tag : IEquatable<Tag>, IDeserializationCallback
	{
		#region Private fields

		[NonSerialized]
		private int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="tagType">The tag type (part-of-speech) of this tag.</param>
		/// <param name="inflections">The inflections, if any, annotating this tag, else null.</param>
		/// <param name="discriminant">Optional discriminant for this tag, else null.</param>
		internal Tag(TagType tagType, IEnumerable<Inflection> inflections = null, String discriminant = null)
		{
			if (tagType == null) throw new ArgumentNullException("tagType");

			this.Type = tagType;

			if (inflections != null)
			{
				this.Inflections = new ReadOnlyMap<string, Inflection>(inflections);
			}
			else
			{
				this.Inflections = new ReadOnlyMap<string, Inflection>();
			}

			if (discriminant != null)
				this.Discriminant = discriminant;
			else
				this.Discriminant = String.Empty;

			CalculateHashCode();
		}

		private void CalculateHashCode()
		{
			hashCode = 0;

			foreach (var inflection in this.Inflections)
			{
				hashCode += inflection.GetHashCode();
			}

			hashCode = hashCode * 23 + this.Type.GetHashCode();

			hashCode = hashCode * 23 + this.Discriminant.GetHashCode();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The tag type (part-of-speech) of this tag.
		/// </summary>
		public TagType Type { get; internal set; }

		/// <summary>
		/// The inflections, if any, annotating this tag, indexed by their name.
		/// </summary>
		public IReadOnlyMap<string, Inflection> Inflections { get; internal set; }

		/// <summary>
		/// Optional discriminant for this tag, else the empty string.
		/// </summary>
		public string Discriminant { get; internal set; }

		#endregion

		#region IEquatable<Tag> Members

		public bool Equals(Tag other)
		{
			if (Object.ReferenceEquals(other, null)) return false;

			if (Object.ReferenceEquals(this, other)) return true;

			if (this.hashCode != other.hashCode) return false;

			if (!this.Type.Equals(other.Type)) return false;

			if (this.Inflections.Count != other.Inflections.Count) return false;

			foreach (var inflection in this.Inflections)
			{
				if (!other.Inflections.ContainsKey(inflection.Key)) return false;
			}

			return this.Discriminant == other.Discriminant;
		}

		#endregion

		#region Public methods

		public override bool Equals(object obj)
		{
			return Equals(obj as Tag);
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendFormat("Tag type: {0} \r\n", this.Type.ToString());

			foreach (var inflection in this.Inflections)
			{
				stringBuilder.AppendFormat("Inflection: {0} \r\n", inflection.ToString());
			}

			if (this.Discriminant.Length > 0) 
				stringBuilder.AppendFormat("Discriminant: {0} \r\n", this.Discriminant);

			return stringBuilder.ToString();
		}

		#endregion

		#region IDeserializationCallback Members

		void IDeserializationCallback.OnDeserialization(object sender)
		{
			CalculateHashCode();
		}

		#endregion
	}
}
