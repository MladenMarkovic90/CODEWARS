using System;
using System.Collections.Generic;
using System.Linq;

//https://www.codewars.com/kata/ranking-poker-hands
namespace CODEWARS.Kata4_PokerHand1
{
    public enum HandType
    {
        StraightFlush = 8,
        FourOfAKind = 7,
        FullHouse = 6,
        Flush = 5,
        Straight = 4,
        ThreeOfAKind = 3,
        TwoPair = 2,
        Pair = 1,
        None = 0
    }

    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public class PokerHand
    {
        private List<PokerHandCard> cards = new List<PokerHandCard>();

        public PokerHand(string cardsAsText)
        {
            foreach (string card in cardsAsText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                cards.Add(new PokerHandCard(card[0], card[1]));
            }

            cards = cards.OrderBy(x => x.Value).ToList();
        }

        private int StraightFlushValue()
        {
            if (!this.IsFlush())
            {
                return 0;
            }

            return this.StraightValue();
        }

        private bool IsStraightFlush()
        {
            return this.StraightFlushValue() > 0;
        }

        private int StraightValue()
        {
            int first = cards[0].Value;

            foreach (PokerHandCard card in cards)
            {
                if (first != card.Value)
                {
                    return 0;
                }

                first++;
            }

            return first;
        }

        private bool IsStraight()
        {
            return !this.IsFlush() && this.StraightValue() > 0;
        }

        private int NumOfAKind(int num)
        {
            foreach (PokerHandCard card in cards)
            {
                int value = card.Value;

                if (cards.Count(x => x.Value == value) == num)
                {
                    return value;
                }
            }

            return 0;
        }

        private bool IsFullHouse()
        {
            return this.FullHouseValue() != null;
        }

        private Tuple<int, int> FullHouseValue()
        {
            int first = ThreeOfAKindValue();

            if (first == 0)
            {
                return null;
            }

            int second = 0;

            foreach (PokerHandCard card in cards.Where(x => x.Value != first))
            {
                second = card.Value;

                if (cards.Count(x => x.Value == second) == 2)
                {
                    return new Tuple<int, int>(first, second);
                }
            }

            return null;
        }

        private int FourOfAKindValue()
        {
            return this.NumOfAKind(4);
        }

        private bool IsFourOfAKind()
        {
            return this.FourOfAKindValue() > 0;
        }

        private int ThreeOfAKindValue()
        {
            return this.NumOfAKind(3);
        }

        private bool IsThreeOfAKind()
        {
            return this.ThreeOfAKindValue() > 0;
        }

        private int PairValue()
        {
            return this.NumOfAKind(2);
        }

        private bool IsPair()
        {
            return this.PairValue() > 0;
        }

        private Tuple<int, int> TwoPairValue()
        {
            int first = this.PairValue();

            if (first == 0)
            {
                return null;
            }

            int second = 0;

            foreach (PokerHandCard card in cards.Where(x => x.Value != first))
            {
                second = card.Value;

                if (cards.Count(x => x.Value == second) == 2)
                {
                    return new Tuple<int, int>(first, second);
                }
            }

            return null;
        }

        private bool IsTwoPair()
        {
            return this.TwoPairValue() != null;
        }

        private bool IsFlush()
        {
            char type = cards[0].CharType;
            return cards.Count(x => x.CharType == type) == 5;
        }

        private Result GetResult(int first, int second, PokerHand firstHand, PokerHand secondHand)
        {
            if (first > second)
            {
                return Result.Win;
            }
            else if (first < second)
            {
                return Result.Loss;
            }

            return this.HigherCard(firstHand, secondHand);
        }

        public Result CompareWith(PokerHand hand)
        {
            HandType first = this.GetHandTypeValue();
            HandType second = hand.GetHandTypeValue();

            if (first > second)
            {
                return Result.Win;
            }
            else if (first < second)
            {
                return Result.Loss;
            }
            else
            {
                if (first == HandType.StraightFlush)
                {
                    return this.GetResult(this.StraightFlushValue(), hand.StraightFlushValue(), this, hand);
                }
                else if (first == HandType.FourOfAKind)
                {
                    return this.GetResult(this.FourOfAKindValue(), hand.FourOfAKindValue(), this, hand);
                }
                else if (first == HandType.Straight)
                {
                    return this.GetResult(this.StraightValue(), hand.StraightValue(), this, hand);
                }
                else if (first == HandType.ThreeOfAKind)
                {
                    return this.GetResult(this.ThreeOfAKindValue(), hand.ThreeOfAKindValue(), this, hand);
                }
                else if (first == HandType.Pair)
                {
                    return this.GetResult(this.PairValue(), hand.PairValue(), this, hand);
                }
            }

            return this.HigherCard(this, hand);
        }

        private HandType GetHandTypeValue()
        {
            if (this.IsStraightFlush())
            {
                return HandType.StraightFlush;
            }
            else if (this.IsFourOfAKind())
            {
                return HandType.FourOfAKind;
            }
            else if (this.IsFullHouse())
            {
                return HandType.FullHouse;
            }
            else if (this.IsFlush())
            {
                return HandType.Flush;
            }
            else if (this.IsStraight())
            {
                return HandType.Straight;
            }
            else if (this.IsThreeOfAKind())
            {
                return HandType.ThreeOfAKind;
            }
            else if (this.IsTwoPair())
            {
                return HandType.TwoPair;
            }
            else if (this.IsPair())
            {
                return HandType.Pair;
            }

            return HandType.None;
        }

        private Result HigherCard(PokerHand first, PokerHand second)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (first.cards[i].Value > second.cards[i].Value)
                {
                    return Result.Win;
                }
                else if (first.cards[i].Value < second.cards[i].Value)
                {
                    return Result.Loss;
                }
            }

            return Result.Tie;
        }

        private class PokerHandCard
        {
            private static Dictionary<char, int> CardValue = new Dictionary<char, int>();

            static PokerHandCard()
            {
                CardValue.Add('2', 2);
                CardValue.Add('3', 3);
                CardValue.Add('4', 4);
                CardValue.Add('5', 5);
                CardValue.Add('6', 6);
                CardValue.Add('7', 7);
                CardValue.Add('8', 8);
                CardValue.Add('9', 9);
                CardValue.Add('T', 10);
                CardValue.Add('J', 11);
                CardValue.Add('Q', 12);
                CardValue.Add('K', 13);
                CardValue.Add('A', 14);
            }

            public int Value { get; }
            public char CharValue { get; }
            public char CharType { get; }

            public PokerHandCard(char value, char type)
            {
                this.Value = CardValue[value];
                this.CharValue = value;
                this.CharType = type;
            }
        }
    }
}