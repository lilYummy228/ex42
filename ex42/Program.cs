using System;
using System.Collections.Generic;
using System.ComponentModel;

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
            Deck deck = new Deck(player);

            bool isOpen = true;

            while (isOpen)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Карты в руке:");
                player.ShowCardsInHand();
                Console.SetCursorPosition(0, 0);
                Console.Write($"{CommandTakeCard} - Взять карты\n{CommandDropCards} - Сбросить карты в отбой\n" +
                    $"{CommandReCreateDeck} - Пересоздать колоду\n{CommandExit} - Выход\nВыберете операцию: ");

                switch (Console.ReadLine())
                {
                    case CommandTakeCard:
                        player.TakeCard(deck);
                        break;

                    case CommandDropCards:
                        player.DropCards();
                        break;

                    case CommandReCreateDeck:
                        deck.GiveDeck(player);
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
        private List<Card> _deck = new List<Card>();

        public Deck(Player player)
        {
            GiveDeck(player);
        }

        public void CreateDeck()
        {
            List<string> cardSuits = new List<string> { "♠", "♥", "♣", "♦" };
            List<string> cardValues = new List<string> { "6", "7", "8", "9", "10", "J", "Q", "K", "T" };

            for (int i = 0; i < cardSuits.Count; i++)
            {
                for (int j = 0; j < cardValues.Count; j++)
                {
                    Card card = new Card(cardSuits[i], cardValues[j]);
                    _deck.Add(card);
                }
            }
        }

        public void ShuffleDeck()
        {
            Random random = new Random();

            for (int i = 0; i < _deck.Count; i++)
            {
                int randomIndex = random.Next(i, _deck.Count);
                (_deck[i], _deck[randomIndex]) = (_deck[randomIndex], _deck[i]);
            }
        }

        public Card GiveCard()
        {
            if (_deck.Count > 0)
            {
                Card lastCard = _deck[_deck.Count - 1];
                _deck.Remove(lastCard);
                return lastCard;
            }
            else
            {
                Console.WriteLine("Карты в колоде закончились...");
                return null;
            }
        }

        public int GiveNeedCardCount()
        {
            Console.Write("Введите сколько карт вы хотите взять? ");
            string cardCount = Console.ReadLine();

            if (int.TryParse(cardCount, out int count))
            {
                if (count <= _deck.Count)
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

        public void GiveDeck(Player player)
        {
            _deck.Clear();
            player.DropCards();
            CreateDeck();
            ShuffleDeck();
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

        public void TakeCard(Deck deck)
        {
            int count = deck.GiveNeedCardCount();

            for (int i = 0; i < count; i++)
            {
                PutCardInHand(deck.GiveCard());
            }
        }

        private void PutCardInHand(Card card)
        {
            if (card != null)
            {
                _cardsInHand.Add(card);
            }
        }

        public void DropCards()
        {
            _cardsInHand.Clear();
        }


    }

    class Card
    {
        private string _cardSuit;
        private string _cardValue;

        public Card(string cardSuit, string cardValue)
        {
            _cardSuit = cardSuit;
            _cardValue = cardValue;
        }

        public void ShowInfo()
        {
            Console.Write($"|{_cardValue}{_cardSuit}|");
        }
    }
}