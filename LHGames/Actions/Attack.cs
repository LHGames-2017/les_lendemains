using StarterProject.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHGames.Actions
{
    public class Attack : HighAction
    {

        public string NextAction(Map map, GameInfo gameInfo)
        {
            if (!done)
            {
                done = true;
                return AIHelper.CreateCollectAction(target);
            }
            else
            {
                return null;
            }
        }
    }
}
