using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Vs.Graph.Core.Data;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;
using Vs.Core;

namespace Vs.Graph.Core
{
    public class GraphController : IGraphController
    {
        private readonly SqlConnection sql;

        public GraphController(string connection)
        {
            sql = new SqlConnection(connection);
        }
        public bool DeleteAllEdges<T>() where T : IEdge
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllNodes<T>() where T : INode
        {
            throw new NotImplementedException();
        }

        public bool DeleteEdge<T>(T obj) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public bool DeleteEdges<T>(IEnumerable<T> list) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public bool DeleteNode<T>(T obj) where T : INode
        {
            throw new NotImplementedException();
        }

        public bool DeleteNodes<T>(IEnumerable<T> list) where T : INode
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllEdges<T>() where T : IEdge
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllNodes<T>() where T : INode
        {
            throw new NotImplementedException();
        }

        public T GetEdge<T>(int id) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public T GetNode<T>(int id) where T : INode
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertEdge<TEdge,TVan, TNaar>(TEdge edge, TVan from, TNaar to) 
            where TEdge : IEdge 
            where TVan : INode 
            where TNaar : INode
        {
            string edgeTable = typeof(TEdge).GetAttributeValue((TableAttribute att) => att.Name);
            string fromNode =typeof(TVan).GetAttributeValue((TableAttribute att) => att.Name);
            string  toNode = typeof(TNaar).GetAttributeValue((TableAttribute att) => att.Name);
            string query = @$"INSERT INTO {edgeTable} VALUES ((SELECT $node_id FROM {fromNode} WHERE ID = {from.Id},
                (SELECT $node_id FROM {toNode} WHERE ID = {to.Id}));";
            SqlCommand command = new SqlCommand(query, sql);
            return (int) await command.ExecuteScalarAsync();
        }

        public int InsertEdges<T>(IEnumerable<T> list) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertNode<T>(T obj) where T : class, INode
        {
            return await sql.InsertAsync(obj);
        }

        public async Task<int[]> InsertNodes<T>(IEnumerable<T> list) where T : class, INode
        {
            throw new NotImplementedException();
        }

        public bool UpdateEdge<T>(T obj) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public bool UpdateEdges<T>(IEnumerable<T> list) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public bool UpdateNode<T>(T obj) where T : INode
        {
            throw new NotImplementedException();
        }

        public bool UpdateNodes<T>(IEnumerable<T> list) where T : INode
        {
            throw new NotImplementedException();
        }
    }
}
