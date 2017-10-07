namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("/")]
    public class GameController : Controller
    {
        AIHelper player = new AIHelper();

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);

            // HOUSE + SHOP OVERRIDES
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
            // END OVERRIDE

            

            // INSERT AI CODE HERE.

            string action = AIHelper.CreateMoveAction(gameInfo.Player.Position);
            return action;
        }
        
        private static int upgradeCounter = 0;
        private static Object important_lock = new object();
        private string HouseOverwrite(GameInfo gameInfo, Tile[,] carte)
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
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.CarryingCapacity);
                            break;
                        case 1:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.CollectingSpeed);
                            break;
                        case 2:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.Defence);
                            break;
                        case 3:
                            returnValue = AIHelper.CreateUpgradeAction(UpgradeType.MaximumHealth);
                            break;
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
        private string ShopOverwrite(GameInfo gameInfo, Tile[,] carte)
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
