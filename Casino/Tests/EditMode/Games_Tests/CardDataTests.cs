using NUnit.Framework;
using Assets.Casino.Games.BlackGreg;

namespace Casino.Tests.EditMode.Games_Tests
{
    [TestFixture]
    public class CardDataTests
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var cardData = new CardData(CardSuit.Hearts, CardRank.Ace, CardType.Standard);

            Assert.AreEqual(CardSuit.Hearts, cardData.suit);
            Assert.AreEqual(CardRank.Ace, cardData.rank);
            Assert.AreEqual(CardType.Standard, cardData.type);
        }

        [Test]
        public void ToString_ReturnsFormattedString()
        {
            var cardData = new CardData(CardSuit.Spades, CardRank.King, CardType.Cornerless);
            string result = cardData.ToString();

            Assert.AreEqual("King of Spades [Cornerless]", result);
        }

        [Test]
        public void Equals_SameValues_ReturnsTrue()
        {
            var cardData1 = new CardData(CardSuit.Diamonds, CardRank.Queen, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Diamonds, CardRank.Queen, CardType.Standard);

            Assert.IsTrue(cardData1.Equals(cardData2));
        }

        [Test]
        public void Equals_DifferentSuit_ReturnsFalse()
        {
            var cardData1 = new CardData(CardSuit.Hearts, CardRank.Ace, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Spades, CardRank.Ace, CardType.Standard);

            Assert.IsFalse(cardData1.Equals(cardData2));
        }

        [Test]
        public void Equals_DifferentRank_ReturnsFalse()
        {
            var cardData1 = new CardData(CardSuit.Clubs, CardRank.Two, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Clubs, CardRank.Three, CardType.Standard);

            Assert.IsFalse(cardData1.Equals(cardData2));
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var cardData1 = new CardData(CardSuit.Hearts, CardRank.Five, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Hearts, CardRank.Five, CardType.Strikethrough);

            Assert.IsFalse(cardData1.Equals(cardData2));
        }

        [Test]
        public void Equals_ObjectParameter_SameValues_ReturnsTrue()
        {
            var cardData1 = new CardData(CardSuit.Diamonds, CardRank.Jack, CardType.Cornerless);
            object cardData2 = new CardData(CardSuit.Diamonds, CardRank.Jack, CardType.Cornerless);

            Assert.IsTrue(cardData1.Equals(cardData2));
        }

        [Test]
        public void Equals_ObjectParameter_DifferentType_ReturnsFalse()
        {
            var cardData = new CardData(CardSuit.Hearts, CardRank.Seven, CardType.Standard);
            object differentObject = "Not a CardData";

            Assert.IsFalse(cardData.Equals(differentObject));
        }

        [Test]
        public void GetHashCode_SameValues_ReturnsSameHash()
        {
            var cardData1 = new CardData(CardSuit.Clubs, CardRank.Nine, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Clubs, CardRank.Nine, CardType.Standard);

            Assert.AreEqual(cardData1.GetHashCode(), cardData2.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentValues_ReturnsDifferentHash()
        {
            var cardData1 = new CardData(CardSuit.Hearts, CardRank.Ace, CardType.Standard);
            var cardData2 = new CardData(CardSuit.Spades, CardRank.King, CardType.Cornerless);

            Assert.AreNotEqual(cardData1.GetHashCode(), cardData2.GetHashCode());
        }

        [Test]
        public void DefaultConstructor_InitializesToDefaultValues()
        {
            var cardData = new CardData();

            Assert.AreEqual(default(CardSuit), cardData.suit);
            Assert.AreEqual(default(CardRank), cardData.rank);
            Assert.AreEqual(default(CardType), cardData.type);
        }
    }
}
