﻿namespace Egts
{
    public enum ProcessingCode : uint
    {
        /// <summary>Completely done</summary>
        EGTS_PC_OK = 0U,
        /// <summary>In progress</summary>
        EGTS_PC_IN_PROGRESS = 1U,
        /// <summary> Unsupported protocol</summary>
        EGTS_PC_UNS_PROTOCOL = 128U,
        /// <summary>Decription error</summary>
        EGTS_PC_DECRYPT_ERROR = 129U,
        /// <summary>Processing denied </summary>
        EGTS_PC_PROC_DENIED = 130U,
        /// <summary>Incorrect header format</summary>
        EGTS_PC_INC_HEADERFORM = 131U,
        /// <summary>Incorrect data format</summary>
        EGTS_PC_INC_DATAFORM = 132U,
        /// <summary>Unsupported type</summary>
        EGTS_PC_UNS_TYPE = 133U,
        /// <summary>Incorrect parameters number</summary>
        EGTS_PC_NOTEN_PARAMS = 134U,
        /// <summary>Attempt to retry processing</summary>
        EGTS_PC_DBL_PROC = 135U,
        /// <summary>Source data processing denied</summary>
        EGTS_PC_PROC_SRC_DENIED = 136U,
        /// <summary>Header CRC error</summary>
        EGTS_PC_HEADERCRC_ERROR = 137U,
        /// <summary>Data CRC error</summary>
        EGTS_PC_DATACRC_ERROR = 138U,
        /// <summary>Invalid data length</summary>
        EGTS_PC_INVDATALEN = 139U,
        /// <summary>Route not found</summary>
        EGTS_PC_ROUTE_NFOUND = 140U,
        /// <summary>Route closed</summary>
        EGTS_PC_ROUTE_CLOSED = 141U,
        /// <summary>Routing denied</summary>
        EGTS_PC_ROUTE_DENIED = 142U,
        /// <summary>Invalid address</summary>
        EGTS_PC_INVADDR = 143U,
        /// <summary>Retranslation data amount exceed</summary>
        EGTS_PC_TTLEXPIRED = 144U,
        /// <summary>No acknowledge</summary>
        EGTS_PC_NO_ACK = 145U,
        /// <summary>Object not found</summary>
        EGTS_PC_OBJ_NFOUND = 146U,
        /// <summary>Event not found</summary>
        EGTS_PC_EVNT_NFOUND = 147U,
        /// <summary>Service not found</summary>
        EGTS_PC_SRVC_NFOUND = 148U,
        /// <summary>Service denied</summary>
        EGTS_PC_SRVC_DENIED = 149U,
        /// <summary>Unknown service type</summary>
        EGTS_PC_SRVC_UNKN = 150U,
        /// <summary>Authorization denied</summary>
        EGTS_PC_AUTH_DENIED = 151U,
        /// <summary>Object already exist</summary>
        EGTS_PC_ALREADY_EXISTS = 152U,
        /// <summary>Identificator not found</summary>
        EGTS_PC_ID_NFOUND = 153U,
        /// <summary>Date or time incorrect</summary>
        EGTS_PC_INC_DATETIME = 154U,
        /// <summary>Input / output error</summary>
        EGTS_PC_IO_ERROR = 155U,
        /// <summary>Resources not available</summary>
        EGTS_PC_NO_RES_AVAIL = 156U,
        /// <summary>Module internal fault</summary>
        EGTS_PC_MODULE_FAULT = 157U,
        /// <summary>Module power fault</summary>
        EGTS_PC_MODULE_PWR_FLT = 158U,
        /// <summary>Module processor fault</summary>
        EGTS_PC_MODULE_PROC_FLT = 159U,
        /// <summary>Module software fault</summary>
        EGTS_PC_MODULE_SW_FLT = 160U,
        /// <summary>Module firmware fault</summary>
        EGTS_PC_MODULE_FW_FLT = 161U,
        /// <summary>Module input/output fault</summary>
        EGTS_PC_MODULE_IO_FLT = 162U,
        /// <summary>Module memory fault</summary>
        EGTS_PC_MODULE_MEM_FLT = 163U,
        /// <summary>Test failed</summary>
        EGTS_PC_TEST_FAILED = 164U,
        /// <summary>Unknown error </summary>
        EGTS_PC_UNKNOWN = 255U
    }
}

namespace Egts.Data
{
    public enum Priority : byte
    {
        Highest = 0,
        High,
        Normal,
        Low
    }

    public enum HeaderFlags : byte
    {
        PR =  0b00000011,    // bit0 | bit1,
        CMP = 0b00000100,   // bit2,
        RTE = 0b00100000,   // bit5,
        PRF = 0b11000000    // bit6 | bit7
    }

    public enum RecordFlags : byte
    {
        OBFE = 0b00000001,  // (1 << 0),
        EVFE = 0b00000010,  // (1 << 1),
        TMFE = 0b00000100,  // (1 << 2),
        RPP =  0b00011000,  // (1 << 3) | (1 << 4),
        GRP =  0b00100000,  // (1 << 5),
        RSOD = 0b01000000,  // (1 << 6),
        SSOD = 0b10000000   // (1 << 7)
    }

    public enum PosDataFlags : byte
    {
        VLD =  0b00000001,  // (1 << 0),
        FIX =  0b00000010,  // (1 << 1),
        CS =   0b00000100,  // (1 << 2),
        BB =   0b00001000,  // (1 << 3),
        MV =   0b00010000,  // (1 << 4),
        LAHS = 0b00100000,  // (1 << 5),
        LOHS = 0b01000000,  // (1 << 6),
        ALTE = 0b10000000   // (1 << 7)
    }
}

namespace Egts.Data.TransportLayer
{
    public enum PacketType : byte
    {
        EGTS_PT_RESPONSE = 0,
        EGTS_PT_APPDATA,
        EGTS_PT_SIGNED_APPDATA
    }
}

namespace Egts.Data.ServiceLayer
{
    public enum Service : byte
    {
        EGTS_AUTH_SERVICE = 1,
        EGTS_TELEDATA_SERVICE = 2,
        EGTS_COMMANDS_SERVICE = 4,
        EGTS_FIRMWARE_SERVICE = 9,
        EGTS_ECALL_SERVICE = 10
    }

    public enum SubrecordType : byte
    {
        EGTS_SR_RECORD_RESPONSE = 0,
        #region EGTS_AUTH_SERVICE
        EGTS_SR_TERM_IDENTITY = 1,
        EGTS_SR_MODULE_DATA = 2,
        EGTS_SR_VEHICLE_DATA = 3,
        EGTS_SR_DISPATCHER_IDENTITY = 5,
        EGTS_SR_AUTH_PARAMS = 6,
        EGTS_SR_AUTH_INFO = 7,
        EGTS_SR_SERVICE_INFO = 8,
        EGTS_SR_RESULT_CODE = 9,
        #endregion
        #region EGTS_TELEDATA_SERVICE
        EGTS_SR_POS_DATA = 16,
        EGTS_SR_EXT_POS_DATA = 17,
        EGTS_SR_AD_SENSORS_DATA = 18,
        EGTS_SR_COUNTERS_DATA = 19,
        EGTS_SR_ACCEL_DATA = 20,
        EGTS_SR_STATE_DATA = 21,
        EGTS_SR_LOOPIN_DATA = 22,
        EGTS_SR_ABS_DIG_SENS_DATA = 23,
        EGTS_SR_ABS_AN_SENS_DATA = 24,
        EGTS_SR_ABS_CNTR_DATA = 25,
        EGTS_SR_ABS_LOOPIN_DATA = 26,
        EGTS_SR_LIQUID_LEVEL_SENSOR = 27,
        EGTS_SR_PASSENGERS_COUNTERS = 28
        #endregion
    }
}