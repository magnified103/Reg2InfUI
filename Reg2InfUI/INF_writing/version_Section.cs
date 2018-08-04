using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Reg2InfUI.INF_writing
{
	static class version_Section
	{
		public static void start()
		{
			Variables.INF_content += "[Version] \n";
			Variables.INF_content += "Signature=\"$Windows NT$\"\n";
			Variables.INF_content += ("Class=" + class_detection() + "\n");
			Variables.INF_content += ("ClassGuid=" + classGUID_detection() + "\n");
			Variables.INF_content += "Provider=" + provider_detection() + "\n";
			Variables.INF_content += "DriverVer=06/21/2006";
			Variables.INF_content += "\n\n";
		}

		private static string classGUID_detection()
		{
			RegistryKey class_detect = Registry.LocalMachine;
			string location = Variables.systemHive_location + "\\ControlSet001\\Control\\Class";
			class_detect = class_detect.OpenSubKey(location);
			string[] temp = class_detect.GetSubKeyNames();
			List<string> listClass = new List<string>();
			foreach (string str in temp)
			{
				listClass.Add(str);
			}
			int i = 0;
			while (i <= (listClass.Count - 1))
			{
				if (listClass[i][0] != '{' || listClass[i][listClass[i].Length - 1] != '}')
				{
					listClass.RemoveAt(i);
				}
				i++;
			}
			i = 0;
			class_detect = Registry.LocalMachine;
			while (i <= (listClass.Count - 1))
			{
				class_detect = class_detect.OpenSubKey(location + "\\" + listClass[i]);
				string[] tempo = class_detect.GetSubKeyNames();
				List<string> listSubKey_class = new List<string>();
				foreach (string str in tempo)
				{
					listSubKey_class.Add(str);
				}
				class_detect = Registry.LocalMachine;
				foreach (string item in listSubKey_class)
				{
					if (Registry.GetValue("HKEY_LOCAL_MACHINE\\" + location + "\\" + listClass[i] + "\\" + item, "InfPath", null) == null)
					{
						continue;
					}
					else if (Registry.GetValue("HKEY_LOCAL_MACHINE\\" + location + "\\" + listClass[i] + "\\" + item, "InfPath", null).ToString() == (Variables.driver_name + ".inf"))
					{
						Variables.driver_GUID = listClass[i];
						return listClass[i];
					}
				}
				i++;
			}
			return null;
		}
		private static string class_detection()
		{
			classGUID_detection();
			return Registry.GetValue("HKEY_LOCAL_MACHINE\\" + Variables.systemHive_location + "\\ControlSet001\\Control\\Class\\" + Variables.driver_GUID, "Class", null).ToString();
		}
		public static string provider_detection()
		{
			Variables.driver_pkg_Name = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + Variables.systemHive_location + "\\DriverDatabase\\DriverInfFiles\\" + Variables.driver_name + ".inf", "Active", null).ToString();
			RegistryKey provider_detect = Registry.LocalMachine;
			provider_detect = provider_detect.OpenSubKey(Variables.systemHive_location + "\\DriverDatabase\\DriverPackages\\" + Variables.driver_pkg_Name + "\\Descriptors");
			string[] temp = provider_detect.GetSubKeyNames();
			provider_detect = Registry.LocalMachine;
			provider_detect = provider_detect.OpenSubKey(Variables.systemHive_location + "\\DriverDatabase\\DriverPackages\\" + Variables.driver_pkg_Name + "\\Descriptors\\" + temp[0]);
			string[] temp2 = provider_detect.GetSubKeyNames();
			if (temp2.Count() == 0)
			{
				return Registry.GetValue("HKEY_LOCAL_MACHINE\\" + Variables.systemHive_location + "\\DriverDatabase\\DriverPackages\\" + Variables.driver_pkg_Name + "\\Descriptors\\" + temp[0], "Manufacturer", null).ToString();
			}
			else
			{
				return Registry.GetValue("HKEY_LOCAL_MACHINE\\" + Variables.systemHive_location + "\\DriverDatabase\\DriverPackages\\" + Variables.driver_pkg_Name + "\\Descriptors\\" + temp[0] + "\\" + temp2[0], "Manufacturer", null).ToString();
			}
		}


	}
}
