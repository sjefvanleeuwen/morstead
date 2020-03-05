using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vs.Core.Collections.NodeTree
{
    //------------------------------------------------------------------------------
    /// <summary>
    /// Generic Tree Node base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //------------------------------------------------------------------------------
    public abstract class TreeNodeBase<T> : ITreeNode<T> where T : class, ITreeNode<T>
    {

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        //------------------------------------------------------------------------------
        protected TreeNodeBase(string name)
        {
            Name = name;
            ChildNodes = new List<T>();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Name
        /// </summary>
        //------------------------------------------------------------------------------
        public string Name
        {
            get;
            private set;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Parent
        /// </summary>
        //------------------------------------------------------------------------------
        public T Parent
        {
            get;
            set;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Children
        /// </summary>
        //------------------------------------------------------------------------------
        public List<T> ChildNodes
        {
            get;
            private set;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// this
        /// </summary>
        //------------------------------------------------------------------------------
        protected abstract T MySelf
        {
            get;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// True if a Leaf Node
        /// </summary>
        //------------------------------------------------------------------------------
        public bool IsLeaf
        {
            get { return ChildNodes.Count == 0; }
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// True if the Root of the Tree
        /// </summary>
        //------------------------------------------------------------------------------
        public bool IsRoot
        {
            get { return Parent == null; }
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// List of Leaf Nodes
        /// </summary>
        //------------------------------------------------------------------------------
        public List<T> GetLeafNodes()
        {
            return ChildNodes.Where(x => x.IsLeaf).ToList();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// List of Non Leaf Nodes
        /// </summary>
        //------------------------------------------------------------------------------
        public List<T> GetNonLeafNodes()
        {
            return ChildNodes.Where(x => !x.IsLeaf).ToList();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Get the Root Node of the Tree
        /// </summary>
        //------------------------------------------------------------------------------
        public T GetRootNode()
        {
            if (Parent == null)
                return MySelf;

            return Parent.GetRootNode();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Dot separated name from the Root to this Tree Node
        /// </summary>
        //------------------------------------------------------------------------------
        public string GetFullyQualifiedName()
        {
            if (Parent == null)
                return Name;

            return string.Format("{0}.{1}", Parent.GetFullyQualifiedName(), Name);
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Add a Child Tree Node
        /// </summary>
        /// <param name="child"></param>
        //------------------------------------------------------------------------------
        public void AddChild(T child)
        {
            child.Parent = MySelf;
            ChildNodes.Add(child);
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Add a collection of child Tree Nodes
        /// </summary>
        /// <param name="children"></param>
        //------------------------------------------------------------------------------
        public void AddChildren(IEnumerable<T> children)
        {
            foreach (T child in children)
                AddChild(child);
        }

    }
}
