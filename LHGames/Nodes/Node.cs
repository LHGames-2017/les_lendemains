using AStar;
using StarterProject.Web.Api;
using StarterProject.Web.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Nodes
{
    public class Node : INode
    {
        public TileType TileType { get; set; }
        public Node(Node goalNode, Point point, Node parent, TileType tileType)
        {
            this.goalNode = goalNode;
            Point = point;
            Parent = parent;
            TileType = TileType;
        }
        private Node goalNode;
        public Point Point { get; set; }
        private bool isOpenList { get; set; }
        private bool isClosedList { get; set; }
        /// <summary>
        /// Determines if this node is on the open list.
        /// </summary>
        public bool IsOpenList(IEnumerable<INode> openList)
        {
            foreach (var node in openList)
            {
                if (Point.X == node.Point.X && Point.Y == node.Point.Y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets this node to be on the open list.
        /// </summary>
        public void SetOpenList(bool value)
        {
            isOpenList = value;
        }

        /// <summary>
        /// Determines if this node is on the closed list.
        /// </summary>
        public bool IsClosedList(IEnumerable<INode> closedList)
        {
            foreach (var node in closedList)
            {
                if (Point.X == node.Point.X && Point.Y == node.Point.Y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets this node to be on the open list.
        /// </summary>
        public void SetClosedList(bool value)
        {
            isClosedList = value;
        }

        /// <summary>
        /// Gets the total cost for this node.
        /// f = g + h
        /// TotalCost = MovementCost + EstimatedCost
        /// </summary>
        public int TotalCost
        {
            get
            {
                return MovementCost + EstimatedCost;
            }
        }

        /// <summary>
        /// Gets the movement cost for this node.
        /// This is the movement cost from this node to the starting node, or g.
        /// </summary>
        public int MovementCost { get { return movementCost; } }
        private int movementCost;
        /// <summary>
        /// Gets the estimated cost for this node.
        /// This is the heuristic from this node to the goal node, or h.
        /// </summary>
        public int EstimatedCost { get { return estimatedCost; } }
        private int estimatedCost;
        /// <summary>
        /// Sets the movement cost for the current node, or g.
        /// </summary>
        /// <param name="parent">Parent node, for access to the parents movement cost.</param>
        public void SetMovementCost(INode parent)
        {
            movementCost = parent.MovementCost + 1;
        }

        /// <summary>
        /// Sets the estimated cost for the current node, or h.
        /// </summary>
        /// <param name="goal">Goal node, for acces to the goals position.</param>
        public void SetEstimatedCost(INode goal)
        {
            estimatedCost = (int)Point.DistanceManhatan(Point, goal.Point);
        }

        /// <summary>
        /// Gets or sets the parent node to this node.
        /// </summary>
        public INode Parent { get; set; }

        /// <summary>
        /// Gets this node's children.
        /// </summary>
        /// <remarks>The children can be setup in a graph before starting the
        /// A* algorithm or they can be dynamically generated the first time
        /// the A* algorithm calls this property.</remarks>
        public IEnumerable<INode> Children {
            get
            {
                List<Node> childs = new List<Node>();
                if (Point.X + 1 < 16000)
                {
                    childs.Add(new Node(goalNode, new Point(Point.X + 1, Point.Y), this, GameController.worldMap.tileTypeMap[Point.X + 1, Point.Y]));
                }
                if (Point.X - 1 >= 0)
                {
                    childs.Add(new Node(goalNode, new Point(Point.X - 1, Point.Y), this, GameController.worldMap.tileTypeMap[Point.X - 1, Point.Y]));
                }
                if (Point.Y -1 >= 0)
                {
                    childs.Add(new Node(goalNode, new Point(Point.X, Point.Y - 1), this, GameController.worldMap.tileTypeMap[Point.X, Point.Y - 1]));
                }
                if (Point.Y + 1 < 16000)
                {
                    childs.Add(new Node(goalNode, new Point(Point.X, Point.Y + 1), this, GameController.worldMap.tileTypeMap[Point.X, Point.Y + 1]));
                }
                return childs;
            }
        }
        /// <summary>
        /// Returns true if this node is the goal, false if it is not the goal.
        /// </summary>
        /// <param name="goal">The goal node to compare this node against.</param>
        /// <returns>True if this node is the goal, false if it s not the goal.</returns>
        public bool IsGoal(INode goal) { return Point.X == goal.Point.X && Point.Y == goal.Point.Y; }
    }
}
