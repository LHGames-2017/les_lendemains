using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames
{
    public class Map
    {

        public TileType[,] tileTypeMap = new TileType[16000,16000];
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
