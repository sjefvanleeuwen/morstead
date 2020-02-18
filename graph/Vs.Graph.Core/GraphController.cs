using System;
using System.Collections.Generic;
using Vs.Graph.Core.Data;

namespace Vs.Graph.Core
{
    public class GraphController : IGraphController
    {
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

        public int InsertEdge<T,TNode>(T obj, TNode van, TNode naar) where T : IEdge where TNode : INode
        {
            throw new NotImplementedException();
        }

        public int InsertEdges<T>(IEnumerable<T> list) where T : IEdge
        {
            throw new NotImplementedException();
        }

        public int InsertNode<T>(T obj) where T : INode
        {
            throw new NotImplementedException();
        }

        public int InsertNodes<T>(IEnumerable<T> list) where T : INode
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
