// function determine if there is a straight in a set of cards
// straight - 5 consecutive cards
// standard set of card 2-10, jack, queen, king, ace (clubs, spades, diamonds and hearts)
// ace either high or low

// ignore the suit

namespace cardchecker
{
    enum CardValue
    {
        Ace,
        Card2,
        Card3,
        Card4,
        Card5,
        Card6,
        Card7,
        Card8,
        Card9,
        Card10,
        Jack,
        Queen,
        King,
        Max
    }

    enum Suit
    {
        Diamonds,
        Spades,
        Hearts,
        Clubs
    }

    class Card
    {
        public CardValue cardValue;
        public Suit suit;
    }

    class CardChecker
    {
        static void Main(string[] args)
        {
            //tests    
        }

        const straight = 5;

        bool isStraight(List<Cards> cards)
        {
            //list of cards is unordered

            //e.g. jack, queen, king, ace, 2 -> no straight

            // 10, jack, queen, king, ace -> straight
            // ace, 2, 3, 4, 5 -> straight

            // solutions:
            // 1. order it by value, check if we have 5 straight values in hand - declare straight

            // 2. take a card, find whether there is another card higher in value -> cycle five times

            for (int i = 0; i < cards.Count; i++) // N elemenents 
            {
                Card cardTaken = cards[i];
                int straight_counter = 1;

                int next_card = cardTaken.cardValue + 1;

                straight_counter = FindNextCard(next_card, cards, ref straight_counter);

                if (straight_counter > straight)
                {
                    return true;
                }

            }
            // O(N*N) - time complexity
            // could be improved with memoization
            // with memoization -> go over all of the cards -> N, finding next card -> reduced for second and follow up queries  
            return false;
        }

        int FindNextCard(CardValue cardValue, List<Cards> cards, ref int straight_counter)
        {
            if (next_card != CardValue.Max)
            {
                bool found = false;
                for (int j = 0; j < cards.Count; j++)// second loop N - elements
                {
                    if (cards[j].Value) == next_card)
                    {
                    straight_counter++;
                    FindNextCard(next_card, cards, straight_counter);
                }
            }

            if (found == false)
            {
                return straight_counter;
            }
        }
            else
            {
                for (int j = 0; j<cards.Count; j++) // special case
                {
                    if (cards[j].Value) == CardValue.Ace)
                    {
                        straight_counter++;
                    }
}
            }
            
            return straight_counter;
        }
    }
}