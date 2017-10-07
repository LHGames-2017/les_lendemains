
using StarterProject.Web.Api;

namespace LHGames
{
    interface HighAction
    {
        // returns null when done
        string NextAction(GameInfo gameInfo);
    }
}