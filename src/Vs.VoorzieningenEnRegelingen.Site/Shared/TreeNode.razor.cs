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

        private void InitTree()
        {
            var nodeWoonland = new Node
            {
                Key = "Woonland",
                Name = "Woonland"
            };

            var nodeWoonlandGeenRecht = new Node
            {
                Key = "Woonland_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeWoonsituatie = new Node
            {
                Key = "Woonsituatie",
                Name = "Woonsituatie"
            };

            var nodeVermogensdrempelAlleenstaande = new Node
            {
                Key = "Alleenstaande.Vermogensdrempel",
                Name = "VermogensDrempel"
            };

            var nodeVermogensdrempelToeslagpartner = new Node
            {
                Key = "Toeslagpartner.Vermogensdrempel",
                Name = "VermogensDrempel"
            };

            var nodeVermogensdrempelAlleenstaandeGeenRecht = new Node
            {
                Key = "Alleenstaande.Vermogensdrempel_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeInkomensdrempelAlleenstaande = new Node
            {
                Key = "Alleenstaande.Inkomensdrempel",
                Name = "Inkomensdrempel"
            };

            var nodeVermogensdrempelToeslagpartnerGeenRecht = new Node
            {
                Key = "Toeslagpartner.Vermogensdrempel_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeInkomensdrempelToeslagpartner = new Node
            {
                Key = "Toeslagpartner.Inkomensdrempel",
                Name = "Inkomensdrempel"
            };

            var nodeInkomensdrempelAlleenstaandeGeenRecht = new Node
            {
                Key = "Alleenstaande.Inkomensdrempel_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeToetsingsInkomenAlleenstaande = new Node
            {
                Key = "Alleenstaande.ToetsingsInkomen",
                Name = "ToetsingsInkomen"
            };

            var nodeInkomensdrempelToeslagpartnerGeenRecht = new Node
            {
                Key = "Toeslagpartner.Inkomensdrempel_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeToetsingsInkomenToeslagpartner = new Node
            {
                Key = "Toeslagpartner.ToetsingsInkomen",
                Name = "ToetsingsInkomen"
            };

            var nodeToetsingsInkomenAlleenstaandeGeenRecht = new Node
            {
                Key = "Alleenstaande.ToetsingsInkomen_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeZorgToeslagAlleenstaande = new Node
            {
                Key = "Alleenstaande.Zorgtoeslag",
                Name = "ToetsingsInkomen"
            };

            var nodeToetsingsInkomenToeslagpartnerGeenRecht = new Node
            {
                Key = "Toeslagpartner.ToetsingsInkomen_GeenRecht",
                Name = "Geen Recht"
            };

            var nodeZorgToeslagToeslagpartner = new Node
            {
                Key = "Toeslagpartner.Zorgtoeslag",
                Name = "ToetsingsInkomen"
            };

            //BaseNode = nodeWoonland;
            nodeWoonland.AddSubNodes(new[] { nodeWoonsituatie, nodeWoonlandGeenRecht });
            nodeWoonsituatie.AddSubNodes(new[] { nodeVermogensdrempelAlleenstaande, nodeVermogensdrempelToeslagpartner });
            nodeVermogensdrempelAlleenstaande.AddSubNodes(new[] { nodeVermogensdrempelAlleenstaandeGeenRecht, nodeInkomensdrempelAlleenstaande });
            nodeVermogensdrempelToeslagpartner.AddSubNodes(new[] { nodeVermogensdrempelToeslagpartnerGeenRecht, nodeInkomensdrempelToeslagpartner });
            nodeInkomensdrempelAlleenstaande.AddSubNodes(new[] { nodeInkomensdrempelAlleenstaandeGeenRecht, nodeToetsingsInkomenAlleenstaande });
            nodeInkomensdrempelToeslagpartner.AddSubNodes(new[] { nodeInkomensdrempelToeslagpartnerGeenRecht, nodeToetsingsInkomenToeslagpartner });
            nodeToetsingsInkomenAlleenstaande.AddSubNodes(new[] { nodeToetsingsInkomenAlleenstaandeGeenRecht, nodeZorgToeslagAlleenstaande });
            nodeToetsingsInkomenToeslagpartner.AddSubNodes(new[] { nodeToetsingsInkomenToeslagpartnerGeenRecht, nodeZorgToeslagToeslagpartner });
        }
    }
}
