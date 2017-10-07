using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterProject.Web.Api
{
    public struct MyPlayer
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public double CollectingSpeed { get; set; }
        public int MaxHealth { get; set; }
        public int CarryingCapacity { get; set; }
        public bool HasSword { get; set; }
        public bool HasShield { get; set; }
        public bool HasBackpack { get; set; }
        public bool HasPickaxe { get; set; }
        public UpgradeType NextUpgrade { get; set; }
        public PurchasableItem NextItem { get; set; }
    }

}
