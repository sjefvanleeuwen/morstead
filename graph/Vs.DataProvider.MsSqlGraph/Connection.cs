using Vs.Graph.Core.Data;

namespace Vs.DataProvider.MsSqlGraph
{
    public class Connection : IConnection
    {
        private string _connection;

        string IConnection.Connection => _connection;

        public Connection(string connection)
        {
            _connection = connection;
        }
    }
}
