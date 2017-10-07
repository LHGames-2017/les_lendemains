
using LHGames.Actions;
using StarterProject.Web.Api;
using System;

namespace LHGames
{
    class Strategy
    {
        bool went_home = false;
        public HighAction NextAction(Map map, GameInfo gameInfo)
        {
            if(!went_home && gameInfo.Player.Position != gameInfo.Player.HouseLocation)
            {
                went_home = true;
                return returnHomeAction(map, gameInfo);
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