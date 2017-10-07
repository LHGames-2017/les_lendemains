
using StarterProject.Web.Api;
using System;

namespace LHGames.Actions
{
    class MultipleActions : HighAction
    {
        private HighAction[] actions;
        int idx = 0;

        MultipleActions(HighAction[] actions)
        {
            if (actions.Length == 0)
                throw new ArgumentException("Length 0 argument");
            this.actions = actions;
        }

        public string NextAction(Map map, GameInfo gameInfo)
        {
            string next = null;
            while(next == null)
            {
                next = actions[idx]?.NextAction(map, gameInfo);
                if(next == null)
                {
                    ++idx;
                }
                if (idx >= actions.Length)
                    break;
            }
            return next;
        }
        
        public static MultipleActions MoveThenCollect(GameInfo gameInfo, Map map, Point target)
        {
            Point diff = target - gameInfo.Player.Position;

            Point p = null;
            if(Math.Abs(diff.X) >= Math.Abs(diff.Y))
            {
                if(diff.X < 0)
                {
                    p = new Point(1, 0);
                }
                else
                {
                    p = new Point(-1, 0);
                }
            }
            else
            {
                if(diff.Y < 0)
                {
                    p = new Point(0, 1);
                }
                else
                {
                    p = new Point(0, -1);
                }
            }
            Move move = new Move(gameInfo, map, target + p);
            Collect collect = new Collect(gameInfo, target);
            return new MultipleActions(new HighAction[] { move, collect });
        }
    }
}