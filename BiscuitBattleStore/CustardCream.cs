using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Store
{
    // CM - created new biscuit class
    public class CustardCreamBiscuit : IBiscuitCard
    {
        public string Name { get; set; }
        public Dictionary<BiscuitAttribute, int> Stats { get; set; }
        public int Quality => Stats.Sum(att => att.Value);
        public string Description { get; set; }
    
        public CustardCreamBiscuit() {
            Stats = new Dictionary<BiscuitAttribute, int>
            {
                { BiscuitAttribute.DunkIntegrity, 50 },
                { BiscuitAttribute.Moistness, 70 },
                { BiscuitAttribute.Snap, 80 },
                { BiscuitAttribute.Sweetness, 80 },
                { BiscuitAttribute.Texture, 50 }
            };

            Name = "Custard Cream";
            Description = "A vanilla biscuit and creme sandwich, what more could you want?";
            
        }

    }
}