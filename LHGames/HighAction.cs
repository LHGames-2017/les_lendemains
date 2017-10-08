
using StarterProject.Web.Api;
using System;

namespace LHGames
{
    interface HighAction
    {
        // returns null when done
        string NextAction(Map map, GameInfo gameInfo);
    }

    class Cancel : Exception
    {

    }
}