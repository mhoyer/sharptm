using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Index
{
	/// <summary>
	/// Implements the <see cref="IScopedIndex"/> interface.
	/// </summary>
	public class ScopedIndex : Index, IScopedIndex
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IAssociation"/> scope.
		/// </summary>
		private readonly List<ITopic> associationThemes;

		private readonly ReadOnlyCollection<ITopic> associationThemesReadOnly;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IName"/> scope.
		/// </summary>
		private readonly List<ITopic> nameThemes;

		private readonly ReadOnlyCollection<ITopic> nameThemesReadOnly;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IOccurrence"/> scope.
		/// </summary>
		private readonly List<ITopic> occurrenceThemes;

		private readonly ReadOnlyCollection<ITopic> occurrenceThemesReadOnly;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IVariant"/> scope.
		/// </summary>
		private readonly List<ITopic> variantThemes;

		private readonly ReadOnlyCollection<ITopic> variantThemesReadOnly;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="ScopedIndex"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system this index is based on.</param>
		/// <param name="enableAutoUpdate">if set to <c>true</c> auto update will be enabled.</param>
		public ScopedIndex(ITopicMapSystem topicMapSystem, bool enableAutoUpdate)
			: base(topicMapSystem, enableAutoUpdate)
		{
			associationThemes = new List<ITopic>();
			associationThemesReadOnly = associationThemes.AsReadOnly();

			nameThemes = new List<ITopic>();
			nameThemesReadOnly = nameThemes.AsReadOnly();

			occurrenceThemes = new List<ITopic>();
			occurrenceThemesReadOnly = occurrenceThemes.AsReadOnly();

			variantThemes = new List<ITopic>();
			variantThemesReadOnly = variantThemes.AsReadOnly();
		}
		#endregion

		#region IScopedIndex properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the scope property of <see cref="T:TMAPI.Net.Core.IAssociation"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> AssociationThemes
		{
			get
			{
				return associationThemesReadOnly;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the scope property of <see cref="T:TMAPI.Net.Core.IName"/>s. 
		///     The return value may be empty but must never be <tt>null</tt>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> NameThemes
		{
			get
			{
				return nameThemesReadOnly;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the scope property of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s. 
		///     The return value may be empty but must never be <tt>null</tt>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> OccurrenceThemes
		{
			get
			{
				return occurrenceThemesReadOnly;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the scope property of <see cref="T:TMAPI.Net.Core.IVariant"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> VariantThemes
		{
			get
			{
				return variantThemesReadOnly;
			}
		}
		#endregion

		#region IScopedIndex methods
		/// <summary>
		///     Synchronize the index with data in the topic map.
		/// </summary>
		public override void Reindex()
		{
			associationThemes.Clear();
			nameThemes.Clear();
			occurrenceThemes.Clear();
			variantThemes.Clear();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ReindexTopicMap(TopicMapSystem.GetTopicMap(locator));
			}
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
		///     scope property contains the specified <paramref name="theme"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="theme">
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> which must be part of the scope. 
		///     If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the 
		///     unconstrained scope are returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
		/// </returns>
		public ReadOnlyCollection<IAssociation> GetAssociations(ITopic theme)
		{
			return GetAssociations(new ITopic[1] { theme }, true);
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
		///     scope property equals one of those <paramref name="themes"/> at least. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="themes">
		///     Scope of the <see cref="T:TMAPI.Net.Core.IAssociation"/>s to be returned.
		/// </param>
		/// <param name="matchAll">
		///     If <c>true</c> the scope property of an association must match all <paramref name="themes"/>, 
		///     if <c>false</c> one theme must be matched at least.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IAssociation> GetAssociations(ITopic[] themes, bool matchAll)
		{
			List<IAssociation> foundAssociations = new List<IAssociation>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				if (topicMap != null)
				{
					foreach (IAssociation association in topicMap.Associations)
					{
						if (AreThemesMatchingScoped(association, themes, matchAll))
						{
							foundAssociations.Add(association);
						}
					}
				}
			}

			return foundAssociations.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
		///     scope property contains the specified <paramref name="theme"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="theme">
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> which must be part of the scope. 
		///     If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IName"/>s in the 
		///     unconstrained scope are returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
		/// </returns>
		public ReadOnlyCollection<IName> GetNames(ITopic theme)
		{
			return GetNames(new ITopic[1] { theme }, true);
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
		///     scope property equals one of those <paramref name="themes"/> at least. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="themes">
		///     Scope of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
		/// </param>
		/// <param name="matchAll">
		///     If <c>true</c> the scope property of an name must match all <paramref name="themes"/>, 
		///     if <c>false</c> one theme must be matched at least.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IName> GetNames(ITopic[] themes, bool matchAll)
		{
			List<IName> foundNames = new List<IName>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (ITopic topic in topicMap.Topics)
				{
					foreach (IName name in topic.Names)
					{
						if (AreThemesMatchingScoped(name, themes, matchAll))
						{
							foundNames.Add(name);
						}
					}
				}
			}

			return foundNames.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
		///     scope property contains the specified <paramref name="theme"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="theme">
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> which must be part of the scope. 
		///     If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the 
		///     unconstrained scope are returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic theme)
		{
			return GetOccurrences(new ITopic[1] { theme }, true);
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
		///     scope property equals one of those <paramref name="themes"/> at least. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="themes">
		///     Scope of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
		/// </param>
		/// <param name="matchAll">
		///     If <c>true</c> the scope property of an occurrence must match all <paramref name="themes"/>, 
		///     if <c>false</c> one theme must be matched at least.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic[] themes, bool matchAll)
		{
			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (ITopic topic in topicMap.Topics)
				{
					foreach (IOccurrence occurrence in topic.Occurrences)
					{
						if (AreThemesMatchingScoped(occurrence, themes, matchAll))
						{
							foundOccurrences.Add(occurrence);
						}
					}
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
		///     scope property contains the specified <paramref name="theme"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="theme">
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> which must be part of the scope. 
		///     This must not be null <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="theme"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IVariant> GetVariants(ITopic theme)
		{
			return GetVariants(new ITopic[1] { theme }, true);
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
		///     scope property equals one of those <paramref name="themes"/> at least. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="themes">
		///     Scope of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
		/// </param>
		/// <param name="matchAll">
		///     If <c>true</c> the scope property of an variant must match all <paramref name="themes"/>, 
		///     if <c>false</c> one theme must be matched at least.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IVariant> GetVariants(ITopic[] themes, bool matchAll)
		{
			List<IVariant> foundVariants = new List<IVariant>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (ITopic topic in topicMap.Topics)
				{
					foreach (IName name in topic.Names)
					{
						foreach (IVariant variant in name.Variants)
						{
							if (AreThemesMatchingScoped(variant, themes, matchAll))
							{
								foundVariants.Add(variant);
							}
						}
					}
				}
			}

			return foundVariants.AsReadOnly();
		}
		#endregion

		#region methods
		private static bool AreThemesMatchingScoped<T>(T scoped, ITopic[] themes, bool matchAll)
			where T : IScoped
		{
			bool matches = matchAll;

			foreach (ITopic theme in themes)
			{
				if (matchAll)
				{
					if ((theme != null && !scoped.Scope.Contains(theme)) ||
					    (theme == null && scoped.Scope.Count > 0))
					{
						matches = false;
						break;
					}
				}
				else
				{
					if ((theme == null && scoped.Scope.Count == 0) ||
					    (theme != null && scoped.Scope.Contains(theme)))
					{
						matches = true;
						break;
					}
				}
			}

			return matches;
		}

		private void ReindexTopicMap(ITopicMap topicMap)
		{
			foreach (ITopic topic in topicMap.Topics)
			{
				foreach (IName name in topic.Names)
				{
					nameThemes.AddRange(name.Scope);

					foreach (IVariant variant in name.Variants)
					{
						variantThemes.AddRange(variant.Scope);
					}
				}

				foreach (IOccurrence occurrence in topic.Occurrences)
				{
					occurrenceThemes.AddRange(occurrence.Scope);
				}
			}

			foreach (IAssociation association in topicMap.Associations)
			{
				associationThemes.AddRange(association.Scope);
			}
		}
		#endregion
	}
}