using Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class With_XTMConnector : BDDTest
	{
		protected static XTMConnector cnx;
		Given an_XTMConnector = () => cnx = new XTMConnector();
	}
}