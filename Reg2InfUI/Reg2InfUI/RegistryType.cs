using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Reg2InfUI
{

    class RegistryType
    {
        RegistryKey regKey = Registry.LocalMachine;

        protected RegistryKey openKey(string location)
        {
            regKey = regKey.OpenSubKey(location);
            return regKey;
        }

        protected List<string> listSubKey()
        {
            List<string> lsSubKey = new List<string>();
            if (regKey != null)
            {
                string[] temp = regKey.GetSubKeyNames();
                foreach (string str in temp)
                {
                    lsSubKey.Add(str);
                }
                return lsSubKey;
            }
            return lsSubKey;
        }
        protected bool Is_SubKey_Exist(string location)
        {
            regKey = regKey.OpenSubKey(location);
            if (regKey != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool Is_KeyName_Exist(string KeyName, string location)
        {
            regKey = regKey.OpenSubKey(location);
            if (Registry.GetValue(location, KeyName, null) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
