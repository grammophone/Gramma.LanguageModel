using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Grammophone.GenericContentModel;

namespace Grammophone.LanguageModel
{
	/// <summary>
	/// A child of a container indexed by name and 
	/// holding a friendly <see cref="Name"/> property.
	/// </summary>
	/// <typeparam name="P">The type of the child's <see cref="IChild{P}.Parent"/>.</typeparam>
	/// <typeparam name="K">The type of the child's <see cref="IKeyedElement{K}.Key"/>.</typeparam>
	[Serializable]
	public class NamedKeyedChild<P, K> : IKeyedChild<P, K>
		where P : class
		where K : class
	{
		#region Private fields

		/// <summary>
		/// The backing field for <see cref="IChild{P}.Parent"/> explicit implementation.
		/// </summary>
		protected P parent;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The child's key.</param>
		/// <param name="name">The child's friendly name.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		public NamedKeyedChild(K key, string name)
			: this(key, name, null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The child's key.</param>
		/// <param name="name">The child's friendly name.</param>
		/// <param name="parent">Optionally, the parent of the object, else null.</param>
		/// <exception cref="ArgumentNullException">
		/// When either <paramref name="key"/> or <paramref name="name"/> are null.
		/// </exception>
		public NamedKeyedChild(K key, string name, P parent)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (name == null) throw new ArgumentNullException("name");

			this.Key = key;
			this.Name = name;
			this.parent = parent;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The friendly name of this entity.
		/// </summary>
		public string Name { get; protected set; }

		#endregion

		#region Explicit IChild<P> Members

		P IChild<P>.Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		#endregion

		#region IKeyedElement<K> Members

		public K Key
		{
			get;
			private set;
		}

		#endregion
	}
}
