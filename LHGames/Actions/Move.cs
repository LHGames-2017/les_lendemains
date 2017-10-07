
using LHGames.Nodes;
using StarterProject.Web.Api;
using System;
using System.Collections.Generic;

namespace LHGames.Actions
{
    class Move : HighAction
    {
        private Point[] path;
        int idx = 0;

        public Move(GameInfo gameInfo, Map map, Point target)
        {
            Node goal = new Node(null, target, null, map.tileTypeMap[target.X, target.Y]);
            Node start = new Node(goal, gameInfo.Player.Position, null, map.tileTypeMap[gameInfo.Player.Position.X, gameInfo.Player.Position.Y]);
            var a = new AStar.AStar(start, goal);
            var status = a.Run();
            if(status != AStar.State.GoalFound)
            {
                path = null;
                Console.WriteLine("Cannot find path");
            }
            else
            {
                var nodes = a.GetPath();
                int size = 0;
                foreach(var _ in nodes) ++size;
                path = new Point[size];

                int idx = 0;
                foreach(var n in nodes)
                {
                    Node node = (Node)n;
                    path[idx++] = node.Point;
                }
            }
        }

        public string NextAction(Map map, GameInfo gameInfo)
        {
            if (path == null)
            {
                return null;
            }
            else if(idx < path.Length)
            {
                return AIHelper.CreateMoveAction(path[idx++]);
            }
            else
            {
                return null;
            }
        }
    }
}