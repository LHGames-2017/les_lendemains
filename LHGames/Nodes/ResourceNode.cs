
using AStar;
using StarterProject.Web.Api;

namespace LHGames.Nodes
{
    class ResourceNode : Node
    {
        public ResourceNode(Node goalNode, Point point, Node parent, TileType tileType) : base(goalNode, point, parent, tileType)
        {
        }

        public override void SetEstimatedCost(INode goal)
        {
            estimatedCost = 0;
        }

        protected override bool filterType(TileType type)
        {
            return type == TileType.R || base.filterType(type);
        }

        public override bool IsGoal(INode goal)
        {
            return TileType == TileType.R;
        }

        protected override Node create(Node goalNode, Point point, Node parent, TileType tileType)
        {
            return new ResourceNode(goalNode, point, parent, tileType);
        }

    }
}