using Microsoft.AspNetCore.Components;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Model;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared
{
    public partial class TreeNode
    {
        [Parameter]
        public INode Node { get; set; }

        string _nodeText => $"<strong>{Node.Name}</strong>" + KeyToText();


        private string KeyToText()
        {
            var result = string.Empty;
            Node.Key.Split(".").ToList().ForEach(s => result += $"<br />{s}");
            return result;
        }
    }
}
