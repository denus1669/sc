using NUnit.Framework;
using Assets.Casino.Exposure;
using System.Collections.Generic;
using UnityEngine;

namespace Casino.Tests.EditMode.Exposure_Tests
{
    /// <summary>
    /// Тесты для структуры ExposureStage.
    /// Проверяют корректность инициализации и полей стадии раскрытия.
    /// </summary>
    [TestFixture]
    public class ExposureStageTests
    {
        [Test]
        public void Constructor_DefaultValues_InitializesCorrectly()
        {
            // Arrange & Act
            var stage = new ExposureStage();

            // Assert
            Assert.That(stage.level, Is.Zero);
            Assert.That(stage.objectsToActivate, Is.Not.Null);
            Assert.That(stage.objectsToActivate.Count, Is.Zero);
            Assert.That(stage.objectsToDeactivate, Is.Not.Null);
            Assert.That(stage.objectsToDeactivate.Count, Is.Zero);
            Assert.That(stage.onStageApplied, Is.Not.Null);
        }

        [Test]
        public void Constructor_WithLevel1_SetsLevelCorrectly()
        {
            // Arrange & Act
            var stage = new ExposureStage { level = 1 };

            // Assert
            Assert.That(stage.level, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithLevel3_SetsMaxLevelCorrectly()
        {
            // Arrange & Act
            var stage = new ExposureStage { level = 3 };

            // Assert
            Assert.That(stage.level, Is.EqualTo(3));
        }

        [Test]
        public void ObjectsToActivate_WhenObjectsAdded_CountIncreases()
        {
            // Arrange
            var stage = new ExposureStage();
            var go1 = new GameObject("Object1");
            var go2 = new GameObject("Object2");

            // Act
            stage.objectsToActivate.Add(go1);
            stage.objectsToActivate.Add(go2);

            // Assert
            Assert.That(stage.objectsToActivate.Count, Is.EqualTo(2));
            
            // Cleanup
            Object.DestroyImmediate(go1);
            Object.DestroyImmediate(go2);
        }

        [Test]
        public void ObjectsToDeactivate_WhenObjectsAdded_CountIncreases()
        {
            // Arrange
            var stage = new ExposureStage();
            var go1 = new GameObject("Object3");

            // Act
            stage.objectsToDeactivate.Add(go1);

            // Assert
            Assert.That(stage.objectsToDeactivate.Count, Is.EqualTo(1));
            
            // Cleanup
            Object.DestroyImmediate(go1);
        }

        [Test]
        public void OnStageApplied_Event_IsNotNull()
        {
            // Arrange
            var stage = new ExposureStage();

            // Assert
            Assert.That(stage.onStageApplied, Is.Not.Null);
        }

        [Test]
        public void Level_NegativeValue_AllowsNegativeLevel()
        {
            // Arrange & Act
            var stage = new ExposureStage { level = -1 };

            // Assert
            Assert.That(stage.level, Is.EqualTo(-1));
        }

        [Test]
        public void Level_ValueGreaterThan3_AllowsHighLevel()
        {
            // Arrange & Act
            var stage = new ExposureStage { level = 10 };

            // Assert
            Assert.That(stage.level, Is.EqualTo(10));
        }

        [Test]
        public void ObjectsToActivate_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var stage = new ExposureStage();

            // Assert
            Assert.That(stage.objectsToActivate, Is.Empty);
        }

        [Test]
        public void ObjectsToDeactivate_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var stage = new ExposureStage();

            // Assert
            Assert.That(stage.objectsToDeactivate, Is.Empty);
        }
    }
}
