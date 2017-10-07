
using StarterProject.Web.Api;

namespace LHGames.Actions
{
    class Move : HighAction
    {
        private Point target;

        public Move(GameInfo gameInfo, Point target)
        {
            this.target = target;
        }

        public string NextAction(GameInfo gameInfo)
        {
            Point pos = gameInfo.Player.Position;

            if (target.X < pos.X)
            {
                pos += new Point(-1, 0);
            }
            else if(target.X > pos.X)
            {
                pos += new Point(1, 0);
            }
            else if(target.Y < pos.Y)
            {
                pos += new Point(0, -1);
            }
            else if(target.Y > pos.Y)
            {
                pos += new Point(0, 1);
            }
            else
            {
                return null;
            }
            return AIHelper.CreateMoveAction(pos);
        }
    }
}