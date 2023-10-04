using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex42
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.CreateDeck();
        }
    }

    class Deck
    {
        private int _maxCardValue = 9;
        private int _maxCardSuits = 4;

        public void CreateDeck()
        {
            List<string> deck = new List<string>();

            for (int i = 0; i < _maxCardSuits; i++)
            {
                for (int j = 0; j < _maxCardValue; j++)
                {
                    deck.Add($"{(CardSuit)i}, {(CardValue)j}");
                }
            }

            foreach (var card in deck)
            {
                Console.WriteLine(card);
            }
        }
    }

    class Card
    {
        public List<CardSuit> cardSuits = new List<CardSuit>();
        public List<CardValue> cardValues = new List<CardValue>();

        public Card(CardValue cardValue, CardSuit cardSuit)
        {
            CardValue = cardValue;
            CardSuit = cardSuit;
        }

        public CardValue CardValue { get; private set; }
        public CardSuit CardSuit { get; private set; }
    }

    enum CardSuit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    enum CardValue
    {
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Quenn,
        King,
        Ace
    }

    class Player
    {

    }
}
