using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Actions
{
    public class Attack : HighAction
    {
        PlayerInfo enemie;
        bool done = false;
        public Attack(PlayerInfo enemie)
        {
            this.enemie = enemie;
        }
        public string NextAction(Map map, GameInfo gameInfo)
        {
            if (!done)
            {
                done = true;
                return AIHelper.CreateAttackAction(enemie.Position);
            }
            else
            {
                return null;
            }
        }
    }
}
