﻿namespace StarterProject.Web.Api.Controllers
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
        static Strategy strategy = new Strategy();
        static HighAction currentAction = null;
        public static Map worldMap = new Map();
        static object mutex = new object();
        static string logContent = "";
        
        [HttpPost]
        public string Index([FromForm]string map)
        {
            lock (mutex)
            {
                GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(map);
                var carte = AIHelper.DeserializeMap(gameInfo.CustomSerializedMap);

                string output = "";

                if (Debug.debug)
                {
                    log("Resources " + gameInfo.Player.CarriedResources.ToString());
                    log("Points " + gameInfo.Player.Score);
                    log("Pos " + gameInfo.Player.Position);
                }

                // HOUSE + SHOP OVERRIDES
                string overwrite = Override.AllOverwrites(gameInfo, carte);
                if (overwrite != null)
                {
                    Console.WriteLine("OVERRIDE: " + overwrite);
                    return overwrite;
                }
                // END OVERRIDE


                //update map of the world
                worldMap.UpdateMap(carte);
                worldMap.UpdateOtherPLayerMap(gameInfo.OtherPlayers);

                string action = null;
                while (action == null)
                {
                    if (currentAction == null)
                    {
                        currentAction = strategy.NextAction(worldMap, gameInfo);
                        if (currentAction == null)
                        {
                            break;
                        }
                        log(currentAction.ToString());
                    }
                    try
                    {
                        action = currentAction.NextAction(worldMap, gameInfo);
                    }
                    catch(Cancel)
                    {
                        // nothing
                    }
                    if (action == null)
                    {
                        currentAction = null;
                    }
                }
                commitLog();
                return action;
            }
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

        public void log(params string[] strs)
        {
            string str = "";
            foreach (var s in strs)
            {
                str += s + "\n";
            }
            logContent += str;
        }
        
        public void commitLog()
        {
            if (Debug.debug)
            {
                Console.Write(logContent);
            }
            else
            {
                Pastebin.AppendString(Pastebin.DEBUG_LOG_CHROUS_URL, logContent);
            }
            logContent = "";
        }
    }
}
