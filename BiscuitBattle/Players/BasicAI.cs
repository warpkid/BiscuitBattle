using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Players
{
    public class BasicAI : IPlayer
    {
        public (BiscuitAttribute Attr, int Score) DecideAttributeToPlay(IBiscuitCard m)
        {
            //we just play the highest attribute we have - simple
            //pretend to think
            Console.WriteLine("Thinking....");
            Thread.Sleep(1500);
            
            
            // CM - added filler text
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Maaaannnnn these biscuits taste good!");
            Console.ResetColor();


            Thread.Sleep(1500);
            var attr =  m.Stats.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            return (attr, m.Stats[attr]);
        }
    }
}
