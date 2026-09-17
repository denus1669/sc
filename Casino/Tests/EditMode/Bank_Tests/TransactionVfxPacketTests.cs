using NUnit.Framework;
using Assets.Casino.Bank;
using Unity.Netcode;

namespace Casino.Tests.EditMode.Bank_Tests
{
    /// <summary>
    /// Тесты для структуры TransactionVfxPacket.
    /// Проверяют корректность сетевой сериализации и данных.
    /// </summary>
    [TestFixture]
    public class TransactionVfxPacketTests
    {
        [Test]
        public void Constructor_WithValidParameters_CreatesPacketCorrectly()
        {
            // Arrange
            ulong operatorId = 12345UL;
            int amount = 500;
            TransactionType type = TransactionType.Deposit;
            string reason = "Ante";
            string tableType = "BlackGreg";

            // Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = operatorId,
                Amount = amount,
                Type = type,
                Reason = reason,
                TableType = tableType
            };

            // Assert
            Assert.That(packet.OperatorClientId, Is.EqualTo(operatorId));
            Assert.That(packet.Amount, Is.EqualTo(amount));
            Assert.That(packet.Type, Is.EqualTo(type));
            Assert.That(packet.Reason, Is.EqualTo(reason));
            Assert.That(packet.TableType, Is.EqualTo(tableType));
        }

        [Test]
        public void Constructor_WithNullReason_SetsNullReason()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = 1UL,
                Amount = 100,
                Type = TransactionType.Withdraw,
                Reason = null,
                TableType = null
            };

            // Assert
            Assert.That(packet.Reason, Is.Null);
            Assert.That(packet.TableType, Is.Null);
        }

        [Test]
        public void Constructor_WithZeroAmount_CreatesPacketWithZero()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = 0UL,
                Amount = 0,
                Type = TransactionType.Deposit,
                Reason = "Test",
                TableType = "TestTable"
            };

            // Assert
            Assert.That(packet.Amount, Is.Zero);
        }

        [Test]
        public void Constructor_WithNegativeAmount_CreatesPacketWithNegativeValue()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = 1UL,
                Amount = -100,
                Type = TransactionType.Withdraw,
                Reason = "Test",
                TableType = "TestTable"
            };

            // Assert
            Assert.That(packet.Amount, Is.EqualTo(-100));
        }

        [Test]
        public void Constructor_WithMaxOperatorId_CreatesSystemPacket()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = ulong.MaxValue,
                Amount = 1000,
                Type = TransactionType.Deposit,
                Reason = "System",
                TableType = "None"
            };

            // Assert
            Assert.That(packet.OperatorClientId, Is.EqualTo(ulong.MaxValue));
        }

        [Test]
        public void Constructor_DepositType_HasCorrectEnumValue()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = 1UL,
                Amount = 100,
                Type = TransactionType.Deposit,
                Reason = "Test",
                TableType = "Test"
            };

            // Assert
            Assert.That(packet.Type, Is.EqualTo(TransactionType.Deposit));
        }

        [Test]
        public void Constructor_WithdrawType_HasCorrectEnumValue()
        {
            // Arrange & Act
            var packet = new TransactionVfxPacket
            {
                OperatorClientId = 1UL,
                Amount = 100,
                Type = TransactionType.Withdraw,
                Reason = "Test",
                TableType = "Test"
            };

            // Assert
            Assert.That(packet.Type, Is.EqualTo(TransactionType.Withdraw));
        }

        [Test]
        public void NetworkSerialize_Deserialize_RoundTripMaintainsData()
        {
            // Arrange
            var original = new TransactionVfxPacket
            {
                OperatorClientId = 98765UL,
                Amount = 250,
                Type = TransactionType.Deposit,
                Reason = "WinBonus",
                TableType = "Slots"
            };

            var serializer = new TestBufferSerializer();
            
            // Act - Serialize
            original.NetworkSerialize(serializer);
            
            // Act - Deserialize
            var deserialized = new TransactionVfxPacket();
            deserialized.NetworkSerialize(serializer);

            // Assert
            Assert.That(deserialized.OperatorClientId, Is.EqualTo(original.OperatorClientId));
            Assert.That(deserialized.Amount, Is.EqualTo(original.Amount));
            Assert.That(deserialized.Type, Is.EqualTo(original.Type));
            Assert.That(deserialized.Reason, Is.EqualTo(original.Reason));
            Assert.That(deserialized.TableType, Is.EqualTo(original.TableType));
        }

        // Вспомогательный класс для тестирования сериализации
        private class TestBufferSerializer : IReaderWriter
        {
            private readonly System.Collections.Generic.List<byte> _data = new System.Collections.Generic.List<byte>();
            private int _position;
            private bool _isWriting;

            public bool IsReader => !_isWriting;
            public bool IsWriter => _isWriting;

            public void SerializeValue(ref byte value)
            {
                if (_isWriting)
                    _data.Add(value);
                else
                    value = _data[_position++];
            }

            public void SerializeValue(ref sbyte value)
            {
                if (_isWriting)
                    _data.Add((byte)value);
                else
                    value = (sbyte)_data[_position++];
            }

            public void SerializeValue(ref short value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToInt16(_data.ToArray(), _position);
                    _position += sizeof(short);
                }
            }

            public void SerializeValue(ref ushort value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToUInt16(_data.ToArray(), _position);
                    _position += sizeof(ushort);
                }
            }

            public void SerializeValue(ref int value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToInt32(_data.ToArray(), _position);
                    _position += sizeof(int);
                }
            }

            public void SerializeValue(ref uint value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToUInt32(_data.ToArray(), _position);
                    _position += sizeof(uint);
                }
            }

            public void SerializeValue(ref long value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToInt64(_data.ToArray(), _position);
                    _position += sizeof(long);
                }
            }

            public void SerializeValue(ref ulong value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToUInt64(_data.ToArray(), _position);
                    _position += sizeof(ulong);
                }
            }

            public void SerializeValue(ref float value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToSingle(_data.ToArray(), _position);
                    _position += sizeof(float);
                }
            }

            public void SerializeValue(ref double value)
            {
                var bytes = System.BitConverter.GetBytes(value);
                if (_isWriting)
                    _data.AddRange(bytes);
                else
                {
                    value = System.BitConverter.ToDouble(_data.ToArray(), _position);
                    _position += sizeof(double);
                }
            }

            public void SerializeValue(ref bool value)
            {
                if (_isWriting)
                    _data.Add((byte)(value ? 1 : 0));
                else
                    value = _data[_position++] == 1;
            }

            public void SerializeValue(ref string value)
            {
                if (_isWriting)
                {
                    byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(value ?? string.Empty);
                    int length = stringBytes.Length;
                    SerializeValue(ref length);
                    _data.AddRange(stringBytes);
                }
                else
                {
                    int length = 0;
                    SerializeValue(ref length);
                    if (length > 0)
                    {
                        value = System.Text.Encoding.UTF8.GetString(_data.ToArray(), _position, length);
                        _position += length;
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }
            }

            public void SerializeValue<T>(ref T value) where T : unmanaged
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue<T>(ref T value, ForceMode forceMode) where T : unmanaged
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Vector2 value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Vector3 value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Vector4 value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Quaternion value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Color value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Color32 value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.Ray value)
            {
                throw new System.NotImplementedException();
            }

            public void SerializeValue(ref UnityEngine.RaycastHit value)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
