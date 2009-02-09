using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class TopicMapToBeMerged : IToBeMerged<ITopicMap>
	{
		ITopicMap topicMapTarget;

		public TopicMapToBeMerged(ITopicMap topicMap)
		{
			ToBeMerged = topicMap;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public ITopicMap ToBeMerged
		{
			get;
			private set;
		}

		/// <summary>
		/// Merges <see cref="IToBeMerged{T}.ToBeMerged"/> into the specified target.
		/// </summary>
		/// <param name="target">The target to merge in.</param>
		public void Into(ITopicMap target)
		{
			topicMapTarget = target;

			Merge.Construct(ToBeMerged).Into(target);
			UnifyTopics();

			ToBeMerged.Remove();
		}

		void UnifyTopics()
		{
			for (int i = 0; i < ToBeMerged.Topics.Count; i++)
			{
				if (ToBeMerged.Topics[i] is Topic)
				{
					Topic topicToBeMerged = (Topic) ToBeMerged.Topics[i];
					Topic topicTarget = ((TopicMap) topicMapTarget).FindDuplicate(topicToBeMerged);

					if (topicTarget != null)
					{
						Merge.Topic(topicToBeMerged).Into(topicTarget);
					}
					else
					{
						((TopicMap) topicMapTarget).AddTopic(topicToBeMerged);
					}
				}
			}
		}
	}
}