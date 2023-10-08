using System;
using System.Collections.Generic;

namespace ex42
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandTakeCard = "1";
            const string CommandExit = "2";

            Player player = new Player();
            Deck deck = new Deck();

            bool isOpen = true;

            while (isOpen)
            {
                Console.SetCursorPosition(0, 7);
                deck.ShowCardsCountInfo();
                Console.WriteLine("Карты в руке:");
                player.ShowCardsInHand();
                Console.SetCursorPosition(0, 0);
                Console.Write($"{CommandTakeCard} - Взять карты\n{CommandExit} - Выход\n\nВыберете операцию: ");

                switch (Console.ReadLine())
                {
                    case CommandTakeCard:
                        int pickCount = deck.GiveRightCardAmount();

                        for (int i = 0; i < pickCount; i++)                        
                            player.PutCardsInHand(deck.GiveCardsFromTop());                        

                        break;

                    case CommandExit:
                        isOpen = false;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            Create();
            Shuffle();
        }

        public Card GiveCardsFromTop()
        {
            if (_cards.Count > 0)
            {
                Card lastCard = _cards[_cards.Count - 1];
                _cards.Remove(lastCard);
                return lastCard;
            }
            else
            {
                return null;
            }
        }

        public void ShowCardsCountInfo()
        {
            Console.WriteLine($"Карт в колоде: {_cards.Count} карт");
        }

        public int GiveRightCardAmount()
        {
            Console.Write("Введите сколько карт вы хотите взять? ");

            if (int.TryParse(Console.ReadLine(), out int cardCount) == false)
                Console.WriteLine("Неверный ввод...");

            return cardCount;
        }

        private void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = random.Next(_cards.Count);
                (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
            }
        }

        private void Create()
        {
            List<string> cardSuits = new List<string> { "♠", "♥", "♣", "♦" };
            List<string> cardValues = new List<string> { "6", "7", "8", "9", "10", "J", "Q", "K", "T" };

            for (int i = 0; i < cardSuits.Count; i++)
            {
                for (int j = 0; j < cardValues.Count; j++)
                {
                    Card card = new Card(cardSuits[i], cardValues[j]);
                    _cards.Add(card);
                }
            }
        }
    }

    class Player
    {
        private List<Card> _cardsInHand = new List<Card>();

        public void ShowCardsInHand()
        {
            foreach (Card card in _cardsInHand)
                card.ShowInfo();
        }

        public void PutCardsInHand(Card card)
        {
            if (card != null)
            {
                _cardsInHand.Add(card);
            }
        }
    }

    class Card
    {
        private string _suit;
        private string _value;

        public Card(string suit, string value)
        {
            _suit = suit;
            _value = value;
        }

        public void ShowInfo()
        {
            Console.Write($"|{_value}{_suit}|");
        }
    }
}