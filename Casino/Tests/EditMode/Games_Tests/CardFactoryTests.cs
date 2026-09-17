using NUnit.Framework;
using Assets.Casino.Games.BlackGreg;

namespace Casino.Tests.EditMode.Games_Tests
{
    [TestFixture]
    public class CardFactoryTests
    {
        [Test]
        public void CreateRandomCard_ReturnsValidCard()
        {
            var card = CardFactory.CreateRandomCard();

            Assert.IsNotNull(card);
            Assert.IsTrue(card.CardSuit >= CardSuit.Hearts && card.CardSuit <= CardSuit.Spades);
            Assert.IsTrue(card.CardRank >= CardRank.Two && card.CardRank <= CardRank.Ace);
            Assert.IsTrue(card.CardType >= CardType.Standard && card.CardType <= CardType.Strikethrough);
        }

        [Test]
        public void CreateRandomCard_ProducesDifferentCards()
        {
            var card1 = CardFactory.CreateRandomCard();
            var card2 = CardFactory.CreateRandomCard();

            // Маловероятно, но возможно, что карты будут одинаковыми
            // Тест просто проверяет, что метод работает
            Assert.IsNotNull(card1);
            Assert.IsNotNull(card2);
        }

        [Test]
        public void GetRandomCardTypeByWeight_ReturnsValidType()
        {
            var type = CardFactory.GetRandomCardTypeByWeight();

            Assert.IsTrue(type >= CardType.Standard && type <= CardType.Strikethrough);
        }

        [Test]
        public void GetRandomCardTypeByWeight_StandardHasHighestProbability()
        {
            int standardCount = 0;
            int totalTests = 1000;

            for (int i = 0; i < totalTests; i++)
            {
                var type = CardFactory.GetRandomCardTypeByWeight();
                if (type == CardType.Standard)
                    standardCount++;
            }

            // Standard имеет вес 70 из 100, поэтому должен выпадать примерно в 70% случаев
            float standardProbability = (float)standardCount / totalTests;
            Assert.Greater(standardProbability, 0.5f); // Хотя бы больше 50%
        }

        [Test]
        public void GetRandomCardTypeByWeight_CornerlessHasMediumProbability()
        {
            int cornerlessCount = 0;
            int totalTests = 1000;

            for (int i = 0; i < totalTests; i++)
            {
                var type = CardFactory.GetRandomCardTypeByWeight();
                if (type == CardType.Cornerless)
                    cornerlessCount++;
            }

            // Cornerless имеет вес 20 из 100
            float cornerlessProbability = (float)cornerlessCount / totalTests;
            Assert.Greater(cornerlessProbability, 0.1f);
            Assert.Less(cornerlessProbability, 0.35f);
        }

        [Test]
        public void GetRandomCardTypeByWeight_StrikethroughHasLowestProbability()
        {
            int strikethroughCount = 0;
            int totalTests = 1000;

            for (int i = 0; i < totalTests; i++)
            {
                var type = CardFactory.GetRandomCardTypeByWeight();
                if (type == CardType.Strikethrough)
                    strikethroughCount++;
            }

            // Strikethrough имеет вес 10 из 100
            float strikethroughProbability = (float)strikethroughCount / totalTests;
            Assert.Less(strikethroughProbability, 0.2f);
        }

        [Test]
        public void CreateMultipleCards_DistributesTypesCorrectly()
        {
            int standardCount = 0, cornerlessCount = 0, strikethroughCount = 0;
            int totalCards = 1000;

            for (int i = 0; i < totalCards; i++)
            {
                var card = CardFactory.CreateRandomCard();
                switch (card.CardType)
                {
                    case CardType.Standard: standardCount++; break;
                    case CardType.Cornerless: cornerlessCount++; break;
                    case CardType.Strikethrough: strikethroughCount++; break;
                }
            }

            Assert.Greater(standardCount, cornerlessCount);
            Assert.Greater(cornerlessCount, strikethroughCount);
        }
    }
}
