using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	//public class RepositoryFactory
	//{
	//    static readonly Dictionary<Type, IRepository> instances = new Dictionary<Type, IRepository>();

	//    public static IRepository<TConnector> GetInstance<TConnector>() where TConnector : IConnector, new()
	//    {
	//        if (instances.ContainsKey(typeof(TConnector)))
	//        {
	//            return (IRepository<TConnector>)instances[typeof(IRepository<TConnector>)];
	//        }

	//        TConnector connector = new TConnector();
	//        instances.Add(typeof(TConnector), connector);

	//        return connector;
	//    }
	//}
}