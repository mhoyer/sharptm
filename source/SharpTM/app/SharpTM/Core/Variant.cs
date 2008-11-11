using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IVariant"/> interface.
	/// </summary>
	public class Variant : DatatypeAware, IVariant
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Variant"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="topicMap">The topic map.</param>
		internal Variant(IName parent, ITopicMap topicMap)
			: base(parent, topicMap)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}

			Parent = parent;
		}
		#endregion

		#region IVariant properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
		/// </returns>
		public new IName Parent
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// An empty set represents the unconstrained scope.
		/// The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		public new ReadOnlyCollection<ITopic> Scope
		{
			get
			{
				List<ITopic> mergedScope = new List<ITopic>(base.Scope);
				mergedScope.AddRange(Parent.Scope);

				return mergedScope.AsReadOnly();
			}
		}
		#endregion
	}
}