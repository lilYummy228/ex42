﻿using System;
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
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Карты в руке:");
                player.ShowHand();
                Console.SetCursorPosition(0, 0);
                Console.Write($"{CommandTakeCard} - взять карту\n{CommandExit} - выйти из программы\nВведите команду: ");

                switch (Console.ReadLine())
                {
                    case CommandTakeCard:
                        player.ReceiveCard(deck);
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
            CreateDeck();
            ShuffleDeck();
        }

        public void CreateDeck()
        {
            List<string> cardValues = new List<string> { "T", "6", "7", "8", "9", "10", "J", "D", "K" };
            List<string> cardSuits = new List<string> { "♣", "♠", "♥", "♦" };

            for (int i = 0; i < cardValues.Count; i++)
            {
                for (int j = 0; j < cardSuits.Count; j++)
                {
                    Card card = new Card(cardValues[i], cardSuits[j]);
                    _cards.Add(card);
                }
            }
        }

        public void ShuffleDeck()
        {
            Random random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = random.Next(0, _cards.Count);
                (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
            }
        }

        public Card GiveCard()
        {
            Card lastCard = _cards[_cards.Count - 1];
            _cards.Remove(lastCard);
            return lastCard;
        }
    }

    class Card
    {
        private string _value;
        private string _suit;

        public Card(string value, string suit)
        {
            _value = value;
            _suit = suit;
        }

        public void ShowInfo()
        {
            Console.Write($"|{_value} {_suit}|");
        }
    }

    class Player
    {
        private List<Card> _playerHand = new List<Card>();

        public void ShowHand()
        {
            foreach (Card card in _playerHand)
            {
                card.ShowInfo();
            }
        }

        public void ReceiveCard(Deck deck)
        {
            TakeCard(deck.GiveCard());
        }

        public void TakeCard(Card card)
        {
            if (card != null)
            {
                _playerHand.Add(card);
            }
        }
    }
}