// <copyright file="TopicToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
#if LOG4NET
using log4net;
#endif
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Index;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class TopicToBeMerged : IToBeMerged<ITopic>
	{
#if LOG4NET
		static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); 
#endif
		ITopic targetTopic;

		public TopicToBeMerged(ITopic topic)
		{
			ToBeMerged = topic;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public ITopic ToBeMerged
		{
			get;
			private set;
		}

		public void Into(ITopic target)
		{
#if LOG4NET
			log.InfoFormat("Merging topic '{0}' into '{1}'.", ToBeMerged, target);
#endif

			if (ToBeMerged.Reified != null &&
			    target.Reified != null &&
			    ToBeMerged.Reified != target.Reified)
			{
				throw new ModelConstraintException(
					target,
					"Unable to merge Topics. It is an error if A and B both have non-null values in their Reifier properties which are different.");
			}

			targetTopic = target;

			UnifyTypes();

			ReplaceScopes();
			ReplaceTypes();
			ReplaceRolesPlayed();
			ReplaceReifier();

			UnifyNames();
			UnifyOccurrences();
			UnifySubjectIdentifiers();
			UnifySubjectLocators();

			Merge.Construct(ToBeMerged).Into(target);

			ToBeMerged.Remove();

			if (ToBeMerged as Topic != null &&
				targetTopic as Topic != null)
			{
				((Topic) ToBeMerged).topicData = ((Topic) targetTopic).topicData;
			}
		}

		void ReplaceReifier()
		{
			if (ToBeMerged.Reified != null)
			{
				targetTopic.Reified.Reifier = ToBeMerged;
			}
		}

		void ReplaceRolesPlayed()
		{
			// Change Player property for each role of topic that is merging to the target property
			for (int i = 0; i < ToBeMerged.RolesPlayed.Count; i++)
			{
				Role role = (Role) ToBeMerged.RolesPlayed[i];
				role.Player = targetTopic;
			}

			// Try to find possible pairs of mergable roles
			for (int i = 0; i < targetTopic.RolesPlayed.Count - 1; i++)
			{
				Role roleTarget = (Role) targetTopic.RolesPlayed[i];
				Role roleToBeMerged = ((Topic) targetTopic).FindRole(roleTarget, true);

				if (roleToBeMerged != null)
				{
					Association associationTarget = (Association) roleTarget.Parent;
					Association associationToBeMerged = (Association) roleToBeMerged.Parent;

					// Remove roles temporary for comparison
					associationTarget.RemoveRole(roleTarget);
					associationToBeMerged.RemoveRole(roleToBeMerged);

					if (associationTarget.Equals(associationToBeMerged))
					{
						associationTarget.AddRole(roleTarget);
						associationTarget.AddRole(roleToBeMerged);

						Merge.Association(associationToBeMerged).Into(associationTarget);
						Merge.Role(roleTarget).Into(roleToBeMerged);
					}
					else
					{
						associationTarget.AddRole(roleTarget);
						associationToBeMerged.AddRole(roleToBeMerged);
					}
				}
			}
		}

		void ReplaceScopes()
		{
			ITopicMap topicMap = targetTopic.Parent;
			ScopedIndex index = topicMap.GetIndex<ScopedIndex>();

			if (!index.AutoUpdated)
			{
				index.Reindex();
			}

			foreach (IScoped scoped in index.GetScoped(ToBeMerged))
			{
				scoped.AddTheme(targetTopic);
				scoped.RemoveTheme(ToBeMerged);
			}
		}

		void ReplaceTypes()
		{
			ITopicMap topicMap = targetTopic.TopicMap;
			TypedInstanceIndex index = topicMap.GetIndex<TypedInstanceIndex>();

			if (!index.AutoUpdated)
			{
				index.Reindex();
			}

			foreach (ITyped typedConstruct in index.GetTyped(ToBeMerged))
			{
				typedConstruct.Type = targetTopic;
			}

			foreach (ITopic topic in index.GetTopics(ToBeMerged))
			{
				topic.RemoveType(ToBeMerged);
				topic.AddType(targetTopic);
			}
		}

		void UnifyNames()
		{
			for (int i = 0; i < ToBeMerged.Names.Count; i++)
			{
				if (ToBeMerged.Names[i] is Name)
				{
					Name nameToBeMerged = (Name) ToBeMerged.Names[i];
					Name nameTarget = ((Topic) targetTopic).FindName(nameToBeMerged, true);

					if (nameTarget != null)
					{
						Merge.Name(nameToBeMerged).Into(nameTarget);
						i--;
					}
					else
					{
						((Topic) targetTopic).AddName(nameToBeMerged);
					}
				}
			}
		}

		void UnifyOccurrences()
		{
			for (int i = 0; i < ToBeMerged.Occurrences.Count; i++)
			{
				if (ToBeMerged.Occurrences[i] is Occurrence)
				{
					Occurrence occurrenceToBeMerged = (Occurrence) ToBeMerged.Occurrences[i];
					Occurrence occurrenceTarget = ((Topic) targetTopic).FindOccurrence(occurrenceToBeMerged, true);

					if (occurrenceTarget != null)
					{
						Merge.Occurrence(occurrenceToBeMerged).Into(occurrenceTarget);
						i--;
					}
					else
					{
						((Topic) targetTopic).AddOccurrence(occurrenceToBeMerged);
					}
				}
			}
		}

		void UnifySubjectIdentifiers()
		{
			for (int i = 0; i < ToBeMerged.SubjectIdentifiers.Count; i++)
			{
				ILocator locator = ToBeMerged.SubjectIdentifiers[i];
				ToBeMerged.RemoveSubjectIdentifier(locator);

				if (!targetTopic.SubjectIdentifiers.Contains(locator))
				{
					targetTopic.AddSubjectIdentifier(locator);
				}
			}
		}

		void UnifySubjectLocators()
		{
			for (int i = 0; i < ToBeMerged.SubjectLocators.Count; i++)
			{
				ILocator locator = ToBeMerged.SubjectLocators[i];
				ToBeMerged.RemoveSubjectLocator(locator);

				if (!targetTopic.SubjectLocators.Contains(locator))
				{
					targetTopic.AddSubjectLocator(locator);
				}
			}
		}

		void UnifyTypes()
		{
			for (int i = 0; i < ToBeMerged.Types.Count; i++)
			{
				ITopic topic = ToBeMerged.Types[i];
				ToBeMerged.RemoveType(topic);

				if (!targetTopic.Types.Contains(topic))
				{
					targetTopic.AddType(topic);
				}
			}
		}
	}
}