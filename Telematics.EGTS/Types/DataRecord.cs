﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Telematics.EGTS.Types
{
    class DataRecord
    {
        private ushort _RL;
        private ushort _RN;
        private byte _RFL;
        private uint _OID;
        private uint _EVID;
        private uint _TM;
        private byte _SST;
        private byte _RST;

        private object[] RecordData;
    }
}