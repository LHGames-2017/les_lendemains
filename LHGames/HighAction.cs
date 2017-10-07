
using StarterProject.Web.Api;

namespace LHGames
{
    interface HighAction
    {
        // returns null when done
        string NextAction(Map map, GameInfo gameInfo);
    }
}