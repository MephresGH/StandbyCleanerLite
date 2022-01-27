//LIBRARY
using System;
using System.Security.Principal;
using System.Runtime.InteropServices;

//FUNCTIONS
class SBCL
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
                        return 1;
                    }
                    if (!AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero))
                    {
                        return 2;
                    }
                    int SystemMemoryListInformation = 0x0050;
                    int MemoryPurgeStandbyList = 4;
                    uint res = NtSetSystemInformation(SystemMemoryListInformation, ref MemoryPurgeStandbyList, Marshal.SizeOf(MemoryPurgeStandbyList));
                    if (res != 0)
                    {
                        return 3;
                    }
                }
                return 0;
            }
            catch(Exception)
            {
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