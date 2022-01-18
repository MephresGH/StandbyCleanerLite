//+---------------------------------------------------------------------------
//
//  STANDBYCLEANERLITE.CS - Minimal Windows Standby List Cleaner By Mephres
//
//+---------------------------------------------------------------------------

using System;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Reflection;

//ASSEMBLYINFO
[assembly: AssemblyTitle("A lightweight 'Empty Standby List' alternative.")]
[assembly: AssemblyDescription("Programmed by Mephres, made as a lightweight alternative to the well-established 'Empty Standby List' program, made by wj32 and based off of their 'Process Hacker'.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mephres")]
[assembly: AssemblyProduct("Standby Cleaner Lite")]
[assembly: AssemblyCopyright("Copyright © 2022 Mephres")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("4.0.0.0")]
[assembly: AssemblyFileVersion("4.0.0.0")]

//FUNCTIONS
namespace ClearMemory
{
    class Program
    {

        static int Main(string[] args)
        {
            try
            {
                using (var current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
                {
                    TokPriv1Luid newst;
                    newst.Count = 1; newst.Luid = 0L; newst.Attr = 2;// SE_PRIVILEGE_ENABLED;
                    if (!LookupPrivilegeValue(null, "SeProfileSingleProcessPrivilege", ref newst.Luid))
                    {
                        Console.Error.Write("Error in LookupPrivilegeValue: {0}", Marshal.GetLastWin32Error());
                        return 1;
                    }
                    if (!AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero))
                    {
                        Console.Error.Write("Error in AdjustTokenPrivileges: {0}", Marshal.GetLastWin32Error());
                        return 2;
                    }

                    int SystemMemoryListInformation = 0x0050;
                    int MemoryPurgeStandbyList = 4;
                    uint res = NtSetSystemInformation(SystemMemoryListInformation, ref MemoryPurgeStandbyList, Marshal.SizeOf(MemoryPurgeStandbyList));
                    if (res != 0)
                    {
                        Console.Error.Write("Error in NtSetSystemInformation: {0}", Marshal.GetLastWin32Error());
                        return 3;
                    }
                }
                return 0;
            }
            catch(Exception ex)
            {
                Console.Error.Write("Unexpected error:\n{0}", ex.ToString());
                return 4;
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern UInt32 NtSetSystemInformation(int InfoClass, ref int Info, int Length);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }
    }
}