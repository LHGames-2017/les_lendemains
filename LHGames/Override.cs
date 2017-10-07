using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames;

namespace StarterProject.Web.Api
{
    public static class Override
    {
        public static string AllOverwrites(GameInfo gameInfo, Tile[,] carte)
        {
            string overwrite = HouseOverwrite(gameInfo, carte);
            if (overwrite != null)
            {
                return overwrite;
            }
            string overwrite2 = ShopOverwrite(gameInfo, carte);
            if (overwrite2 != null)
            {
                return overwrite2;
            }
            return null;
        }
        
        private static bool previouslyOnHouse = false;
        private static Object important_lock = new object();
        private static string HouseOverwrite(GameInfo gameInfo, Tile[,] carte)
        {
            lock (important_lock)
            {
                string returnValue = null;

                // If we are on the house, do upgrades
                if (gameInfo.Player.Position.X == gameInfo.Player.HouseLocation.X && gameInfo.Player.Position.Y == gameInfo.Player.HouseLocation.Y)
                {
                    if (!previouslyOnHouse)
                    {
                        MyPlayer myPlayer = Pastebin.GetSavedObject<MyPlayer>(Pastebin.MY_PLAYER_STATS_URL);
                        returnValue = AIHelper.CreateUpgradeAction(myPlayer.NextUpgrade);
                    }
                    previouslyOnHouse = true;
                }
                else
                {
                    previouslyOnHouse = false;
                }
                return returnValue;
            }
        }

        private static bool previouslyOnShop = false;
        private static Object importanter_lock = new object();
        private static string ShopOverwrite(GameInfo gameInfo, Tile[,] carte)
        {
            lock (importanter_lock)
            {
                string returnValue = null;

                // Find on what tile we are
                TileContent myTile = TileContent.Empty;
                foreach (Tile t in carte)
                {
                    if (t.X == gameInfo.Player.Position.X && t.Y == gameInfo.Player.Position.Y)
                    {
                        myTile = (TileContent)t.C;
                        break;
                    }
                }

                // If we are on the shop, buy stuff
                if (myTile == TileContent.Shop)
                {
                    if (!previouslyOnShop)
                    {
                        MyPlayer myPlayer = Pastebin.GetSavedObject<MyPlayer>(Pastebin.MY_PLAYER_STATS_URL);
                        returnValue = AIHelper.CreatePurchaseAction(myPlayer.NextItem);
                    }
                    previouslyOnShop = true;
                }
                else
                {
                    previouslyOnShop = false;
                }
                return returnValue;
            }
        }
    }
}
