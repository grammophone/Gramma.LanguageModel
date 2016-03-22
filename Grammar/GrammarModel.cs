using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using Gramma.GenericContentModel;

namespace Gramma.LanguageModel.Grammar
{
	/// <summary>
	/// Represents a language grammar model.
	/// </summary>
	[Serializable]
	public class GrammarModel
	{
		#region Private fields

		private ConcurrentDictionary<Tag, Tag> tagsDictionary;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="tagTypes">The tag types (parts-of-speech) of the language.</param>
		/// <param name="inflectionTypes">The inflection types of the language.</param>
		/// <param name="inflectionTagRelationship">The relationship between tag types and inflection types.</param>
		public GrammarModel(IEnumerable<TagType> tagTypes, IEnumerable<InflectionType> inflectionTypes, IEnumerable<Tuple<TagType, InflectionType>> inflectionTagRelationship)
		{
			if (inflectionTypes == null) throw new ArgumentNullException("inflectionTypes");
			if (tagTypes == null) throw new ArgumentNullException("tagTypes");
			if (inflectionTagRelationship == null) throw new ArgumentNullException("inflectionTagRelationship");

			this.tagsDictionary = new ConcurrentDictionary<Tag, Tag>();

			this.InflectionTypes = new KeyedReadOnlyChildren<GrammarModel, string, InflectionType>(this, inflectionTypes);
			this.TagTypes = new KeyedReadOnlyChildren<GrammarModel, string, TagType>(this, tagTypes);

			this.EstablishInflectionTagRelationship(inflectionTagRelationship);

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="tagTypes">The tag types (parts-of-speech) of the language.</param>
		/// <param name="inflectionTypes">The inflection types of the language.</param>
		/// <param name="inflectionTagRelationship">
		/// The relationship between tag types and inflection types specified by typles of [tag type key, inflection type key].
		/// </param>
		public GrammarModel(IEnumerable<TagType> tagTypes, IEnumerable<InflectionType> inflectionTypes, IEnumerable<Tuple<string, string>> inflectionTagRelationship)
		{
			if (inflectionTypes == null) throw new ArgumentNullException("inflectionTypes");
			if (tagTypes == null) throw new ArgumentNullException("tagTypes");
			if (inflectionTagRelationship == null) throw new ArgumentNullException("inflectionTagRelationship");

			this.tagsDictionary = new ConcurrentDictionary<Tag, Tag>();

			this.InflectionTypes = new KeyedReadOnlyChildren<GrammarModel, string, InflectionType>(this, inflectionTypes);
			this.TagTypes = new KeyedReadOnlyChildren<GrammarModel, string, TagType>(this, tagTypes);

			var relationshipResolution = from namesTuple in inflectionTagRelationship
																	 select new Tuple<TagType, InflectionType>(
																			this.TagTypes[namesTuple.Item1], this.InflectionTypes[namesTuple.Item2]
																		);

			this.EstablishInflectionTagRelationship(relationshipResolution);

		}

		#endregion

		#region Public properties

		/// <summary>
		/// The inflection types of this language, indexed by their key.
		/// </summary>
		public IKeyedReadOnlyChildren<GrammarModel, string, InflectionType> InflectionTypes { get; protected set; }

		/// <summary>
		/// The tag types of this language, indexed by their key.
		/// </summary>
		public IKeyedReadOnlyChildren<GrammarModel, string, TagType> TagTypes { get; protected set; }

		#endregion

		#region Public methods

		/// <summary>
		/// Get a tag from the cache or create a new one if not previously created.
		/// </summary>
		/// <param name="tagType">The tag type (part-of-speech) of this tag. It must belong to this model.</param>
		/// <param name="inflections">The inflections, if any, annotating this tag, else null. They must belong to this model.</param>
		/// <param name="discriminant">Optional discriminant for this tag, else null.</param>
		public Tag GetTag(TagType tagType, IEnumerable<Inflection> inflections = null, String discriminant = null)
		{
			var volatileTag = CreateTag(tagType, inflections, discriminant);

			var cachedTag = tagsDictionary.GetOrAdd(volatileTag, volatileTag);

			return cachedTag;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Create a fresh, non-cached tag.
		/// </summary>
		/// <param name="tagType">The tag type (part-of-speech) of this tag.</param>
		/// <param name="inflections">The inflections, if any, annotating this tag, else null.</param>
		/// <param name="discriminant">Optional discriminant for this tag, else null.</param>
		internal Tag CreateTag(TagType tagType, IEnumerable<Inflection> inflections = null, String discriminant = null)
		{
			if (tagType == null) throw new ArgumentNullException("tagType");

			if (!this.TagTypes.ContainsKey(tagType.Key))
				throw new ArgumentException("The tag type is not part of this grammar model.", "tagtype");

			if (inflections != null)
			{
				foreach (var inflection in inflections)
				{
					var inflectionType = inflection.Type;

					if (!this.InflectionTypes.ContainsKey(inflectionType.Key))
						throw new ArgumentException("An inflection's type does not belong to this grammar model.", "inflections");

					if (!inflectionType.Inflections.ContainsKey(inflection.Key))
						throw new ArgumentException("An inflection does not belong to its designated inflection type.", "inflections");
				}
			}

			return new Tag(tagType, inflections, discriminant);
		}

		private void EstablishInflectionTagRelationship(IEnumerable<Tuple<TagType, InflectionType>> inflectionTagRelationship)
		{
			RelationshipBuilder.ActManyToMany(
				this.TagTypes,
				this.InflectionTypes,
				inflectionTagRelationship,
				delegate(TagType tagType, IEnumerable<InflectionType> inflectionTypesOfTagType)
				{
					tagType.InflectionTypes = new ReadOnlyMap<string, InflectionType>(inflectionTypesOfTagType);
				},
				delegate(InflectionType inflectionType, IEnumerable<TagType> tagTypesOfInflectionType)
				{
					inflectionType.TagTypes = new ReadOnlyMap<string, TagType>(tagTypesOfInflectionType);
				}
			);
		}

		#endregion
	}
}
