using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vs.Core.Collections.NodeTree
{
    
    //------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    //------------------------------------------------------------------------------
    public class SpecialTreeNode : TreeNodeBase<SpecialTreeNode>
    {
        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        //------------------------------------------------------------------------------
        public SpecialTreeNode(string name)
            : base(name)
        {
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        protected override SpecialTreeNode MySelf
        {
            get { return this; }
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public bool IsSpecial
        {
            get;
            set;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //------------------------------------------------------------------------------
        public List<SpecialTreeNode> GetSpecialNodes()
        {
            return ChildNodes.Where(x => x.IsSpecial).ToList();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //------------------------------------------------------------------------------
        public List<SpecialTreeNode> GetSpecialLeafNodes()
        {
            return ChildNodes.Where(x => x.IsSpecial && x.IsLeaf).ToList();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //------------------------------------------------------------------------------
        public List<SpecialTreeNode> GetSpecialNonLeafNodes()
        {
            return ChildNodes.Where(x => x.IsSpecial && !x.IsLeaf).ToList();
        }

    } 
}
