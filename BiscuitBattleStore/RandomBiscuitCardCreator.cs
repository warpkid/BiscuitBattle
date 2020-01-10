using System;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Store
{
    public class RandomBiscuitCardCreator : BaseBiscuitCreator
    {
        public override IBiscuitCard GetCard()
        {
            var ran = new Random();
            // extended random range to include two new biscuits (custard cream and random)
            var choose = ran.Next(1, 8);
            switch (choose)
            {
                case 1:
                    return new NiceBiscuit();
                case 2:
                    return new DigestiveBiscuit();
                case 3:
                    return new PinkWaferBiscuit();
                case 4:
                    return new BalancedBiscuit();
                case 5:
                    return new GingernutBiscuit();
                    // new biscuit case added
                case 6:
                    return new CustardCreamBiscuit();
                    /* random biscuit case added - 
                    Only one random biscuit added per game */
                case 7:
                    return new RandomBiscuitBuilder();
                default:
                    return new BalancedBiscuit();
            }
        }
    }
}
