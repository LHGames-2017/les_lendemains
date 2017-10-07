
using LHGames.Actions;
using LHGames.Nodes;
using StarterProject.Web.Api;
using System;
using System.Collections.Generic;

namespace LHGames
{
    class Strategy
    {
        bool went_home = false;
        public HighAction NextAction(Map map, GameInfo gameInfo)
        {
            if (!went_home)
            {
                went_home = true;
                if (gameInfo.Player.Position != gameInfo.Player.HouseLocation)
                {
                    return returnHomeAction(map, gameInfo);
                }
            }
            if(gameInfo.Player.CarriedResources < gameInfo.Player.CarryingCapacity)
            {
                return collectAction(map, gameInfo);
            }
            else
            {
                return returnHomeAction(map, gameInfo);
            }
        }

        private HighAction returnHomeAction(Map map, GameInfo gameInfo)
        {
            return new Move(gameInfo, map, gameInfo.Player.HouseLocation);
        }

        private HighAction DefendHome(Map map, GameInfo gameInfo)
        {
            return null;
        }

        private HighAction DefendSelf(Map map, GameInfo gameInfo)
        {
            Player player = gameInfo.Player;
            foreach (KeyValuePair<string, PlayerInfo> otherPlayer in gameInfo.OtherPlayers)
            {
                if (Point.DistanceManhatan(player.Position, otherPlayer.Value.Position) <= 1)
                {
                    return new Attack(otherPlayer.Value);
                }
            }
            return null;

        }

        private HighAction KillPlayer(Map map, GameInfo gameInfo)
        {
            return null;
        }

        private HighAction collectAction(Map map, GameInfo gameInfo)
        {
            Point player = gameInfo.Player.Position;
            for (int edge = 1; edge < map.tileTypeMap.GetLength(0); edge++)
            {
                for (int i = player.X - edge; i <= player.X + edge && i >= 0 && i < map.tileTypeMap.GetLength(0); i++)
                {
                    for (int j = player.Y - edge; j <= player.Y + edge && j >= 0 && j < map.tileTypeMap.GetLength(1); j++)
                    {
                        if (map.tileTypeMap[i, j] == TileType.R)
                        {
                            Point target = new Point(i, j);
                            if (Point.DistanceManhatan(target, gameInfo.Player.Position) <= 1)
                            {
                                return new Collect(gameInfo, target);
                            }
                            else
                            {
                                return MultipleActions.MoveThenCollect(gameInfo, map, target);
                            }
                        }
                    }
                }
            }
<<<<<<< HEAD
            return exploreAction(map,gameInfo);
        }
        private HighAction exploreAction(Map map, GameInfo gameInfo)
        {
            Point player = gameInfo.Player.Position;
            for (int edge = 1; edge < map.tileTypeMap.GetLength(0); edge++)
            {
                for (int i = player.X - edge; i <= player.X + edge && i >= 0 && i < map.tileTypeMap.GetLength(0); i++)
                {
                    for (int j = player.Y - edge; j <= player.Y + edge && j >= 0 && j < map.tileTypeMap.GetLength(1); j++)
                    {
                        if (map.tileTypeMap[i, j] == TileType.U)
                        {
                            Point target = new Point(i, j);
                            if (Point.DistanceManhatan(target, gameInfo.Player.Position) <= 1)
                            {
                                return new Collect(gameInfo, target);
                            }
                            else
                            {
                                return new Actions.Move(gameInfo, map, target);
                            }
                        }
                    }
                }
            }
            //ResourceNode node = new ResourceNode(null, player, null, map.tileTypeMap[player.X, player.Y]);
            //var astar = new AStar.AStar(node, null);
            //if(astar.Run() == AStar.State.GoalFound)
            //{
            //    var nodes = astar.GetPath();
            //    Point final = null;
            //    foreach(var n in nodes)
            //    {
            //        final = n.Point;
            //    }
            //    return MultipleActions.MoveThenCollect(gameInfo, map, final);
            //}
            return null;
        }

    }
}