
using LHGames.Actions;
using StarterProject.Web.Api;

namespace LHGames
{
    class Strategy
    {
        bool done = false;
        public HighAction NextAction(GameInfo gameInfo)
        {
            if(!done)
            {
                done = true;
                return new Move(gameInfo.Player.Position + new Point(1, 0));
            }
            else
            {
                return null;
            }
        }
    }
}