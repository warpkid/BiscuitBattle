using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Players
{
    public class CrazyAI : IPlayer
    {
        public (BiscuitAttribute Attr, int Score) DecideAttributeToPlay(IBiscuitCard m)
        {
            // CM - pretend to think
            Console.WriteLine("Thinking....");
            Thread.Sleep(1500);
            
            
            // CM - added filler text
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Maaaannnnn these biscuits taste good!");
            Console.ResetColor();

            // CM - Chooseing random attribute to return (not a very good player!)
            // CM - counting dictionary length (more future proof than manually setting random args) 
            int size = m.Stats.Count;

            // CM - get random index for dictionary lookup
            Random random = new Random();
            int ran = random.Next(0, size);

            // CM - find key that relates to random index
            var keyOfRandomElement = m.Stats.ElementAt(ran).Key;

            Thread.Sleep(1500);

            // CM - return random attribute
            var attr =  keyOfRandomElement;
            return (attr, m.Stats[attr]);
        }
    }
}
