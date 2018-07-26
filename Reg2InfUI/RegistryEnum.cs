using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Reg2InfUI
{
    class RegistryEnum
    {
        string KeyName;
        RegistryValueKind KeyType;
        object KeyValue;

        protected string bitmask_value()
        {
            switch (KeyType)
            {
                case RegistryValueKind.Binary:
                    return "0x00000001";
                case RegistryValueKind.DWord:
                    return "0x00010001";
                case RegistryValueKind.ExpandString:
                    return "0x00020000";
                case RegistryValueKind.MultiString:
                    return "0x00010000";
                case RegistryValueKind.None:
                    return "0x00020001";
                case RegistryValueKind.String:
                    return "0x00000000";
                default:
                    return null;
            }
        }

    }
}
