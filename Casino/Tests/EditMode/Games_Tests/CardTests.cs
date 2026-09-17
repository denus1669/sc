using NUnit.Framework;
using Assets.Casino.Games.BlackGreg;

namespace Casino.Tests.EditMode.Games_Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var card = new Card(CardSuit.Hearts, CardRank.Ace, CardType.Standard);

            Assert.AreEqual(CardSuit.Hearts, card.CardSuit);
            Assert.AreEqual(CardRank.Ace, card.CardRank);
            Assert.AreEqual(CardType.Standard, card.CardType);
        }

        [Test]
        public void BlackGregValue_Ace_Returns11()
        {
            var card = new Card(CardSuit.Spades, CardRank.Ace, CardType.Standard);
            Assert.AreEqual(11, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Jack_Returns10()
        {
            var card = new Card(CardSuit.Hearts, CardRank.Jack, CardType.Cornerless);
            Assert.AreEqual(10, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Queen_Returns10()
        {
            var card = new Card(CardSuit.Diamonds, CardRank.Queen, CardType.Strikethrough);
            Assert.AreEqual(10, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_King_Returns10()
        {
            var card = new Card(CardSuit.Clubs, CardRank.King, CardType.Standard);
            Assert.AreEqual(10, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_NumberCards_ReturnsFaceValue()
        {
            var cardTwo = new Card(CardSuit.Hearts, CardRank.Two, CardType.Standard);
            Assert.AreEqual(2, cardTwo.BlackGregValue);

            var cardFive = new Card(CardSuit.Spades, CardRank.Five, CardType.Standard);
            Assert.AreEqual(5, cardFive.BlackGregValue);

            var cardTen = new Card(CardSuit.Diamonds, CardRank.Ten, CardType.Standard);
            Assert.AreEqual(10, cardTen.BlackGregValue);
        }

        [Test]
        public void ToString_ReturnsFormattedString()
        {
            var card = new Card(CardSuit.Clubs, CardRank.Seven, CardType.Cornerless);
            string result = card.ToString();

            Assert.AreEqual("Seven of Clubs [Cornerless]", result);
        }

        [Test]
        public void Properties_SetterWorksCorrectly()
        {
            var card = new Card(CardSuit.Hearts, CardRank.Two, CardType.Standard);

            card.CardSuit = CardSuit.Spades;
            card.CardRank = CardRank.Ace;
            card.CardType = CardType.Strikethrough;

            Assert.AreEqual(CardSuit.Spades, card.CardSuit);
            Assert.AreEqual(CardRank.Ace, card.CardRank);
            Assert.AreEqual(CardType.Strikethrough, card.CardType);
        }

        [Test]
        public void BlackGregValue_Three_Returns3()
        {
            var card = new Card(CardSuit.Diamonds, CardRank.Three, CardType.Standard);
            Assert.AreEqual(3, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Four_Returns4()
        {
            var card = new Card(CardSuit.Hearts, CardRank.Four, CardType.Cornerless);
            Assert.AreEqual(4, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Six_Returns6()
        {
            var card = new Card(CardSuit.Clubs, CardRank.Six, CardType.Strikethrough);
            Assert.AreEqual(6, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Eight_Returns8()
        {
            var card = new Card(CardSuit.Spades, CardRank.Eight, CardType.Standard);
            Assert.AreEqual(8, card.BlackGregValue);
        }

        [Test]
        public void BlackGregValue_Nine_Returns9()
        {
            var card = new Card(CardSuit.Diamonds, CardRank.Nine, CardType.Cornerless);
            Assert.AreEqual(9, card.BlackGregValue);
        }
    }
}
