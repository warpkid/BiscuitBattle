using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Store
{
    // CM - Created random biscuit generator
    public class RandomBiscuitBuilder : IBiscuitCard
{
    public string Name { get; set; }
    public Dictionary<BiscuitAttribute, int> Stats { get; set; }
    public int Quality => Stats.Sum(att => att.Value);
    public string Description { get; set; }
    public RandomBiscuitBuilder() {
        Stats = new Dictionary<BiscuitAttribute, int>
            {
                { BiscuitAttribute.DunkIntegrity, GenerateRandomAttribute() },
                { BiscuitAttribute.Moistness, GenerateRandomAttribute() },
                { BiscuitAttribute.Snap, GenerateRandomAttribute() },
                { BiscuitAttribute.Sweetness, GenerateRandomAttribute() },
                { BiscuitAttribute.Texture, GenerateRandomAttribute() }
            };

            Name = GenerateBiscuitName();
            Description = "The clue is in the name.";
    }

    public int GenerateRandomAttribute() {

        Random random = new Random();  
        return random.Next(0, 100);
    }

    public string GenerateBiscuitName() {
        string name= "Random";

        if (Stats[BiscuitAttribute.DunkIntegrity] > 50) {
            name += " Waterproof";
        } else {
            name += " Soggy";
        }
        
        if (Stats[BiscuitAttribute.Moistness] > 50) {
            name += " Moist";
        } else {
            name += " Dry";
        }

        if (Stats[BiscuitAttribute.Snap] > 50) {
            name += " Brittle";
        } else {
            name += " Pliable";
        }

        if (Stats[BiscuitAttribute.Sweetness] > 50) {
            name += " Sweet";
        } else {
            name += " Sour";
        }

        if (Stats[BiscuitAttribute.Texture] > 50) {
            name += " Rough";
        } else {
            name += " Smooth";
        }

        name += " Biscuit";
        return name;
    }
}
}