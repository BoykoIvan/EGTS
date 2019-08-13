﻿using System;
using System.IO;

namespace Telematics.EGTS
{
    public class EgtsPacket
    {
        #region Конструкторы
        /// <summary>Конструктор пакета указанного типа.</summary>
        /// <param name="type">Тип пакета.</param>
        public EgtsPacket(EgtsPacketType type)
        {
            _PRV = 1;
            _PT = (byte)type;
            ServiceData = CreateAppDataInstance(type);

        }
        /// <summary>Конструктор пакета из двоичных данных.</summary>
        /// <param name="stream">Двоичные данные пакета</param>
        public EgtsPacket(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                _PRV = reader.ReadByte();
                _SKID = reader.ReadByte();
                _Flags = reader.ReadByte();
                _HL = reader.ReadByte();
                _HE = reader.ReadByte();
                _FDL = reader.ReadUInt16();
                _PID = reader.ReadUInt16();
                _PT = reader.ReadByte();
                // Если Route, то считываем опциональные поля.
                if (Route)
                {
                    _PRA = reader.ReadUInt16();
                    _RCA = reader.ReadUInt16();
                    _TTL = reader.ReadByte();
                }

                ServiceData = CreateAppDataInstance(PacketType);

                if (_FDL > 0)
                {
                    //    _Data = new PacketData(stream);
                    //    _SFRCS = reader.ReadUInt16();
                };

            }
        }
        #endregion

        #region Свойства
        /// <summary>Параметр определяет версию используемой структуры заголовка.</summary>
        /// <value>Версия используемого протокола.</value>
        /// <remarks>Должен содержать значение 1</remarks>
        public byte ProtocolVersion
        {
            get => _PRV;
            set => _PRV = value;
        }
        /// <summary>Параметр определяет идентификатор ключа, используемый при шифровании.</summary>
        public byte SecurityKeyId
        {
            get => _SKID;
            set => _SKID = value;
        }
        /// <summary>Данный параметр определяет префикс заголовка Транспортного Уровня</summary>
        /// <value>The prefix.</value>
        /// <exception cref="ArgumentOutOfRangeException">value - Для данной версии протокола префикс должен иметь значение 0</exception>
        /// <remarks>Для данной версии протокола должен содержать значение 0.</remarks>
        public byte Prefix
        {
            get => (byte)((_Flags & 0b11000000) >> 6);
            set
            {
                if (value != 0)
                    throw new ArgumentOutOfRangeException("value", value, "Для данной версии протокола префикс должен иметь значение 0");
                else
                    _Flags = (byte)((value << 6) | (_Flags & 0b00111111));
            }
        }
        /// <summary>
        /// Определяет необходимость дальнейшей маршрутизации данного пакета на удалённую телематическую платформу, а также наличие опциональных параметров <see cref="PeerAddress"/>, <see cref="RecipientAddress"/>, <see cref="TTL"/>.
        /// </summary>
        public bool Route
        {
            get => (_Flags & 0b00100000) == 0b00100000;
            set => _Flags = (byte)((value ? 0b00100000 : 0) | (_Flags & 0b11011111));
        }
        /// <summary>Битовое поле определяет код алгоритма, используемый для шифрования данных из поля SFRD.
        /// Если поле имеет значение 0  то данные в поле SFRD не шифруются.</summary>
        /// <value>The encryption algorithm.</value>
        /// <exception cref="ArgumentOutOfRangeException">value - Допустимы значения 0 - 3</exception>
        /// <remarks>Состав и коды алгоритмов не определены в данной версии Протокола</remarks>
        public byte EncryptionAlgorithm
        {
            get => (byte)((_Flags & 0b00011000) >> 3);
            private set
            {
                if (value > 3)
                    throw new ArgumentOutOfRangeException("value", value, "Допустимы значения 0 - 3");
                else
                    _Flags = (byte)((value << 3) | (_Flags & 0b00011000));
            }
        }
        /// <summary>Определяет, используется ли сжатие данных из поля SFRD.
        /// Если поле имеет значение 1, то данные в поле SFRD считаются сжатыми.</summary>
        /// <remarks>Алгоритм сжатия не определен в данной версии Протокола.</remarks>
        public bool Compressed
        {
            get => (_Flags & 0b00000100) == 0b00000100;
            private set => _Flags = (byte)((value ? 0b000000100 : 0) | (_Flags & 0b11111011));
        }
        /// <summary>Определяет приоритет маршрутизации данного пакета.
        /// <seealso cref="EgtsPriority"/></summary>
        /// <value>The priority.</value>
        public EgtsPriority Priority
        {
            get => (EgtsPriority)(_Flags & 0b00000011);
            set => _Flags = (byte)((byte)value | (_Flags & 0b00000011));
        }
        /// <summary>Содержит номер пакета Транспортного Уровня.</summary>
        /// <value>The packet identifier.</value>
        public ushort PacketIdentifier
        {
            get => _PID;
            set => _PID = value;
        }
        /// <summary>Тип пакета Транспортного Уровня.
        /// <seealso cref="Types.PacketType"/></summary>
        public EgtsPacketType PacketType
        {
            get => (EgtsPacketType)_PT;
        }
        /// <summary>Адрес ТП, на которой данный пакет сгенерирован.</summary>
        /// <remarks>Данный адрес является уникальным в рамках связной сети и используется для создания пакета-подтверждения на принимающей стороне.</remarks>
        public ushort PeerAddress
        {
            get => _PRA;
            set => _PRA = value;
        }
        /// <summary>Адрес ТП, для которой данный пакет предназначен.</summary>
        /// <remarks>По данному адресу производится идентификация принадлежности пакета определённой ТП и его маршрутизация при использовании промежуточных ТП.</remarks>
        public ushort RecipientAddress
        {
            get => _RCA;
            set => _RCA = value;
        }
        /// <summary>
        /// Время жизни пакета при его маршрутизации между ТП.
        /// Значение TTL уменьшается на единицу при трансляции пакета через каждую ТП, при этом пересчитывается контрольная сумма заголовка Транспортного Уровня.
        /// При достижении данным параметром значения 0 и при обнаружении необходимости дальнейшей маршрутизации пакета, происходит уничтожение пакета и выдача подтверждения с соответствующим кодом <see cref="Types.ProcessingCode.EGTS_PC_TTLEXPIRED"/>
        /// </summary>
        public byte TTL
        {
            get => _TTL;
            set => _TTL = value;
        }
        public IEgtsAppData ServiceData { get; }
        #endregion

        #region Поля
        private byte _PRV;
        private byte _SKID;
        private byte _Flags;
        private byte _HL;
        private byte _HE;
        private ushort _FDL;
        private ushort _PID;
        private readonly byte _PT;
        private ushort _PRA;
        private ushort _RCA;
        private byte _TTL;
        private byte _HCS;
        private ushort _SFRCS;
        #endregion

        #region Служебные методы
        private static IEgtsAppData CreateAppDataInstance(EgtsPacketType type)
        {
            switch (type)
            {
                case EgtsPacketType.EGTS_PT_APPDATA:
                    return new EgtsAppData();
                case EgtsPacketType.EGTS_PT_RESPONSE:
                    return new EgtsResponseAppData();
                case EgtsPacketType.EGTS_PT_SIGNED_APPDATA:
                    return new EgtsSignedAppData();
                default:
                    throw new ArgumentException("Неизвестный типа пакета EGTS.", "type");
            };
        }
        #endregion
    }
}
