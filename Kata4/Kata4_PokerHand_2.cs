using System;
using System.Collections.Generic;
using System.Linq;

//https://www.codewars.com/kata/sortable-poker-hands
// This version is optimized. It can be more optimized, but it would loss readability and some functions.
namespace CODEWARS.Kata4_PokerHand_2
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
        Win = -1,
        Loss = 1,
        Tie = 0
    }

    public class PokerHand : IComparable
    {
        private int handValue = 0;
        private HandType handType = HandType.None;
        private long cardValue = 0;
        private List<PokerHandCard> cards = new List<PokerHandCard>();

        public PokerHand(string cardsAsText)
        {
            foreach (string card in cardsAsText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                cards.Add(new PokerHandCard(card[0], card[1]));
            }

            cards = cards.OrderBy(x => x.Value).ToList();
        }

        private int StraightValue()
        {
            int max = 5;

            // check for: A,2,X,X,X
            if (cards[0].Value == 12 && cards[4].Value == 24)
            {
                max = 4;
            }

            int value = cards[0].Value + 1;

            for (int i = 1; i < max; i++, value++)
            {
                if (cards[i].Value != value)
                {
                    return 0;
                }
            }

            return value;
        }
        
        private Result GetResult(PokerHand secondHand)
        {
            if (this.handValue > secondHand.handValue)
            {
                return Result.Win;
            }
            else if (this.handValue < secondHand.handValue)
            {
                return Result.Loss;
            }

            return this.HigherCard(secondHand);
        }

        public Result CompareWith(PokerHand hand)
        {
            if (this.handValue == 0)
            {
                this.CalculateHandValues();
            }

            if (hand.handValue == 0)
            {
                hand.CalculateHandValues();
            }

            HandType firstType = this.handType;
            HandType secondType = hand.handType;

            if (firstType > secondType)
            {
                return Result.Win;
            }
            else if (firstType < secondType)
            {
                return Result.Loss;
            }

            return this.GetResult(hand);
        }

        private void CalculateCardValue()
        {
            for (int i = 4; i >= 0; i--)
            {
                this.cardValue = this.cardValue * 100 + cards[i].Value;
            }
        }

        private void CalculateHandValues()
        {
            bool isFlush = cards.Count(x => x.CharType == cards[0].CharType) == 5;
            int straightValue = this.StraightValue();

            if (isFlush)
            {
                this.handValue = straightValue;

                if (this.handValue > 0)
                {
                    this.handType = HandType.StraightFlush;
                    return;
                }
            }

            int twoOfAKind = 0;
            int threeOfAKind = 0;
            int fourOfAKind = 0;
            bool isTwoPair = false;

            foreach (PokerHandCard card in cards)
            {
                int count = cards.Count(x => x.Value == card.Value);
                
                if (count == 2)
                {
                    if (twoOfAKind == 0)
                    {
                        twoOfAKind = card.Value;
                    }
                    else if (twoOfAKind != card.Value)
                    {
                        isTwoPair = true;
                    }
                }
                else if (count == 3)
                {
                    threeOfAKind = card.Value;
                }
                else if (count == 4)
                {
                    fourOfAKind = card.Value;
                    break;
                }
            }

            if (fourOfAKind > 0)
            {
                this.handValue = fourOfAKind;
                this.handType = HandType.FourOfAKind;
                return;
            }

            if (twoOfAKind > 0 && threeOfAKind > 0)
            {
                this.handType = HandType.FullHouse;
                this.handValue = 0;
                return;
            }

            if (isFlush)
            {
                this.handType = HandType.Flush;
                this.handValue = 0;
                return;
            }

            if (straightValue > 0)
            {
                this.handType = HandType.Straight;
                this.handValue = straightValue;
                return;
            }

            if (threeOfAKind > 0)
            {
                this.handValue = threeOfAKind;
                this.handType = HandType.ThreeOfAKind;
                return;
            }

            if (isTwoPair)
            {
                this.handType = HandType.TwoPair;
                this.handValue = 0;
                return;
            }
            
            if (twoOfAKind > 0)
            {
                this.handValue = twoOfAKind;
                this.handType = HandType.Pair;
                return;
            }

            this.handType = HandType.None;
            this.handValue = 0;
        }

        private Result HigherCard(PokerHand second)
        {
            if (this.cardValue == 0)
            {
                this.CalculateCardValue();
            }

            if (second.cardValue == 0)
            {
                second.CalculateCardValue();
            }

            if (this.cardValue > second.cardValue)
            {
                return Result.Win;
            }
            else if (this.cardValue < second.cardValue)
            {
                return Result.Loss;
            }

            return Result.Tie;
        }

        public int CompareTo(object other)
        {
            return (int)this.CompareWith(other as PokerHand);
        }

        public override string ToString()
        {
            return string.Join(" ", cards.Select(x => x.CharValue.ToString() + x.CharType.ToString()));
        }
        
        private class PokerHandCard
        {
            private static Dictionary<char, int> CardValue = new Dictionary<char, int>();

            static PokerHandCard()
            {
                CardValue.Add('2', 12);
                CardValue.Add('3', 13);
                CardValue.Add('4', 14);
                CardValue.Add('5', 15);
                CardValue.Add('6', 16);
                CardValue.Add('7', 17);
                CardValue.Add('8', 18);
                CardValue.Add('9', 19);
                CardValue.Add('T', 20);
                CardValue.Add('J', 21);
                CardValue.Add('Q', 22);
                CardValue.Add('K', 23);
                CardValue.Add('A', 24);
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