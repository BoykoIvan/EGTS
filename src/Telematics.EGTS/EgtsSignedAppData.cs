﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Telematics.EGTS
{
    public partial class Packet
    {
        /// <summary>
        /// Пакет данного типа применяется для передачи помимо структур, содержащих информацию Уровня Поддержки Услуг, также информации о так называемой  «цифровой подписи», идентифицирующей отправителя данного пакета. 
        /// </summary>
        internal partial class EgtsSignedAppData : EgtsAppData, IEgtsAppData
        {
            private short _SIGL;
            private byte[] _SIGD;

            public EgtsSignedAppData() : base() { }
        }
    }
}