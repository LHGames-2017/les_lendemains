namespace StarterProject.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using LHGames;

    public enum MapTileContent
    {
        E,
        W,
        H,
        L,
        R,
        S,
        P
    }
    [Route("/")]
    public class GameController : Controller
    {
        AIHelper player = new AIHelper();

        [HttpPost]
        public string Index([FromForm]string map)
        {
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
            var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            // INSERT AI CODE HERE.

            string action = AIHelper.CreateMoveAction(gameInfo.Player.Position);
            if (Debug.debug)
            {
                PrintMap(carte);
            }
            return action;
        }

        public void PrintMap(Tile[,] carte)
        {
            for(int i = 0; i < carte.GetLength(0); ++i)
            {
                string line = "";
                for (int j = 0; j < carte.GetLength(1); ++j)
                {

                    line += (MapTileContent)carte[i, j].C;
                }
                Console.WriteLine(line);
            }
        }
    }
}
