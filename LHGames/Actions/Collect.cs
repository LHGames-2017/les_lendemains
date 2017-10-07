
using StarterProject.Web.Api;

namespace LHGames.Actions
{
    class Collect : HighAction
    {
        private Point target;
        private bool done = false;

        public Collect(GameInfo gameInfo, Point target)
        {
            this.target = target;
        }

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

        public override string ToString()
        {
            return "Collect at " + target.ToString();
        }
    }
}