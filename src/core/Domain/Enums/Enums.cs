using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NodeType
    {
        DirectDevice,
        GateWayDevice,
        GatewaySubDevice
    }

    public enum ProtocolType
    {
        ModbusRTU,
        ModbusTCP,
        OPC
    }

    public enum NetType
    {
        MQTT,
        WiFi,
        LoRaWAN,
        Other
    }
}
