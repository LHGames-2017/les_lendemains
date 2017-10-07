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


        private static int upgradeCounter = 0;
        private static Object important_lock = new object();
        private static string HouseOverwrite(GameInfo gameInfo, Tile[,] carte)
        {
            lock (important_lock)
            {
                string returnValue = null;

                // If we are on the house, do upgrades
                if (gameInfo.Player.Position.X == gameInfo.Player.HouseLocation.X && gameInfo.Player.Position.Y == gameInfo.Player.HouseLocation.Y)
                {
                    switch (upgradeCounter)
                    {
                        case 0:
                            Random rnd = new Random();
                            int rndNumber = rnd.Next(1, 5);
                            Console.WriteLine("RANDOM");
                            Console.WriteLine(rndNumber);
                            if(rndNumber != 4)
                            {
                                // 80% chance to skip
                                returnValue = null;
                                upgradeCounter = 1024; //mucho mucho
                                break;
                            }

                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.CarryingCapacity);
                            break;
                        case 1:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.CollectingSpeed);
                            break;
                        /*case 2:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.Defence);
                            break;
                        case 3:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.MaximumHealth);
                            break;*/
                        default:
                            returnValue = null;
                            break;
                    }
                    upgradeCounter++;
                }
                else
                {
                    //reset the upgrade counter
                    upgradeCounter = 0;
                }
                return returnValue;
            }
        }

        private static int buyCounter = 0;
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
                    switch (buyCounter)
                    {
                        case 0:
                            returnValue = AIHelper.CreatePurchaseAction(PurchasableItem.DevolutionsPickaxe);
                            break;
                        case 1:
                            returnValue = AIHelper.CreatePurchaseAction(PurchasableItem.DevolutionsBackpack);
                            break;
                        /*case 2:
                            returnValue = AIHelper.CreatePurchaseAction(PurchasableItem.UbisoftShield);
                            break;
                        case 3:
                            returnValue = AIHelper.CreatePurchaseAction(PurchasableItem.HealthPotion);
                            break;*/
                        default:
                            returnValue = null;
                            break;
                    }
                    buyCounter++;
                }
                else
                {
                    //reset the upgrade counter
                    buyCounter = 0;
                }
                return returnValue;
            }
        }
    }
}
