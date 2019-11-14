using Laboratorio_8_OOP_201920.Cards;
using Laboratorio_8_OOP_201920.Enums;
using System.Collections.Generic;

namespace Laboratorio_8_OOP_201920
{
    public static class Effect
    {
        private static Dictionary<EnumEffect, string> effectDescriptions = new Dictionary<EnumEffect, string>()
        {
            { EnumEffect.bitingFrost, "Sets the strength of all melee cards to 1 for both players" },
            { EnumEffect.impenetrableFog, "Sets the strength of all range cards to 1 for both players" },
            { EnumEffect.torrentialRain, "Sets the strength of all longRange cards to 1 for both players" },
            { EnumEffect.clearWeather, "Removes all Weather Card (Biting Frost, Impenetrable Fog and Torrential Rain) effects" },
            { EnumEffect.moraleBoost, "Adds +1 to all units in the row (excluding itself)" },
            { EnumEffect.spy, "Place on your opponent's battlefield (counts towards opponent's total) and draw 2 cards from your deck" },
            { EnumEffect.tightBond, "Place next to a card with the same name to double the strength of both cards" },
            { EnumEffect.buff, "Doubles the strength of all unit cards in that row. Limited to 1 per row" },
            { EnumEffect.none, "None" },
        };

        public static string GetEffectDescription(EnumEffect e)
        {
            return effectDescriptions[e];
        }

        

        public static void ApplyEffect(Card playedCard, Player activePlayer, Player opponent, Board board)
        {
            if (playedCard.Type == EnumType.weather)
            {
                switch (playedCard.CardEffect)
                {
                    case (EnumEffect.bitingFrost):
                        DecreaseAP(EnumType.melee, board);
                        break;
                    case (EnumEffect.clearWeather):
                        RestoreAP(board);
                        break;
                    case (EnumEffect.impenetrableFog):
                        DecreaseAP(EnumType.range, board);
                        break;
                    case (EnumEffect.torrentialRain):
                        DecreaseAP(EnumType.longRange, board);
                        break;
                }
            }
        }
        private static void DecreaseAP(EnumType type, Board board)
        { 
            foreach (CombatCard card in board.PlayerCards[0][type])
            {
               
                card.AttackPoints = 1;
            }
            foreach (CombatCard card in board.PlayerCards[1][type])
            {
               
                card.AttackPoints = 1;
            }
        }
        private static void RestoreAP(Board board)
        {
            board.WeatherCards.Clear();
            List<EnumType> EnumList = new List<EnumType>() { EnumType.melee, EnumType.range, EnumType.longRange };
            foreach (EnumType et in EnumList)
            {
                foreach (CombatCard card in board.PlayerCards[0][et])
                {
                    card.AttackPoints = card.OriginalAttackPoints;
                }
                foreach (CombatCard card in board.PlayerCards[1][et])
                {
                    card.AttackPoints = card.OriginalAttackPoints;
                }
            }
           
        }
    }
}
