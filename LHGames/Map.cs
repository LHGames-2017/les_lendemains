using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames
{
    public class Map
    {
        public Dictionary<Point, KeyValuePair<string, PlayerInfo>> playersDictionnary = new Dictionary<Point, KeyValuePair<string, PlayerInfo>>();
        public TileType[,] tileTypeMap = new TileType[1024,1024];
        public Map()
        {
            for (int i = 0; i < tileTypeMap.GetLength(0); i++)
            {
                for (int j = 0; j < tileTypeMap.GetLength(1); j++)
                {
                    tileTypeMap[i, j] = TileType.U;
                }
            }
        }
        public void UpdateOtherPLayerMap(List<KeyValuePair<string, PlayerInfo>> OtherPlayers)
        {
            foreach (KeyValuePair<string, PlayerInfo> playerInfo in OtherPlayers)
            {
                playersDictionnary[playerInfo.Value.Position] = playerInfo;
            }
        }
        public void UpdateMap(Tile[,] vision)
        {
            for (int i = 0; i < vision.GetLength(0); ++i)
            {
                for (int j = 0; j < vision.GetLength(1); j++)
                {
                    tileTypeMap[vision[i, j].X, vision[i, j].Y] = (TileType)vision[i, j].C;
                }
            }
        }
    }
}
