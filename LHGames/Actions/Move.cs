
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
                path = new Point[size - 1];

                int idx = 0;
                foreach(var n in nodes)
                {
                    if (idx != 0)
                    {
                        Node node = (Node)n;
                        path[idx - 1] = node.Point;
                    }
                    idx++;
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
                Point p = path[idx];
                var type = map.tileTypeMap[p.X, p.Y];
                if (type == TileType.L || type == TileType.R || type == TileType.U)
                {
                    throw new Cancel();
                }
                else if (type == TileType.W)
                {
                    Console.WriteLine("RAWR");
                    return AIHelper.CreateAttackAction(p);
                    
                }
                idx++;
                return AIHelper.CreateMoveAction(p);
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            if (path.Length > 0)
            {
                return "Move to " + path[path.Length - 1];
            }
            else
            {
                return "";
            }
        }

        public static Move MoveAdjencent(GameInfo gameInfo, Map m, Point target)
        {
            Point diff = target - gameInfo.Player.Position;
            if(diff.X >= diff.Y)
            {
                diff = new Point(diff.X > 0 ? 1 : -1, 0);
            }
            else
            {
                diff = new Point(0, diff.Y > 0 ? 1 : -1);
            }
            return new Move(gameInfo, m, target - diff);
        }
    }
}