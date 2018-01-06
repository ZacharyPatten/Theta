//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;

//namespace Theta.Input
//{
//    public static class UnusedGamepadCode
//    {
//        const string lib = "hid.dll";

//        #region Externs

//        [DllImport("kernel32.dll")]
//        internal static extern void SetLastError(Int32 dwErrCode);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetLinkCollectionNodes")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetLinkCollectionNodes(
//            [Out] HidProtocolLinkCollectionNode[] LinkCollectionNodes,
//            ref int LinkCollectionNodesLength,
//            [In] byte[] PreparsedData);


//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetButtonCaps")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetButtonCaps(Theta.Input.GamePad.HidProtocolReportType hidProtocolReportType,
//            IntPtr button_caps, ref ushort p, [In] byte[] preparsed_data);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetData")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetData(Theta.Input.GamePad.HidProtocolReportType type,
//            IntPtr data, ref int data_length,
//            [In] byte[] preparsed_data, IntPtr report, int report_length);


//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetData")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetData(Theta.Input.GamePad.HidProtocolReportType type,
//            [Out] Theta.Input.GamePad.HidProtocolData[] data, ref int data_length,
//            [In] byte[] preparsed_data, IntPtr report, int report_length);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetScaledUsageValue")]
//        static public extern Theta.Input.GamePad.HidProtocolStatus GetScaledUsageValue(Theta.Input.GamePad.HidProtocolReportType type,
//            Theta.Input.GamePad.HIDPage usage_page, short link_collection, short usage, ref int usage_value,
//            [In] byte[] preparsed_data, IntPtr report, int report_length);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetSpecificButtonCaps")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetSpecificButtonCaps(Theta.Input.GamePad.HidProtocolReportType ReportType,
//            Theta.Input.GamePad.HIDPage UsagePage, ushort LinkCollection, ushort Usage,
//            [Out] Theta.Input.GamePad.HidProtocolButtonCaps[] ButtonCaps, ref ushort ButtonCapsLength,
//            [In] byte[] PreparsedData);


//        [SuppressUnmanagedCodeSecurity]
//        [DllImport(lib, SetLastError = true, EntryPoint = "HidP_GetValueCaps")]
//        public static extern Theta.Input.GamePad.HidProtocolStatus GetValueCaps(Theta.Input.GamePad.HidProtocolReportType type,
//            IntPtr caps, ref ushort caps_length, [In] byte[] preparsed_data);


//        [CLSCompliant(false)]
//        [DllImport("user32.dll", SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern bool RegisterRawInputDevices(
//            Theta.Input.GamePad.RawInputDevice[] RawInputDevices,
//            UInt32 NumDevices,
//            UInt32 Size
//        );

//        [CLSCompliant(false)]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern UInt32 GetRawInputDeviceList(
//            [In, Out] Theta.Input.GamePad.RawInputDeviceList[] RawInputDeviceList,
//            [In, Out] ref UInt32 NumDevices,
//            UInt32 Size
//        );

//        [DllImport("user32.dll")]
//        public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);


//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLong")]
//        static extern Int32 SetWindowLongInternal(IntPtr hWnd, Theta.Input.GamePad.GetWindowLongOffsets nIndex,
//            [MarshalAs(UnmanagedType.FunctionPtr)]Theta.Input.GamePad.WindowProcedure dwNewLong);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtr")]
//        static extern IntPtr SetWindowLongPtrInternal(IntPtr hWnd, Theta.Input.GamePad.GetWindowLongOffsets nIndex,
//            [MarshalAs(UnmanagedType.FunctionPtr)]Theta.Input.GamePad.WindowProcedure dwNewLong);

//        [CLSCompliant(false)]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern UInt32 GetRawInputDeviceList(
//            [In, Out] IntPtr RawInputDeviceList,
//            [In, Out] ref UInt32 NumDevices,
//            UInt32 Size
//        );

//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputDeviceList(
//            [In, Out] IntPtr RawInputDeviceList,
//            [In, Out] ref int NumDevices,
//            int Size
//        );


//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputData(
//            IntPtr RawInput,
//            Theta.Input.GamePad.GetRawInputDataEnum Command,
//            /*[MarshalAs(UnmanagedType.LPStruct)]*/ [Out] out Theta.Input.GamePad.RawInput Data,
//            [In, Out] ref int Size,
//            int SizeHeader
//        );

//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        unsafe internal static extern int GetRawInputData(
//            IntPtr RawInput,
//            Theta.Input.GamePad.GetRawInputDataEnum Command,
//            Theta.Input.GamePad.RawInput* Data,
//            [In, Out] ref int Size,
//            int SizeHeader
//        );


//        [CLSCompliant(false)]
//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern UInt32 GetRawInputDeviceInfo(
//            IntPtr Device,
//            [MarshalAs(UnmanagedType.U4)] Theta.Input.GamePad.RawInputDeviceInfoEnum Command,
//            [In, Out] Theta.Input.GamePad.RawInputDeviceInfo Data,
//            [In, Out] ref UInt32 Size
//        );


//        #endregion

//        #region Enums

//        public enum HidProtocolCollectionType : byte
//        {

//        }

//        #endregion

//        #region Structs

//        [StructLayout(LayoutKind.Sequential)]
//        public struct HidProtocolLinkCollectionNode
//        {
//            public ushort LinkUsage;
//            public Theta.Input.GamePad.HIDPage LinkUsagePage;
//            public ushort Parent;
//            public ushort NumberOfChildren;
//            public ushort NextSibling;
//            public ushort FirstChild;
//            int bitfield;
//            public IntPtr UserContext;
//            public HidProtocolCollectionType CollectionType
//            {
//                get
//                {
//                    return (HidProtocolCollectionType)(bitfield & 0xff);
//                }
//            }
//            public bool IsAlias
//            {
//                get
//                {
//                    return (bitfield & 0x100) == 1;
//                }
//            }
//        }

//        #endregion


//    }
//}
