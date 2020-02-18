using System.Collections.Generic;
using Vs.Graph.Core.Data;

namespace Vs.Graph.Core
{
    public interface IGraphController
    {
        T GetNode<T>(int id) where T : INode;
        IEnumerable<T> GetAllNodes<T>() where T : INode;
        int InsertNode<T>(T obj) where T : INode;
        int InsertNodes<T>(IEnumerable<T> list) where T : INode;
        bool UpdateNode<T>(T obj) where T : INode;
        bool UpdateNodes<T>(IEnumerable<T> list) where T : INode;
        bool DeleteNode<T>(T obj) where T : INode;
        bool DeleteNodes<T>(IEnumerable<T> list) where T : INode;
        bool DeleteAllNodes<T>() where T : INode;
        T GetEdge<T>(int id) where T : IEdge;
        IEnumerable<T> GetAllEdges<T>() where T : IEdge;
        int InsertEdge<T, TVan, TNaar>(T obj, TVan van, TNaar naar)
            where T : IEdge
            where TVan : INode
            where TNaar : INode;
        int InsertEdges<T>(IEnumerable<T> list) where T : IEdge;
        bool UpdateEdge<T>(T obj) where T : IEdge;
        bool UpdateEdges<T>(IEnumerable<T> list) where T : IEdge;
        bool DeleteEdge<T>(T obj) where T : IEdge;
        bool DeleteEdges<T>(IEnumerable<T> list) where T : IEdge;
        bool DeleteAllEdges<T>() where T : IEdge;
    }
}