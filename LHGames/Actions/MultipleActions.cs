
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
            Move move = Move.MoveAdjencent(gameInfo, map, target);
            Collect collect = new Collect(gameInfo, target);
            return new MultipleActions(new HighAction[] { move, collect });
        }

        public override string ToString()
        {
            string res = "=== Multiple Actions\n";
            foreach (var a in actions)
            {
                res += "   " + a.ToString() + "\n";
            }
            res += "===";
            return res;
        }
    }
}