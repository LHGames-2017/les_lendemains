
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
            Point player = gameInfo.Player.Position - new Point(1, 1);
            for(int edge = 1; edge < map.tileTypeMap.GetLength(0); edge++)
            {
                for(int i = player.X - edge; i <= player.X + edge && i >= 0 && i < map.tileTypeMap.GetLength(0); i++)
                {
                    for(int j = player.Y - edge; j <= player.Y + edge && j >= 0 && j < map.tileTypeMap.GetLength(1); j++)
                    {
                        if(map.tileTypeMap[i,j] == TileType.R)
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
            return null;
        }
    }
}