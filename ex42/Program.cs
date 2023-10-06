using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace ex42
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandTakeCard = "1";
            const string CommandDropCards = "2";
            const string CommandReCreateDeck = "3";
            const string CommandExit = "4";

            Player player = new Player();
            Deck deck = new Deck();
            bool isOpen = true;

            deck.Create();

            while (isOpen)
            {
                Console.SetCursorPosition(0, 10);
                deck.ShowCountInfo();
                Console.WriteLine("Карты в руке:");
                player.ShowCardsInHand();
                Console.SetCursorPosition(0, 0);
                Console.Write($"{CommandTakeCard} - Взять карты\n{CommandDropCards} - Сбросить карты в отбой\n" +
                    $"{CommandReCreateDeck} - Пересоздать колоду\n{CommandExit} - Выход\nВыберете операцию: ");

                switch (Console.ReadLine())
                {
                    case CommandTakeCard:
                        deck.TakeCard(player);
                        break;

                    case CommandDropCards:
                        player.DropCards();
                        break;

                    case CommandReCreateDeck:
                        player.Recreate(deck);
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

        public void TakeCard(Player player)
        {
            int count = GiveEnteredAmount();

            for (int i = 0; i < count; i++)
            {
                player.PutCardInHand(GiveCard());
            }
        }

        public Card GiveCard()
        {
            if (_cards.Count > 0)
            {
                Card lastCard = _cards[_cards.Count - 1];
                _cards.Remove(lastCard);
                return lastCard;
            }
            else
            {
                Console.WriteLine("Карты в колоде закончились...");
                return null;
            }
        }

        public int GiveEnteredAmount()
        {
            Console.Write("Введите сколько карт вы хотите взять? ");
            string cardCount = Console.ReadLine();

            if (int.TryParse(cardCount, out int count))
            {
                if (count <= _cards.Count)
                {
                    return count;
                }
                else
                {
                    Console.WriteLine("В колоде недостаточно карт...");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод...");
                return 0;
            }
        }

        public void ShowCountInfo()
        {
            Console.WriteLine($"Карт в колоде: {_cards.Count} карт");
        }

        public void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = random.Next(_cards.Count);
                (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
            }
        }

        public void Create()
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

        public void Clear()
        {
            _cards.Clear();
        }
    }

    class Player
    {
        private List<Card> _cardsInHand = new List<Card>();

        public void ShowCardsInHand()
        {
            foreach (Card card in _cardsInHand)
            {
                card.ShowInfo();
            }
        }

        public void Recreate(Deck deck)
        {
            DropCards();
            deck.Clear();
            deck.Create();
            deck.Shuffle();
        }

        public void DropCards()
        {
            _cardsInHand.Clear();
        }

        public void PutCardInHand(Card card)
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