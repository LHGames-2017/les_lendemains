
using LHGames.Actions;
using StarterProject.Web.Api;
using System;
using System.Collections.Generic;

namespace LHGames
{
    class Strategy
    {
        
        public HighAction NextAction(Map map, GameInfo gameInfo)
        {
            var action = DefendSelf(map, gameInfo);
            if (action != null)
                return action;
            if (gameInfo.Player.CarriedResources < gameInfo.Player.CarryingCapacity)
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
            double closest = 0;
            Point target = null;
            for (int i = 0; i < map.tileTypeMap.GetLength(0); i++)
            {
                for (int j = 0; j < map.tileTypeMap.GetLength(1); j++)
                {
                    if (map.tileTypeMap[i, j] == TileType.R)
                    {
                        if (target == null)
                        {
                            target = new Point(i, j);
                            closest = Point.Distance(gameInfo.Player.Position, target);
                        }
                        else
                        {
                            if (Point.Distance(gameInfo.Player.Position, new Point(i, j)) < closest)
                            {
                                target = new Point(i, j);
                                closest = Point.Distance(gameInfo.Player.Position, target);
                            }
                        }
                    }
                }
            }
            // Console.WriteLine("Found " + (target?.ToString() ?? "null"));
            if (target == null)
            {
                return null;
            }
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