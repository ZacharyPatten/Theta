//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Security;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Theta.Mathematics;

//namespace Theta.Input
//{
//    public sealed class GamePad
//    {
//        #region Delegates

//        [SuppressUnmanagedCodeSecurity]
//        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
//        internal delegate IntPtr WindowProcedure(IntPtr handle, Theta.Input.GamePad.WindowMessage message, IntPtr wParam, IntPtr lParam);

//        #endregion

//        #region Externs

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetButtonCaps")]
//        public static extern HidProtocolStatus GetButtonCaps(HidProtocolReportType hidProtocolReportType,
//            [Out] HidProtocolButtonCaps[] button_caps, ref ushort p, [In] byte[] preparsed_data);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetCaps")]
//        public static extern HidProtocolStatus GetCaps([In] byte[] preparsed_data, ref HidProtocolCaps capabilities);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetUsages")]
//        unsafe public static extern HidProtocolStatus GetUsages(HidProtocolReportType type,
//            HIDPage usage_page, short link_collection, short* usage_list, ref int usage_length,
//            [In] byte[] preparsed_data, IntPtr report, int report_length);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetUsageValue")]
//        public static extern HidProtocolStatus GetUsageValue(HidProtocolReportType type,
//            HIDPage usage_page, short link_collection, short usage, ref uint usage_value,
//            [In] byte[] preparsed_data, IntPtr report, int report_length);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_GetValueCaps")]
//        public static extern HidProtocolStatus GetValueCaps(HidProtocolReportType type,
//            [Out] HidProtocolValueCaps[] caps, ref ushort caps_length, [In] byte[] preparsed_data);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("hid.dll", SetLastError = true, EntryPoint = "HidP_MaxDataListLength")]
//        public static extern int MaxDataListLength(HidProtocolReportType type, [In] byte[] preparsed_data);


//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLong")]
//        static extern Int32 SetWindowLongInternal(IntPtr hWnd, GetWindowLongOffsets nIndex, Int32 dwNewLong);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtr")]
//        static extern IntPtr SetWindowLongPtrInternal(IntPtr hWnd, GetWindowLongOffsets nIndex, IntPtr dwNewLong);

//        [DllImport("kernel32.dll")]
//        internal static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

//        [DllImport("kernel32.dll")]
//        internal static extern void SetLastError(Int32 dwErrCode);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        internal static extern IntPtr LoadLibrary(string dllName);

//        [DllImport("kernel32.dll")]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern bool FreeLibrary(IntPtr handle);

//        [DllImport("user32.dll")]
//        public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorName);


//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient,
//            IntPtr NotificationFilter, DeviceNotification Flags);

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool UnregisterDeviceNotification(IntPtr Handle);

//        [DllImport("user32.dll", SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern bool RegisterRawInputDevices(
//            RawInputDevice[] RawInputDevices,
//            int NumDevices,
//            int Size
//        );

//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputDeviceList(
//            [In, Out] RawInputDeviceList[] RawInputDeviceList,
//            [In, Out] ref int NumDevices,
//            int Size
//        );

//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputDeviceInfo(
//            IntPtr Device,
//            [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
//            [In, Out] IntPtr Data,
//            [In, Out] ref int Size
//        );

//        [CLSCompliant(false)]
//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputDeviceInfo(
//            IntPtr Device,
//            [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
//            [In, Out] byte[] Data,
//            [In, Out] ref int Size
//        );

//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputDeviceInfo(
//            IntPtr Device,
//            [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
//            [In, Out] RawInputDeviceInfo Data,
//            [In, Out] ref int Size
//        );

//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern int GetRawInputData(
//            IntPtr RawInput,
//            GetRawInputDataEnum Command,
//            [Out] IntPtr Data,
//            [In, Out] ref int Size,
//            int SizeHeader
//        );


//        [System.Security.SuppressUnmanagedCodeSecurity]
//        [DllImport("User32.dll"), CLSCompliant(false)]
//        //[return: MarshalAs(UnmanagedType.Bool)]
//        internal static extern int GetMessage(ref MSG msg,
//            IntPtr windowHandle, int messageFilterMin, int messageFilterMax);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("User32.dll"), CLSCompliant(false)]
//        internal static extern bool TranslateMessage(ref MSG lpMsg);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("User32.dll"), CLSCompliant(false)]
//        internal static extern IntPtr DispatchMessage(ref MSG msg);

//        [SuppressUnmanagedCodeSecurity]
//        [DllImport("user32.dll", SetLastError = true)]
//        internal static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, WindowMessage Msg,
//            IntPtr wParam, IntPtr lParam);

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool SetParent(IntPtr child, IntPtr newParent);

//        #endregion
        
//        #region Enums

//        [Flags]
//        public enum GameWindowFlags
//        {
//            /// <summary>
//            /// Indicates default construction options.
//            /// </summary>
//            Default = 0,

//            /// <summary>
//            /// Indicates that the GameWindow should cover the whole screen.
//            /// </summary>
//            Fullscreen = 1,

//            /// <summary>
//            /// Indicates that the GameWindow should be a fixed window.
//            /// </summary>
//            FixedWindow = 2,
//        }

//        public enum DeviceNotification
//        {
//            WINDOW_IntPtr = 0x00000000,
//            SERVICE_IntPtr = 0x00000001,
//            ALL_intERFACE_CLASSES = 0x00000004,
//        }

//        enum DeviceBroadcastType
//        {
//            OEM = 0,
//            VOLUME = 2,
//            PORT = 3,
//            intERFACE = 5,
//            IntPtr = 6,
//        }

//        public enum HidProtocolReportType : ushort
//        {
//            Input,
//            Output,
//            Feature
//        }

//        public enum HidProtocolStatus : uint
//        {
//            Success = 0x00110000,
//            Null = 0x80110001,
//            InvalidPreparsedData = 0xc0110001,
//            InvalidReportType = 0xc0110002,
//            InvalidReportLength = 0xc0110003,
//            UsageNotFound = 0xc0110004,
//            ValueOutOfRange = 0xc0110005,
//            BadLogPhyValues = 0xc0110006,
//            BufferTooSmall = 0xc0110007,
//            InternallError = 0xc0110008,
//            I8042TransNotKnown = 0xc0110009,
//            IncompatibleReportId = 0xc011000a,
//            NotValueArray = 0xc011000b,
//            IsValueArray = 0xc011000c,
//            DataIndexNotFound = 0xc011000d,
//            DataIndexOutOfRange = 0xc011000e,
//            ButtonNotPressed = 0xc011000f,
//            ReportDoesNotExist = 0xc0110010,
//            NotImplemented = 0xc0110020,
//        }

//        internal enum RawInputKeyboardDataFlags : short //: ushort
//        {
//            MAKE = 0,
//            BREAK = 1,
//            E0 = 2,
//            E1 = 4,
//            TERMSRV_SET_LED = 8,
//            TERMSRV_SHADOW = 0x10
//        }

//        internal enum VirtualKeys : short
//        {
//            /*
//             * Virtual Key, Standard Set
//             */
//            LBUTTON = 0x01,
//            RBUTTON = 0x02,
//            CANCEL = 0x03,
//            MBUTTON = 0x04,   /* NOT contiguous with L & RBUTTON */

//            XBUTTON1 = 0x05,   /* NOT contiguous with L & RBUTTON */
//            XBUTTON2 = 0x06,   /* NOT contiguous with L & RBUTTON */

//            /*
//             * 0x07 : unassigned
//             */

//            BACK = 0x08,
//            TAB = 0x09,

//            /*
//             * 0x0A - 0x0B : reserved
//             */

//            CLEAR = 0x0C,
//            RETURN = 0x0D,

//            SHIFT = 0x10,
//            CONTROL = 0x11,
//            MENU = 0x12,
//            PAUSE = 0x13,
//            CAPITAL = 0x14,

//            KANA = 0x15,
//            HANGEUL = 0x15,  /* old name - should be here for compatibility */
//            HANGUL = 0x15,
//            JUNJA = 0x17,
//            FINAL = 0x18,
//            HANJA = 0x19,
//            KANJI = 0x19,

//            ESCAPE = 0x1B,

//            CONVERT = 0x1C,
//            NONCONVERT = 0x1D,
//            ACCEPT = 0x1E,
//            MODECHANGE = 0x1F,

//            SPACE = 0x20,
//            PRIOR = 0x21,
//            NEXT = 0x22,
//            END = 0x23,
//            HOME = 0x24,
//            LEFT = 0x25,
//            UP = 0x26,
//            RIGHT = 0x27,
//            DOWN = 0x28,
//            SELECT = 0x29,
//            PRint = 0x2A,
//            EXECUTE = 0x2B,
//            SNAPSHOT = 0x2C,
//            INSERT = 0x2D,
//            DELETE = 0x2E,
//            HELP = 0x2F,

//            /*
//             * 0 - 9 are the same as ASCII '0' - '9' (0x30 - 0x39)
//             * 0x40 : unassigned
//             * A - Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
//             */

//            LWIN = 0x5B,
//            RWIN = 0x5C,
//            APPS = 0x5D,

//            /*
//             * 0x5E : reserved
//             */

//            SLEEP = 0x5F,

//            NUMPAD0 = 0x60,
//            NUMPAD1 = 0x61,
//            NUMPAD2 = 0x62,
//            NUMPAD3 = 0x63,
//            NUMPAD4 = 0x64,
//            NUMPAD5 = 0x65,
//            NUMPAD6 = 0x66,
//            NUMPAD7 = 0x67,
//            NUMPAD8 = 0x68,
//            NUMPAD9 = 0x69,
//            MULTIPLY = 0x6A,
//            ADD = 0x6B,
//            SEPARATOR = 0x6C,
//            SUBTRACT = 0x6D,
//            DECIMAL = 0x6E,
//            DIVIDE = 0x6F,
//            F1 = 0x70,
//            F2 = 0x71,
//            F3 = 0x72,
//            F4 = 0x73,
//            F5 = 0x74,
//            F6 = 0x75,
//            F7 = 0x76,
//            F8 = 0x77,
//            F9 = 0x78,
//            F10 = 0x79,
//            F11 = 0x7A,
//            F12 = 0x7B,
//            F13 = 0x7C,
//            F14 = 0x7D,
//            F15 = 0x7E,
//            F16 = 0x7F,
//            F17 = 0x80,
//            F18 = 0x81,
//            F19 = 0x82,
//            F20 = 0x83,
//            F21 = 0x84,
//            F22 = 0x85,
//            F23 = 0x86,
//            F24 = 0x87,

//            /*
//             * 0x88 - 0x8F : unassigned
//             */

//            NUMLOCK = 0x90,
//            SCROLL = 0x91,

//            /*
//             * NEC PC-9800 kbd definitions
//             */
//            OEM_NEC_EQUAL = 0x92,  // '=' key on numpad

//            /*
//             * Fujitsu/OASYS kbd definitions
//             */
//            OEM_FJ_JISHO = 0x92,  // 'Dictionary' key
//            OEM_FJ_MASSHOU = 0x93,  // 'Unregister word' key
//            OEM_FJ_TOUROKU = 0x94,  // 'Register word' key
//            OEM_FJ_LOYA = 0x95,  // 'Left OYAYUBI' key
//            OEM_FJ_ROYA = 0x96,  // 'Right OYAYUBI' key

//            /*
//             * 0x97 - 0x9F : unassigned
//             */

//            /*
//             * L* & R* - left and right Alt, Ctrl and Shift virtual keys.
//             * Used only as parameters to GetAsyncKeyState() and GetKeyState().
//             * No other API or message will distinguish left and right keys in this way.
//             */
//            LSHIFT = 0xA0,
//            RSHIFT = 0xA1,
//            LCONTROL = 0xA2,
//            RCONTROL = 0xA3,
//            LMENU = 0xA4,
//            RMENU = 0xA5,

//            BROWSER_BACK = 0xA6,
//            BROWSER_FORWARD = 0xA7,
//            BROWSER_REFRESH = 0xA8,
//            BROWSER_STOP = 0xA9,
//            BROWSER_SEARCH = 0xAA,
//            BROWSER_FAVORITES = 0xAB,
//            BROWSER_HOME = 0xAC,

//            VOLUME_MUTE = 0xAD,
//            VOLUME_DOWN = 0xAE,
//            VOLUME_UP = 0xAF,
//            MEDIA_NEXT_TRACK = 0xB0,
//            MEDIA_PREV_TRACK = 0xB1,
//            MEDIA_STOP = 0xB2,
//            MEDIA_PLAY_PAUSE = 0xB3,
//            LAUNCH_MAIL = 0xB4,
//            LAUNCH_MEDIA_SELECT = 0xB5,
//            LAUNCH_APP1 = 0xB6,
//            LAUNCH_APP2 = 0xB7,

//            /*
//             * 0xB8 - 0xB9 : reserved
//             */

//            OEM_1 = 0xBA,   // ';:' for US
//            OEM_PLUS = 0xBB,   // '+' any country
//            OEM_COMMA = 0xBC,   // ',' any country
//            OEM_MINUS = 0xBD,   // '-' any country
//            OEM_PERIOD = 0xBE,   // '.' any country
//            OEM_2 = 0xBF,   // '/?' for US
//            OEM_3 = 0xC0,   // '`~' for US

//            /*
//             * 0xC1 - 0xD7 : reserved
//             */

//            /*
//             * 0xD8 - 0xDA : unassigned
//             */

//            OEM_4 = 0xDB,  //  '[{' for US
//            OEM_5 = 0xDC,  //  '\|' for US
//            OEM_6 = 0xDD,  //  ']}' for US
//            OEM_7 = 0xDE,  //  ''"' for US
//            OEM_8 = 0xDF,

//            /*
//             * 0xE0 : reserved
//             */

//            /*
//             * Various extended or enhanced keyboards
//             */
//            OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
//            OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
//            ICO_HELP = 0xE3,  //  Help key on ICO
//            ICO_00 = 0xE4,  //  00 key on ICO

//            PROCESSKEY = 0xE5,

//            ICO_CLEAR = 0xE6,


//            PACKET = 0xE7,

//            /*
//             * 0xE8 : unassigned
//             */

//            /*
//             * Nokia/Ericsson definitions
//             */
//            OEM_RESET = 0xE9,
//            OEM_JUMP = 0xEA,
//            OEM_PA1 = 0xEB,
//            OEM_PA2 = 0xEC,
//            OEM_PA3 = 0xED,
//            OEM_WSCTRL = 0xEE,
//            OEM_CUSEL = 0xEF,
//            OEM_ATTN = 0xF0,
//            OEM_FINISH = 0xF1,
//            OEM_COPY = 0xF2,
//            OEM_AUTO = 0xF3,
//            OEM_ENLW = 0xF4,
//            OEM_BACKTAB = 0xF5,

//            ATTN = 0xF6,
//            CRSEL = 0xF7,
//            EXSEL = 0xF8,
//            EREOF = 0xF9,
//            PLAY = 0xFA,
//            ZOOM = 0xFB,
//            NONAME = 0xFC,
//            PA1 = 0xFD,
//            OEM_CLEAR = 0xFE,

//            Last
//        }

//        internal enum GetRawInputDataEnum
//        {
//            INPUT = 0x10000003,
//            HEADER = 0x10000005
//        }

//        internal enum GetWindowLongOffsets : int
//        {
//            WNDPROC = (-4),
//            IntPtr = (-6),
//            IntPtrPARENT = (-8),
//            STYLE = (-16),
//            EXSTYLE = (-20),
//            USERDATA = (-21),
//            ID = (-12),
//        }

//        [Flags]
//        internal enum SetWindowPosFlags : int
//        {
//            /// <summary>
//            /// Retains the current size (ignores the cx and cy parameters).
//            /// </summary>
//            NOSIZE = 0x0001,
//            /// <summary>
//            /// Retains the current position (ignores the x and y parameters).
//            /// </summary>
//            NOMOVE = 0x0002,
//            /// <summary>
//            /// Retains the current Z order (ignores the hwndInsertAfter parameter).
//            /// </summary>
//            NOZORDER = 0x0004,
//            /// <summary>
//            /// Does not redraw changes. If this flag is set, no repainting of any kind occurs.
//            /// This applies to the client area, the nonclient area (including the title bar and scroll bars),
//            /// and any part of the parent window uncovered as a result of the window being moved.
//            /// When this flag is set, the application must explicitly invalidate or redraw any parts
//            /// of the window and parent window that need redrawing.
//            /// </summary>
//            NOREDRAW = 0x0008,
//            /// <summary>
//            /// Does not activate the window. If this flag is not set,
//            /// the window is activated and moved to the top of either the topmost or non-topmost group
//            /// (depending on the setting of the hwndInsertAfter member).
//            /// </summary>
//            NOACTIVATE = 0x0010,
//            /// <summary>
//            /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
//            /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
//            /// </summary>
//            FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
//            /// <summary>
//            /// Displays the window.
//            /// </summary>
//            SHOWWINDOW = 0x0040,
//            /// <summary>
//            /// Hides the window.
//            /// </summary>
//            HIDEWINDOW = 0x0080,
//            /// <summary>
//            /// Discards the entire contents of the client area. If this flag is not specified,
//            /// the valid contents of the client area are saved and copied back into the client area 
//            /// after the window is sized or repositioned.
//            /// </summary>
//            NOCOPYBITS = 0x0100,
//            /// <summary>
//            /// Does not change the owner window's position in the Z order.
//            /// </summary>
//            NOOWNERZORDER = 0x0200, /* Don't do owner Z ordering */
//            /// <summary>
//            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
//            /// </summary>
//            NOSENDCHANGING = 0x0400, /* Don't send WM_WINDOWPOSCHANGING */

//            /// <summary>
//            /// Draws a frame (defined in the window's class description) around the window.
//            /// </summary>
//            DRAWFRAME = FRAMECHANGED,
//            /// <summary>
//            /// Same as the NOOWNERZORDER flag.
//            /// </summary>
//            NOREPOSITION = NOOWNERZORDER,

//            DEFERERASE = 0x2000,
//            ASYNCWINDOWPOS = 0x4000
//        }

//        internal enum WindowMessage : int
//        {
//            NULL = 0x0000,
//            CREATE = 0x0001,
//            DESTROY = 0x0002,
//            MOVE = 0x0003,
//            SIZE = 0x0005,
//            ACTIVATE = 0x0006,
//            SETFOCUS = 0x0007,
//            KILLFOCUS = 0x0008,
//            //              internal const uint SETVISIBLE           = 0x0009;
//            ENABLE = 0x000A,
//            SETREDRAW = 0x000B,
//            SETTEXT = 0x000C,
//            GETTEXT = 0x000D,
//            GETTEXTLENGTH = 0x000E,
//            PAint = 0x000F,
//            CLOSE = 0x0010,
//            QUERYENDSESSION = 0x0011,
//            QUIT = 0x0012,
//            QUERYOPEN = 0x0013,
//            ERASEBKGND = 0x0014,
//            SYSCOLORCHANGE = 0x0015,
//            ENDSESSION = 0x0016,
//            //              internal const uint SYSTEMERROR          = 0x0017;
//            SHOWWINDOW = 0x0018,
//            CTLCOLOR = 0x0019,
//            WININICHANGE = 0x001A,
//            SETTINGCHANGE = 0x001A,
//            DEVMODECHANGE = 0x001B,
//            ACTIVATEAPP = 0x001C,
//            FONTCHANGE = 0x001D,
//            TIMECHANGE = 0x001E,
//            CANCELMODE = 0x001F,
//            SETCURSOR = 0x0020,
//            MOUSEACTIVATE = 0x0021,
//            CHILDACTIVATE = 0x0022,
//            QUEUESYNC = 0x0023,
//            GETMINMAXINFO = 0x0024,
//            PAintICON = 0x0026,
//            ICONERASEBKGND = 0x0027,
//            NEXTDLGCTL = 0x0028,
//            //              internal const uint ALTTABACTIVE         = 0x0029;
//            SPOOLERSTATUS = 0x002A,
//            DRAWITEM = 0x002B,
//            MEASUREITEM = 0x002C,
//            DELETEITEM = 0x002D,
//            VKEYTOITEM = 0x002E,
//            CHARTOITEM = 0x002F,
//            SETFONT = 0x0030,
//            GETFONT = 0x0031,
//            SETHOTKEY = 0x0032,
//            GETHOTKEY = 0x0033,
//            //              internal const uint FILESYSCHANGE        = 0x0034;
//            //              internal const uint ISACTIVEICON         = 0x0035;
//            //              internal const uint QUERYPARKICON        = 0x0036;
//            QUERYDRAGICON = 0x0037,
//            COMPAREITEM = 0x0039,
//            //              internal const uint TESTING              = 0x003a;
//            //              internal const uint OTHERWINDOWCREATED = 0x003c;
//            GETOBJECT = 0x003D,
//            //                      internal const uint ACTIVATESHELLWINDOW        = 0x003e;
//            COMPACTING = 0x0041,
//            COMMNOTIFY = 0x0044,
//            WINDOWPOSCHANGING = 0x0046,
//            WINDOWPOSCHANGED = 0x0047,
//            POWER = 0x0048,
//            COPYDATA = 0x004A,
//            CANCELJOURNAL = 0x004B,
//            NOTIFY = 0x004E,
//            INPUTLANGCHANGEREQUEST = 0x0050,
//            INPUTLANGCHANGE = 0x0051,
//            TCARD = 0x0052,
//            HELP = 0x0053,
//            USERCHANGED = 0x0054,
//            NOTIFYFORMAT = 0x0055,
//            CONTEXTMENU = 0x007B,
//            STYLECHANGING = 0x007C,
//            STYLECHANGED = 0x007D,
//            DISPLAYCHANGE = 0x007E,
//            GETICON = 0x007F,

//            // Non-Client messages
//            SETICON = 0x0080,
//            NCCREATE = 0x0081,
//            NCDESTROY = 0x0082,
//            NCCALCSIZE = 0x0083,
//            NCHITTEST = 0x0084,
//            NCPAint = 0x0085,
//            NCACTIVATE = 0x0086,
//            GETDLGCODE = 0x0087,
//            SYNCPAint = 0x0088,
//            //              internal const uint SYNCTASK       = 0x0089;
//            NCMOUSEMOVE = 0x00A0,
//            NCLBUTTONDOWN = 0x00A1,
//            NCLBUTTONUP = 0x00A2,
//            NCLBUTTONDBLCLK = 0x00A3,
//            NCRBUTTONDOWN = 0x00A4,
//            NCRBUTTONUP = 0x00A5,
//            NCRBUTTONDBLCLK = 0x00A6,
//            NCMBUTTONDOWN = 0x00A7,
//            NCMBUTTONUP = 0x00A8,
//            NCMBUTTONDBLCLK = 0x00A9,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            NCXBUTTONDOWN = 0x00ab,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            NCXBUTTONUP = 0x00ac,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            NCXBUTTONDBLCLK = 0x00ad,

//            INPUT = 0x00FF,

//            KEYDOWN = 0x0100,
//            KEYFIRST = 0x0100,
//            KEYUP = 0x0101,
//            CHAR = 0x0102,
//            DEADCHAR = 0x0103,
//            SYSKEYDOWN = 0x0104,
//            SYSKEYUP = 0x0105,
//            SYSCHAR = 0x0106,
//            SYSDEADCHAR = 0x0107,
//            KEYLAST = 0x0108,
//            IME_STARTCOMPOSITION = 0x010D,
//            IME_ENDCOMPOSITION = 0x010E,
//            IME_COMPOSITION = 0x010F,
//            IME_KEYLAST = 0x010F,
//            INITDIALOG = 0x0110,
//            COMMAND = 0x0111,
//            SYSCOMMAND = 0x0112,
//            TIMER = 0x0113,
//            HSCROLL = 0x0114,
//            VSCROLL = 0x0115,
//            INITMENU = 0x0116,
//            INITMENUPOPUP = 0x0117,
//            //              internal const uint SYSTIMER       = 0x0118;
//            MENUSELECT = 0x011F,
//            MENUCHAR = 0x0120,
//            ENTERIDLE = 0x0121,
//            MENURBUTTONUP = 0x0122,
//            MENUDRAG = 0x0123,
//            MENUGETOBJECT = 0x0124,
//            UNINITMENUPOPUP = 0x0125,
//            MENUCOMMAND = 0x0126,

//            CHANGEUISTATE = 0x0127,
//            UPDATEUISTATE = 0x0128,
//            QUERYUISTATE = 0x0129,

//            //              internal const uint LBTRACKPOint     = 0x0131;
//            CTLCOLORMSGBOX = 0x0132,
//            CTLCOLOREDIT = 0x0133,
//            CTLCOLORLISTBOX = 0x0134,
//            CTLCOLORBTN = 0x0135,
//            CTLCOLORDLG = 0x0136,
//            CTLCOLORSCROLLBAR = 0x0137,
//            CTLCOLORSTATIC = 0x0138,
//            MOUSEMOVE = 0x0200,
//            MOUSEFIRST = 0x0200,
//            LBUTTONDOWN = 0x0201,
//            LBUTTONUP = 0x0202,
//            LBUTTONDBLCLK = 0x0203,
//            RBUTTONDOWN = 0x0204,
//            RBUTTONUP = 0x0205,
//            RBUTTONDBLCLK = 0x0206,
//            MBUTTONDOWN = 0x0207,
//            MBUTTONUP = 0x0208,
//            MBUTTONDBLCLK = 0x0209,
//            MOUSEWHEEL = 0x020A,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            XBUTTONDOWN = 0x020B,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            XBUTTONUP = 0x020C,
//            /// <summary>
//            /// Windows 2000 and higher only.
//            /// </summary>
//            XBUTTONDBLCLK = 0x020D,
//            /// <summary>
//            /// Windows Vista and higher only.
//            /// </summary>
//            MOUSEHWHEEL = 0x020E,
//            PARENTNOTIFY = 0x0210,
//            ENTERMENULOOP = 0x0211,
//            EXITMENULOOP = 0x0212,
//            NEXTMENU = 0x0213,
//            SIZING = 0x0214,
//            CAPTURECHANGED = 0x0215,
//            MOVING = 0x0216,
//            //              internal const uint POWERBROADCAST   = 0x0218;
//            DEVICECHANGE = 0x0219,
//            MDICREATE = 0x0220,
//            MDIDESTROY = 0x0221,
//            MDIACTIVATE = 0x0222,
//            MDIRESTORE = 0x0223,
//            MDINEXT = 0x0224,
//            MDIMAXIMIZE = 0x0225,
//            MDITILE = 0x0226,
//            MDICASCADE = 0x0227,
//            MDIICONARRANGE = 0x0228,
//            MDIGETACTIVE = 0x0229,
//            /* D&D messages */
//            //              internal const uint DROPOBJECT     = 0x022A;
//            //              internal const uint QUERYDROPOBJECT  = 0x022B;
//            //              internal const uint BEGINDRAG      = 0x022C;
//            //              internal const uint DRAGLOOP       = 0x022D;
//            //              internal const uint DRAGSELECT     = 0x022E;
//            //              internal const uint DRAGMOVE       = 0x022F;
//            MDISETMENU = 0x0230,
//            ENTERSIZEMOVE = 0x0231,
//            EXITSIZEMOVE = 0x0232,
//            DROPFILES = 0x0233,
//            MDIREFRESHMENU = 0x0234,
//            IME_SETCONTEXT = 0x0281,
//            IME_NOTIFY = 0x0282,
//            IME_CONTROL = 0x0283,
//            IME_COMPOSITIONFULL = 0x0284,
//            IME_SELECT = 0x0285,
//            IME_CHAR = 0x0286,
//            IME_REQUEST = 0x0288,
//            IME_KEYDOWN = 0x0290,
//            IME_KEYUP = 0x0291,
//            NCMOUSEHOVER = 0x02A0,
//            MOUSEHOVER = 0x02A1,
//            NCMOUSELEAVE = 0x02A2,
//            MOUSELEAVE = 0x02A3,
//            CUT = 0x0300,
//            COPY = 0x0301,
//            PASTE = 0x0302,
//            CLEAR = 0x0303,
//            UNDO = 0x0304,
//            RENDERFORMAT = 0x0305,
//            RENDERALLFORMATS = 0x0306,
//            DESTROYCLIPBOARD = 0x0307,
//            DRAWCLIPBOARD = 0x0308,
//            PAintCLIPBOARD = 0x0309,
//            VSCROLLCLIPBOARD = 0x030A,
//            SIZECLIPBOARD = 0x030B,
//            ASKCBFORMATNAME = 0x030C,
//            CHANGECBCHAIN = 0x030D,
//            HSCROLLCLIPBOARD = 0x030E,
//            QUERYNEWPALETTE = 0x030F,
//            PALETTEISCHANGING = 0x0310,
//            PALETTECHANGED = 0x0311,
//            HOTKEY = 0x0312,
//            PRint = 0x0317,
//            PRintCLIENT = 0x0318,
//            HANDHELDFIRST = 0x0358,
//            HANDHELDLAST = 0x035F,
//            AFXFIRST = 0x0360,
//            AFXLAST = 0x037F,
//            PENWINFIRST = 0x0380,
//            PENWINLAST = 0x038F,
//            APP = 0x8000,
//            USER = 0x0400,

//            // Our "private" ones
//            MOUSE_ENTER = 0x0401,
//            ASYNC_MESSAGE = 0x0403,
//            REFLECT = USER + 0x1c00,
//            CLOSE_intERNAL = USER + 0x1c01,

//            // NotifyIcon (Systray) Balloon messages 
//            BALLOONSHOW = USER + 0x0002,
//            BALLOONHIDE = USER + 0x0003,
//            BALLOONTIMEOUT = USER + 0x0004,
//            BALLOONUSERCLICK = USER + 0x0005
//        }

//        [Flags]
//        internal enum RawMouseFlags : ushort
//        {
//            /// <summary>
//            /// LastX/Y indicate relative motion.
//            /// </summary>
//            MOUSE_MOVE_RELATIVE = 0x00,
//            /// <summary>
//            /// LastX/Y indicate absolute motion.
//            /// </summary>
//            MOUSE_MOVE_ABSOLUTE = 0x01,
//            /// <summary>
//            /// The coordinates are mapped to the virtual desktop.
//            /// </summary>
//            MOUSE_VIRTUAL_DESKTOP = 0x02,
//            /// <summary>
//            /// Requery for mouse attributes.
//            /// </summary>
//            MOUSE_ATTRIBUTES_CHANGED = 0x04,
//        }

//        [Flags]
//        internal enum RawInputMouseState : ushort
//        {
//            LEFT_BUTTON_DOWN = 0x0001,  // Left Button changed to down.
//            LEFT_BUTTON_UP = 0x0002,  // Left Button changed to up.
//            RIGHT_BUTTON_DOWN = 0x0004,  // Right Button changed to down.
//            RIGHT_BUTTON_UP = 0x0008,  // Right Button changed to up.
//            MIDDLE_BUTTON_DOWN = 0x0010,  // Middle Button changed to down.
//            MIDDLE_BUTTON_UP = 0x0020,  // Middle Button changed to up.

//            BUTTON_1_DOWN = LEFT_BUTTON_DOWN,
//            BUTTON_1_UP = LEFT_BUTTON_UP,
//            BUTTON_2_DOWN = RIGHT_BUTTON_DOWN,
//            BUTTON_2_UP = RIGHT_BUTTON_UP,
//            BUTTON_3_DOWN = MIDDLE_BUTTON_DOWN,
//            BUTTON_3_UP = MIDDLE_BUTTON_UP,

//            BUTTON_4_DOWN = 0x0040,
//            BUTTON_4_UP = 0x0080,
//            BUTTON_5_DOWN = 0x0100,
//            BUTTON_5_UP = 0x0200,

//            WHEEL = 0x0400,
//            HWHEEL = 0x0800,
//        }

//        internal enum RawInputDeviceType : int
//        {
//            MOUSE = 0,
//            KEYBOARD = 1,
//            HID = 2
//        }

//        internal enum RawInputDeviceInfoEnum
//        {
//            PREPARSEDDATA = 0x20000005,
//            DEVICENAME = 0x20000007,  // the return valus is the character length, not the byte size
//            DEVICEINFO = 0x2000000b
//        }

//        [Flags]
//        public enum RawInputDeviceFlags : int
//        {
//            /// <summary>
//            /// If set, this removes the top level collection from the inclusion list.
//            /// This tells the operating system to stop reading from a device which matches the top level collection.
//            /// </summary>
//            REMOVE = 0x00000001,
//            /// <summary>
//            /// If set, this specifies the top level collections to exclude when reading a complete usage page.
//            /// This flag only affects a TLC whose usage page is already specified with RawInputDeviceEnum.PAGEONLY. 
//            /// </summary>
//            EXCLUDE = 0x00000010,
//            /// <summary>
//            /// If set, this specifies all devices whose top level collection is from the specified UsagePage.
//            /// Note that usUsage must be zero. To exclude a particular top level collection, use EXCLUDE.
//            /// </summary>
//            PAGEONLY = 0x00000020,
//            /// <summary>
//            /// If set, this prevents any devices specified by UsagePage or Usage from generating legacy messages.
//            /// This is only for the mouse and keyboard. See RawInputDevice Remarks.
//            /// </summary>
//            NOLEGACY = 0x00000030,
//            /// <summary>
//            /// If set, this enables the caller to receive the input even when the caller is not in the foreground.
//            /// Note that Target must be specified in RawInputDevice.
//            /// </summary>
//            INPUTSINK = 0x00000100,
//            /// <summary>
//            /// If set, the mouse button click does not activate the other window.
//            /// </summary>
//            CAPTUREMOUSE = 0x00000200, // effective when mouse nolegacy is specified, otherwise it would be an error
//            /// <summary>
//            /// If set, the application-defined keyboard device hotkeys are not handled.
//            /// However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled.
//            /// By default, all keyboard hotkeys are handled.
//            /// NOHOTKEYS can be specified even if NOLEGACY is not specified and Target is NULL in RawInputDevice.
//            /// </summary>
//            NOHOTKEYS = 0x00000200, // effective for keyboard.
//            /// <summary>
//            /// Microsoft Windows XP Service Pack 1 (SP1): If set, the application command keys are handled. APPKEYS can be specified only if NOLEGACY is specified for a keyboard device.
//            /// </summary>
//            APPKEYS = 0x00000400, // effective for keyboard.
//            /// <summary>
//            /// If set, this enables the caller to receive input in the background only if the foreground application
//            /// does not process it. In other words, if the foreground application is not registered for raw input,
//            /// then the background application that is registered will receive the input.
//            /// </summary>
//            EXINPUTSINK = 0x00001000,
//            DEVNOTIFY = 0x00002000,
//            //EXMODEMASK      = 0x000000F0
//        }

//        public enum CursorName : int
//        {
//            Arrow = 32512
//        }

//        public enum HIDUsageSim : ushort
//        {
//            FlightSimulationDevice = 0x01, // Application Collection 
//            AutomobileSimulationDevice = 0x02, //             Application Collection 
//            TankSimulationDevice = 0x03, //             Application Collection 
//            SpaceshipSimulationDevice = 0x04, //             Application Collection 
//            SubmarineSimulationDevice = 0x05, //             Application Collection 
//            SailingSimulationDevice = 0x06, //             Application Collection 
//            MotorcycleSimulationDevice = 0x07, //             Application Collection 
//            SportsSimulationDevice = 0x08, //             Application Collection 
//            AirplaneSimulationDevice = 0x09, //             Application Collection 
//            HelicopterSimulationDevice = 0x0A, //             Application Collection 
//            MagicCarpetSimulationDevice = 0x0B, //             Application Collection 
//            BicycleSimulationDevice = 0x0C, //             Application Collection 
//            // 0x0D - 0x1F Reserved 
//            FlightControlStick = 0x20, //             Application Collection 
//            FlightStick = 0x21, //             Application Collection 
//            CyclicControl = 0x22, //             Physical Collection 
//            CyclicTrim = 0x23, //             Physical Collection 
//            FlightYoke = 0x24, //             Application Collection 
//            TrackControl = 0x25, //             Physical Collection 
//            // 0x26 - 0xAF Reserved 
//            Aileron = 0xB0, //             Dynamic Value 
//            AileronTrim = 0xB1, //             Dynamic Value 
//            AntiTorqueControl = 0xB2, //             Dynamic Value 
//            AutopilotEnable = 0xB3, //             On/Off Control 
//            ChaffRelease = 0xB4, //             One-Shot Control 
//            CollectiveControl = 0xB5, //             Dynamic Value 
//            DiveBrake = 0xB6, //             Dynamic Value 
//            ElectronicCountermeasures = 0xB7, //             On/Off Control 
//            Elevator = 0xB8, //             Dynamic Value 
//            ElevatorTrim = 0xB9, //             Dynamic Value 
//            Rudder = 0xBA, //             Dynamic Value 
//            Throttle = 0xBB, //             Dynamic Value 
//            FlightCommunications = 0xBC, //             On/Off Control 
//            FlareRelease = 0xBD, //             One-Shot Control 
//            LandingGear = 0xBE, //             On/Off Control 
//            ToeBrake = 0xBF, //             Dynamic Value 
//            Trigger = 0xC0, //             Momentary Control 
//            WeaponsArm = 0xC1, //             On/Off Control 
//            Weapons = 0xC2, //             Selector 
//            WingFlaps = 0xC3, //             Dynamic Value 
//            Accelerator = 0xC4, //             Dynamic Value 
//            Brake = 0xC5, //             Dynamic Value 
//            Clutch = 0xC6, //             Dynamic Value 
//            Shifter = 0xC7, //             Dynamic Value 
//            Steering = 0xC8, //             Dynamic Value 
//            TurretDirection = 0xC9, //             Dynamic Value 
//            BarrelElevation = 0xCA, //             Dynamic Value 
//            DivePlane = 0xCB, //             Dynamic Value 
//            Ballast = 0xCC, //             Dynamic Value 
//            BicycleCrank = 0xCD, //             Dynamic Value 
//            HandleBars = 0xCE, //             Dynamic Value 
//            FrontBrake = 0xCF, //             Dynamic Value 
//            RearBrake = 0xD0, //             Dynamic Value 
//            // 0xD1 - 0xFFFF Reserved 
//            Reserved = 0xFFFF
//        }

//        public enum HIDUsageGD : ushort
//        {
//            Pointer = 0x01, // Physical Collection 
//            Mouse = 0x02, // Application Collection 
//            // 0x03 Reserved 
//            Joystick = 0x04, // Application Collection 
//            GamePad = 0x05, // Application Collection 
//            Keyboard = 0x06, // Application Collection 
//            Keypad = 0x07, // Application Collection 
//            MultiAxisController = 0x08, // Application Collection 
//            // 0x09 - 0x2F Reserved 
//            X = 0x30, // Dynamic Value 
//            Y = 0x31, // Dynamic Value 
//            Z = 0x32, // Dynamic Value 
//            Rx = 0x33, // Dynamic Value 
//            Ry = 0x34, // Dynamic Value 
//            Rz = 0x35, // Dynamic Value 
//            Slider = 0x36, // Dynamic Value 
//            Dial = 0x37, // Dynamic Value 
//            Wheel = 0x38, // Dynamic Value 
//            Hatswitch = 0x39, // Dynamic Value 
//            CountedBuffer = 0x3A, // Logical Collection 
//            ByteCount = 0x3B, // Dynamic Value 
//            MotionWakeup = 0x3C, // One-Shot Control 
//            Start = 0x3D, // On/Off Control 
//            Select = 0x3E, // On/Off Control 
//            // 0x3F Reserved 
//            Vx = 0x40, // Dynamic Value 
//            Vy = 0x41, // Dynamic Value 
//            Vz = 0x42, // Dynamic Value 
//            Vbrx = 0x43, // Dynamic Value 
//            Vbry = 0x44, // Dynamic Value 
//            Vbrz = 0x45, // Dynamic Value 
//            Vno = 0x46, // Dynamic Value 
//            // 0x47 - 0x7F Reserved 
//            SystemControl = 0x80, // Application Collection 
//            SystemPowerDown = 0x81, // One-Shot Control 
//            SystemSleep = 0x82, // One-Shot Control 
//            SystemWakeUp = 0x83, // One-Shot Control 
//            SystemContextMenu = 0x84, // One-Shot Control 
//            SystemMainMenu = 0x85, // One-Shot Control 
//            SystemAppMenu = 0x86, // One-Shot Control 
//            SystemMenuHelp = 0x87, // One-Shot Control 
//            SystemMenuExit = 0x88, // One-Shot Control 
//            SystemMenu = 0x89, // Selector 
//            SystemMenuRight = 0x8A, // Re-Trigger Control 
//            SystemMenuLeft = 0x8B, // Re-Trigger Control 
//            SystemMenuUp = 0x8C, // Re-Trigger Control 
//            SystemMenuDown = 0x8D, // Re-Trigger Control 
//            // 0x8E - 0x8F Reserved 
//            DPadUp = 0x90, // On/Off Control 
//            DPadDown = 0x91, // On/Off Control 
//            DPadRight = 0x92, // On/Off Control 
//            DPadLeft = 0x93, // On/Off Control 
//            // 0x94 - 0xFFFF Reserved 
//            Reserved = 0xFFFF
//        }

//        public enum HIDPage : ushort
//        {
//            Undefined = 0x00,
//            GenericDesktop = 0x01,
//            Simulation = 0x02,
//            VR = 0x03,
//            Sport = 0x04,
//            Game = 0x05,
//            // Reserved 0x06 
//            KeyboardOrKeypad = 0x07, // USB Device Class Definition for Human Interface Devices (HID). Note: the usage type for all key codes is Selector (Sel). 
//            LEDs = 0x08,
//            Button = 0x09,
//            Ordinal = 0x0A,
//            Telephony = 0x0B,
//            Consumer = 0x0C,
//            Digitizer = 0x0D,
//            // Reserved 0x0E 
//            PID = 0x0F, // USB Physical Interface Device definitions for force feedback and related devices. 
//            Unicode = 0x10,
//            // Reserved 0x11 - 0x13 
//            AlphanumericDisplay = 0x14,
//            // Reserved 0x15 - 0x7F 
//            // Monitor 0x80 - 0x83   USB Device Class Definition for Monitor Devices 
//            // Power 0x84 - 0x87     USB Device Class Definition for Power Devices 
//            PowerDevice = 0x84,                // Power Device Page 
//            BatterySystem = 0x85,              // Battery System Page 
//            // Reserved 0x88 - 0x8B 
//            BarCodeScanner = 0x8C, // (Point of Sale) USB Device Class Definition for Bar Code Scanner Devices 
//            WeighingDevice = 0x8D, // (Point of Sale) USB Device Class Definition for Weighing Devices 
//            Scale = 0x8D, // (Point of Sale) USB Device Class Definition for Scale Devices 
//            MagneticStripeReader = 0x8E,
//            // ReservedPointofSalepages 0x8F 
//            CameraControl = 0x90, // USB Device Class Definition for Image Class Devices 
//            Arcade = 0x91, // OAAF Definitions for arcade and coinop related Devices 
//            // Reserved 0x92 - 0xFEFF 
//            // VendorDefined 0xFF00 - 0xFFFF 
//            VendorDefinedStart = 0xFF00
//        }

//        public enum HatPosition : byte
//        {
//            /// <summary>
//            /// The hat is in its centered (neutral) position
//            /// </summary>
//            Centered = 0,
//            /// <summary>
//            /// The hat is in its top position.
//            /// </summary>
//            Up,
//            /// <summary>
//            /// The hat is in its top-right position.
//            /// </summary>
//            UpRight,
//            /// <summary>
//            /// The hat is in its right position.
//            /// </summary>
//            Right,
//            /// <summary>
//            /// The hat is in its bottom-right position.
//            /// </summary>
//            DownRight,
//            /// <summary>
//            /// The hat is in its bottom position.
//            /// </summary>
//            Down,
//            /// <summary>
//            /// The hat is in its bottom-left position.
//            /// </summary>
//            DownLeft,
//            /// <summary>
//            /// The hat is in its left position.
//            /// </summary>
//            Left,
//            /// <summary>
//            /// The hat is in its top-left position.
//            /// </summary>
//            UpLeft,
//        }

//        public enum JoystickHat
//        {
//            /// <summary>
//            /// The first hat of the Joystick device.
//            /// </summary>
//            Hat0,
//            /// <summary>
//            /// The second hat of the Joystick device.
//            /// </summary>
//            Hat1,
//            /// <summary>
//            /// The third hat of the Joystick device.
//            /// </summary>
//            Hat2,
//            /// <summary>
//            /// The fourth hat of the Joystick device.
//            /// </summary>
//            Hat3,
//            /// <summary>
//            /// The last hat of the Joystick device.
//            /// </summary>
//            Last = Hat3
//        }

//        public enum JoystickButton
//        {
//            /// <summary>The first button of the JoystickDevice.</summary>
//            Button0 = 0,
//            /// <summary>The second button of the JoystickDevice.</summary>
//            Button1,
//            /// <summary>The third button of the JoystickDevice.</summary>
//            Button2,
//            /// <summary>The fourth button of the JoystickDevice.</summary>
//            Button3,
//            /// <summary>The fifth button of the JoystickDevice.</summary>
//            Button4,
//            /// <summary>The sixth button of the JoystickDevice.</summary>
//            Button5,
//            /// <summary>The seventh button of the JoystickDevice.</summary>
//            Button6,
//            /// <summary>The eighth button of the JoystickDevice.</summary>
//            Button7,
//            /// <summary>The ninth button of the JoystickDevice.</summary>
//            Button8,
//            /// <summary>The tenth button of the JoystickDevice.</summary>
//            Button9,
//            /// <summary>The eleventh button of the JoystickDevice.</summary>
//            Button10,
//            /// <summary>The twelfth button of the JoystickDevice.</summary>
//            Button11,
//            /// <summary>The thirteenth button of the JoystickDevice.</summary>
//            Button12,
//            /// <summary>The fourteenth button of the JoystickDevice.</summary>
//            Button13,
//            /// <summary>The fifteenth button of the JoystickDevice.</summary>
//            Button14,
//            /// <summary>The sixteenth button of the JoystickDevice.</summary>
//            Button15,
//            /// <summary>The seventeenth button of the JoystickDevice.</summary>
//            Button16,
//            /// <summary>The eighteenth button of the JoystickDevice.</summary>
//            Button17,
//            /// <summary>The nineteenth button of the JoystickDevice.</summary>
//            Button18,
//            /// <summary>The twentieth button of the JoystickDevice.</summary>
//            Button19,
//            /// <summary>The twentyfirst button of the JoystickDevice.</summary>
//            Button20,
//            /// <summary>The twentysecond button of the JoystickDevice.</summary>
//            Button21,
//            /// <summary>The twentythird button of the JoystickDevice.</summary>
//            Button22,
//            /// <summary>The twentyfourth button of the JoystickDevice.</summary>
//            Button23,
//            /// <summary>The twentyfifth button of the JoystickDevice.</summary>
//            Button24,
//            /// <summary>The twentysixth button of the JoystickDevice.</summary>
//            Button25,
//            /// <summary>The twentyseventh button of the JoystickDevice.</summary>
//            Button26,
//            /// <summary>The twentyeighth button of the JoystickDevice.</summary>
//            Button27,
//            /// <summary>The twentynineth button of the JoystickDevice.</summary>
//            Button28,
//            /// <summary>The thirtieth button of the JoystickDevice.</summary>
//            Button29,
//            /// <summary>The thirtyfirst button of the JoystickDevice.</summary>
//            Button30,
//            /// <summary>The thirtysecond button of the JoystickDevice.</summary>
//            Button31,
//            /// <summary>The last supported button of the JoystickDevice.</summary>
//            Last = Button31,
//        }

//        public enum JoystickAxis
//        {
//            /// <summary>The first axis of the JoystickDevice.</summary>
//            Axis0 = 0,
//            /// <summary>The second axis of the JoystickDevice.</summary>
//            Axis1,
//            /// <summary>The third axis of the JoystickDevice.</summary>
//            Axis2,
//            /// <summary>The fourth axis of the JoystickDevice.</summary>
//            Axis3,
//            /// <summary>The fifth axis of the JoystickDevice.</summary>
//            Axis4,
//            /// <summary>The sixth axis of the JoystickDevice.</summary>
//            Axis5,
//            /// <summary>The seventh axis of the JoystickDevice.</summary>
//            Axis6,
//            /// <summary>The eighth axis of the JoystickDevice.</summary>
//            Axis7,
//            /// <summary>The ninth axis of the JoystickDevice.</summary>
//            Axis8,
//            /// <summary>The tenth axis of the JoystickDevice.</summary>
//            Axis9,
//            /// <summary>The eleventh axis of the JoystickDevice.</summary>
//            Axis10,
//            /// <summary>The highest supported axis of the JoystickDevice.</summary>
//            Last = Axis10,
//        }

//        enum ConfigurationType
//        {
//            Unmapped = 0,
//            Axis,
//            Button,
//            Hat
//        }

//        public enum GamePadType
//        {
//            Unknown = 0,
//            ArcadeStick,
//            DancePad,
//            FlightStick,
//            Guitar,
//            Wheel,
//            AlternateGuitar,
//            BigButtonPad,
//            DrumKit,
//            GamePad,
//            ArcadePad,
//            BassGuitar,
//        }

//        [Flags]
//        internal enum GamePadAxes : byte
//        {
//            LeftX = 1 << 0,
//            LeftY = 1 << 1,
//            LeftTrigger = 1 << 2,
//            RightX = 1 << 3,
//            RightY = 1 << 4,
//            RightTrigger = 1 << 5,
//        }

//        public enum Buttons
//        {
//            DPadUp,
//            DPadDown,
//            DPadLeft,
//            DPadRight,

//            Start,
//            Back,

//            LeftStick,
//            RightStick,

//            LeftShoulder,
//            RightShoulder,

//            Home,

//            A,
//            B,
//            X,
//            Y,

//            RightTrigger,
//            LeftTrigger,

//            RightThumbstickUp,
//            RightThumbstickDown,
//            RightThumbstickRight,
//            RightThumbstickLeft,

//            LeftThumbstickUp,
//            LeftThumbstickDown,
//            LeftThumbstickRight,
//            LeftThumbstickLeft,
//        }

//        #endregion

//        #region Struct

//        struct BroadcastDeviceInterface
//        {
//            public Int32 Size;
//            public DeviceBroadcastType DeviceType;
//            Int32 dbcc_reserved;
//            public Guid ClassGuid;
//            public char dbcc_name;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawInputDeviceList
//        {
//            /// <summary>
//            /// Handle to the raw input device.
//            /// </summary>
//            internal IntPtr Device;
//            /// <summary>
//            /// Type of device.
//            /// </summary>
//            internal RawInputDeviceType Type;

//            public override string ToString()
//            {
//                return String.Format("{0}, Handle: {1}", Type, Device);
//            }
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct POint
//        {
//            internal int X;
//            internal int Y;

//            internal POint(int x, int y)
//            {
//                this.X = x;
//                this.Y = y;
//            }

//            public override string ToString()
//            {
//                return "Point {" + X.ToString() + ", " + Y.ToString() + ")";
//            }
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        public struct HidProtocolCaps
//        {
//            public short Usage;
//            public short UsagePage;
//            public ushort InputReportByteLength;
//            public ushort OutputReportByteLength;
//            public ushort FeatureReportByteLength;
//            unsafe fixed ushort Reserved[17];
//            public ushort NumberLinkCollectionNodes;
//            public ushort NumberInputButtonCaps;
//            public ushort NumberInputValueCaps;
//            public ushort NumberInputDataIndices;
//            public ushort NumberOutputButtonCaps;
//            public ushort NumberOutputValueCaps;
//            public ushort NumberOutputDataIndices;
//            public ushort NumberFeatureButtonCaps;
//            public ushort NumberFeatureValueCaps;
//            public ushort NumberFeatureDataIndices;
//        }

//        [StructLayout(LayoutKind.Explicit)]
//        public struct HidProtocolData
//        {
//            [FieldOffset(0)]
//            public short DataIndex;
//            //[FieldOffset(2)] public short Reserved;
//            [FieldOffset(4)]
//            public int RawValue;
//            [FieldOffset(4), MarshalAs(UnmanagedType.U1)]
//            public bool On;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        public struct HidProtocolNotRange
//        {
//#pragma warning disable 169 // private field is never used
//            public short Usage;
//            short Reserved1;
//            public short StringIndex;
//            short Reserved2;
//            public short DesignatorIndex;
//            short Reserved3;
//            public short DataIndex;
//            short Reserved4;
//#pragma warning restore 169
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawHID
//        {
//            /// <summary>
//            /// Size, in bytes, of each HID input in bRawData.
//            /// </summary>
//            internal Int32 Size;
//            /// <summary>
//            /// Number of HID inputs in bRawData.
//            /// </summary>
//            internal Int32 Count;
//            /// <summary>
//            /// Raw input data as an array of bytes.
//            /// </summary>
//            internal byte RawData;

//            internal byte this[int index]
//            {
//                get
//                {
//                    if (index < 0 || index > Size * Count)
//                        throw new ArgumentOutOfRangeException("index");
//                    unsafe
//                    {
//                        fixed (byte* data = &RawData)
//                        {
//                            return *(data + index);
//                        }
//                    }
//                }
//            }
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawKeyboard
//        {
//            /// <summary>
//            /// Scan code from the key depression. The scan code for keyboard overrun is KEYBOARD_OVERRUN_MAKE_CODE.
//            /// </summary>
//            //internal USHORT MakeCode;
//            internal Int16 MakeCode;
//            /// <summary>
//            /// Flags for scan code information. It can be one or more of the following.
//            /// RI_KEY_MAKE
//            /// RI_KEY_BREAK
//            /// RI_KEY_E0
//            /// RI_KEY_E1
//            /// RI_KEY_TERMSRV_SET_LED
//            /// RI_KEY_TERMSRV_SHADOW
//            /// </summary>
//            internal RawInputKeyboardDataFlags Flags;
//            /// <summary>
//            /// Reserved; must be zero.
//            /// </summary>
//            UInt16 Reserved;
//            /// <summary>
//            /// Microsoft Windows message compatible virtual-key code. For more information, see Virtual-Key Codes.
//            /// </summary>
//            //internal USHORT VKey;
//            internal VirtualKeys VKey;
//            /// <summary>
//            /// Corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth.
//            /// </summary>
//            //internal UInt32 Message;
//            internal int Message;
//            /// <summary>
//            /// Device-specific additional information for the event.
//            /// </summary>
//            //internal UInt32 ExtraInformation;
//            internal int ExtraInformation;
//        }

//        [StructLayout(LayoutKind.Explicit)]
//        internal struct RawMouse
//        {
//            /// <summary>
//            /// Mouse state. This member can be any reasonable combination of the following. 
//            /// MOUSE_ATTRIBUTES_CHANGED
//            /// Mouse attributes changed; application needs to query the mouse attributes.
//            /// MOUSE_MOVE_RELATIVE
//            /// Mouse movement data is relative to the last mouse position.
//            /// MOUSE_MOVE_ABSOLUTE
//            /// Mouse movement data is based on absolute position.
//            /// MOUSE_VIRTUAL_DESKTOP
//            /// Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).
//            /// </summary>
//            [FieldOffset(0)]
//            public RawMouseFlags Flags;  // USHORT in winuser.h, but only int works -- USHORT returns 0.

//            [FieldOffset(4)]
//            public RawInputMouseState ButtonFlags;

//            /// <summary>
//            /// If usButtonFlags is RI_MOUSE_WHEEL, this member is a signed value that specifies the wheel delta.
//            /// </summary>
//            [FieldOffset(6)]
//            public UInt16 ButtonData;

//            /// <summary>
//            /// Raw state of the mouse buttons.
//            /// </summary>
//            [FieldOffset(8)]
//            public UInt32 RawButtons;

//            /// <summary>
//            /// Motion in the X direction. This is signed relative motion or absolute motion, depending on the value of usFlags.
//            /// </summary>
//            [FieldOffset(12)]
//            public Int32 LastX;

//            /// <summary>
//            /// Motion in the Y direction. This is signed relative motion or absolute motion, depending on the value of usFlags.
//            /// </summary>
//            [FieldOffset(16)]
//            public Int32 LastY;

//            /// <summary>
//            /// Device-specific additional information for the event.
//            /// </summary>
//            [FieldOffset(20)]
//            public UInt32 ExtraInformation;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawInputHIDDeviceInfo
//        {
//            /// <summary>
//            /// Vendor ID for the HID.
//            /// </summary>
//            internal Int32 VendorId;
//            /// <summary>
//            /// Product ID for the HID.
//            /// </summary>
//            internal Int32 ProductId;
//            /// <summary>
//            /// Version number for the HID.
//            /// </summary>
//            internal Int32 VersionNumber;
//            /// <summary>
//            /// Top-level collection Usage Page for the device.
//            /// </summary>
//            //internal USHORT UsagePage;
//            internal Int16 UsagePage;
//            /// <summary>
//            /// Top-level collection Usage for the device.
//            /// </summary>
//            //internal USHORT Usage;
//            internal Int16 Usage;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawInputKeyboardDeviceInfo
//        {
//            /// <summary>
//            /// Type of the keyboard.
//            /// </summary>
//            internal Int32 Type;
//            /// <summary>
//            /// Subtype of the keyboard.
//            /// </summary>
//            internal Int32 SubType;
//            /// <summary>
//            /// Scan code mode.
//            /// </summary>
//            internal Int32 KeyboardMode;
//            /// <summary>
//            /// Number of function keys on the keyboard.
//            /// </summary>
//            internal Int32 NumberOfFunctionKeys;
//            /// <summary>
//            /// Number of LED indicators on the keyboard.
//            /// </summary>
//            internal Int32 NumberOfIndicators;
//            /// <summary>
//            /// Total number of keys on the keyboard.
//            /// </summary>
//            internal Int32 NumberOfKeysTotal;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawInputMouseDeviceInfo
//        {
//            /// <summary>
//            /// ID for the mouse device.
//            /// </summary>
//            internal Int32 Id;
//            /// <summary>
//            /// Number of buttons for the mouse.
//            /// </summary>
//            internal Int32 NumberOfButtons;
//            /// <summary>
//            /// Number of data points per second. This information may not be applicable for every mouse device.
//            /// </summary>
//            internal Int32 SampleRate;
//            /// <summary>
//            /// TRUE if the mouse has a wheel for horizontal scrolling; otherwise, FALSE.
//            /// </summary>
//            /// <remarks>
//            /// This member is only supported under Microsoft Windows Vista and later versions.
//            /// </remarks>
//            internal bool HasHorizontalWheel;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        public struct RawInputHeader
//        {
//            /// <summary>
//            /// Type of raw input.
//            /// </summary>
//            internal RawInputDeviceType Type;
//            /// <summary>
//            /// Size, in bytes, of the entire input packet of data. This includes the RawInput struct plus possible extra input reports in the RAWHID variable length array.
//            /// </summary>
//            internal Int32 Size;
//            /// <summary>
//            /// Handle to the device generating the raw input data.
//            /// </summary>
//            internal IntPtr Device;
//            /// <summary>
//            /// Value passed in the wParam parameter of the WM_INPUT message.
//            /// </summary>
//            internal IntPtr Param;

//            public static readonly int SizeInBytes =
//                BlittableValueType<RawInputHeader>.Stride;
//        }

//        [StructLayout(LayoutKind.Explicit)]
//        public struct RawInputData
//        {
//            [FieldOffset(0)]
//            internal RawMouse Mouse;
//            [FieldOffset(0)]
//            internal RawKeyboard Keyboard;
//            [FieldOffset(0)]
//            internal RawHID HID;

//            public static readonly int SizeInBytes =
//                BlittableValueType<RawInputData>.Stride;
//        }

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct RawInput
//        {
//            public RawInputHeader Header;
//            public RawInputData Data;

//            public static readonly int SizeInBytes =
//                BlittableValueType<RawInput>.Stride;
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal struct RawInputDevice
//        {
//            /// <summary>
//            /// Top level collection Usage page for the raw input device.
//            /// </summary>
//            internal HIDPage UsagePage;
//            /// <summary>
//            /// Top level collection Usage for the raw input device.
//            /// </summary>
//            //internal USHORT Usage;
//            internal Int16 Usage;
//            /// <summary>
//            /// Mode flag that specifies how to interpret the information provided by UsagePage and Usage.
//            /// It can be zero (the default) or one of the following values.
//            /// By default, the operating system sends raw input from devices with the specified top level collection (TLC)
//            /// to the registered application as long as it has the window focus. 
//            /// </summary>
//            internal RawInputDeviceFlags Flags;
//            /// <summary>
//            /// Handle to the target window. If NULL it follows the keyboard focus.
//            /// </summary>
//            internal IntPtr Target;

//            public RawInputDevice(HIDUsageGD usage, RawInputDeviceFlags flags, IntPtr target)
//            {
//                UsagePage = HIDPage.GenericDesktop;
//                Usage = (short)usage;
//                Flags = flags;
//                Target = target;
//            }

//            public RawInputDevice(HIDUsageSim usage, RawInputDeviceFlags flags, IntPtr target)
//            {
//                UsagePage = HIDPage.Simulation;
//                Usage = (short)usage;
//                Flags = flags;
//                Target = target;
//            }

//            public override string ToString()
//            {
//                return String.Format("{0}/{1}, flags: {2}, window: {3}", UsagePage, Usage, Flags, Target);
//            }
//        }

//        [StructLayout(LayoutKind.Explicit)]
//        public struct HidProtocolButtonCaps
//        {
//            [FieldOffset(0)]
//            public HIDPage UsagePage;
//            [FieldOffset(2)]
//            public byte ReportID;
//            [FieldOffset(3), MarshalAs(UnmanagedType.U1)]
//            public bool IsAlias;
//            [FieldOffset(4)]
//            public short BitField;
//            [FieldOffset(6)]
//            public short LinkCollection;
//            [FieldOffset(8)]
//            public short LinkUsage;
//            [FieldOffset(10)]
//            public short LinkUsagePage;
//            [FieldOffset(12), MarshalAs(UnmanagedType.U1)]
//            public bool IsRange;
//            [FieldOffset(13), MarshalAs(UnmanagedType.U1)]
//            public bool IsStringRange;
//            [FieldOffset(14), MarshalAs(UnmanagedType.U1)]
//            public bool IsDesignatorRange;
//            [FieldOffset(15), MarshalAs(UnmanagedType.U1)]
//            public bool IsAbsolute;
//            //[FieldOffset(16)] unsafe fixed int Reserved[10]; // no need when LayoutKind.Explicit
//            [FieldOffset(56)]
//            public HidProtocolRange Range;
//            [FieldOffset(56)]
//            public HidProtocolNotRange NotRange;
//        }

//        public struct HidProtocolRange
//        {
//#pragma warning disable 0649
//            public short UsageMin;
//            public short UsageMax;
//            public short StringMin;
//            public short StringMax;
//            public short DesignatorMin;
//            public short DesignatorMax;
//            public short DataIndexMin;
//            public short DataIndexMax;
//#pragma warning restore 0649
//        }

//        [StructLayout(LayoutKind.Explicit)]
//        public struct HidProtocolValueCaps
//        {
//#pragma warning disable 169 // private field is never used
//            [FieldOffset(0)]
//            public HIDPage UsagePage;
//            [FieldOffset(2)]
//            public byte ReportID;
//            [FieldOffset(3), MarshalAs(UnmanagedType.U1)]
//            public bool IsAlias;
//            [FieldOffset(4)]
//            public ushort BitField;
//            [FieldOffset(6)]
//            public short LinkCollection;
//            [FieldOffset(8)]
//            public ushort LinkUsage;
//            [FieldOffset(10)]
//            public ushort LinkUsagePage;
//            [FieldOffset(12), MarshalAs(UnmanagedType.U1)]
//            public bool IsRange;
//            [FieldOffset(13), MarshalAs(UnmanagedType.U1)]
//            public bool IsStringRange;
//            [FieldOffset(14), MarshalAs(UnmanagedType.U1)]
//            public bool IsDesignatorRange;
//            [FieldOffset(15), MarshalAs(UnmanagedType.U1)]
//            public bool IsAbsolute;
//            [FieldOffset(16), MarshalAs(UnmanagedType.U1)]
//            public bool HasNull;
//            [FieldOffset(17)]
//            byte Reserved;
//            [FieldOffset(18)]
//            public short BitSize;
//            [FieldOffset(20)]
//            public short ReportCount;
//            //[FieldOffset(22)] ushort Reserved2a;
//            //[FieldOffset(24)] ushort Reserved2b;
//            //[FieldOffset(26)] ushort Reserved2c;
//            //[FieldOffset(28)] ushort Reserved2d;
//            //[FieldOffset(30)] ushort Reserved2e;
//            [FieldOffset(32)]
//            public int UnitsExp;
//            [FieldOffset(36)]
//            public int Units;
//            [FieldOffset(40)]
//            public int LogicalMin;
//            [FieldOffset(44)]
//            public int LogicalMax;
//            [FieldOffset(48)]
//            public int PhysicalMin;
//            [FieldOffset(52)]
//            public int PhysicalMax;
//            [FieldOffset(56)]
//            public HidProtocolRange Range;
//            [FieldOffset(56)]
//            public HidProtocolNotRange NotRange;
//#pragma warning restore 169
//        }

//        public struct JoystickCapabilities : IEquatable<JoystickCapabilities>
//        {
//            byte axis_count;
//            byte button_count;
//            byte hat_count;
//            bool is_connected;

//            #region Constructors

//            internal JoystickCapabilities(int axis_count, int button_count, int hat_count, bool is_connected)
//            {
//                if (axis_count < 0 || axis_count > JoystickState.MaxAxes)
//                    System.Diagnostics.Debug.Print("[{0}] Axis count {1} out of range (0, {2})",
//                        typeof(JoystickCapabilities).Name, axis_count, JoystickState.MaxAxes);
//                if (button_count < 0 || button_count > JoystickState.MaxButtons)
//                    System.Diagnostics.Debug.Print("[{0}] Button count {1} out of range (0, {2})",
//                        typeof(JoystickCapabilities).Name, button_count, JoystickState.MaxButtons);
//                if (hat_count < 0 || hat_count > JoystickState.MaxHats)
//                    System.Diagnostics.Debug.Print("[{0}] Hat count {1} out of range (0, {2})",
//                        typeof(JoystickCapabilities).Name, hat_count, JoystickState.MaxHats);

//                axis_count = Compute<int>.Clamp(axis_count, 0, JoystickState.MaxAxes);
//                button_count = Compute<int>.Clamp(button_count, 0, JoystickState.MaxButtons);
//                hat_count = Compute<int>.Clamp(hat_count, 0, JoystickState.MaxHats);

//                this.axis_count = (byte)axis_count;
//                this.button_count = (byte)button_count;
//                this.hat_count = (byte)hat_count;
//                this.is_connected = is_connected;
//            }

//            #endregion

//            #region Internal Members

//            internal void SetIsConnected(bool value)
//            {
//                is_connected = value;
//            }

//            #endregion

//            #region Public Members

//            /// <summary>
//            /// Gets the number of axes supported by this <see cref="JoystickDevice"/>.
//            /// </summary>
//            public int AxisCount
//            {
//                get { return axis_count; }
//            }

//            /// <summary>
//            /// Gets the number of buttons supported by this <see cref="JoystickDevice"/>.
//            /// </summary>
//            public int ButtonCount
//            {
//                get { return button_count; }
//            }

//            /// <summary>
//            /// Gets the number of hats supported by this <see cref="JoystickDevice"/>.
//            /// </summary>
//            public int HatCount
//            {
//                get { return hat_count; }
//            }

//            /// <summary>
//            /// Gets a value indicating whether this <see cref="JoystickDevice"/> is connected.
//            /// </summary>
//            /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
//            public bool IsConnected
//            {
//                get { return is_connected; }
//                private set { is_connected = value; }
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickCapabilities"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickCapabilities"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{Axes: {0}; Buttons: {1}; Hats: {2}; IsConnected: {3}}}",
//                    AxisCount, ButtonCount, HatCount, IsConnected);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.JoystickCapabilities"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return
//                    AxisCount.GetHashCode() ^
//                    ButtonCount.GetHashCode() ^
//                    HatCount.GetHashCode() ^
//                    IsConnected.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.JoystickCapabilities"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.JoystickCapabilities"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickCapabilities"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is JoystickCapabilities &&
//                    Equals((JoystickCapabilities)obj);
//            }

//            #endregion

//            #region IEquatable<JoystickCapabilities> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.JoystickCapabilities"/> is equal to the current <see cref="OpenTK.Input.JoystickCapabilities"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.JoystickCapabilities"/> to compare with the current <see cref="OpenTK.Input.JoystickCapabilities"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.JoystickCapabilities"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickCapabilities"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(JoystickCapabilities other)
//            {
//                return
//                    AxisCount == other.AxisCount &&
//                    ButtonCount == other.ButtonCount &&
//                    HatCount == other.HatCount &&
//                    IsConnected == other.IsConnected;
//            }

//            #endregion
//        }

//        [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
//        internal struct MSG
//        {
//            internal IntPtr HWnd;
//            internal Theta.Input.GamePad.WindowMessage Message;
//            internal IntPtr WParam;
//            internal IntPtr LParam;
//            internal uint Time;
//            internal Theta.Input.GamePad.POint Point;
//            //internal object RefObject;

//            public override string ToString()
//            {
//                return String.Format("msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} pt=0x{5:x}", (int)Message, Message.ToString(), HWnd.ToInt32(), WParam.ToInt32(), LParam.ToInt32(), Point);
//            }
//        }

//        #endregion

//        #region Classes

//        public class NativeWindow : INativeWindow
//        {
//            #region --- Fields ---

//            private readonly GameWindowFlags options;

//            private readonly DisplayDevice device;

//            private readonly INativeWindow implementation;

//            private bool disposed, events;
//            private bool cursor_visible = true;
//            private bool previous_cursor_visible = true;

//            /// <summary>
//            /// System.Threading.Thread.CurrentThread.ManagedThreadId of the thread that created this <see cref="OpenTK.NativeWindow"/>.
//            /// </summary>
//            private int thread_id;
//            #endregion

//            #region --- Contructors ---

//            /// <summary>Constructs a new NativeWindow with default attributes without enabling events.</summary>
//            public NativeWindow()
//                : this(640, 480, "OpenTK Native Window", GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.Default) { }

//            // TODO: Remaining constructors.

//            /// <summary>Constructs a new centered NativeWindow with the specified attributes.</summary>
//            /// <param name="width">The width of the NativeWindow in pixels.</param>
//            /// <param name="height">The height of the NativeWindow in pixels.</param>
//            /// <param name="title">The title of the NativeWindow.</param>
//            /// <param name="options">GameWindow options specifying window appearance and behavior.</param>
//            /// <param name="mode">The OpenTK.Graphics.GraphicsMode of the NativeWindow.</param>
//            /// <param name="device">The OpenTK.Graphics.DisplayDevice to construct the NativeWindow in.</param>
//            /// <exception cref="System.ArgumentOutOfRangeException">If width or height is less than 1.</exception>
//            /// <exception cref="System.ArgumentNullException">If mode or device is null.</exception>
//            public NativeWindow(int width, int height, string title, GameWindowFlags options, GraphicsMode mode, DisplayDevice device)
//                : this(device != null ? device.Bounds.Left + (device.Bounds.Width - width) / 2 : 0,
//                       device != null ? device.Bounds.Top + (device.Bounds.Height - height) / 2 : 0,
//                       width, height, title, options, mode, device) { }

//            /// <summary>Constructs a new NativeWindow with the specified attributes.</summary>
//            /// <param name="x">Horizontal screen space coordinate of the NativeWindow's origin.</param>
//            /// <param name="y">Vertical screen space coordinate of the NativeWindow's origin.</param>
//            /// <param name="width">The width of the NativeWindow in pixels.</param>
//            /// <param name="height">The height of the NativeWindow in pixels.</param>
//            /// <param name="title">The title of the NativeWindow.</param>
//            /// <param name="options">GameWindow options specifying window appearance and behavior.</param>
//            /// <param name="mode">The OpenTK.Graphics.GraphicsMode of the NativeWindow.</param>
//            /// <param name="device">The OpenTK.Graphics.DisplayDevice to construct the NativeWindow in.</param>
//            /// <exception cref="System.ArgumentOutOfRangeException">If width or height is less than 1.</exception>
//            /// <exception cref="System.ArgumentNullException">If mode or device is null.</exception>
//            public NativeWindow(int x, int y, int width, int height, string title, GameWindowFlags options, GraphicsMode mode, DisplayDevice device)
//            {
//                // TODO: Should a constraint be added for the position?
//                if (width < 1)
//                    throw new ArgumentOutOfRangeException("width", "Must be greater than zero.");
//                if (height < 1)
//                    throw new ArgumentOutOfRangeException("height", "Must be greater than zero.");
//                if (mode == null)
//                    throw new ArgumentNullException("mode");

//                this.options = options;
//                this.device = device;

//                this.thread_id = System.Threading.Thread.CurrentThread.ManagedThreadId;

//                IPlatformFactory factory = Factory.Default;
//                implementation = factory.CreateNativeWindow(x, y, width, height, title, mode, options, this.device);
//                factory.RegisterResource(this);

//                if ((options & GameWindowFlags.Fullscreen) != 0)
//                {
//                    if (this.device != null)
//                    {
//                        this.device.ChangeResolution(width, height, mode.ColorFormat.BitsPerPixel, 0);
//                    }
//                    WindowState = WindowState.Fullscreen;
//                }

//                if ((options & GameWindowFlags.FixedWindow) != 0)
//                {
//                    WindowBorder = WindowBorder.Fixed;
//                }
//            }

//            #endregion

//            #region --- INativeWindow Members ---

//            #region Methods

//            #region Close

//            /// <summary>
//            /// Closes the NativeWindow.
//            /// </summary>
//            public void Close()
//            {
//                EnsureUndisposed();
//                implementation.Close();
//            }

//            #endregion

//            #region PointToClient

//            /// <summary>
//            /// Transforms the specified point from screen to client coordinates. 
//            /// </summary>
//            /// <param name="point">
//            /// A <see cref="System.Drawing.Point"/> to transform.
//            /// </param>
//            /// <returns>
//            /// The point transformed to client coordinates.
//            /// </returns>
//            public Point PointToClient(Point point)
//            {
//                return implementation.PointToClient(point);
//            }

//            #endregion

//            #region PointToScreen

//            /// <summary>
//            /// Transforms the specified point from client to screen coordinates.
//            /// </summary>
//            /// <param name="point">
//            /// A <see cref="System.Drawing.Point"/> to transform.
//            /// </param>
//            /// <returns>
//            /// The point transformed to screen coordinates.
//            /// </returns>
//            public Point PointToScreen(Point point)
//            {
//                return implementation.PointToScreen(point);
//            }

//            #endregion

//            #region ProcessEvents

//            /// <summary>
//            /// Processes operating system events until the NativeWindow becomes idle.
//            /// </summary>
//            public void ProcessEvents()
//            {
//                ProcessEvents(false);
//            }

//            #endregion

//            #endregion

//            #region Properties

//            #region Bounds

//            /// <summary>
//            /// Gets or sets a <see cref="System.Drawing.Rectangle"/> structure
//            /// that specifies the external bounds of this window, in screen coordinates.
//            /// The coordinates are specified in device-independent points and
//            /// include the title bar, borders and drawing area of the window.
//            /// </summary>
//            public Rectangle Bounds
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Bounds;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Bounds = value;
//                }
//            }

//            #endregion

//            #region ClientRectangle

//            /// <summary>
//            /// Gets or sets a <see cref="System.Drawing.Rectangle"/> structure
//            /// that defines the bounds of the OpenGL surface, in window coordinates.
//            /// The coordinates are specified in device-dependent pixels.
//            /// </summary>
//            public Rectangle ClientRectangle
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.ClientRectangle;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.ClientRectangle = value;
//                }
//            }

//            #endregion

//            #region ClientSize

//            /// <summary>
//            /// Gets or sets a <see cref="System.Drawing.Size"/> structure
//            /// that defines the size of the OpenGL surface in window coordinates.
//            /// The coordinates are specified in device-dependent pixels.
//            /// </summary>
//            public Size ClientSize
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.ClientSize;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.ClientSize = value;
//                }
//            }

//            #endregion

//            #region Cursor

//            /// <summary>
//            /// Gets or sets the <see cref="OpenTK.MouseCursor"/> for this window.
//            /// </summary>
//            public MouseCursor Cursor
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Cursor;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    if (value == null)
//                    {
//                        value = MouseCursor.Empty;
//                    }
//                    implementation.Cursor = value;
//                }
//            }

//            #endregion

//            #region Exists

//            /// <summary>
//            /// Gets a value indicating whether a render window exists.
//            /// </summary>
//            public bool Exists
//            {
//                get
//                {
//                    return IsDisposed ? false : implementation.Exists; // TODO: Should disposed be ignored instead?
//                }
//            }

//            #endregion

//            #region Focused

//            /// <summary>
//            /// Gets a System.Boolean that indicates whether this NativeWindow has input focus.
//            /// </summary>
//            public bool Focused
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Focused;
//                }
//            }

//            #endregion

//            #region Height

//            /// <summary>
//            /// Gets or sets the height of the OpenGL surface in window coordinates.
//            /// The coordinates are specified in device-dependent pixels.
//            /// </summary>
//            public int Height
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Height;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Height = value;
//                }
//            }

//            #endregion

//            #region Icon

//            /// <summary>
//            /// Gets or sets the System.Drawing.Icon for this GameWindow.
//            /// </summary>
//            public Icon Icon
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Icon;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Icon = value;
//                }
//            }

//            #endregion

//            #region InputDriver

//            /// <summary>
//            /// This property is deprecated.
//            /// </summary>
//            [Obsolete]
//            public IInputDriver InputDriver
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.InputDriver;
//                }
//            }

//            #endregion

//            #region Location

//            /// <summary>
//            /// Gets or sets a <see cref="System.Drawing.Point"/> structure that contains the location of this window on the desktop.
//            /// </summary>
//            public Point Location
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Location;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Location = value;
//                }
//            }

//            #endregion

//            #region Size

//            /// <summary>
//            /// Gets or sets a <see cref="System.Drawing.Size"/> structure that contains the external size of this window.
//            /// </summary>
//            public Size Size
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Size;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Size = value;
//                }
//            }

//            #endregion

//            #region Title

//            /// <summary>
//            /// Gets or sets the NativeWindow title.
//            /// </summary>
//            public string Title
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Title;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Title = value;
//                }
//            }

//            #endregion

//            #region Visible

//            /// <summary>
//            /// Gets or sets a System.Boolean that indicates whether this NativeWindow is visible.
//            /// </summary>
//            public bool Visible
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Visible;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Visible = value;
//                }
//            }

//            #endregion

//            #region Width

//            /// <summary>
//            /// Gets or sets the width of the OpenGL surface in window coordinates.
//            /// The coordinates are specified in device-dependent pixels.
//            /// </summary>
//            public int Width
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Width;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Width = value;
//                }
//            }

//            #endregion

//            #region WindowBorder

//            /// <summary>
//            /// Gets or sets the border of the NativeWindow.
//            /// </summary>
//            public WindowBorder WindowBorder
//            {
//                get
//                {
//                    return implementation.WindowBorder;
//                }
//                set
//                {
//                    implementation.WindowBorder = value;
//                }
//            }

//            #endregion

//            #region WindowInfo

//            /// <summary>
//            /// Gets the <see cref="OpenTK.Platform.IWindowInfo"/> of this window.
//            /// </summary>
//            public IWindowInfo WindowInfo
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.WindowInfo;
//                }
//            }

//            #endregion

//            #region WindowState

//            /// <summary>
//            /// Gets or sets the state of the NativeWindow.
//            /// </summary>
//            public virtual WindowState WindowState
//            {
//                get
//                {
//                    return implementation.WindowState;
//                }
//                set
//                {
//                    implementation.WindowState = value;
//                }
//            }

//            #endregion

//            #region X

//            /// <summary>
//            /// Gets or sets the horizontal location of this window in screen coordinates.
//            /// The coordinates are specified in device-independent points.
//            /// </summary>
//            public int X
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.X;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.X = value;
//                }
//            }

//            #endregion

//            #region Y

//            /// <summary>
//            /// Gets or sets the vertical location of this window in screen coordinates.
//            /// The coordinates are specified in device-independent points.
//            /// </summary>
//            public int Y
//            {
//                get
//                {
//                    EnsureUndisposed();
//                    return implementation.Y;
//                }
//                set
//                {
//                    EnsureUndisposed();
//                    implementation.Y = value;
//                }
//            }

//            #endregion

//            #region CursorVisible

//            /// <summary>
//            /// Gets or sets a value indicating whether the mouse cursor is visible.
//            /// </summary>
//            public bool CursorVisible
//            {
//                get { return cursor_visible; }
//                set
//                {
//                    cursor_visible = value;
//                    implementation.CursorVisible = value;
//                }
//            }

//            #endregion

//            #endregion

//            #region Events

//            /// <summary>
//            /// Occurs after the window has closed.
//            /// </summary>
//            public event EventHandler<EventArgs> Closed = delegate { };

//            /// <summary>
//            /// Occurs when the window is about to close.
//            /// </summary>
//            public event EventHandler<CancelEventArgs> Closing = delegate { };

//            /// <summary>
//            /// Occurs when the window is disposed.
//            /// </summary>
//            public event EventHandler<EventArgs> Disposed = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="Focused"/> property of the window changes.
//            /// </summary>
//            public event EventHandler<EventArgs> FocusedChanged = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="Icon"/> property of the window changes. 
//            /// </summary>
//            public event EventHandler<EventArgs> IconChanged = delegate { };

//            /// <summary>
//            /// Occurs whenever the window is moved.
//            /// </summary>
//            public event EventHandler<EventArgs> Move = delegate { };

//            /// <summary>
//            /// Occurs whenever the mouse cursor enters the window <see cref="Bounds"/>.
//            /// </summary>
//            public event EventHandler<EventArgs> MouseEnter = delegate { };

//            /// <summary>
//            /// Occurs whenever the mouse cursor leaves the window <see cref="Bounds"/>.
//            /// </summary>
//            public event EventHandler<EventArgs> MouseLeave = delegate { };

//            /// <summary>
//            /// Occurs whenever the window is resized.
//            /// </summary>
//            public event EventHandler<EventArgs> Resize = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="Title"/> property of the window changes.
//            /// </summary>
//            public event EventHandler<EventArgs> TitleChanged = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="Visible"/> property of the window changes.
//            /// </summary>
//            public event EventHandler<EventArgs> VisibleChanged = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="WindowBorder"/> property of the window changes.
//            /// </summary>
//            public event EventHandler<EventArgs> WindowBorderChanged = delegate { };

//            /// <summary>
//            /// Occurs when the <see cref="WindowState"/> property of the window changes.
//            /// </summary>
//            public event EventHandler<EventArgs> WindowStateChanged = delegate { };

//            #endregion

//            #endregion

//            #region --- IDisposable Members ---

//            #region Dispose

//            /// <summary>
//            /// Releases all non-managed resources belonging to this NativeWindow.
//            /// </summary>
//            public virtual void Dispose()
//            {
//                if (!IsDisposed)
//                {
//                    if ((options & GameWindowFlags.Fullscreen) != 0)
//                    {
//                        if (device != null)
//                        {
//                            device.RestoreResolution();
//                        }
//                    }
//                    implementation.Dispose();
//                    GC.SuppressFinalize(this);

//                    IsDisposed = true;
//                }
//            }

//            #endregion

//            #endregion

//            #region --- Protected Members ---

//            #region Methods

//            #region EnsureUndisposed

//            /// <summary>
//            /// Ensures that this NativeWindow has not been disposed.
//            /// </summary>
//            /// <exception cref="System.ObjectDisposedException">
//            /// If this NativeWindow has been disposed.
//            /// </exception>
//            protected void EnsureUndisposed()
//            {
//                if (IsDisposed) throw new ObjectDisposedException(GetType().Name);
//            }

//            #endregion

//            #region IsDisposed

//            /// <summary>
//            /// Gets or sets a <see cref="System.Boolean"/>, which indicates whether
//            /// this instance has been disposed.
//            /// </summary>
//            protected bool IsDisposed
//            {
//                get { return disposed; }
//                set { disposed = value; }
//            }

//            #endregion

//            #region OnClosed

//            /// <summary>
//            /// Called when the NativeWindow has closed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnClosed(EventArgs e)
//            {
//                Closed(this, e);
//            }

//            #endregion

//            #region OnClosing

//            /// <summary>
//            /// Called when the NativeWindow is about to close.
//            /// </summary>
//            /// <param name="e">
//            /// The <see cref="System.ComponentModel.CancelEventArgs" /> for this event.
//            /// Set e.Cancel to true in order to stop the NativeWindow from closing.</param>
//            protected virtual void OnClosing(CancelEventArgs e)
//            {
//                Closing(this, e);
//            }

//            #endregion

//            #region OnDisposed

//            /// <summary>
//            /// Called when the NativeWindow is disposed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnDisposed(EventArgs e)
//            {
//                Disposed(this, e);
//            }

//            #endregion

//            #region OnFocusedChanged

//            /// <summary>
//            /// Called when the <see cref="OpenTK.INativeWindow.Focused"/> property of the NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnFocusedChanged(EventArgs e)
//            {
//                if (!Focused)
//                {
//                    // Release cursor when losing focus, to ensure
//                    // IDEs continue working as expected.
//                    previous_cursor_visible = CursorVisible;
//                    CursorVisible = true;
//                }
//                else if (!previous_cursor_visible)
//                {
//                    // Make cursor invisible when focus is regained
//                    // if cursor was invisible on previous focus loss.
//                    previous_cursor_visible = true;
//                    CursorVisible = false;
//                }
//                FocusedChanged(this, e);
//            }

//            #endregion

//            #region OnIconChanged

//            /// <summary>
//            /// Called when the <see cref="OpenTK.INativeWindow.Icon"/> property of the NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnIconChanged(EventArgs e)
//            {
//                IconChanged(this, e);
//            }

//            #endregion

//            #region OnKeyDown

//            /// <summary>
//            /// Occurs whenever a keybord key is pressed.
//            /// </summary>
//            protected virtual void OnKeyDown(KeyboardKeyEventArgs e)
//            {
//                KeyDown(this, e);
//            }

//            #endregion

//            #region OnKeyPress

//            /// <summary>
//            /// Called when a character is typed.
//            /// </summary>
//            /// <param name="e">The <see cref="OpenTK.KeyPressEventArgs"/> for this event.</param>
//            protected virtual void OnKeyPress(KeyPressEventArgs e)
//            {
//                KeyPress(this, e);
//            }

//            #endregion

//            #region OnKeyUp

//            /// <summary>
//            /// Called when a keybord key is released.
//            /// </summary>
//            /// <param name="e">The <see cref="OpenTK.Input.KeyboardKeyEventArgs"/> for this event.</param>
//            protected virtual void OnKeyUp(KeyboardKeyEventArgs e)
//            {
//                KeyUp(this, e);
//            }

//            #endregion

//            #region OnMove

//            /// <summary>
//            /// Called when the NativeWindow is moved.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnMove(EventArgs e)
//            {
//                Move(this, e);
//            }

//            #endregion

//            #region OnMouseEnter

//            /// <summary>
//            /// Called whenever the mouse cursor reenters the window <see cref="Bounds"/>.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnMouseEnter(EventArgs e)
//            {
//                MouseEnter(this, e);
//            }

//            #endregion

//            #region OnMouseLeave

//            /// <summary>
//            /// Called whenever the mouse cursor leaves the window <see cref="Bounds"/>.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnMouseLeave(EventArgs e)
//            {
//                MouseLeave(this, e);
//            }

//            #endregion

//            /// <summary>
//            /// Raises the <see cref="MouseDown"/> event.
//            /// </summary>
//            /// <param name="e">
//            /// A <see cref="MouseButtonEventArgs"/> instance carrying mouse state information.
//            /// The information carried by this instance is only valid within this method body.
//            /// </param>
//            protected virtual void OnMouseDown(MouseButtonEventArgs e)
//            {
//                MouseDown(this, e);
//            }

//            /// <summary>
//            /// Raises the <see cref="MouseUp"/> event.
//            /// </summary>
//            /// <param name="e">
//            /// A <see cref="MouseButtonEventArgs"/> instance carrying mouse state information.
//            /// The information carried by this instance is only valid within this method body.
//            /// </param>
//            protected virtual void OnMouseUp(MouseButtonEventArgs e)
//            {
//                MouseUp(this, e);
//            }

//            /// <summary>
//            /// Raises the <see cref="MouseMove"/> event.
//            /// </summary>
//            /// <param name="e">
//            /// A <see cref="MouseMoveEventArgs"/> instance carrying mouse state information.
//            /// The information carried by this instance is only valid within this method body.
//            /// </param>
//            protected virtual void OnMouseMove(MouseMoveEventArgs e)
//            {
//                MouseMove(this, e);
//            }

//            /// <summary>
//            /// Raises the <see cref="MouseWheel"/> event.
//            /// </summary>
//            /// <param name="e">
//            /// A <see cref="MouseWheelEventArgs"/> instance carrying mouse state information.
//            /// The information carried by this instance is only valid within this method body.
//            /// </param>
//            protected virtual void OnMouseWheel(MouseWheelEventArgs e)
//            {
//                MouseWheel(this, e);
//            }

//            #region OnResize

//            /// <summary>
//            /// Called when the NativeWindow is resized.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnResize(EventArgs e)
//            {
//                Resize(this, e);
//            }

//            #endregion

//            #region OnTitleChanged

//            /// <summary>
//            /// Called when the <see cref="OpenTK.INativeWindow.Title"/> property of the NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnTitleChanged(EventArgs e)
//            {
//                TitleChanged(this, e);
//            }

//            #endregion

//            #region OnVisibleChanged

//            /// <summary>
//            /// Called when the <see cref="OpenTK.INativeWindow.Visible"/> property of the NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnVisibleChanged(EventArgs e)
//            {
//                VisibleChanged(this, e);
//            }

//            #endregion

//            #region OnWindowBorderChanged

//            /// <summary>
//            /// Called when the WindowBorder of this NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnWindowBorderChanged(EventArgs e)
//            {
//                WindowBorderChanged(this, e);
//            }

//            #endregion

//            #region OnWindowStateChanged

//            /// <summary>
//            /// Called when the WindowState of this NativeWindow has changed.
//            /// </summary>
//            /// <param name="e">Not used.</param>
//            protected virtual void OnWindowStateChanged(EventArgs e)
//            {
//                WindowStateChanged(this, e);
//            }

//            #endregion

//            #region ProcessEvents

//            /// <summary>
//            /// Processes operating system events until the NativeWindow becomes idle.
//            /// </summary>
//            /// <param name="retainEvents">If true, the state of underlying system event propagation will be preserved, otherwise event propagation will be enabled if it has not been already.</param>
//            protected void ProcessEvents(bool retainEvents)
//            {
//                EnsureUndisposed();
//                if (this.thread_id != System.Threading.Thread.CurrentThread.ManagedThreadId)
//                {
//                    throw new InvalidOperationException("ProcessEvents must be called on the same thread that created the window.");
//                }
//                if (!retainEvents && !events) Events = true;
//                implementation.ProcessEvents();
//            }

//            #endregion

//            #endregion

//            #endregion

//            #region --- Private Members ---

//            #region Methods

//            #region OnClosedInternal

//            private void OnClosedInternal(object sender, EventArgs e)
//            {
//                OnClosed(e);
//                Events = false;
//            }

//            #endregion

//            #region OnClosingInternal

//            private void OnClosingInternal(object sender, CancelEventArgs e) { OnClosing(e); }

//            #endregion

//            #region OnDisposedInternal

//            private void OnDisposedInternal(object sender, EventArgs e) { OnDisposed(e); }

//            #endregion

//            #region OnFocusedChangedInternal

//            private void OnFocusedChangedInternal(object sender, EventArgs e) { OnFocusedChanged(e); }

//            #endregion

//            #region OnIconChangedInternal

//            private void OnIconChangedInternal(object sender, EventArgs e) { OnIconChanged(e); }

//            #endregion

//            #region OnKeyDownInternal

//            private void OnKeyDownInternal(object sender, KeyboardKeyEventArgs e) { OnKeyDown(e); }

//            #endregion

//            #region OnKeyPressInternal

//            private void OnKeyPressInternal(object sender, KeyPressEventArgs e) { OnKeyPress(e); }

//            #endregion

//            #region OnKeyUpInternal

//            private void OnKeyUpInternal(object sender, KeyboardKeyEventArgs e) { OnKeyUp(e); }

//            #endregion

//            #region OnMouseEnterInternal

//            private void OnMouseEnterInternal(object sender, EventArgs e) { OnMouseEnter(e); }

//            #endregion

//            #region OnMouseLeaveInternal

//            private void OnMouseLeaveInternal(object sender, EventArgs e) { OnMouseLeave(e); }

//            #endregion

//            private void OnMouseDownInternal(object sender, MouseButtonEventArgs e) { OnMouseDown(e); }
//            private void OnMouseUpInternal(object sender, MouseButtonEventArgs e) { OnMouseUp(e); }
//            private void OnMouseMoveInternal(object sender, MouseMoveEventArgs e) { OnMouseMove(e); }
//            private void OnMouseWheelInternal(object sender, MouseWheelEventArgs e) { OnMouseWheel(e); }

//            #region OnMoveInternal

//            private void OnMoveInternal(object sender, EventArgs e) { OnMove(e); }

//            #endregion

//            #region OnResizeInternal

//            private void OnResizeInternal(object sender, EventArgs e) { OnResize(e); }

//            #endregion

//            #region OnTitleChangedInternal

//            private void OnTitleChangedInternal(object sender, EventArgs e) { OnTitleChanged(e); }

//            #endregion

//            #region OnVisibleChangedInternal

//            private void OnVisibleChangedInternal(object sender, EventArgs e) { OnVisibleChanged(e); }

//            #endregion

//            #region OnWindowBorderChangedInternal

//            private void OnWindowBorderChangedInternal(object sender, EventArgs e) { OnWindowBorderChanged(e); }

//            #endregion

//            #region OnWindowStateChangedInternal

//            private void OnWindowStateChangedInternal(object sender, EventArgs e) { OnWindowStateChanged(e); }

//            #endregion

//            #endregion

//            #region Properties

//            #region Events

//            private bool Events
//            {
//                set
//                {
//                    if (value)
//                    {
//                        if (events)
//                        {
//                            throw new InvalidOperationException("Event propagation is already enabled.");
//                        }
//                        implementation.Closed += OnClosedInternal;
//                        implementation.Closing += OnClosingInternal;
//                        implementation.Disposed += OnDisposedInternal;
//                        implementation.FocusedChanged += OnFocusedChangedInternal;
//                        implementation.IconChanged += OnIconChangedInternal;
//                        //implementation.KeyDown += OnKeyDownInternal;
//                        //implementation.KeyPress += OnKeyPressInternal;
//                        //implementation.KeyUp += OnKeyUpInternal;
//                        implementation.MouseEnter += OnMouseEnterInternal;
//                        implementation.MouseLeave += OnMouseLeaveInternal;
//                        //implementation.MouseDown += OnMouseDownInternal;
//                        //implementation.MouseUp += OnMouseUpInternal;
//                        //implementation.MouseMove += OnMouseMoveInternal;
//                        //implementation.MouseWheel += OnMouseWheelInternal;
//                        implementation.Move += OnMoveInternal;
//                        implementation.Resize += OnResizeInternal;
//                        implementation.TitleChanged += OnTitleChangedInternal;
//                        implementation.VisibleChanged += OnVisibleChangedInternal;
//                        implementation.WindowBorderChanged += OnWindowBorderChangedInternal;
//                        implementation.WindowStateChanged += OnWindowStateChangedInternal;
//                        events = true;
//                    }
//                    else if (events)
//                    {
//                        implementation.Closed -= OnClosedInternal;
//                        implementation.Closing -= OnClosingInternal;
//                        implementation.Disposed -= OnDisposedInternal;
//                        implementation.FocusedChanged -= OnFocusedChangedInternal;
//                        implementation.IconChanged -= OnIconChangedInternal;
//                        //implementation.KeyDown -= OnKeyDownInternal;
//                        //implementation.KeyPress -= OnKeyPressInternal;
//                        //implementation.KeyUp -= OnKeyUpInternal;
//                        implementation.MouseEnter -= OnMouseEnterInternal;
//                        implementation.MouseLeave -= OnMouseLeaveInternal;
//                        //implementation.MouseDown -= OnMouseDownInternal;
//                        //implementation.MouseUp -= OnMouseUpInternal;
//                        //implementation.MouseMove -= OnMouseMoveInternal;
//                        //implementation.MouseWheel -= OnMouseWheelInternal;
//                        implementation.Move -= OnMoveInternal;
//                        implementation.Resize -= OnResizeInternal;
//                        implementation.TitleChanged -= OnTitleChangedInternal;
//                        implementation.VisibleChanged -= OnVisibleChangedInternal;
//                        implementation.WindowBorderChanged -= OnWindowBorderChangedInternal;
//                        implementation.WindowStateChanged -= OnWindowStateChangedInternal;
//                        events = false;
//                    }
//                    else
//                    {
//                        throw new InvalidOperationException("Event propagation is already disabled.");
//                    }
//                }
//            }

//            #endregion

//            #endregion

//            #endregion
//        }

//        public static class BlittableValueType
//        {
//            #region Check

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">An instance of the type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            public static bool Check<T>(T type)
//            {
//                return BlittableValueType<T>.Check();
//            }

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">An instance of the type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            [CLSCompliant(false)]
//            public static bool Check<T>(T[] type)
//            {
//                return BlittableValueType<T>.Check();
//            }

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">An instance of the type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            [CLSCompliant(false)]
//            public static bool Check<T>(T[,] type)
//            {
//                return BlittableValueType<T>.Check();
//            }

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">An instance of the type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            [CLSCompliant(false)]
//            public static bool Check<T>(T[, ,] type)
//            {
//                return BlittableValueType<T>.Check();
//            }

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">An instance of the type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            [CLSCompliant(false)]
//            public static bool Check<T>(T[][] type)
//            {
//                return BlittableValueType<T>.Check();
//            }

//            #endregion

//            #region StrideOf

//            /// <summary>
//            /// Returns the size of the specified value type in bytes or 0 if the type is not blittable.
//            /// </summary>
//            /// <typeparam name="T">The value type. Must be blittable.</typeparam>
//            /// <param name="type">An instance of the value type.</param>
//            /// <returns>An integer, specifying the size of the type in bytes.</returns>
//            /// <exception cref="System.ArgumentException">Occurs when type is not blittable.</exception>
//            public static int StrideOf<T>(T type)
//            {
//                if (!Check(type))
//                    throw new ArgumentException("type");

//                return BlittableValueType<T>.Stride;
//            }

//            /// <summary>
//            /// Returns the size of a single array element in bytes  or 0 if the element is not blittable.
//            /// </summary>
//            /// <typeparam name="T">The value type.</typeparam>
//            /// <param name="type">An instance of the value type.</param>
//            /// <returns>An integer, specifying the size of the type in bytes.</returns>
//            /// <exception cref="System.ArgumentException">Occurs when type is not blittable.</exception>
//            [CLSCompliant(false)]
//            public static int StrideOf<T>(T[] type)
//            {
//                if (!Check(type))
//                    throw new ArgumentException("type");

//                return BlittableValueType<T>.Stride;
//            }

//            /// <summary>
//            /// Returns the size of a single array element in bytes or 0 if the element is not blittable.
//            /// </summary>
//            /// <typeparam name="T">The value type.</typeparam>
//            /// <param name="type">An instance of the value type.</param>
//            /// <returns>An integer, specifying the size of the type in bytes.</returns>
//            /// <exception cref="System.ArgumentException">Occurs when type is not blittable.</exception>
//            [CLSCompliant(false)]
//            public static int StrideOf<T>(T[,] type)
//            {
//                if (!Check(type))
//                    throw new ArgumentException("type");

//                return BlittableValueType<T>.Stride;
//            }

//            /// <summary>
//            /// Returns the size of a single array element in bytes or 0 if the element is not blittable.
//            /// </summary>
//            /// <typeparam name="T">The value type.</typeparam>
//            /// <param name="type">An instance of the value type.</param>
//            /// <returns>An integer, specifying the size of the type in bytes.</returns>
//            /// <exception cref="System.ArgumentException">Occurs when type is not blittable.</exception>
//            [CLSCompliant(false)]
//            public static int StrideOf<T>(T[, ,] type)
//            {
//                if (!Check(type))
//                    throw new ArgumentException("type");

//                return BlittableValueType<T>.Stride;
//            }

//            #endregion
//        }

//        sealed class WinRawInput : WinInputBase
//        {
//            #region Fields

//            // Input event data.

//            WinRawJoystick joystick_driver;

//            IntPtr DevNotifyHandle;
//            static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030");

//            #endregion

//            #region Constructors

//            public WinRawInput()
//                : base()
//            {
//                Debug.WriteLine("Using WinRawInput.");
//            }

//            #endregion

//            #region Private Members

//            static IntPtr RegisterForDeviceNotifications(WinWindowInfo parent)
//            {
//                IntPtr dev_notify_handle;
//                BroadcastDeviceInterface bdi = new BroadcastDeviceInterface();
//                bdi.Size = BlittableValueType.StrideOf(bdi);
//                bdi.DeviceType = DeviceBroadcastType.intERFACE;
//                bdi.ClassGuid = DeviceInterfaceHid;
//                unsafe
//                {
//                    dev_notify_handle = RegisterDeviceNotification(parent.Handle,
//                        new IntPtr((void*)&bdi), DeviceNotification.WINDOW_IntPtr);
//                }
//                if (dev_notify_handle == IntPtr.Zero)
//                    Debug.Print("[Warning] Failed to register for device notifications. Error: {0}", Marshal.GetLastWin32Error());

//                return dev_notify_handle;
//            }


//            #endregion

//            #region Protected Members

//            #region WindowProcedure

//            // Processes the input Windows Message, routing the buffer to the correct Keyboard, Mouse or HID.
//            protected unsafe override IntPtr WindowProcedure(
//                IntPtr handle, WindowMessage message, IntPtr wParam, IntPtr lParam)
//            {
//                try
//                {
//                    switch (message)
//                    {
//                        case WindowMessage.INPUT:
//                            {
//                                // Retrieve the raw input data buffer
//                                RawInputHeader header;
//                                if (Functions.GetRawInputData(lParam, out header) == RawInputHeader.SizeInBytes)
//                                {
//                                    switch (header.Type)
//                                    {
//                                        case RawInputDeviceType.HID:
//                                            if (((WinRawJoystick)JoystickDriver).ProcessEvent(lParam))
//                                                return IntPtr.Zero;
//                                            break;
//                                    }
//                                }
//                            }
//                            break;

//                        case WindowMessage.DEVICECHANGE:
//                            ((WinRawJoystick)JoystickDriver).RefreshDevices();
//                            break;
//                    }
//                    return base.WindowProcedure(handle, message, wParam, lParam);
//                }
//                catch (Exception e)
//                {
//                    Debug.Print("[WinRawInput] Caught unhandled exception {0}", e);
//                    return IntPtr.Zero;
//                }
//            }

//            #endregion

//            #region CreateDrivers

//            protected override void CreateDrivers()
//            {
//                joystick_driver = new WinRawJoystick(Parent.Handle);
//                DevNotifyHandle = RegisterForDeviceNotifications(Parent);
//            }

//            #endregion

//            protected override void Dispose(bool manual)
//            {
//                if (!Disposed)
//                {
//                    UnregisterDeviceNotification(DevNotifyHandle);
//                    base.Dispose(manual);
//                }
//            }

//            #endregion

//            #region Public Members

//            #region DeviceCount

//            public static int DeviceCount
//            {
//                get
//                {
//                    int deviceCount = 0;
//                    GetRawInputDeviceList(null, ref deviceCount, API.RawInputDeviceListSize);
//                    return deviceCount;
//                }
//            }

//            #endregion

//            #region GetDeviceList

//            public static RawInputDeviceList[] GetDeviceList()
//            {
//                int count = WinRawInput.DeviceCount;
//                RawInputDeviceList[] ridl = new RawInputDeviceList[count];
//                for (int i = 0; i < count; i++)
//                    ridl[i] = new RawInputDeviceList();
//                GetRawInputDeviceList(ridl, ref count, API.RawInputDeviceListSize);
//                return ridl;
//            }

//            #endregion

//            public override IJoystickDriver2 JoystickDriver
//            {
//                get { return joystick_driver; }
//            }

//            #endregion
//        }

//        internal static class API
//        {
//            // Prevent BeforeFieldInit optimization, and initialize 'size' fields.
//            static API()
//            {
//                RawInputHeaderSize = Marshal.SizeOf(typeof(RawInputHeader));
//                RawInputSize = Marshal.SizeOf(typeof(RawInput));
//                RawMouseSize = Marshal.SizeOf(typeof(RawMouse));
//                RawInputDeviceSize = Marshal.SizeOf(typeof(RawInputDevice));
//                RawInputDeviceListSize = Marshal.SizeOf(typeof(RawInputDeviceList));
//                RawInputDeviceInfoSize = Marshal.SizeOf(typeof(RawInputDeviceInfo));
//                PixelFormatDescriptorVersion = 1;
//                //PixelFormatDescriptorSize = (short)Marshal.SizeOf(typeof(PixelFormatDescriptor));
//                //WindowInfoSize = Marshal.SizeOf(typeof(WindowInfo));
//            }

//            //internal static readonly short PixelFormatDescriptorSize;
//            internal static readonly short PixelFormatDescriptorVersion;
//            internal static readonly int RawInputSize;
//            internal static readonly int RawInputDeviceSize;
//            internal static readonly int RawInputHeaderSize;
//            internal static readonly int RawInputDeviceListSize;
//            internal static readonly int RawInputDeviceInfoSize;
//            internal static readonly int RawMouseSize;
//            internal static readonly int WindowInfoSize;
//        }

//        public class WindowIcon
//        {
//            byte[] data;
//            int width;
//            int height;

//            /// \internal
//            /// <summary>
//            /// Initializes a new instance of the <see cref="OpenTK.WindowIcon"/> class.
//            /// </summary>
//            internal protected WindowIcon()
//            {
//            }

//            WindowIcon(int width, int height)
//            {
//                if (width < 0 || width > 256 || height < 0 || height > 256)
//                    throw new ArgumentOutOfRangeException();

//                this.width = width;
//                this.height = height;
//            }

//            internal WindowIcon(int width, int height, byte[] data)
//                : this(width, height)
//            {
//                if (data == null)
//                    throw new ArgumentNullException();
//                if (data.Length < Width * Height * 4)
//                    throw new ArgumentOutOfRangeException();

//                this.data = data;
//            }

//            internal WindowIcon(int width, int height, IntPtr data)
//                : this(width, height)
//            {
//                if (data == IntPtr.Zero)
//                    throw new ArgumentNullException();

//                // We assume that width and height are correctly set.
//                // If they are not, we will read garbage and probably
//                // crash.
//                this.data = new byte[width * height * 4];
//                Marshal.Copy(data, this.data, 0, this.data.Length);
//            }

//            internal byte[] Data { get { return data; } }
//            internal int Width { get { return width; } }
//            internal int Height { get { return height; } }
//        }

//        public sealed class MouseCursor : WindowIcon
//        {
//            static readonly MouseCursor default_cursor = new MouseCursor();
//            static readonly MouseCursor empty_cursor = new MouseCursor(
//                0, 0, 16, 16, new byte[16 * 16 * 4]);

//            int x;
//            int y;

//            MouseCursor()
//            {
//            }

//            /// <summary>
//            /// Initializes a new <see cref="MouseCursor"/> instance from a
//            /// contiguous array of BGRA pixels.
//            /// Each pixel is composed of 4 bytes, representing B, G, R and A values,
//            /// respectively. For correct antialiasing of translucent cursors,
//            /// the B, G and R components should be premultiplied with the A component:
//            /// <code>
//            /// B = (byte)((B * A) / 255)
//            /// G = (byte)((G * A) / 255)
//            /// R = (byte)((R * A) / 255)
//            /// </code>
//            /// </summary>
//            /// <param name="hotx">The x-coordinate of the cursor hotspot, in the range [0, width]</param>
//            /// <param name="hoty">The y-coordinate of the cursor hotspot, in the range [0, height]</param>
//            /// <param name="width">The width of the cursor data, in pixels.</param>
//            /// <param name="height">The height of the cursor data, in pixels.</param>
//            /// <param name="data">
//            /// A byte array representing the cursor image,
//            /// laid out as a contiguous array of BGRA pixels.
//            /// </param>
//            public MouseCursor(int hotx, int hoty, int width, int height, byte[] data)
//                : base(width, height, data)
//            {
//                if (hotx < 0 || hotx >= Width || hoty < 0 || hoty >= Height)
//                    throw new ArgumentOutOfRangeException();

//                x = hotx;
//                y = hoty;
//            }

//            /// <summary>
//            /// Initializes a new <see cref="MouseCursor"/> instance from a
//            /// contiguous array of BGRA pixels.
//            /// Each pixel is composed of 4 bytes, representing B, G, R and A values,
//            /// respectively. For correct antialiasing of translucent cursors,
//            /// the B, G and R components should be premultiplied with the A component:
//            /// <code>
//            /// B = (byte)((B * A) / 255)
//            /// G = (byte)((G * A) / 255)
//            /// R = (byte)((R * A) / 255)
//            /// </code>
//            /// </summary>
//            /// <param name="hotx">The x-coordinate of the cursor hotspot, in the range [0, width]</param>
//            /// <param name="hoty">The y-coordinate of the cursor hotspot, in the range [0, height]</param>
//            /// <param name="width">The width of the cursor data, in pixels.</param>
//            /// <param name="height">The height of the cursor data, in pixels.</param>
//            /// <param name="data">
//            /// A pointer to the cursor image, laid out as a contiguous array of BGRA pixels.
//            /// </param>
//            public MouseCursor(int hotx, int hoty, int width, int height, IntPtr data)
//                : base(width, height, data)
//            {
//                if (hotx < 0 || hotx >= Width || hoty < 0 || hoty >= Height)
//                    throw new ArgumentOutOfRangeException();

//                x = hotx;
//                y = hoty;
//            }

//            internal int X { get { return x; } }
//            internal int Y { get { return y; } }

//            /// <summary>
//            /// Gets the default mouse cursor for this platform.
//            /// </summary>
//            public static MouseCursor Default
//            {
//                get
//                {
//                    return default_cursor;
//                }
//            }

//            /// <summary>
//            /// Gets an empty (invisible) mouse cursor.
//            /// </summary>
//            public static MouseCursor Empty
//            {
//                get
//                {
//                    return empty_cursor;
//                }
//            }
//        }

//        sealed class WinWindowInfo : IWindowInfo
//        {
//            IntPtr handle, dc;
//            WinWindowInfo parent;
//            bool disposed;

//            #region --- Constructors ---

//            /// <summary>
//            /// Constructs a new instance.
//            /// </summary>
//            public WinWindowInfo()
//            {
//            }

//            /// <summary>
//            /// Constructs a new instance with the specified window handle and paren.t
//            /// </summary>
//            /// <param name="handle">The window handle for this instance.</param>
//            /// <param name="parent">The parent window of this instance (may be null).</param>
//            public WinWindowInfo(IntPtr handle, WinWindowInfo parent)
//            {
//                this.handle = handle;
//                this.parent = parent;
//            }

//            #endregion

//            #region --- Public Methods ---

//            /// <summary>
//            /// Gets or sets the handle of the window.
//            /// </summary>
//            public IntPtr Handle { get { return handle; } set { handle = value; } }

//            /// <summary>
//            /// Gets or sets the Parent of the window (may be null).
//            /// </summary>
//            public WinWindowInfo Parent { get { return parent; } set { parent = value; } }

//            ///// <summary>
//            ///// Gets the device context for this window instance.
//            ///// </summary>
//            //public IntPtr DeviceContext
//            //{
//            //    get
//            //    {
//            //        if (dc == IntPtr.Zero)
//            //            dc = Functions.GetDC(this.Handle);
//            //        //dc = Functions.GetWindowDC(this.Handle);
//            //        return dc;
//            //    }
//            //}

//            // For compatibility with whoever thought it would be
//            // a good idea to access internal APIs through reflection
//            // (e.g. MonoGame)
//            public IntPtr WindowHandle { get { return Handle; } set { Handle = value; } }

//            #region public override string ToString()

//            /// <summary>Returns a System.String that represents the current window.</summary>
//            /// <returns>A System.String that represents the current window.</returns>
//            public override string ToString()
//            {
//                return String.Format("Windows.WindowInfo: Handle {0}, Parent ({1})",
//                    this.Handle, this.Parent != null ? this.Parent.ToString() : "null");
//            }

//            /// <summary>Checks if <c>this</c> and <c>obj</c> reference the same win32 window.</summary>
//            /// <param name="obj">The object to check against.</param>
//            /// <returns>True if <c>this</c> and <c>obj</c> reference the same win32 window; false otherwise.</returns>
//            public override bool Equals(object obj)
//            {
//                if (obj == null) return false;
//                if (this.GetType() != obj.GetType()) return false;
//                WinWindowInfo info = (WinWindowInfo)obj;

//                if (info == null) return false;
//                // TODO: Assumes windows will always have unique handles.
//                return handle.Equals(info.handle);
//            }

//            /// <summary>Returns the hash code for this instance.</summary>
//            /// <returns>A hash code for the current <c>WinWindowInfo</c>.</returns>
//            public override int GetHashCode()
//            {
//                return handle.GetHashCode();
//            }

//            #endregion

//            #endregion

//            #region --- IDisposable ---

//            #region public void Dispose()

//            /// <summary>Releases the unmanaged resources consumed by this instance.</summary>
//            public void Dispose()
//            {
//                this.Dispose(true);
//                GC.SuppressFinalize(this);
//            }

//            #endregion

//            #region void Dispose(bool manual)

//            void Dispose(bool manual)
//            {
//                if (!disposed)
//                {
//                    //if (this.dc != IntPtr.Zero)
//                    //    if (!Functions.ReleaseDC(this.handle, this.dc))
//                    //        Debug.Print("[Warning] Failed to release device context {0}. Windows error: {1}.", this.dc, Marshal.GetLastWin32Error());

//                    if (manual)
//                    {
//                        if (parent != null)
//                            parent.Dispose();
//                    }

//                    disposed = true;
//                }
//            }

//            #endregion

//            #region ~WinWindowInfo()

//            ~WinWindowInfo()
//            {
//                this.Dispose(false);
//            }

//            #endregion

//            #endregion
//        }

//        abstract class WinInputBase
//        {
//            #region Fields

//            readonly WindowProcedure WndProc;
//            readonly Thread InputThread;
//            readonly AutoResetEvent InputReady = new AutoResetEvent(false);

//            IntPtr OldWndProc;
//            INativeWindow native;

//            protected INativeWindow Native { get { return native; } private set { native = value; } }
//            protected WinWindowInfo Parent { get { return (WinWindowInfo)Native.WindowInfo; } }

//            static readonly IntPtr Unhandled = new IntPtr(-1);

//            #endregion

//            #region Constructors

//            public WinInputBase()
//            {
//                WndProc = WindowProcedure;

//                InputThread = new Thread(ProcessEvents);
//                InputThread.SetApartmentState(ApartmentState.STA);
//                InputThread.IsBackground = true;
//                InputThread.Start();

//                InputReady.WaitOne();
//            }

//            #endregion

//            #region Private Members

//            #region ConstructMessageWindow

//            INativeWindow ConstructMessageWindow()
//            {
//                Debug.WriteLine("Initializing input driver.");
//                Debug.Indent();

//                throw new System.Exception();

//                //// Create a new message-only window to retrieve WM_INPUT messages.
//                //INativeWindow native = new NativeWindow();
//                //native.ProcessEvents();
//                //WinWindowInfo parent = native.WindowInfo as WinWindowInfo;
//                //SetParent(parent.Handle, Constants.MESSAGE_ONLY);
//                //native.ProcessEvents();

//                //Debug.Unindent();
//                //return native;
//            }


//            #endregion

//            #region ProcessEvents

//            void ProcessEvents()
//            {
//                Native = ConstructMessageWindow();
//                CreateDrivers();

//                // Subclass the window to retrieve the events we are interested in.
//                //OldWndProc = Functions.SetWindowLong(Parent.Handle, WndProc);
//                //Debug.Print("Input window attached to {0}", Parent);

//                InputReady.Set();

//                MSG msg = new MSG();
//                while (Native.Exists)
//                {
//                    int ret = GetMessage(ref msg, Parent.Handle, 0, 0);
//                    if (ret == -1)
//                    {
//                        throw new Exception(String.Format(
//                            "An error happened while processing the message queue. Windows error: {0}",
//                            Marshal.GetLastWin32Error()));
//                    }

//                    TranslateMessage(ref msg);
//                    DispatchMessage(ref msg);
//                }
//            }

//            #endregion

//            #region WndProcHandler

//            //IntPtr WndProcHandler(
//            //    IntPtr handle, WindowMessage message, IntPtr wParam, IntPtr lParam)
//            //{
//            //    IntPtr ret = WindowProcedure(handle, message, wParam, lParam);
//            //    if (ret == Unhandled)
//            //        return CallWindowProc(OldWndProc, handle, message, wParam, lParam);
//            //    else
//            //        return ret;
//            //}

//            #endregion

//            #endregion

//            #region Protected Members

//            #region WindowProcedure

//            protected virtual IntPtr WindowProcedure(
//                IntPtr handle, WindowMessage message, IntPtr wParam, IntPtr lParam)
//            {
//                return Unhandled;
//            }

//            #endregion

//            #region CreateDrivers

//            // Note: this method is called through the input thread.
//            protected abstract void CreateDrivers();

//            #endregion

//            #endregion

//            #region Public Members

//            public abstract IJoystickDriver2 JoystickDriver { get; }

//            #endregion

//            #region IDisposable Members

//            protected bool Disposed;

//            public void Dispose()
//            {
//                Dispose(true);
//                GC.SuppressFinalize(this);
//            }

//            protected virtual void Dispose(bool manual)
//            {
//                if (!Disposed)
//                {
//                    if (manual)
//                    {
//                        if (Native != null)
//                        {
//                            Native.Close();
//                            Native.Dispose();
//                        }
//                    }

//                    Disposed = true;
//                }
//            }

//            ~WinInputBase()
//            {
//                Debug.Print("[Warning] Resource leaked: {0}.", this);
//                Dispose(false);
//            }

//            #endregion
//        }

//        class DeviceCollection<T> : IEnumerable<T>
//        {
//            readonly Dictionary<long, int> Map = new Dictionary<long, int>();
//            readonly List<T> Devices = new List<T>();

//            #region IEnumerable<T> Members

//            IEnumerator<T> IEnumerable<T>.GetEnumerator()
//            {
//                return Devices.GetEnumerator();
//            }

//            #endregion

//            #region IEnumerable implementation

//            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//            {
//                return GetEnumerator();
//            }

//            #endregion

//            #region Public Members

//            // This avoids boxing when using foreach loops
//            public List<T>.Enumerator GetEnumerator()
//            {
//                return Devices.GetEnumerator();
//            }

//            public T this[int index]
//            {
//                get { return FromIndex(index); }
//            }

//            /// \internal
//            /// <summary>
//            /// Adds or replaces a device based on its hardware id.
//            /// A zero-based device index will be generated automatically
//            /// for the first available device slot.
//            /// </summary>
//            /// <param name="id">The hardware id for the device.</param>
//            /// <param name="device">The device instance.</param>
//            public void Add(long id, T device)
//            {
//                if (!Map.ContainsKey(id))
//                {
//                    int index = GetIndex();
//                    Map.Add(id, index);
//                }

//                Devices[Map[id]] = device;
//            }

//            public void Remove(long id)
//            {
//                if (!TryRemove(id))
//                {
//                    System.Diagnostics.Debug.Print("Invalid DeviceCollection<{0}> id: {1}", typeof(T).FullName, id);
//                }
//            }

//            public bool TryRemove(long id)
//            {
//                if (!Map.ContainsKey(id))
//                {
//                    return false;
//                }

//                Devices[Map[id]] = default(T);
//                Map.Remove(id);
//                return true;
//            }

//            public T FromIndex(int index)
//            {
//                if (index >= 0 && index < Devices.Count)
//                {
//                    return Devices[index];
//                }
//                else
//                {
//                    return default(T);
//                }
//            }

//            public bool FromIndex(int index, out T device)
//            {
//                if (index >= 0 && index < Devices.Count)
//                {
//                    device = Devices[index];
//                    return true;
//                }
//                else
//                {
//                    device = default(T);
//                    return false;
//                }
//            }

//            public T FromHardwareId(long id)
//            {
//                if (Map.ContainsKey(id))
//                {
//                    return FromIndex(Map[id]);
//                }
//                else
//                {
//                    return default(T);
//                }
//            }

//            public bool FromHardwareId(long id, out T device)
//            {
//                if (Map.ContainsKey(id))
//                {
//                    device = FromIndex(Map[id]);
//                    return true;
//                }
//                else
//                {
//                    device = default(T);
//                    return false;
//                }
//            }

//            public int Count
//            {
//                get { return Map.Count; }
//            }

//            #endregion

//            #region Private Members

//            // Return the index of the first empty slot in Devices.
//            // If no empty slot exists, append a new one and return
//            // that index.
//            int GetIndex()
//            {
//                for (int i = 0; i < Devices.Count; i++)
//                {
//                    if (Devices[i] == null)
//                    {
//                        return i;
//                    }
//                }

//                Devices.Add(default(T));
//                return Devices.Count - 1;
//            }

//            #endregion
//        }

//        public static class BlittableValueType<T>
//        {
//            #region Fields

//            static readonly Type Type;
//            static readonly int stride;

//            #endregion

//            #region Constructors

//            static BlittableValueType()
//            {
//                Type = typeof(T);
//                if (Type.IsValueType && !Type.IsGenericType)
//                {
//                    // Does this support generic types? On Mono 2.4.3 it does
//                    // On .Net it doesn't.
//                    // http://msdn.microsoft.com/en-us/library/5s4920fa.aspx
//                    stride = Marshal.SizeOf(typeof(T));
//                }
//            }

//            #endregion

//            #region Public Members

//            /// <summary>
//            /// Gets the size of the type in bytes or 0 for non-blittable types.
//            /// </summary>
//            /// <remarks>
//            /// This property returns 0 for non-blittable types.
//            /// </remarks>
//            public static int Stride { get { return stride; } }

//            #region Check

//            /// <summary>
//            /// Checks whether the current typename T is blittable.
//            /// </summary>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            public static bool Check()
//            {
//                return Check(Type);
//            }

//            /// <summary>
//            /// Checks whether type is a blittable value type.
//            /// </summary>
//            /// <param name="type">A System.Type to check.</param>
//            /// <returns>True if T is blittable; false otherwise.</returns>
//            public static bool Check(Type type)
//            {
//                if (!CheckStructLayoutAttribute(type))
//                    System.Diagnostics.Debug.Print("Warning: type {0} does not specify a StructLayoutAttribute with Pack=1. The memory layout of the struct may change between platforms.", type.Name);

//                return CheckType(type);
//            }

//            #endregion

//            #endregion

//            #region Private Members

//            // Checks whether the parameter is a primitive type or consists of primitive types recursively.
//            // Throws a NotSupportedException if it is not.
//            static bool CheckType(Type type)
//            {
//                //Debug.Print("Checking type {0} (size: {1} bytes).", type.Name, Marshal.SizeOf(type));
//                if (type.IsPrimitive)
//                    return true;

//                if (!type.IsValueType)
//                    return false;

//                FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//                System.Diagnostics.Debug.Indent();
//                foreach (FieldInfo field in fields)
//                {
//                    if (!CheckType(field.FieldType))
//                        return false;
//                }
//                System.Diagnostics.Debug.Unindent();

//                return Stride != 0;
//            }

//            // Checks whether the specified struct defines [StructLayout(LayoutKind.Sequential, Pack=1)]
//            // or [StructLayout(LayoutKind.Explicit)]
//            static bool CheckStructLayoutAttribute(Type type)
//            {
//                StructLayoutAttribute[] attr = (StructLayoutAttribute[])
//                    type.GetCustomAttributes(typeof(StructLayoutAttribute), true);

//                if ((attr == null) ||
//                    (attr != null && attr.Length > 0 && attr[0].Value != LayoutKind.Explicit && attr[0].Pack != 1))
//                    return false;

//                return true;
//            }

//            #endregion
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        internal class RawInputDeviceInfo
//        {
//            /// <summary>
//            /// Size, in bytes, of the RawInputDeviceInfo structure.
//            /// </summary>
//            internal Int32 Size = Marshal.SizeOf(typeof(RawInputDeviceInfo));
//            /// <summary>
//            /// Type of raw input data.
//            /// </summary>
//            internal RawInputDeviceType Type;
//            internal DeviceStruct Device;
//            [StructLayout(LayoutKind.Explicit)]
//            internal struct DeviceStruct
//            {
//                [FieldOffset(0)]
//                internal RawInputMouseDeviceInfo Mouse;
//                [FieldOffset(0)]
//                internal RawInputKeyboardDeviceInfo Keyboard;
//                [FieldOffset(0)]
//                internal RawInputHIDDeviceInfo HID;
//            };
//        }

//        internal static class Functions
//        {
//            // SetWindowLongPtr does not exist on x86 platforms (it's a macro that resolves to SetWindowLong).
//            // We need to detect if we are on x86 or x64 at runtime and call the correct function
//            // (SetWindowLongPtr on x64 or SetWindowLong on x86). Fun!
//            internal static IntPtr SetWindowLong(IntPtr handle, GetWindowLongOffsets item, IntPtr newValue)
//            {
//                // SetWindowPos defines its error condition as an IntPtr.Zero retval and a non-0 GetLastError.
//                // We need to SetLastError(0) to ensure we are not detecting on older error condition (from another function).

//                IntPtr retval = IntPtr.Zero;
//                SetLastError(0);

//                if (IntPtr.Size == 4)
//                    retval = new IntPtr(SetWindowLongInternal(handle, item, newValue.ToInt32()));
//                else
//                    retval = SetWindowLongPtrInternal(handle, item, newValue);

//                if (retval == IntPtr.Zero)
//                {
//                    int error = Marshal.GetLastWin32Error();
//                    if (error != 0)
//                        throw new Exception(String.Format("Failed to modify window border. Error: {0}", error));
//                }

//                return retval;
//            }

//            public static IntPtr LoadCursor(CursorName lpCursorName)
//            {
//                return GamePad.LoadCursor(IntPtr.Zero, new IntPtr((int)lpCursorName));
//            }

//            internal static int GetRawInputData(IntPtr raw, out RawInputHeader header)
//            {
//                int size = RawInputHeader.SizeInBytes;
//                unsafe
//                {
//                    fixed (RawInputHeader* pheader = &header)
//                    {
//                        if (GamePad.GetRawInputData(raw, GetRawInputDataEnum.HEADER,
//                            (IntPtr)pheader, ref size, API.RawInputHeaderSize) != RawInputHeader.SizeInBytes)
//                        {
//                            Debug.Print("[Error] Failed to retrieve raw input header. Error: {0}",
//                                Marshal.GetLastWin32Error());
//                        }
//                    }
//                }
//                return size;
//            }

//            internal static int GetRawInputData(IntPtr raw, out RawInput data)
//            {
//                int size = RawInput.SizeInBytes;
//                unsafe
//                {
//                    fixed (RawInput* pdata = &data)
//                    {
//                        GamePad.GetRawInputData(raw, GetRawInputDataEnum.INPUT,
//                            (IntPtr)pdata, ref size, API.RawInputHeaderSize);
//                    }
//                }
//                return size;
//            }

//            internal static int GetRawInputData(IntPtr raw, byte[] data)
//            {
//                int size = data.Length;
//                unsafe
//                {
//                    fixed (byte* pdata = data)
//                    {
//                        GamePad.GetRawInputData(raw, GetRawInputDataEnum.INPUT,
//                            (IntPtr)pdata, ref size, API.RawInputHeaderSize);
//                    }
//                }
//                return size;
//            }
//        }

//        class XInputJoystick : IJoystickDriver2, IDisposable
//        {
//            // All XInput devices use the same Guid
//            // (only one GamePadConfiguration entry required)
//            static readonly Guid guid =
//                new Guid("78696e70757400000000000000000000"); // equiv. to "xinput"

//            XInput xinput = new XInput();

//            #region IJoystickDriver2 Members

//            public JoystickState GetState(int index)
//            {
//                XInputState xstate;
//                XInputErrorCode error = xinput.GetState((XInputUserIndex)index, out xstate);

//                JoystickState state = new JoystickState();
//                if (error == XInputErrorCode.Success)
//                {
//                    state.SetIsConnected(true);

//                    state.SetAxis(JoystickAxis.Axis0, (short)xstate.GamePad.ThumbLX);
//                    state.SetAxis(JoystickAxis.Axis1, (short)Math.Min(short.MaxValue, -xstate.GamePad.ThumbLY));
//                    state.SetAxis(JoystickAxis.Axis2, (short)HidHelper.ScaleValue(xstate.GamePad.LeftTrigger, 0, byte.MaxValue, short.MinValue, short.MaxValue));
//                    state.SetAxis(JoystickAxis.Axis3, (short)xstate.GamePad.ThumbRX);
//                    state.SetAxis(JoystickAxis.Axis4, (short)Math.Min(short.MaxValue, -xstate.GamePad.ThumbRY));
//                    state.SetAxis(JoystickAxis.Axis5, (short)HidHelper.ScaleValue(xstate.GamePad.RightTrigger, 0, byte.MaxValue, short.MinValue, short.MaxValue));

//                    state.SetButton(JoystickButton.Button0, (xstate.GamePad.Buttons & XInputButtons.A) != 0);
//                    state.SetButton(JoystickButton.Button1, (xstate.GamePad.Buttons & XInputButtons.B) != 0);
//                    state.SetButton(JoystickButton.Button2, (xstate.GamePad.Buttons & XInputButtons.X) != 0);
//                    state.SetButton(JoystickButton.Button3, (xstate.GamePad.Buttons & XInputButtons.Y) != 0);
//                    state.SetButton(JoystickButton.Button4, (xstate.GamePad.Buttons & XInputButtons.LeftShoulder) != 0);
//                    state.SetButton(JoystickButton.Button5, (xstate.GamePad.Buttons & XInputButtons.RightShoulder) != 0);
//                    state.SetButton(JoystickButton.Button6, (xstate.GamePad.Buttons & XInputButtons.Back) != 0);
//                    state.SetButton(JoystickButton.Button7, (xstate.GamePad.Buttons & XInputButtons.Start) != 0);
//                    state.SetButton(JoystickButton.Button8, (xstate.GamePad.Buttons & XInputButtons.LeftThumb) != 0);
//                    state.SetButton(JoystickButton.Button9, (xstate.GamePad.Buttons & XInputButtons.RightThumb) != 0);
//                    state.SetButton(JoystickButton.Button10, (xstate.GamePad.Buttons & XInputButtons.Guide) != 0);

//                    state.SetHat(JoystickHat.Hat0, new JoystickHatState(TranslateHat(xstate.GamePad.Buttons)));
//                }

//                return state;
//            }

//            private HatPosition TranslateHat(XInputButtons buttons)
//            {
//                XInputButtons dir = 0;

//                dir = XInputButtons.DPadUp | XInputButtons.DPadLeft;
//                if ((buttons & dir) == dir)
//                    return HatPosition.UpLeft;
//                dir = XInputButtons.DPadUp | XInputButtons.DPadRight;
//                if ((buttons & dir) == dir)
//                    return HatPosition.UpRight;
//                dir = XInputButtons.DPadDown | XInputButtons.DPadLeft;
//                if ((buttons & dir) == dir)
//                    return HatPosition.DownLeft;
//                dir = XInputButtons.DPadDown | XInputButtons.DPadRight;
//                if ((buttons & dir) == dir)
//                    return HatPosition.DownRight;

//                dir = XInputButtons.DPadUp;
//                if ((buttons & dir) == dir)
//                    return HatPosition.Up;
//                dir = XInputButtons.DPadRight;
//                if ((buttons & dir) == dir)
//                    return HatPosition.Right;
//                dir = XInputButtons.DPadDown;
//                if ((buttons & dir) == dir)
//                    return HatPosition.Down;
//                dir = XInputButtons.DPadLeft;
//                if ((buttons & dir) == dir)
//                    return HatPosition.Left;

//                return HatPosition.Centered;
//            }

//            public JoystickCapabilities GetCapabilities(int index)
//            {
//                XInputDeviceCapabilities xcaps;
//                XInputErrorCode error = xinput.GetCapabilities(
//                    (XInputUserIndex)index,
//                    XInputCapabilitiesFlags.Default,
//                    out xcaps);

//                if (error == XInputErrorCode.Success)
//                {
//                    //GamePadType type = TranslateSubType(xcaps.SubType);
//                    int buttons = TranslateButtons(xcaps.GamePad.Buttons);
//                    int axes = TranslateAxes(ref xcaps.GamePad);

//                    return new JoystickCapabilities(axes, buttons, 0, true);
//                }
//                return new JoystickCapabilities();
//            }

//            public string GetName(int index)
//            {
//                return String.Empty;
//            }

//            public Guid GetGuid(int index)
//            {
//                return guid;
//            }

//            public bool SetVibration(int index, float left, float right)
//            {
//                left = Compute<float>.Clamp(left, 0.0f, 1.0f);
//                right = Compute<float>.Clamp(right, 0.0f, 1.0f);

//                XInputVibration vibration = new XInputVibration(
//                    (ushort)(left * UInt16.MaxValue),
//                    (ushort)(right * UInt16.MaxValue));

//                return xinput.SetState((XInputUserIndex)index, ref vibration) == XInputErrorCode.Success;
//            }

//            #endregion

//            #region Private Members

//            int TranslateAxes(ref XInputGamePad pad)
//            {
//                int count = 0;
//                count += pad.ThumbLX != 0 ? 1 : 0;
//                count += pad.ThumbLY != 0 ? 1 : 0;
//                count += pad.ThumbRX != 0 ? 1 : 0;
//                count += pad.ThumbRY != 0 ? 1 : 0;
//                count += pad.LeftTrigger != 0 ? 1 : 0;
//                count += pad.RightTrigger != 0 ? 1 : 0;
//                return count;
//            }

//            int NumberOfSetBits(int i)
//            {
//                i = i - ((i >> 1) & 0x55555555);
//                i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
//                return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
//            }

//            int TranslateButtons(XInputButtons xbuttons)
//            {
//                return NumberOfSetBits((int)xbuttons);
//            }

//#if false
//        // Todo: Implement JoystickType enumeration
//        GamePadType TranslateSubType(XInputDeviceSubType xtype)
//        {
//            switch (xtype)
//            {
//                case XInputDeviceSubType.ArcadePad: return GamePadType.ArcadePad;
//                case XInputDeviceSubType.ArcadeStick: return GamePadType.ArcadeStick;
//                case XInputDeviceSubType.DancePad: return GamePadType.DancePad;
//                case XInputDeviceSubType.DrumKit: return GamePadType.DrumKit;
//                case XInputDeviceSubType.FlightStick: return GamePadType.FlightStick;
//                case XInputDeviceSubType.GamePad: return GamePadType.GamePad;
//                case XInputDeviceSubType.Guitar: return GamePadType.Guitar;
//                case XInputDeviceSubType.GuitarAlternate: return GamePadType.AlternateGuitar;
//                case XInputDeviceSubType.GuitarBass: return GamePadType.BassGuitar;
//                case XInputDeviceSubType.Wheel: return GamePadType.Wheel;
//                case XInputDeviceSubType.Unknown:
//                default:
//                    return GamePadType.Unknown;
//            }
//        }
//#endif

//            enum XInputErrorCode
//            {
//                Success = 0,
//                DeviceNotConnected
//            }

//            enum XInputDeviceType : byte
//            {
//                GamePad
//            }

//            enum XInputDeviceSubType : byte
//            {
//                Unknown = 0,
//                GamePad = 1,
//                Wheel = 2,
//                ArcadeStick = 3,
//                FlightStick = 4,
//                DancePad = 5,
//                Guitar = 6,
//                GuitarAlternate = 7,
//                DrumKit = 8,
//                GuitarBass = 0xb,
//                ArcadePad = 0x13
//            }

//            enum XInputCapabilities
//            {
//                ForceFeedback = 0x0001,
//                Wireless = 0x0002,
//                Voice = 0x0004,
//                PluginModules = 0x0008,
//                NoNavigation = 0x0010,
//            }

//            enum XInputButtons : ushort
//            {
//                DPadUp = 0x0001,
//                DPadDown = 0x0002,
//                DPadLeft = 0x0004,
//                DPadRight = 0x0008,
//                Start = 0x0010,
//                Back = 0x0020,
//                LeftThumb = 0x0040,
//                RightThumb = 0x0080,
//                LeftShoulder = 0x0100,
//                RightShoulder = 0x0200,
//                Guide = 0x0400, // Undocumented, requires XInputGetStateEx + XINPUT_1_3.dll or higher
//                A = 0x1000,
//                B = 0x2000,
//                X = 0x4000,
//                Y = 0x8000
//            }

//            [Flags]
//            enum XInputCapabilitiesFlags
//            {
//                Default = 0,
//                GamePadOnly = 1
//            }

//            enum XInputBatteryType : byte
//            {
//                Disconnected = 0x00,
//                Wired = 0x01,
//                Alkaline = 0x02,
//                NiMH = 0x03,
//                Unknown = 0xff
//            }

//            enum XInputBatteryLevel : byte
//            {
//                Empty = 0x00,
//                Low = 0x01,
//                Medium = 0x02,
//                Full = 0x03
//            }

//            enum XInputUserIndex
//            {
//                First = 0,
//                Second,
//                Third,
//                Fourth,
//                Any = 0xff
//            }

//#pragma warning disable 0649 // field is never assigned

//            struct XInputThresholds
//            {
//                public const int LeftThumbDeadzone = 7849;
//                public const int RightThumbDeadzone = 8689;
//                public const int TriggerThreshold = 30;
//            }

//            struct XInputGamePad
//            {
//                public XInputButtons Buttons;
//                public byte LeftTrigger;
//                public byte RightTrigger;
//                public short ThumbLX;
//                public short ThumbLY;
//                public short ThumbRX;
//                public short ThumbRY;
//            }

//            struct XInputState
//            {
//                public int PacketNumber;
//                public XInputGamePad GamePad;
//            }

//            struct XInputVibration
//            {
//                public ushort LeftMotorSpeed;
//                public ushort RightMotorSpeed;

//                public XInputVibration(ushort left, ushort right)
//                {
//                    LeftMotorSpeed = left;
//                    RightMotorSpeed = right;
//                }
//            }

//            struct XInputDeviceCapabilities
//            {
//                public XInputDeviceType Type;
//                public XInputDeviceSubType SubType;
//                public short Flags;
//                public XInputGamePad GamePad;
//                public XInputVibration Vibration;
//            }

//            struct XInputBatteryInformation
//            {
//                public XInputBatteryType Type;
//                public XInputBatteryLevel Level;
//            }

//            class XInput : IDisposable
//            {
//                IntPtr dll;

//                internal XInput()
//                {
//                    // Try to load the newest XInput***.dll installed on the system
//                    // The delegates below will be loaded dynamically from that dll
//                    dll = LoadLibrary("XINPUT1_4");
//                    if (dll == IntPtr.Zero)
//                        dll = LoadLibrary("XINPUT1_3");
//                    if (dll == IntPtr.Zero)
//                        dll = LoadLibrary("XINPUT1_2");
//                    if (dll == IntPtr.Zero)
//                        dll = LoadLibrary("XINPUT1_1");
//                    if (dll == IntPtr.Zero)
//                        dll = LoadLibrary("XINPUT9_1_0");
//                    if (dll == IntPtr.Zero)
//                        throw new NotSupportedException("XInput was not found on this platform");

//                    // Load the entry points we are interested in from that dll
//                    GetCapabilities = (XInputGetCapabilities)Load("XInputGetCapabilities", typeof(XInputGetCapabilities));
//                    GetState =
//                        // undocumented XInputGetStateEx with support for the "Guide" button (requires XINPUT_1_3+)
//                        (XInputGetState)Load("XInputGetStateEx", typeof(XInputGetState)) ??
//                        // documented XInputGetState (no support for the "Guide" button)
//                        (XInputGetState)Load("XInputGetState", typeof(XInputGetState));
//                    SetState = (XInputSetState)Load("XInputSetState", typeof(XInputSetState));
//                }

//                #region Private Members

//                Delegate Load(string name, Type type)
//                {
//                    IntPtr pfunc = GetProcAddress(dll, name);
//                    if (pfunc != IntPtr.Zero)
//                        return Marshal.GetDelegateForFunctionPointer(pfunc, type);
//                    return null;
//                }

//                #endregion

//                #region Internal Members

//                internal XInputGetCapabilities GetCapabilities;
//                internal XInputGetState GetState;
//                internal XInputSetState SetState;

//                [SuppressUnmanagedCodeSecurity]
//                internal delegate XInputErrorCode XInputGetCapabilities(
//                    XInputUserIndex dwUserIndex,
//                    XInputCapabilitiesFlags dwFlags,
//                    out XInputDeviceCapabilities pCapabilities);

//                [SuppressUnmanagedCodeSecurity]
//                internal delegate XInputErrorCode XInputGetState
//                (
//                    XInputUserIndex dwUserIndex,
//                    out XInputState pState
//                );

//                [SuppressUnmanagedCodeSecurity]
//                internal delegate XInputErrorCode XInputSetState
//                (
//                    XInputUserIndex dwUserIndex,
//                    ref XInputVibration pVibration
//                );

//                #endregion

//                #region IDisposable Members

//                public void Dispose()
//                {
//                    Dispose(true);
//                    GC.SuppressFinalize(this);
//                }

//                void Dispose(bool manual)
//                {
//                    if (manual)
//                    {
//                        if (dll != IntPtr.Zero)
//                        {
//                            FreeLibrary(dll);
//                            dll = IntPtr.Zero;
//                        }
//                    }
//                }

//                #endregion
//            }

//            #endregion

//            #region IDisposable Members

//            public void Dispose()
//            {
//                Dispose(true);
//                GC.SuppressFinalize(this);
//            }

//            void Dispose(bool manual)
//            {
//                if (manual)
//                {
//                    xinput.Dispose();
//                }
//                else
//                {
//                    System.Diagnostics.Debug.Print("{0} leaked, did you forget to call Dispose()?", typeof(XInputJoystick).Name);
//                }
//            }

//#if DEBUG
//            ~XInputJoystick()
//            {
//                Dispose(false);
//            }
//#endif

//            #endregion
//        }

//        class HidHelper
//        {
//            /// <summary>
//            /// Scales the specified value linearly between min and max.
//            /// </summary>
//            /// <param name="value">The value to scale</param>
//            /// <param name="value_min">The minimum expected value (inclusive)</param>
//            /// <param name="value_max">The maximum expected value (inclusive)</param>
//            /// <param name="result_min">The minimum output value (inclusive)</param>
//            /// <param name="result_max">The maximum output value (inclusive)</param>
//            /// <returns>The value, scaled linearly between min and max</returns>
//            public static int ScaleValue(int value, int value_min, int value_max,
//                int result_min, int result_max)
//            {
//                if (value_min >= value_max || result_min >= result_max)
//                    throw new ArgumentOutOfRangeException();
//                Compute<int>.Clamp(value, value_min, value_max);

//                int range = result_max - result_min;
//                long temp = (value - value_min) * range; // need long to avoid overflow
//                return (int)(temp / (value_max - value_min) + result_min);
//            }

//            public static JoystickAxis TranslateJoystickAxis(HIDPage page, int usage)
//            {
//                switch (page)
//                {
//                    case HIDPage.GenericDesktop:
//                        switch ((HIDUsageGD)usage)
//                        {
//                            case HIDUsageGD.X:
//                                return JoystickAxis.Axis0;
//                            case HIDUsageGD.Y:
//                                return JoystickAxis.Axis1;

//                            case HIDUsageGD.Z:
//                                return JoystickAxis.Axis2;
//                            case HIDUsageGD.Rz:
//                                return JoystickAxis.Axis3;

//                            case HIDUsageGD.Rx:
//                                return JoystickAxis.Axis4;
//                            case HIDUsageGD.Ry:
//                                return JoystickAxis.Axis5;

//                            case HIDUsageGD.Slider:
//                                return JoystickAxis.Axis6;
//                            case HIDUsageGD.Dial:
//                                return JoystickAxis.Axis7;
//                            case HIDUsageGD.Wheel:
//                                return JoystickAxis.Axis8;
//                        }
//                        break;

//                    case HIDPage.Simulation:
//                        switch ((HIDUsageSim)usage)
//                        {
//                            case HIDUsageSim.Rudder:
//                                return JoystickAxis.Axis9;
//                            case HIDUsageSim.Throttle:
//                                return JoystickAxis.Axis10;
//                        }
//                        break;
//                }

//                System.Diagnostics.Debug.Print("[Input] Unknown axis with HID page/usage {0}/{1}", page, usage);
//                return 0;
//            }
//        }

//        class WinRawJoystick : IJoystickDriver2
//        {
//            class Device
//            {
//                public IntPtr Handle;
//                JoystickCapabilities Capabilities;
//                JoystickState State;
//                Guid Guid;

//                internal readonly List<HidProtocolValueCaps> AxisCaps =
//                    new List<HidProtocolValueCaps>();
//                internal readonly List<HidProtocolButtonCaps> ButtonCaps =
//                    new List<HidProtocolButtonCaps>();
//                internal readonly bool IsXInput;
//                internal readonly int XInputIndex;

//                readonly Dictionary<int, JoystickAxis> axes =
//                    new Dictionary<int, JoystickAxis>();
//                readonly Dictionary<int, JoystickButton> buttons =
//                    new Dictionary<int, JoystickButton>();
//                readonly Dictionary<int, JoystickHat> hats =
//                    new Dictionary<int, JoystickHat>();

//                #region Constructors

//                public Device(IntPtr handle, Guid guid, bool is_xinput, int xinput_index)
//                {
//                    Handle = handle;
//                    Guid = guid;
//                    IsXInput = is_xinput;
//                    XInputIndex = xinput_index;
//                }

//                #endregion

//                #region Public Members

//                public void ClearButtons()
//                {
//                    State.ClearButtons();
//                }

//                public void SetAxis(short collection, HIDPage page, short usage, short value)
//                {
//                    JoystickAxis axis = GetAxis(collection, page, usage);
//                    State.SetAxis(axis, value);
//                }

//                public void SetButton(short collection, HIDPage page, short usage, bool value)
//                {
//                    JoystickButton button = GetButton(collection, page, usage);
//                    State.SetButton(button, value);
//                }

//                public void SetHat(short collection, HIDPage page, short usage, HatPosition pos)
//                {
//                    JoystickHat hat = GetHat(collection, page, usage);
//                    State.SetHat(hat, new JoystickHatState(pos));
//                }

//                public void SetConnected(bool value)
//                {
//                    Capabilities.SetIsConnected(value);
//                    State.SetIsConnected(value);
//                }

//                public JoystickCapabilities GetCapabilities()
//                {
//                    Capabilities = new JoystickCapabilities(
//                        axes.Count, buttons.Count, hats.Count,
//                        Capabilities.IsConnected);
//                    return Capabilities;
//                }

//                internal void SetCapabilities(JoystickCapabilities caps)
//                {
//                    Capabilities = caps;
//                }

//                public Guid GetGuid()
//                {
//                    return Guid;
//                }

//                public JoystickState GetState()
//                {
//                    return State;
//                }

//                #endregion

//                #region Private Members

//                static int MakeKey(short collection, HIDPage page, short usage)
//                {
//                    byte coll_byte = unchecked((byte)collection);
//                    byte page_byte = unchecked((byte)(((ushort)page & 0xff00) >> 8 | ((ushort)page & 0xff)));
//                    return (coll_byte << 24) | (page_byte << 16) | unchecked((ushort)usage);
//                }

//                JoystickAxis GetAxis(short collection, HIDPage page, short usage)
//                {
//                    int key = MakeKey(collection, page, usage);
//                    if (!axes.ContainsKey(key))
//                    {
//                        JoystickAxis axis = HidHelper.TranslateJoystickAxis(page, usage);
//                        axes.Add(key, axis);
//                    }
//                    return axes[key];
//                }

//                JoystickButton GetButton(short collection, HIDPage page, short usage)
//                {
//                    int key = MakeKey(collection, page, usage);
//                    if (!buttons.ContainsKey(key))
//                    {
//                        buttons.Add(key, JoystickButton.Button0 + buttons.Count);
//                    }
//                    return buttons[key];
//                }

//                JoystickHat GetHat(short collection, HIDPage page, short usage)
//                {
//                    int key = MakeKey(collection, page, usage);
//                    if (!hats.ContainsKey(key))
//                    {
//                        hats.Add(key, JoystickHat.Hat0 + hats.Count);
//                    }
//                    return hats[key];
//                }

//                #endregion
//            }

//            static readonly string TypeName = typeof(WinRawJoystick).Name;

//            XInputJoystick XInput = new XInputJoystick();

//            // Defines which types of HID devices we are interested in
//            readonly RawInputDevice[] DeviceTypes;

//            readonly object UpdateLock = new object();
//            readonly DeviceCollection<Device> Devices = new DeviceCollection<Device>();

//            byte[] HIDData = new byte[1024];
//            byte[] PreparsedData = new byte[1024];
//            HidProtocolData[] DataBuffer = new HidProtocolData[16];

//            public WinRawJoystick(IntPtr window)
//            {
//                System.Diagnostics.Debug.WriteLine("Using WinRawJoystick.");
//                System.Diagnostics.Debug.Indent();

//                if (window == IntPtr.Zero)
//                    throw new ArgumentNullException("window");

//                DeviceTypes = new RawInputDevice[]
//            {
//                new RawInputDevice(HIDUsageGD.Joystick, RawInputDeviceFlags.DEVNOTIFY | RawInputDeviceFlags.INPUTSINK, window),
//                new RawInputDevice(HIDUsageGD.GamePad, RawInputDeviceFlags.DEVNOTIFY | RawInputDeviceFlags.INPUTSINK, window),
//            };

//                if (!RegisterRawInputDevices(DeviceTypes, DeviceTypes.Length, API.RawInputDeviceSize))
//                {
//                    System.Diagnostics.Debug.Print("[Warning] Raw input registration failed with error: {0}.",
//                        Marshal.GetLastWin32Error());
//                }
//                else
//                {
//                    System.Diagnostics.Debug.Print("[WinRawJoystick] Registered for raw input");
//                }

//                RefreshDevices();

//                System.Diagnostics.Debug.Unindent();
//            }

//            #region Public Members

//            public void RefreshDevices()
//            {
//                // Mark all devices as disconnected. We will check which of those
//                // are connected below.
//                foreach (var device in Devices)
//                {
//                    device.SetConnected(false);
//                }

//                // Discover joystick devices
//                int xinput_device_count = 0;
//                foreach (RawInputDeviceList dev in WinRawInput.GetDeviceList())
//                {
//                    // Skip non-joystick devices
//                    if (dev.Type != RawInputDeviceType.HID)
//                        continue;

//                    // We use the device handle as the hardware id.
//                    // This works, but the handle will change whenever the
//                    // device is unplugged/replugged. We compensate for this
//                    // by checking device GUIDs, below.
//                    // Note: we cannot use the GUID as the hardware id,
//                    // because it is costly to query (and we need to query
//                    // that every time we process a device event.)
//                    IntPtr handle = dev.Device;
//                    bool is_xinput = IsXInput(handle);
//                    Guid guid = GetDeviceGuid(handle);
//                    long hardware_id = handle.ToInt64();

//                    Device device = Devices.FromHardwareId(hardware_id);
//                    if (device != null)
//                    {
//                        // We have already opened this device, mark it as connected
//                        device.SetConnected(true);
//                    }
//                    else
//                    {
//                        device = new Device(handle, guid, is_xinput,
//                            is_xinput ? xinput_device_count++ : 0);

//                        // This is a new device, query its capabilities and add it
//                        // to the device list
//                        if (!QueryDeviceCaps(device))
//                        {
//                            continue;
//                        }
//                        device.SetConnected(true);

//                        // Check if a disconnected device with identical GUID already exists.
//                        // If so, replace that device with this instance.
//                        Device match = null;
//                        foreach (Device candidate in Devices)
//                        {
//                            if (candidate.GetGuid() == guid && !candidate.GetCapabilities().IsConnected)
//                            {
//                                match = candidate;
//                            }
//                        }
//                        if (match != null)
//                        {
//                            Devices.Remove(match.Handle.ToInt64());
//                        }

//                        Devices.Add(hardware_id, device);

//                        Debug.Print("[{0}] Connected joystick {1} ({2})",
//                            GetType().Name, device.GetGuid(), device.GetCapabilities());
//                    }
//                }
//            }

//            public unsafe bool ProcessEvent(IntPtr raw)
//            {
//                // Query the size of the raw HID data buffer
//                int size = 0;
//                GetRawInputData(raw, GetRawInputDataEnum.INPUT, IntPtr.Zero, ref size, RawInputHeader.SizeInBytes);
//                if (size > HIDData.Length)
//                {
//                    Array.Resize(ref HIDData, size);
//                }

//                // Retrieve the raw HID data buffer
//                if (Functions.GetRawInputData(raw, HIDData) > 0)
//                {
//                    fixed (byte* pdata = HIDData)
//                    {
//                        RawInput* rin = (RawInput*)pdata;

//                        IntPtr handle = rin->Header.Device;
//                        Device stick = GetDevice(handle);
//                        if (stick == null)
//                        {
//                            System.Diagnostics.Debug.Print("[WinRawJoystick] Unknown device {0}", handle);
//                            return false;
//                        }

//                        if (stick.IsXInput)
//                        {
//                            return true;
//                        }

//                        if (!GetPreparsedData(handle, ref PreparsedData))
//                        {
//                            return false;
//                        }

//                        // Query current state
//                        // Allocate enough storage to hold the data of the current report
//                        int report_count = MaxDataListLength(HidProtocolReportType.Input, PreparsedData);
//                        if (report_count == 0)
//                        {
//                            System.Diagnostics.Debug.Print("[WinRawJoystick] HidProtocol.MaxDataListLength() failed with {0}",
//                                Marshal.GetLastWin32Error());
//                            return false;
//                        }

//                        // Fill the data buffer
//                        if (DataBuffer.Length < report_count)
//                        {
//                            Array.Resize(ref DataBuffer, report_count);
//                        }

//                        UpdateAxes(rin, stick);
//                        UpdateButtons(rin, stick);
//                        return true;
//                    }
//                }

//                return false;
//            }

//            HatPosition GetHatPosition(uint value, HidProtocolValueCaps caps)
//            {
//                if (caps.LogicalMax == 8)
//                    return (HatPosition)value;
//                else
//                    return HatPosition.Centered;
//            }

//            unsafe void UpdateAxes(RawInput* rin, Device stick)
//            {
//                for (int i = 0; i < stick.AxisCaps.Count; i++)
//                {
//                    if (stick.AxisCaps[i].IsRange)
//                    {
//                        Debug.Print("[{0}] Axis range collections not implemented. Please report your controller type at http://www.opentk.com",
//                            GetType().Name);
//                        continue;
//                    }

//                    HIDPage page = stick.AxisCaps[i].UsagePage;
//                    short usage = stick.AxisCaps[i].NotRange.Usage;
//                    uint value = 0;
//                    short collection = stick.AxisCaps[i].LinkCollection;

//                    HidProtocolStatus status = GetUsageValue(
//                        HidProtocolReportType.Input,
//                        page, 0, usage, ref value,
//                        PreparsedData,
//                        new IntPtr((void*)&rin->Data.HID.RawData),
//                        rin->Data.HID.Size);

//                    if (status != HidProtocolStatus.Success)
//                    {
//                        Debug.Print("[{0}] HidProtocol.GetScaledUsageValue() failed. Error: {1}",
//                            GetType().Name, status);
//                        continue;
//                    }

//                    if (page == HIDPage.GenericDesktop && (HIDUsageGD)usage == HIDUsageGD.Hatswitch)
//                    {
//                        stick.SetHat(collection, page, usage, GetHatPosition(value, stick.AxisCaps[i]));
//                    }
//                    else
//                    {
//                        short scaled_value = (short)HidHelper.ScaleValue(
//                            (int)((long)value + stick.AxisCaps[i].LogicalMin),
//                            stick.AxisCaps[i].LogicalMin, stick.AxisCaps[i].LogicalMax,
//                            Int16.MinValue, Int16.MaxValue);
//                        stick.SetAxis(collection, page, usage, scaled_value);
//                    }
//                }
//            }

//            unsafe void UpdateButtons(RawInput* rin, Device stick)
//            {
//                stick.ClearButtons();

//                for (int i = 0; i < stick.ButtonCaps.Count; i++)
//                {
//                    short* usage_list = stackalloc short[(int)JoystickButton.Last + 1];
//                    int usage_length = (int)JoystickButton.Last;
//                    HIDPage page = stick.ButtonCaps[i].UsagePage;
//                    short collection = stick.ButtonCaps[i].LinkCollection;

//                    HidProtocolStatus status = GetUsages(
//                        HidProtocolReportType.Input,
//                        page, 0, usage_list, ref usage_length,
//                        PreparsedData,
//                        new IntPtr((void*)&rin->Data.HID.RawData),
//                        rin->Data.HID.Size);

//                    if (status != HidProtocolStatus.Success)
//                    {
//                        Debug.Print("[WinRawJoystick] HidProtocol.GetUsages() failed with {0}",
//                            Marshal.GetLastWin32Error());
//                        continue;
//                    }

//                    for (int j = 0; j < usage_length; j++)
//                    {
//                        short usage = *(usage_list + j);
//                        stick.SetButton(collection, page, usage, true);
//                    }
//                }
//            }

//            #endregion

//            #region Private Members

//            static bool GetPreparsedData(IntPtr handle, ref byte[] prepared_data)
//            {
//                // Query the size of the _HIDP_PREPARSED_DATA structure for this event.
//                int preparsed_size = 0;
//                GetRawInputDeviceInfo(handle, RawInputDeviceInfoEnum.PREPARSEDDATA,
//                    IntPtr.Zero, ref preparsed_size);
//                if (preparsed_size == 0)
//                {
//                    Debug.Print("[WinRawJoystick] Functions.GetRawInputDeviceInfo(PARSEDDATA) failed with {0}",
//                        Marshal.GetLastWin32Error());
//                    return false;
//                }

//                // Allocate space for _HIDP_PREPARSED_DATA.
//                // This is an untyped blob of data.
//                if (prepared_data.Length < preparsed_size)
//                {
//                    Array.Resize(ref prepared_data, preparsed_size);
//                }

//                if (GetRawInputDeviceInfo(handle, RawInputDeviceInfoEnum.PREPARSEDDATA,
//                    prepared_data, ref preparsed_size) < 0)
//                {
//                    Debug.Print("[WinRawJoystick] Functions.GetRawInputDeviceInfo(PARSEDDATA) failed with {0}",
//                        Marshal.GetLastWin32Error());
//                    return false;
//                }

//                return true;
//            }

//            bool QueryDeviceCaps(Device stick)
//            {
//                Debug.Print("[{0}] Querying joystick {1}",
//                    TypeName, stick.GetGuid());

//                try
//                {
//                    Debug.Indent();
//                    HidProtocolCaps caps;

//                    if (GetPreparsedData(stick.Handle, ref PreparsedData) &&
//                        GetDeviceCaps(stick, PreparsedData, out caps))
//                    {
//                        if (stick.AxisCaps.Count >= JoystickState.MaxAxes ||
//                            stick.ButtonCaps.Count >= JoystickState.MaxButtons)
//                        {
//                            Debug.Print("Device {0} has {1} and {2} buttons. This might be a touch device - skipping.",
//                                stick.Handle, stick.AxisCaps.Count, stick.ButtonCaps.Count);
//                            return false;
//                        }

//                        for (int i = 0; i < stick.AxisCaps.Count; i++)
//                        {
//                            Debug.Print("Analyzing value collection {0} {1} {2}",
//                                i,
//                                stick.AxisCaps[i].IsRange ? "range" : "",
//                                stick.AxisCaps[i].IsAlias ? "alias" : "");

//                            if (stick.AxisCaps[i].IsRange || stick.AxisCaps[i].IsAlias)
//                            {
//                                Debug.Print("Skipping value collection {0}", i);
//                                continue;
//                            }

//                            HIDPage page = stick.AxisCaps[i].UsagePage;
//                            short collection = stick.AxisCaps[i].LinkCollection;
//                            switch (page)
//                            {
//                                case HIDPage.GenericDesktop:
//                                    HIDUsageGD gd_usage = (HIDUsageGD)stick.AxisCaps[i].NotRange.Usage;
//                                    switch (gd_usage)
//                                    {
//                                        case HIDUsageGD.X:
//                                        case HIDUsageGD.Y:
//                                        case HIDUsageGD.Z:
//                                        case HIDUsageGD.Rx:
//                                        case HIDUsageGD.Ry:
//                                        case HIDUsageGD.Rz:
//                                        case HIDUsageGD.Slider:
//                                        case HIDUsageGD.Dial:
//                                        case HIDUsageGD.Wheel:
//                                            Debug.Print("Found axis {0} ({1} / {2})",
//                                                JoystickAxis.Axis0 + stick.GetCapabilities().AxisCount,
//                                                page, (HIDUsageGD)stick.AxisCaps[i].NotRange.Usage);
//                                            stick.SetAxis(collection, page, stick.AxisCaps[i].NotRange.Usage, 0);
//                                            break;

//                                        case HIDUsageGD.Hatswitch:
//                                            Debug.Print("Found hat {0} ({1} / {2})",
//                                                JoystickHat.Hat0 + stick.GetCapabilities().HatCount,
//                                                page, (HIDUsageGD)stick.AxisCaps[i].NotRange.Usage);
//                                            stick.SetHat(collection, page, stick.AxisCaps[i].NotRange.Usage, HatPosition.Centered);
//                                            break;

//                                        default:
//                                            Debug.Print("Unknown usage {0} for page {1}",
//                                                gd_usage, page);
//                                            break;
//                                    }
//                                    break;

//                                case HIDPage.Simulation:
//                                    switch ((HIDUsageSim)stick.AxisCaps[i].NotRange.Usage)
//                                    {
//                                        case HIDUsageSim.Rudder:
//                                        case HIDUsageSim.Throttle:
//                                            Debug.Print("Found simulation axis {0} ({1} / {2})",
//                                                JoystickAxis.Axis0 + stick.GetCapabilities().AxisCount,
//                                                page, (HIDUsageSim)stick.AxisCaps[i].NotRange.Usage);
//                                            stick.SetAxis(collection, page, stick.AxisCaps[i].NotRange.Usage, 0);
//                                            break;
//                                    }
//                                    break;

//                                default:
//                                    Debug.Print("Unknown page {0}", page);
//                                    break;
//                            }
//                        }

//                        for (int i = 0; i < stick.ButtonCaps.Count; i++)
//                        {
//                            Debug.Print("Analyzing button collection {0} {1} {2}",
//                                i,
//                                stick.ButtonCaps[i].IsRange ? "range" : "",
//                                stick.ButtonCaps[i].IsAlias ? "alias" : "");

//                            if (stick.ButtonCaps[i].IsAlias)
//                            {
//                                Debug.Print("Skipping button collection {0}", i);
//                                continue;
//                            }

//                            bool is_range = stick.ButtonCaps[i].IsRange;
//                            HIDPage page = stick.ButtonCaps[i].UsagePage;
//                            short collection = stick.ButtonCaps[i].LinkCollection;
//                            switch (page)
//                            {
//                                case HIDPage.Button:
//                                    if (is_range)
//                                    {
//                                        for (short usage = stick.ButtonCaps[i].Range.UsageMin; usage <= stick.ButtonCaps[i].Range.UsageMax; usage++)
//                                        {
//                                            Debug.Print("Found button {0} ({1} / {2})",
//                                                JoystickButton.Button0 + stick.GetCapabilities().ButtonCount,
//                                                page, usage);
//                                            stick.SetButton(collection, page, usage, false);
//                                        }
//                                    }
//                                    else
//                                    {
//                                        Debug.Print("Found button {0} ({1} / {2})",
//                                            JoystickButton.Button0 + stick.GetCapabilities().ButtonCount,
//                                            page, stick.ButtonCaps[i].NotRange.Usage);
//                                        stick.SetButton(collection, page, stick.ButtonCaps[i].NotRange.Usage, false);
//                                    }
//                                    break;

//                                default:
//                                    Debug.Print("Unknown page {0} for button.", page);
//                                    break;
//                            }
//                        }
//                    }
//                }
//                finally
//                {
//                    Debug.Unindent();
//                }

//                return true;
//            }

//            static bool GetDeviceCaps(Device stick, byte[] preparsed_data, out HidProtocolCaps caps)
//            {
//                // Query joystick capabilities
//                caps = new HidProtocolCaps();
//                if (GetCaps(preparsed_data, ref caps) != HidProtocolStatus.Success)
//                {
//                    Debug.Print("[WinRawJoystick] HidProtocol.GetCaps() failed with {0}",
//                        Marshal.GetLastWin32Error());
//                    return false;
//                }

//                // Make sure our caps arrays are big enough
//                HidProtocolValueCaps[] axis_caps = new HidProtocolValueCaps[caps.NumberInputValueCaps];
//                HidProtocolButtonCaps[] button_caps = new HidProtocolButtonCaps[caps.NumberInputButtonCaps];

//                // Axis capabilities
//                ushort axis_count = (ushort)axis_caps.Length;
//                if (GetValueCaps(HidProtocolReportType.Input,
//                    axis_caps, ref axis_count, preparsed_data) !=
//                    HidProtocolStatus.Success)
//                {
//                    Debug.Print("[WinRawJoystick] HidProtocol.GetValueCaps() failed with {0}",
//                        Marshal.GetLastWin32Error());
//                    return false;
//                }

//                // Button capabilities
//                ushort button_count = (ushort)button_caps.Length;
//                if (GetButtonCaps(HidProtocolReportType.Input,
//                    button_caps, ref button_count, preparsed_data) !=
//                    HidProtocolStatus.Success)
//                {
//                    Debug.Print("[WinRawJoystick] HidProtocol.GetButtonCaps() failed with {0}",
//                        Marshal.GetLastWin32Error());
//                    return false;
//                }

//                stick.AxisCaps.Clear();
//                stick.AxisCaps.AddRange(axis_caps);

//                stick.ButtonCaps.Clear();
//                stick.ButtonCaps.AddRange(button_caps);

//                return true;
//            }

//            // Get a DirectInput-compatible Guid
//            // (equivalent to DIDEVICEINSTANCE guidProduct field)
//            Guid GetDeviceGuid(IntPtr handle)
//            {
//                // Retrieve a RID_DEVICE_INFO struct which contains the VID and PID
//                RawInputDeviceInfo info = new RawInputDeviceInfo();
//                int size = info.Size;
//                if (GetRawInputDeviceInfo(handle, RawInputDeviceInfoEnum.DEVICEINFO, info, ref size) < 0)
//                {
//                    Debug.Print("[WinRawJoystick] Functions.GetRawInputDeviceInfo(DEVICEINFO) failed with error {0}",
//                        Marshal.GetLastWin32Error());
//                    return Guid.Empty;
//                }

//                // Todo: this Guid format is only valid for USB joysticks.
//                // Bluetooth devices, such as OUYA controllers, have a totally
//                // different PID/VID format in DirectInput.
//                // Do we need to use the same guid or could we simply use PID/VID
//                // there too? (Test with an OUYA controller.)
//                int vid = info.Device.HID.VendorId;
//                int pid = info.Device.HID.ProductId;
//                return new Guid(
//                    (pid << 16) | vid,
//                    0, 0,
//                    0, 0,
//                    (byte)'P', (byte)'I', (byte)'D',
//                    (byte)'V', (byte)'I', (byte)'D');
//            }

//            // Checks whether this is an XInput device.
//            // XInput devices should be handled through
//            // the XInput API.
//            bool IsXInput(IntPtr handle)
//            {
//                bool is_xinput = false;

//                unsafe
//                {
//                    // Find out how much memory we need to allocate
//                    // for the DEVICENAME string
//                    int size = 0;
//                    if (GetRawInputDeviceInfo(handle, RawInputDeviceInfoEnum.DEVICENAME, IntPtr.Zero, ref size) < 0 || size == 0)
//                    {
//                        Debug.Print("[WinRawJoystick] Functions.GetRawInputDeviceInfo(DEVICENAME) failed with error {0}",
//                            Marshal.GetLastWin32Error());
//                        return is_xinput;
//                    }

//                    // Allocate memory and retrieve the DEVICENAME string
//                    sbyte* pname = stackalloc sbyte[size + 1];
//                    if (GetRawInputDeviceInfo(handle, RawInputDeviceInfoEnum.DEVICENAME, (IntPtr)pname, ref size) < 0)
//                    {
//                        Debug.Print("[WinRawJoystick] Functions.GetRawInputDeviceInfo(DEVICENAME) failed with error {0}",
//                            Marshal.GetLastWin32Error());
//                        return is_xinput;
//                    }

//                    // Convert the buffer to a .Net string, and split it into parts
//                    string name = new string(pname);
//                    if (String.IsNullOrEmpty(name))
//                    {
//                        Debug.Print("[WinRawJoystick] Failed to construct device name");
//                        return is_xinput;
//                    }

//                    is_xinput = name.Contains("IG_");
//                }

//                return is_xinput;
//            }

//            Device GetDevice(IntPtr handle)
//            {
//                long hardware_id = handle.ToInt64();
//                bool is_device_known = false;

//                lock (UpdateLock)
//                {
//                    is_device_known = Devices.FromHardwareId(hardware_id) != null;
//                }

//                if (!is_device_known)
//                {
//                    RefreshDevices();
//                }

//                lock (UpdateLock)
//                {
//                    return Devices.FromHardwareId(hardware_id);
//                }
//            }

//            bool IsValid(int index)
//            {
//                return Devices.FromIndex(index) != null;
//            }

//            #endregion

//            #region IJoystickDriver2 Members

//            public JoystickState GetState(int index)
//            {
//                lock (UpdateLock)
//                {
//                    if (IsValid(index))
//                    {
//                        Device dev = Devices.FromIndex(index);
//                        if (dev.IsXInput)
//                        {
//                            return XInput.GetState(dev.XInputIndex);
//                        }
//                        else
//                        {
//                            return dev.GetState();
//                        }
//                    }
//                    return new JoystickState();
//                }
//            }

//            public JoystickCapabilities GetCapabilities(int index)
//            {
//                lock (UpdateLock)
//                {
//                    if (IsValid(index))
//                    {
//                        Device dev = Devices.FromIndex(index);
//                        if (dev.IsXInput)
//                        {
//                            return XInput.GetCapabilities(dev.XInputIndex);
//                        }
//                        else
//                        {
//                            return dev.GetCapabilities();
//                        }
//                    }
//                    return new JoystickCapabilities();
//                }
//            }

//            public Guid GetGuid(int index)
//            {
//                lock (UpdateLock)
//                {
//                    if (IsValid(index))
//                    {
//                        Device dev = Devices.FromIndex(index);
//                        if (dev.IsXInput)
//                        {
//                            return XInput.GetGuid(dev.XInputIndex);
//                        }
//                        else
//                        {
//                            return dev.GetGuid();
//                        }
//                    }
//                    return new Guid();
//                }
//            }

//            #endregion
//        }

//        #endregion

//        #region Interface

//        public interface IWindowInfo : IDisposable
//        {
//            /// <summary>
//            /// Retrieves a platform-specific handle to this window.
//            /// </summary>
//            IntPtr Handle { get; }
//        }

//        public interface INativeWindow : IDisposable
//        {
//            ///// <summary>
//            ///// Gets or sets the <see cref="System.Drawing.Icon"/> of the window.
//            ///// </summary>
//            //Icon Icon { get; set; }

//            /// <summary>
//            /// Gets or sets the title of the window.
//            /// </summary>
//            string Title { get; set; }

//            /// <summary>
//            /// Gets a System.Boolean that indicates whether this window has input focus.
//            /// </summary>
//            bool Focused { get; }

//            /// <summary>
//            /// Gets or sets a System.Boolean that indicates whether the window is visible.
//            /// </summary>
//            bool Visible { get; set; }

//            /// <summary>
//            /// Gets a System.Boolean that indicates whether the window has been created and has not been destroyed.
//            /// </summary>
//            bool Exists { get; }

//            /// <summary>
//            /// Gets the <see cref="OpenTK.Platform.IWindowInfo"/> for this window.
//            /// </summary>
//            IWindowInfo WindowInfo { get; }

//            ///// <summary>
//            ///// Gets or sets the <see cref="OpenTK.WindowState"/> for this window.
//            ///// </summary>
//            //WindowState WindowState { get; set; }

//            ///// <summary>
//            ///// Gets or sets the <see cref="OpenTK.WindowBorder"/> for this window.
//            ///// </summary>
//            //WindowBorder WindowBorder { get; set; }

//            ///// <summary>
//            ///// Gets or sets a <see cref="System.Drawing.Rectangle"/> structure the contains the external bounds of this window, in screen coordinates.
//            ///// External bounds include the title bar, borders and drawing area of the window.
//            ///// </summary>
//            //Rectangle Bounds { get; set; }

//            ///// <summary>
//            ///// Gets or sets a <see cref="System.Drawing.Point"/> structure that contains the location of this window on the desktop.
//            ///// </summary>
//            //Point Location { get; set; }

//            ///// <summary>
//            ///// Gets or sets a <see cref="System.Drawing.Size"/> structure that contains the external size of this window.
//            ///// </summary>
//            //Size Size { get; set; }

//            /// <summary>
//            /// Gets or sets the horizontal location of this window on the desktop.
//            /// </summary>
//            int X { get; set; }

//            /// <summary>
//            /// Gets or sets the vertical location of this window on the desktop.
//            /// </summary>
//            int Y { get; set; }

//            /// <summary>
//            /// Gets or sets the external width of this window.
//            /// </summary>
//            int Width { get; set; }

//            /// <summary>
//            /// Gets or sets the external height of this window.
//            /// </summary>
//            int Height { get; set; }

//            ///// <summary>
//            ///// Gets or sets a <see cref="System.Drawing.Rectangle"/> structure that contains the internal bounds of this window, in client coordinates.
//            ///// The internal bounds include the drawing area of the window, but exclude the titlebar and window borders.
//            ///// </summary>
//            //Rectangle ClientRectangle { get; set; }

//            ///// <summary>
//            ///// Gets or sets a <see cref="System.Drawing.Size"/> structure that contains the internal size this window.
//            ///// </summary>
//            //Size ClientSize { get; set; }

//            ///// <summary>
//            ///// This property is deprecated and should not be used.
//            ///// </summary>
//            //[Obsolete("Use OpenTK.Input.Mouse/Keyboard/Joystick/GamePad instead.")]
//            //OpenTK.Input.IInputDriver InputDriver { get; }

//            /// <summary>
//            /// Gets or sets the <see cref="OpenTK.MouseCursor"/> for this window.
//            /// </summary>
//            /// <value>The cursor.</value>
//            MouseCursor Cursor { get; set; }

//            /// <summary>
//            /// Gets or sets a value, indicating whether the mouse cursor is visible.
//            /// </summary>
//            bool CursorVisible { get; set; }

//            //        /// <summary>
//            //        /// Gets or sets a value, indicating whether the mouse cursor is confined inside the window size.
//            //        /// </summary>
//            //        bool CursorGrabbed { get; set; }

//            /// <summary>
//            /// Closes this window.
//            /// </summary>
//            void Close();

//            /// <summary>
//            /// Processes pending window events.
//            /// </summary>
//            void ProcessEvents();

//            ///// <summary>
//            ///// Transforms the specified point from screen to client coordinates. 
//            ///// </summary>
//            ///// <param name="point">
//            ///// A <see cref="System.Drawing.Point"/> to transform.
//            ///// </param>
//            ///// <returns>
//            ///// The point transformed to client coordinates.
//            ///// </returns>
//            //Point PointToClient(Point point);

//            ///// <summary>
//            ///// Transforms the specified point from client to screen coordinates. 
//            ///// </summary>
//            ///// <param name="point">
//            ///// A <see cref="System.Drawing.Point"/> to transform.
//            ///// </param>
//            ///// <returns>
//            ///// The point transformed to screen coordinates.
//            ///// </returns>
//            //Point PointToScreen(Point point);

//            /// <summary>
//            /// Occurs whenever the window is moved. 
//            /// </summary>
//            event EventHandler<EventArgs> Move;

//            /// <summary>
//            /// Occurs whenever the window is resized. 
//            /// </summary>
//            event EventHandler<EventArgs> Resize;

//            ///// <summary>
//            ///// Occurs when the window is about to close. 
//            ///// </summary>
//            //event EventHandler<CancelEventArgs> Closing;

//            /// <summary>
//            /// Occurs after the window has closed. 
//            /// </summary>
//            event EventHandler<EventArgs> Closed;

//            /// <summary>
//            /// Occurs when the window is disposed. 
//            /// </summary>
//            event EventHandler<EventArgs> Disposed;

//            /// <summary>
//            /// Occurs when the <see cref="Icon"/> property of the window changes. 
//            /// </summary>
//            event EventHandler<EventArgs> IconChanged;

//            /// <summary>
//            /// Occurs when the <see cref="Title"/> property of the window changes.
//            /// </summary>
//            event EventHandler<EventArgs> TitleChanged;

//            /// <summary>
//            /// Occurs when the <see cref="Visible"/> property of the window changes.
//            /// </summary>
//            event EventHandler<EventArgs> VisibleChanged;

//            /// <summary>
//            /// Occurs when the <see cref="Focused"/> property of the window changes.
//            /// </summary>
//            event EventHandler<EventArgs> FocusedChanged;

//            /// <summary>
//            /// Occurs when the <see cref="WindowBorder"/> property of the window changes.
//            /// </summary>
//            event EventHandler<EventArgs> WindowBorderChanged;

//            /// <summary>
//            /// Occurs when the <see cref="WindowState"/> property of the window changes.
//            /// </summary>
//            event EventHandler<EventArgs> WindowStateChanged;

//            /// <summary>
//            /// Occurs whenever the mouse cursor leaves the window <see cref="Bounds"/>.
//            /// </summary>
//            event EventHandler<EventArgs> MouseLeave;

//            /// <summary>
//            /// Occurs whenever the mouse cursor enters the window <see cref="Bounds"/>.
//            /// </summary>
//            event EventHandler<EventArgs> MouseEnter;

//            //event EventHandler<MouseEventArgs> MouseClick;
//            //event EventHandler<MouseEventArgs> MouseDoubleClick;

//            //event EventHandler<DragEventArgs> DragDrop;
//            //event EventHandler<DragEventArgs> DragEnter;
//            //event EventHandler<DragEventArgs> DragOver;
//            //event EventHandler<EventArgs> DragLeave;
//        }

//        interface IJoystickDriver2
//        {
//            JoystickState GetState(int index);
//            JoystickCapabilities GetCapabilities(int index);
//            Guid GetGuid(int index);
//        }

//        #endregion

//        #region Driver

//        public struct JoystickHatState : IEquatable<JoystickHatState>
//        {
//            HatPosition position;

//            internal JoystickHatState(HatPosition pos)
//            {
//                position = pos;
//            }

//            /// <summary>
//            /// Gets a <see cref="HatPosition"/> value indicating
//            /// the position of this hat. 
//            /// </summary>
//            /// <value>The position.</value>
//            public HatPosition Position { get { return position; } }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> indicating
//            /// whether this hat lies in the top hemicircle.
//            /// </summary>
//            /// <value><c>true</c> if this hat lies in the top hemicircle; otherwise, <c>false</c>.</value>
//            public bool IsUp
//            {
//                get
//                {
//                    return
//                        Position == HatPosition.Up ||
//                        Position == HatPosition.UpLeft ||
//                        Position == HatPosition.UpRight;
//                }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> indicating
//            /// whether this hat lies in the bottom hemicircle.
//            /// </summary>
//            /// <value><c>true</c> if this hat lies in the bottom hemicircle; otherwise, <c>false</c>.</value>
//            public bool IsDown
//            {
//                get
//                {
//                    return
//                        Position == HatPosition.Down ||
//                        Position == HatPosition.DownLeft ||
//                        Position == HatPosition.DownRight;
//                }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> indicating
//            /// whether this hat lies in the left hemicircle.
//            /// </summary>
//            /// <value><c>true</c> if this hat lies in the left hemicircle; otherwise, <c>false</c>.</value>
//            public bool IsLeft
//            {
//                get
//                {
//                    return
//                        Position == HatPosition.Left ||
//                        Position == HatPosition.UpLeft ||
//                        Position == HatPosition.DownLeft;
//                }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> indicating
//            /// whether this hat lies in the right hemicircle.
//            /// </summary>
//            /// <value><c>true</c> if this hat lies in the right hemicircle; otherwise, <c>false</c>.</value>
//            public bool IsRight
//            {
//                get
//                {
//                    return
//                        Position == HatPosition.Right ||
//                        Position == HatPosition.UpRight ||
//                        Position == HatPosition.DownRight;
//                }
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickHatState"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickHatState"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{{0}{1}{2}{3}}}",
//                    IsUp ? "U" : String.Empty,
//                    IsLeft ? "L" : String.Empty,
//                    IsDown ? "D" : String.Empty,
//                    IsRight ? "R" : String.Empty);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.JoystickHatState"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return Position.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.JoystickHatState"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.JoystickHatState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickHatState"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is JoystickHatState &&
//                    Equals((JoystickHatState)obj);
//            }

//            #region IEquatable<JoystickHatState> implementation

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.JoystickHatState"/> is equal to the current <see cref="OpenTK.Input.JoystickHatState"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.JoystickHatState"/> to compare with the current <see cref="OpenTK.Input.JoystickHatState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.JoystickHatState"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickHatState"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(JoystickHatState other)
//            {
//                return Position == other.Position;
//            }

//            #endregion
//        }

//        public struct JoystickState : IEquatable<JoystickState>
//        {
//            // If we ever add more values to JoystickAxis or JoystickButton
//            // then we'll need to increase these limits.
//            internal const int MaxAxes = (int)JoystickAxis.Last + 1;
//            internal const int MaxButtons = (int)JoystickButton.Last + 1;
//            internal const int MaxHats = (int)JoystickHat.Last + 1;

//            const float ConversionFactor = 1.0f / (short.MaxValue + 0.5f);

//            int packet_number;
//            int buttons;
//            unsafe fixed short axes[MaxAxes];
//            JoystickHatState hat0;
//            JoystickHatState hat1;
//            JoystickHatState hat2;
//            JoystickHatState hat3;
//            bool is_connected;

//            #region Public Members

//            /// <summary>
//            /// Gets a value between -1.0 and 1.0 representing the current offset of the specified  <see cref="JoystickAxis"/>.
//            /// </summary>
//            /// <returns>
//            /// A value between -1.0 and 1.0 representing offset of the specified  <see cref="JoystickAxis"/>.
//            /// If the specified axis does not exist, then the return value is 0.0. Use <see cref="Joystick.GetCapabilities"/>
//            /// to query the number of available axes.
//            /// </returns>
//            /// <param name="axis">The <see cref="JoystickAxis"/> to query.</param>
//            public float GetAxis(JoystickAxis axis)
//            {
//                return GetAxisRaw(axis) * ConversionFactor;
//            }

//            /// <summary>
//            /// Gets the current <see cref="ButtonState"/> of the specified <see cref="JoystickButton"/>.
//            /// </summary>
//            /// <returns><see cref="ButtonState.Pressed"/> if the specified button is pressed; otherwise, <see cref="ButtonState.Released"/>.</returns>
//            /// <param name="button">The <see cref="JoystickButton"/> to query.</param>
//            public bool GetButton(JoystickButton button)
//            {
//                return (buttons & (1 << (int)button)) != 0;
//            }

//            /// <summary>
//            /// Gets the hat.
//            /// </summary>
//            /// <returns>The hat.</returns>
//            /// <param name="hat">Hat.</param>
//            public JoystickHatState GetHat(JoystickHat hat)
//            {
//                switch (hat)
//                {
//                    case JoystickHat.Hat0:
//                        return hat0;
//                    case JoystickHat.Hat1:
//                        return hat1;
//                    case JoystickHat.Hat2:
//                        return hat2;
//                    case JoystickHat.Hat3:
//                        return hat3;
//                    default:
//                        return new JoystickHatState();
//                }
//            }

//            /// <summary>
//            /// Gets a value indicating whether the specified <see cref="JoystickButton"/> is currently pressed.
//            /// </summary>
//            /// <returns>true if the specified button is pressed; otherwise, false.</returns>
//            /// <param name="button">The <see cref="JoystickButton"/> to query.</param>
//            public bool IsButtonDown(JoystickButton button)
//            {
//                return (buttons & (1 << (int)button)) != 0;
//            }

//            /// <summary>
//            /// Gets a value indicating whether the specified <see cref="JoystickButton"/> is currently released.
//            /// </summary>
//            /// <returns>true if the specified button is released; otherwise, false.</returns>
//            /// <param name="button">The <see cref="JoystickButton"/> to query.</param>
//            public bool IsButtonUp(JoystickButton button)
//            {
//                return (buttons & (1 << (int)button)) == 0;
//            }

//            /// <summary>
//            /// Gets a value indicating whether this instance is connected.
//            /// </summary>
//            /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
//            public bool IsConnected
//            {
//                get { return is_connected; }
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickState"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.JoystickState"/>.</returns>
//            public override string ToString()
//            {
//                StringBuilder sb = new StringBuilder();
//                for (int i = 0; i < MaxAxes; i++)
//                {
//                    sb.Append(" ");
//                    sb.Append(String.Format("{0:f4}", GetAxis(JoystickAxis.Axis0 + i)));
//                }
//                return String.Format(
//                    "{{Axes:{0}; Buttons: {1}; Hat: {2}; IsConnected: {3}}}",
//                    sb.ToString(),
//                    Convert.ToString((int)buttons, 2).PadLeft(16, '0'),
//                    hat0,
//                    IsConnected);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.JoystickState"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                int hash = buttons.GetHashCode() ^ IsConnected.GetHashCode();
//                for (int i = 0; i < MaxAxes; i++)
//                {
//                    hash ^= GetAxisUnsafe(i).GetHashCode();
//                }
//                return hash;
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.JoystickState"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.JoystickState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickState"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is JoystickState &&
//                    Equals((JoystickState)obj);
//            }

//            #endregion

//            #region Internal Members

//            internal int PacketNumber
//            {
//                get { return packet_number; }
//            }

//            internal short GetAxisRaw(JoystickAxis axis)
//            {
//                return GetAxisRaw((int)axis);
//            }

//            internal short GetAxisRaw(int axis)
//            {
//                short value = 0;
//                if (axis >= 0 && axis < MaxAxes)
//                {
//                    value = GetAxisUnsafe(axis);
//                }
//                else
//                {
//                    System.Diagnostics.Debug.Print("[Joystick] Invalid axis {0}", axis);
//                }
//                return value;
//            }

//            internal void SetAxis(JoystickAxis axis, short value)
//            {
//                int index = (int)axis;
//                if (index < 0 || index >= MaxAxes)
//                    throw new ArgumentOutOfRangeException("axis");

//                unsafe
//                {
//                    fixed (short* paxes = axes)
//                    {
//                        *(paxes + index) = value;
//                    }
//                }
//            }

//            internal void ClearButtons()
//            {
//                buttons = 0;
//            }

//            internal void SetButton(JoystickButton button, bool value)
//            {
//                int index = (int)button;
//                if (index < 0 || index >= MaxButtons)
//                    throw new ArgumentOutOfRangeException("button");

//                if (value)
//                {
//                    buttons |= 1 << index;
//                }
//                else
//                {
//                    buttons &= ~(1 << index);
//                }
//            }

//            internal void SetHat(JoystickHat hat, JoystickHatState value)
//            {
//                switch (hat)
//                {
//                    case JoystickHat.Hat0:
//                        hat0 = value;
//                        break;
//                    case JoystickHat.Hat1:
//                        hat1 = value;
//                        break;
//                    case JoystickHat.Hat2:
//                        hat2 = value;
//                        break;
//                    case JoystickHat.Hat3:
//                        hat3 = value;
//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException("hat");
//                }
//            }

//            internal void SetIsConnected(bool value)
//            {
//                is_connected = value;
//            }

//            internal void SetPacketNumber(int number)
//            {
//                packet_number = number;
//            }

//            #endregion

//            #region Private Members

//            short GetAxisUnsafe(int index)
//            {
//                unsafe
//                {
//                    fixed (short* paxis = axes)
//                    {
//                        return *(paxis + index);
//                    }
//                }
//            }

//            #endregion

//            #region IEquatable<JoystickState> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.JoystickState"/> is equal to the current <see cref="OpenTK.Input.JoystickState"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.JoystickState"/> to compare with the current <see cref="OpenTK.Input.JoystickState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.JoystickState"/> is equal to the current
//            /// <see cref="OpenTK.Input.JoystickState"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(JoystickState other)
//            {
//                bool equals =
//                    buttons == other.buttons &&
//                    IsConnected == other.IsConnected;
//                for (int i = 0; equals && i < MaxAxes; i++)
//                {
//                    equals &= GetAxisUnsafe(i) == other.GetAxisUnsafe(i);
//                }
//                for (int i = 0; equals && i < MaxHats; i++)
//                {
//                    JoystickHat hat = JoystickHat.Hat0 + i;
//                    equals &= GetHat(hat).Equals(other.GetHat(hat));
//                }
//                return equals;
//            }

//            #endregion
//        }

//        public sealed class Joystick
//        {
//            static readonly IJoystickDriver2 implementation = new WinRawInput().JoystickDriver;

//            private Joystick() { }

//            /// <summary>
//            /// Retrieves the <see cref="JoystickCapabilities"/> of the device connected
//            /// at the specified index.
//            /// </summary>
//            /// <returns>
//            /// A <see cref="JoystickCapabilities"/> structure describing
//            /// the capabilities of the device at the specified index.
//            /// If no device is connected at the specified index, the <c>IsConnected</c>
//            /// property of the returned structure will be false.
//            /// </returns>
//            /// <param name="index">The zero-based index of the device to poll.</param>
//            public static JoystickCapabilities GetCapabilities(int index)
//            {
//                return implementation.GetCapabilities(index);
//            }

//            /// <summary>
//            /// Retrieves the <see cref="JoystickState"/> of the device connected
//            /// at the specified index.
//            /// </summary>
//            /// <returns>A <see cref="JoystickState"/> structure describing
//            /// the current state of the device at the specified index.
//            /// If no device is connected at this index, the <c>IsConnected</c>
//            /// property of the returned structure will be false.
//            /// </returns>
//            /// <param name="index">The zero-based index of the device to poll.</param>
//            public static JoystickState GetState(int index)
//            {
//                return implementation.GetState(index);
//            }

//            internal static Guid GetGuid(int index)
//            {
//                return implementation.GetGuid(index);
//            }

//            //public string GetName(int index)
//            //{
//            //    return implementation.GetName(index);
//            //}
//        }

//        struct GamePadConfigurationTarget
//        {
//            ConfigurationType map_type;
//            Nullable<Buttons> map_button;
//            Nullable<GamePadAxes> map_axis;

//            public GamePadConfigurationTarget(Buttons button)
//                : this()
//            {
//                Type = ConfigurationType.Button;
//                map_button = button;
//            }

//            public GamePadConfigurationTarget(GamePadAxes axis)
//                : this()
//            {
//                Type = ConfigurationType.Axis;
//                map_axis = axis;
//            }

//            public ConfigurationType Type
//            {
//                get { return map_type; }
//                private set { map_type = value; }
//            }

//            public GamePadAxes Axis
//            {
//                get { return map_axis.Value; }
//                private set { map_axis = value; }
//            }

//            public Buttons Button
//            {
//                get { return map_button.Value; }
//                private set { map_button = value; }
//            }
//        }

//        struct GamePadConfigurationSource
//        {
//            ConfigurationType map_type;
//            JoystickButton? map_button;
//            JoystickAxis? map_axis;
//            JoystickHat? map_hat;
//            HatPosition? map_hat_position;

//            public GamePadConfigurationSource(JoystickAxis axis)
//                : this()
//            {
//                Type = ConfigurationType.Axis;
//                Axis = axis;
//            }

//            public GamePadConfigurationSource(JoystickButton button)
//                : this()
//            {
//                Type = ConfigurationType.Button;
//                Button = button;
//            }

//            public GamePadConfigurationSource(JoystickHat hat, HatPosition pos)
//                : this()
//            {
//                Type = ConfigurationType.Hat;
//                Hat = hat;
//                map_hat_position = pos;
//            }

//            public ConfigurationType Type
//            {
//                get { return map_type; }
//                private set { map_type = value; }
//            }

//            public JoystickAxis Axis
//            {
//                get { return map_axis.Value; }
//                private set { map_axis = value; }
//            }

//            public JoystickButton Button
//            {
//                get { return map_button.Value; }
//                private set { map_button = value; }
//            }

//            public JoystickHat Hat
//            {
//                get { return map_hat.Value; }
//                private set { map_hat = value; }
//            }

//            public HatPosition HatPosition
//            {
//                get { return map_hat_position.Value; }
//                private set { map_hat_position = value; }
//            }
//        }

//        class GamePadConfigurationItem
//        {
//            GamePadConfigurationSource source;
//            GamePadConfigurationTarget target;

//            public GamePadConfigurationItem(GamePadConfigurationSource source, GamePadConfigurationTarget target)
//            {
//                Source = source;
//                Target = target;
//            }

//            public GamePadConfigurationSource Source
//            {
//                get { return source; }
//                private set { source = value; }
//            }

//            public GamePadConfigurationTarget Target
//            {
//                get { return target; }
//                private set { target = value; }
//            }
//        }

//        public struct GamePadCapabilities : IEquatable<GamePadCapabilities>
//        {
//            Buttons buttons;
//            GamePadAxes axes;
//            byte gamepad_type;
//            bool is_connected;
//            bool is_mapped;

//            #region Constructors

//            internal GamePadCapabilities(GamePadType type, GamePadAxes axes, Buttons buttons, bool is_connected, bool is_mapped)
//                : this()
//            {
//                gamepad_type = (byte)type;
//                this.axes = axes;
//                this.buttons = buttons;
//                this.is_connected = is_connected;
//                this.is_mapped = is_mapped;
//            }

//            #endregion

//            #region Public Members

//            /// <summary>
//            /// Gets a <see cref="GamePadType"/>  value describing the type of a <see cref="GamePad"/> input device.
//            /// This value depends on the connected device and the drivers in use. If <c>IsConnected</c>
//            /// is false, then this value will be <c>GamePadType.Unknown</c>.
//            /// </summary>
//            /// <value>The <c>GamePadType</c> of the connected input device.</value>
//            public GamePadType GamePadType
//            {
//                get { return (GamePadType)gamepad_type; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// an up digital pad button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has an up digital pad button; otherwise, <c>false</c>.</value>
//            public bool HasDPadUpButton
//            {
//                get { return (buttons & Buttons.DPadUp) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a down digital pad button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a down digital pad button; otherwise, <c>false</c>.</value>
//            public bool HasDPadDownButton
//            {
//                get { return (buttons & Buttons.DPadDown) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left digital pad button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left digital pad button; otherwise, <c>false</c>.</value>
//            public bool HasDPadLeftButton
//            {
//                get { return (buttons & Buttons.DPadLeft) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right digital pad button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right digital pad button; otherwise, <c>false</c>.</value>
//            public bool HasDPadRightButton
//            {
//                get { return (buttons & Buttons.DPadRight) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// an A button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has an A button; otherwise, <c>false</c>.</value>
//            public bool HasAButton
//            {
//                get { return (buttons & Buttons.A) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a B button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a B button; otherwise, <c>false</c>.</value>
//            public bool HasBButton
//            {
//                get { return (buttons & Buttons.B) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a X button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a X button; otherwise, <c>false</c>.</value>
//            public bool HasXButton
//            {
//                get { return (buttons & Buttons.X) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a Y button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a Y button; otherwise, <c>false</c>.</value>
//            public bool HasYButton
//            {
//                get { return (buttons & Buttons.Y) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left stick button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left stick button; otherwise, <c>false</c>.</value>
//            public bool HasLeftStickButton
//            {
//                get { return (buttons & Buttons.LeftStick) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right stick button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right stick button; otherwise, <c>false</c>.</value>
//            public bool HasRightStickButton
//            {
//                get { return (buttons & Buttons.RightStick) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left shoulder button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left shoulder button; otherwise, <c>false</c>.</value>
//            public bool HasLeftShoulderButton
//            {
//                get { return (buttons & Buttons.LeftShoulder) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right shoulder button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right shoulder button; otherwise, <c>false</c>.</value>
//            public bool HasRightShoulderButton
//            {
//                get { return (buttons & Buttons.RightShoulder) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a back button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a back button; otherwise, <c>false</c>.</value>
//            public bool HasBackButton
//            {
//                get { return (buttons & Buttons.Back) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a big button. (also known as "guide" or "home" button).
//            /// </summary>
//            /// <value><c>true</c> if this instance has a big button; otherwise, <c>false</c>.</value>
//            public bool HasBigButton
//            {
//                get { return (buttons & Buttons.Home) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a start button.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a start button; otherwise, <c>false</c>.</value>
//            public bool HasStartButton
//            {
//                get { return (buttons & Buttons.Start) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left thumbstick with a x-axis.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left thumbstick with a x-axis; otherwise, <c>false</c>.</value>
//            public bool HasLeftXThumbStick
//            {
//                get { return (axes & GamePadAxes.LeftX) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left thumbstick with a y-axis.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left thumbstick with a y-axis; otherwise, <c>false</c>.</value>
//            public bool HasLeftYThumbStick
//            {
//                get { return (axes & GamePadAxes.LeftY) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right thumbstick with a x-axis.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right thumbstick with a x-axis; otherwise, <c>false</c>.</value>
//            public bool HasRightXThumbStick
//            {
//                get { return (axes & GamePadAxes.RightX) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right thumbstick with a y-axis.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right thumbstick with a y-axis; otherwise, <c>false</c>.</value>
//            public bool HasRightYThumbStick
//            {
//                get { return (axes & GamePadAxes.RightY) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a left trigger.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a left trigger; otherwise, <c>false</c>.</value>
//            public bool HasLeftTrigger
//            {
//                get { return (axes & GamePadAxes.LeftTrigger) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a right trigger.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a right trigger; otherwise, <c>false</c>.</value>
//            public bool HasRightTrigger
//            {
//                get { return (axes & GamePadAxes.RightTrigger) != 0; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a low-frequency vibration motor.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a low-frequency vibration motor; otherwise, <c>false</c>.</value>
//            public bool HasLeftVibrationMotor
//            {
//                get { return false; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a high-frequency vibration motor.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a high frequency vibration motor; otherwise, <c>false</c>.</value>
//            public bool HasRightVibrationMotor
//            {
//                get { return false; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> has
//            /// a microphone input.
//            /// </summary>
//            /// <value><c>true</c> if this instance has a microphone input; otherwise, <c>false</c>.</value>
//            public bool HasVoiceSupport
//            {
//                get { return false; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether this <c>GamePad</c> is
//            /// currently connected.
//            /// </summary>
//            /// <value><c>true</c> if this instance is currently connected; otherwise, <c>false</c>.</value>
//            public bool IsConnected
//            {
//                get { return is_connected; }
//            }

//            /// <summary>
//            /// Gets a <see cref="System.Boolean"/> value describing whether a valid button configuration
//            /// exists for this <c>GamePad</c> in the GamePad configuration database.
//            /// </summary>
//            public bool IsMapped
//            {
//                get { return is_mapped; }
//            }

//            /// <param name="left">A <see cref="GamePadCapabilities"/> structure to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadCapabilities"/> structure to test for equality.</param>
//            public static bool operator ==(GamePadCapabilities left, GamePadCapabilities right)
//            {
//                return left.Equals(right);
//            }

//            /// <param name="left">A <see cref="GamePadCapabilities"/> structure to test for inequality.</param>
//            /// <param name="right">A <see cref="GamePadCapabilities"/> structure to test for inequality.</param>
//            public static bool operator !=(GamePadCapabilities left, GamePadCapabilities right)
//            {
//                return !left.Equals(right);
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadCapabilities"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadCapabilities"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{Type: {0}; Axes: {1}; Buttons: {2}; {3}; {4}}}",
//                    GamePadType,
//                    Convert.ToString((int)axes, 2),
//                    Convert.ToString((int)buttons, 2),
//                    IsMapped ? "Mapped" : "Unmapped",
//                    IsConnected ? "Connected" : "Disconnected");
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadCapabilities"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return
//                    buttons.GetHashCode() ^
//                    is_connected.GetHashCode() ^
//                    is_mapped.GetHashCode() ^
//                    gamepad_type.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadCapabilities"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadCapabilities"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadCapabilities"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadCapabilities &&
//                    Equals((GamePadCapabilities)obj);
//            }

//            #endregion

//            #region IEquatable<GamePadCapabilities> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadCapabilities"/> is equal to the current <see cref="OpenTK.Input.GamePadCapabilities"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadCapabilities"/> to compare with the current <see cref="OpenTK.Input.GamePadCapabilities"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadCapabilities"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadCapabilities"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadCapabilities other)
//            {
//                return
//                    buttons == other.buttons &&
//                    is_connected == other.is_connected &&
//                    is_mapped == other.is_mapped &&
//                    gamepad_type == other.gamepad_type;
//            }

//            #endregion
//        }

//        sealed class GamePadConfiguration
//        {
//            static readonly char[] ConfigurationSeparator = new char[] { ',' };

//            Guid guid;
//            string name;
//            readonly List<GamePadConfigurationItem> configuration_items =
//                new List<GamePadConfigurationItem>();

//            public Guid Guid
//            {
//                get { return guid; }
//                private set { guid = value; }
//            }

//            public string Name
//            {
//                get { return name; }
//                private set { name = value; }
//            }

//            public GamePadConfiguration(string configuration)
//            {
//                ParseConfiguration(configuration);
//            }

//            public List<GamePadConfigurationItem>.Enumerator GetEnumerator()
//            {
//                return configuration_items.GetEnumerator();
//            }

//            #region Private Members

//            // Parses a GamePad configuration string. The string
//            // follows the rules for SDL2 GameController, outlined here:
//            // http://wiki.libsdl.org/SDL_GameControllerAddMapping
//            void ParseConfiguration(string configuration)
//            {
//                if (String.IsNullOrEmpty(configuration))
//                {
//                    throw new ArgumentNullException();
//                }

//                // The mapping string has the format "GUID,name,config"
//                // - GUID is a unigue identifier returned by Joystick.GetGuid()
//                // - name is a human-readable name for the controller
//                // - config is a comma-separated list of configurations as follows:
//                //   - [gamepad axis or button name]:[joystick axis, button or hat number]
//                string[] items = configuration.Split(ConfigurationSeparator, StringSplitOptions.RemoveEmptyEntries);
//                if (items.Length < 3)
//                {
//                    throw new ArgumentException();
//                }

//                GamePadConfiguration map = this;
//                map.Guid = new Guid(items[0]);
//                map.Name = items[1];
//                for (int i = 2; i < items.Length; i++)
//                {
//                    string[] config = items[i].Split(':');
//                    GamePadConfigurationTarget target = ParseTarget(config[0]);
//                    GamePadConfigurationSource source = ParseSource(config[1]);
//                    configuration_items.Add(new GamePadConfigurationItem(source, target));
//                }
//            }

//            static GamePadConfigurationTarget ParseTarget(string target)
//            {
//                switch (target)
//                {
//                    // Buttons
//                    case "a":
//                        return new GamePadConfigurationTarget(Buttons.A);
//                    case "b":
//                        return new GamePadConfigurationTarget(Buttons.B);
//                    case "x":
//                        return new GamePadConfigurationTarget(Buttons.X);
//                    case "y":
//                        return new GamePadConfigurationTarget(Buttons.Y);
//                    case "start":
//                        return new GamePadConfigurationTarget(Buttons.Start);
//                    case "back":
//                        return new GamePadConfigurationTarget(Buttons.Back);
//                    case "guide":
//                        return new GamePadConfigurationTarget(Buttons.Home);
//                    case "leftshoulder":
//                        return new GamePadConfigurationTarget(Buttons.LeftShoulder);
//                    case "rightshoulder":
//                        return new GamePadConfigurationTarget(Buttons.RightShoulder);
//                    case "leftstick":
//                        return new GamePadConfigurationTarget(Buttons.LeftStick);
//                    case "rightstick":
//                        return new GamePadConfigurationTarget(Buttons.RightStick);
//                    case "dpup":
//                        return new GamePadConfigurationTarget(Buttons.DPadUp);
//                    case "dpdown":
//                        return new GamePadConfigurationTarget(Buttons.DPadDown);
//                    case "dpleft":
//                        return new GamePadConfigurationTarget(Buttons.DPadLeft);
//                    case "dpright":
//                        return new GamePadConfigurationTarget(Buttons.DPadRight);

//                    // Axes
//                    case "leftx":
//                        return new GamePadConfigurationTarget(GamePadAxes.LeftX);
//                    case "lefty":
//                        return new GamePadConfigurationTarget(GamePadAxes.LeftY);
//                    case "rightx":
//                        return new GamePadConfigurationTarget(GamePadAxes.RightX);
//                    case "righty":
//                        return new GamePadConfigurationTarget(GamePadAxes.RightY);

//                    // Triggers
//                    case "lefttrigger":
//                        return new GamePadConfigurationTarget(GamePadAxes.LeftTrigger);
//                    case "righttrigger":
//                        return new GamePadConfigurationTarget(GamePadAxes.RightTrigger);


//                    // Unmapped
//                    default:
//                        return new GamePadConfigurationTarget();
//                }
//            }

//            static GamePadConfigurationSource ParseSource(string item)
//            {
//                if (String.IsNullOrEmpty(item))
//                {
//                    return new GamePadConfigurationSource();
//                }

//                switch (item[0])
//                {
//                    case 'a':
//                        return new GamePadConfigurationSource(ParseAxis(item));

//                    case 'b':
//                        return new GamePadConfigurationSource(ParseButton(item));

//                    case 'h':
//                        {
//                            //HatPosition position;
//                            //JoystickHat hat = ParseHat(item, out position);
//                            JoystickHat hat = JoystickHat.Hat0;
//                            HatPosition position = HatPosition.Centered;
//                            return new GamePadConfigurationSource(hat, position);
//                        }

//                    default:
//                        throw new InvalidOperationException("[Input] Invalid GamePad configuration value");
//                }
//            }

//            static JoystickAxis ParseAxis(string item)
//            {
//                // item is in the format "a#" where # a zero-based integer number
//                JoystickAxis axis = JoystickAxis.Axis0;
//                int id = Int32.Parse(item.Substring(1));
//                return axis + id;
//            }

//            static JoystickButton ParseButton(string item)
//            {
//                // item is in the format "b#" where # a zero-based integer number
//                JoystickButton button = JoystickButton.Button0;
//                int id = Int32.Parse(item.Substring(1));
//                return button + id;
//            }
            
//            #endregion
//        }

//        public struct GamePadTriggers : IEquatable<GamePadTriggers>
//        {
//            const float ConversionFactor = 1.0f / byte.MaxValue;
//            byte left;
//            byte right;

//            internal GamePadTriggers(byte left, byte right)
//            {
//                this.left = left;
//                this.right = right;
//            }

//            #region Public Members

//            /// <summary>
//            /// Gets the offset of the left trigger button, between 0.0 and 1.0.
//            /// </summary>
//            public float Left
//            {
//                get { return left * ConversionFactor; }
//            }

//            /// <summary>
//            /// Gets the offset of the left trigger button, between 0.0 and 1.0.
//            /// </summary>
//            public float Right
//            {
//                get { return right * ConversionFactor; }
//            }

//            /// <param name="left">A <see cref="GamePadTriggers"/> instance to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadTriggers"/> instance to test for equality.</param>
//            public static bool operator ==(GamePadTriggers left, GamePadTriggers right)
//            {
//                return left.Equals(right);
//            }

//            /// <param name="left">A <see cref="GamePadTriggers"/> instance to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadTriggers"/> instance to test for equality.</param>
//            public static bool operator !=(GamePadTriggers left, GamePadTriggers right)
//            {
//                return !left.Equals(right);
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadTriggers"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadTriggers"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "({0:f2}; {1:f2})",
//                    Left, Right);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadTriggers"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return
//                    left.GetHashCode() ^ right.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadTriggers"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadTriggers"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadTriggers"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadTriggers &&
//                    Equals((GamePadTriggers)obj);
//            }

//            #endregion

//            #region IEquatable<GamePadTriggers> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadTriggers"/> is equal to the current <see cref="OpenTK.Input.GamePadTriggers"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadTriggers"/> to compare with the current <see cref="OpenTK.Input.GamePadTriggers"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadTriggers"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadTriggers"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadTriggers other)
//            {
//                return
//                    left == other.left &&
//                    right == other.right;
//            }

//            #endregion
//        }

//        public struct GamePadButtons : IEquatable<GamePadButtons>
//        {
//            Buttons buttons;

//            /// <summary>
//            /// Initializes a new instance of the <see cref="OpenTK.Input.GamePadButtons"/> structure.
//            /// </summary>
//            /// <param name="state">A bitmask containing the button state.</param>
//            public GamePadButtons(Buttons state)
//            {
//                buttons = state;
//            }

//            #region Public Members

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the A button.
//            /// </summary>
//            public bool A
//            {
//                get { return GetButton(Buttons.A); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the B button.
//            /// </summary>
//            public bool B
//            {
//                get { return GetButton(Buttons.B); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the X button.
//            /// </summary>
//            public bool X
//            {
//                get { return GetButton(Buttons.X); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the Y button.
//            /// </summary>
//            public bool Y
//            {
//                get { return GetButton(Buttons.Y); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the Back button.
//            /// </summary>
//            public bool Back
//            {
//                get { return GetButton(Buttons.Back); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the big button.
//            /// This button is also known as Home or Guide.
//            /// </summary>
//            public bool Home
//            {
//                get { return GetButton(Buttons.Home); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the left shoulder button.
//            /// </summary>
//            public bool LeftShoulder
//            {
//                get { return GetButton(Buttons.LeftShoulder); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the left stick button.
//            /// This button represents a left stick that is pressed in.
//            /// </summary>
//            public bool LeftStick
//            {
//                get { return GetButton(Buttons.LeftStick); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the right shoulder button.
//            /// </summary>
//            public bool RightShoulder
//            {
//                get { return GetButton(Buttons.RightShoulder); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the right stick button.
//            /// This button represents a right stick that is pressed in.
//            /// </summary>
//            public bool RightStick
//            {
//                get { return GetButton(Buttons.RightStick); }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the starth button.
//            /// </summary>
//            public bool Start
//            {
//                get { return GetButton(Buttons.Start); }
//            }

//            /// <param name="left">A <see cref="GamePadButtons"/> instance to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadButtons"/> instance to test for equality.</param>
//            public static bool operator ==(GamePadButtons left, GamePadButtons right)
//            {
//                return left.Equals(right);
//            }

//            /// <param name="left">A <see cref="GamePadButtons"/> instance to test for inequality.</param>
//            /// <param name="right">A <see cref="GamePadButtons"/> instance to test for inequality.</param>
//            public static bool operator !=(GamePadButtons left, GamePadButtons right)
//            {
//                return !left.Equals(right);
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadButtons"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadButtons"/>.</returns>
//            public override string ToString()
//            {
//                System.Text.StringBuilder sb = new System.Text.StringBuilder();
//                if (A)
//                    sb.Append("A");
//                if (B)
//                    sb.Append("B");
//                if (X)
//                    sb.Append("X");
//                if (Y)
//                    sb.Append("Y");
//                if (Back)
//                    sb.Append("Bk");
//                if (Start)
//                    sb.Append("St");
//                if (Home)
//                    sb.Append("Gd");
//                if (Back)
//                    sb.Append("Bk");
//                if (LeftShoulder)
//                    sb.Append("L");
//                if (RightShoulder)
//                    sb.Append("R");
//                if (LeftStick)
//                    sb.Append("Ls");
//                if (RightStick)
//                    sb.Append("Rs");

//                return sb.ToString();
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadButtons"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return buttons.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadButtons"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadButtons"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadButtons"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadButtons &&
//                    Equals((GamePadButtons)obj);
//            }

//            #endregion

//            #region IEquatable<GamePadButtons> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadButtons"/> is equal to the current <see cref="OpenTK.Input.GamePadButtons"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadButtons"/> to compare with the current <see cref="OpenTK.Input.GamePadButtons"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadButtons"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadButtons"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadButtons other)
//            {
//                return buttons == other.buttons;
//            }

//            #endregion

//            #region Private Members

//            bool GetButton(Buttons b)
//            {
//                return (buttons & b) != 0;
//            }

//            #endregion
//        }

//        public struct GamePadDPad : IEquatable<GamePadDPad>
//        {
//            [Flags]
//            enum DPadButtons : byte
//            {
//                Up = Buttons.DPadUp,
//                Down = Buttons.DPadDown,
//                Left = Buttons.DPadLeft,
//                Right = Buttons.DPadRight
//            }

//            DPadButtons buttons;

//            #region Internal Members

//            internal GamePadDPad(Buttons state)
//            {
//                // DPad butons are stored in the lower 4bits
//                // of the Buttons enumeration.
//                buttons = (DPadButtons)((int)state & 0x0f);
//            }

//            #endregion

//            #region Public Members

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the up button.
//            /// </summary>
//            /// <value><c>ButtonState.Pressed</c> if the up button is pressed; otherwise, <c>ButtonState.Released</c>.</value>
//            public bool Up
//            {
//                get { return IsUp; }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the down button.
//            /// </summary>
//            /// <value><c>ButtonState.Pressed</c> if the down button is pressed; otherwise, <c>ButtonState.Released</c>.</value>
//            public bool Down
//            {
//                get { return IsDown; }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the left button.
//            /// </summary>
//            /// <value><c>ButtonState.Pressed</c> if the left button is pressed; otherwise, <c>ButtonState.Released</c>.</value>
//            public bool Left
//            {
//                get { return IsLeft; }
//            }

//            /// <summary>
//            /// Gets the <see cref="ButtonState"/> for the right button.
//            /// </summary>
//            /// <value><c>ButtonState.Pressed</c> if the right button is pressed; otherwise, <c>ButtonState.Released</c>.</value>
//            public bool Right
//            {
//                get { return IsRight; }
//            }

//            /// <summary>
//            /// Gets a value indicating whether the up button is pressed.
//            /// </summary>
//            /// <value><c>true</c> if the up button is pressed; otherwise, <c>false</c>.</value>
//            public bool IsUp
//            {
//                get { return (buttons & DPadButtons.Up) != 0; }
//                internal set { SetButton(DPadButtons.Up, value); }
//            }

//            /// <summary>
//            /// Gets a value indicating whether the down button is pressed.
//            /// </summary>
//            /// <value><c>true</c> if the down button is pressed; otherwise, <c>false</c>.</value>
//            public bool IsDown
//            {
//                get { return (buttons & DPadButtons.Down) != 0; }
//                internal set { SetButton(DPadButtons.Down, value); }
//            }

//            /// <summary>
//            /// Gets a value indicating whether the left button is pressed.
//            /// </summary>
//            /// <value><c>true</c> if the left button is pressed; otherwise, <c>false</c>.</value>
//            public bool IsLeft
//            {
//                get { return (buttons & DPadButtons.Left) != 0; }
//                internal set { SetButton(DPadButtons.Left, value); }
//            }

//            /// <summary>
//            /// Gets a value indicating whether the right button is pressed.
//            /// </summary>
//            /// <value><c>true</c> if the right button is pressed; otherwise, <c>false</c>.</value>
//            public bool IsRight
//            {
//                get { return (buttons & DPadButtons.Right) != 0; }
//                internal set { SetButton(DPadButtons.Right, value); }
//            }

//            /// <param name="left">A <see cref="GamePadDPad"/> instance to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadDPad"/> instance to test for equality.</param>
//            public static bool operator ==(GamePadDPad left, GamePadDPad right)
//            {
//                return left.Equals(right);
//            }

//            /// <param name="left">A <see cref="GamePadDPad"/> instance to test for inequality.</param>
//            /// <param name="right">A <see cref="GamePadDPad"/> instance to test for inequality.</param>
//            public static bool operator !=(GamePadDPad left, GamePadDPad right)
//            {
//                return !left.Equals(right);
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadDPad"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadDPad"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{{0}{1}{2}{3}}}",
//                    IsUp ? "U" : String.Empty,
//                    IsLeft ? "L" : String.Empty,
//                    IsDown ? "D" : String.Empty,
//                    IsRight ? "R" : String.Empty);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadDPad"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return buttons.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadDPad"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadDPad"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadDPad"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadDPad &&
//                    Equals((GamePadDPad)obj);
//            }

//            #endregion

//            #region Private Members

//            void SetButton(DPadButtons button, bool value)
//            {
//                if (value)
//                {
//                    buttons |= button;
//                }
//                else
//                {
//                    buttons &= ~button;
//                }
//            }

//            #endregion

//            #region IEquatable<GamePadDPad> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadDPad"/> is equal to the current <see cref="OpenTK.Input.GamePadDPad"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadDPad"/> to compare with the current <see cref="OpenTK.Input.GamePadDPad"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadDPad"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadDPad"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadDPad other)
//            {
//                return buttons == other.buttons;
//            }

//            #endregion
//        }

//        public struct GamePadThumbSticks : IEquatable<GamePadThumbSticks>
//        {
//            const float ConversionFactor = 1.0f / short.MaxValue;
//            short left_x, left_y;
//            short right_x, right_y;

//            internal GamePadThumbSticks(
//                short left_x, short left_y,
//                short right_x, short right_y)
//            {
//                this.left_x = left_x;
//                this.left_y = left_y;
//                this.right_x = right_x;
//                this.right_y = right_y;
//            }

//            #region Public Members

//            /// <summary>
//            /// Gets a <see cref="Vector2"/> describing the state of the left thumb stick.
//            /// </summary>
//            public Vector<float> Left
//            {
//                get { return new Vector<float>(left_x * ConversionFactor, left_y * ConversionFactor); }
//            }

//            /// <summary>
//            /// Gets a <see cref="Vector2"/> describing the state of the right thumb stick.
//            /// </summary>
//            public Vector<float> Right
//            {
//                get { return new Vector<float>(right_x * ConversionFactor, right_y * ConversionFactor); }
//            }

//            /// <param name="left">A <see cref="GamePadThumbSticks"/> instance to test for equality.</param>
//            /// <param name="right">A <see cref="GamePadThumbSticks"/> instance to test for equality.</param>
//            public static bool operator ==(GamePadThumbSticks left, GamePadThumbSticks right)
//            {
//                return left.Equals(right);
//            }

//            /// <param name="left">A <see cref="GamePadThumbSticks"/> instance to test for inequality.</param>
//            /// <param name="right">A <see cref="GamePadThumbSticks"/> instance to test for inequality.</param>
//            public static bool operator !=(GamePadThumbSticks left, GamePadThumbSticks right)
//            {
//                return !left.Equals(right);
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{Left: ({0:f4}; {1:f4}); Right: ({2:f4}; {3:f4})}}",
//                    Left.X, Left.Y, Right.X, Right.Y);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadThumbSticks"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return
//                    left_x.GetHashCode() ^ left_y.GetHashCode() ^
//                    right_x.GetHashCode() ^ right_y.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadThumbSticks"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadThumbSticks &&
//                    Equals((GamePadThumbSticks)obj);
//            }

//            #endregion

//            #region IEquatable<GamePadThumbSticks> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadThumbSticks"/> is equal to the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadThumbSticks"/> to compare with the current <see cref="OpenTK.Input.GamePadThumbSticks"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadThumbSticks"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadThumbSticks"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadThumbSticks other)
//            {
//                return
//                    left_x == other.left_x &&
//                    left_y == other.left_y &&
//                    right_x == other.right_x &&
//                    right_y == other.right_y;
//            }

//            #endregion
//        }

//        public struct GamePadState : IEquatable<GamePadState>
//        {
//            const float RangeMultiplier = 1.0f / (short.MaxValue + 1);

//            Buttons buttons;
//            int packet_number;
//            short left_stick_x;
//            short left_stick_y;
//            short right_stick_x;
//            short right_stick_y;
//            byte left_trigger;
//            byte right_trigger;
//            bool is_connected;

//            #region Public Members

//            /// <summary>
//            /// Gets a <see cref="GamePadThumbSticks"/> structure describing the
//            /// state of the <c>GamePad</c> thumb sticks.
//            /// </summary>
//            public GamePadThumbSticks ThumbSticks
//            {
//                get { return new GamePadThumbSticks(left_stick_x, left_stick_y, right_stick_x, right_stick_y); }
//            }

//            /// <summary>
//            /// Gets a <see cref="GamePadButtons"/> structure describing the
//            /// state of the <c>GamePad</c> buttons.
//            /// </summary>
//            public GamePadButtons Buttons
//            {
//                get { return new GamePadButtons(buttons); }
//            }

//            /// <summary>
//            /// Gets a <see cref="GamePadDPad"/> structure describing the
//            /// state of the <c>GamePad</c> directional pad.
//            /// </summary>
//            public GamePadDPad DPad
//            {
//                get { return new GamePadDPad(buttons); }
//            }

//            /// <summary>
//            /// Gets a <see cref="GamePadTriggers"/> structure describing the
//            /// state of the <c>GamePad</c> triggers.
//            /// </summary>
//            public GamePadTriggers Triggers
//            {
//                get { return new GamePadTriggers(left_trigger, right_trigger); }
//            }

//            /// <summary>
//            /// Gets a value indicating whether this <c>GamePad</c> instance is connected.
//            /// </summary>
//            /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
//            public bool IsConnected
//            {
//                get { return is_connected; }
//            }

//            /// <summary>
//            /// Gets the packet number for this <c>GamePadState</c> instance.
//            /// Use the packet number to determine whether the state of a
//            /// <c>GamePad</c> device has changed.
//            /// </summary>
//            public int PacketNumber
//            {
//                get { return packet_number; }
//            }

//            /// <summary>
//            /// Returns a <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadState"/>.
//            /// </summary>
//            /// <returns>A <see cref="System.String"/> that represents the current <see cref="OpenTK.Input.GamePadState"/>.</returns>
//            public override string ToString()
//            {
//                return String.Format(
//                    "{{Sticks: {0}; Triggers: {1}; Buttons: {2}; DPad: {3}; IsConnected: {4}}}",
//                    ThumbSticks, Triggers, Buttons, DPad, IsConnected);
//            }

//            /// <summary>
//            /// Serves as a hash function for a <see cref="OpenTK.Input.GamePadState"/> object.
//            /// </summary>
//            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
//            /// hash table.</returns>
//            public override int GetHashCode()
//            {
//                return
//                    ThumbSticks.GetHashCode() ^ Buttons.GetHashCode() ^
//                    DPad.GetHashCode() ^ IsConnected.GetHashCode();
//            }

//            /// <summary>
//            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="OpenTK.Input.GamePadState"/>.
//            /// </summary>
//            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="OpenTK.Input.GamePadState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadState"/>; otherwise, <c>false</c>.</returns>
//            public override bool Equals(object obj)
//            {
//                return
//                    obj is GamePadState &&
//                    Equals((GamePadState)obj);
//            }

//            #endregion

//            #region IEquatable<GamePadState> Members

//            /// <summary>
//            /// Determines whether the specified <see cref="OpenTK.Input.GamePadState"/> is equal to the current <see cref="OpenTK.Input.GamePadState"/>.
//            /// </summary>
//            /// <param name="other">The <see cref="OpenTK.Input.GamePadState"/> to compare with the current <see cref="OpenTK.Input.GamePadState"/>.</param>
//            /// <returns><c>true</c> if the specified <see cref="OpenTK.Input.GamePadState"/> is equal to the current
//            /// <see cref="OpenTK.Input.GamePadState"/>; otherwise, <c>false</c>.</returns>
//            public bool Equals(GamePadState other)
//            {
//                return
//                    ThumbSticks == other.ThumbSticks &&
//                    Buttons == other.Buttons &&
//                    DPad == other.DPad &&
//                    IsConnected == other.IsConnected;
//            }

//            #endregion

//            #region Internal Members

//            internal void SetAxis(GamePadAxes axis, short value)
//            {
//                if ((axis & GamePadAxes.LeftX) != 0)
//                {
//                    left_stick_x = value;
//                }

//                if ((axis & GamePadAxes.LeftY) != 0)
//                {
//                    left_stick_y = value;
//                }

//                if ((axis & GamePadAxes.RightX) != 0)
//                {
//                    right_stick_x = value;
//                }

//                if ((axis & GamePadAxes.RightY) != 0)
//                {
//                    right_stick_y = value;
//                }

//                if ((axis & GamePadAxes.LeftTrigger) != 0)
//                {
//                    // Adjust from [-32768, 32767] to [0, 255]
//                    left_trigger = (byte)((value - short.MinValue) >> 8);
//                }

//                if ((axis & GamePadAxes.RightTrigger) != 0)
//                {
//                    // Adjust from [-32768, 32767] to [0, 255]
//                    right_trigger = (byte)((value - short.MinValue) >> 8);
//                }
//            }

//            internal void SetButton(Buttons button, bool pressed)
//            {
//                if (pressed)
//                {
//                    buttons |= button;
//                }
//                else
//                {
//                    buttons &= ~button;
//                }
//            }

//            internal void SetConnected(bool connected)
//            {
//                is_connected = connected;
//            }

//            internal void SetTriggers(byte left, byte right)
//            {
//                left_trigger = left;
//                right_trigger = right;
//            }

//            internal void SetPacketNumber(int number)
//            {
//                packet_number = number;
//            }

//            #endregion

//            #region Private Members

//            bool IsAxisValid(GamePadAxes axis)
//            {
//                int index = (int)axis;
//                //return index >= 0 && index < GamePad.MaxAxisCount;
//                return index >= 0 && index < 10;
//            }

//            bool IsDPadValid(int index)
//            {
//                //return index >= 0 && index < GamePad.MaxDPadCount;
//                return index >= 0 && index < 2;
//            }

//            #endregion
//        }

//        class GamePadConfigurationDatabase
//        {
//            internal const string UnmappedName = "Unmapped Controller";

//            readonly Dictionary<Guid, string> Configurations = new Dictionary<Guid, string>();

//            internal GamePadConfigurationDatabase()
//            {
//                // Configuration database copied from SDL

//                #region License
//                // Simple DirectMedia Layer
//                // Copyright (C) 1997-2013 Sam Lantinga <slouken@libsdl.org>
//                //
//                // This software is provided 'as-is', without any express or implied
//                // warranty.  In no event will the authors be held liable for any damages
//                //    arising from the use of this software.
//                //
//                //    Permission is granted to anyone to use this software for any purpose,
//                //        including commercial applications, and to alter it and redistribute it
//                //        freely, subject to the following restrictions:
//                //
//                //       1. The origin of this software must not be misrepresented; you must not
//                // claim that you wrote the original software. If you use this software
//                // in a product, an acknowledgment in the product documentation would be
//                // appreciated but is not required.
//                // 2. Altered source versions must be plainly marked as such, and must not be
//                // misrepresented as being the original software.
//                // 3. This notice may not be removed or altered from any source distribution.
//                #endregion

//                //// Default (unknown) configuration
//                //if (!Configuration.RunningOnSdl2)
//                //{
//                //    Add("00000000000000000000000000000000,Unmapped Controller,a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b10,leftshoulder:b4,leftstick:b8,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b9,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                //}
//                //else
//                //{
//                //    // Old SDL2 mapping for XInput devices (pre SDL-2.0.4)
//                //    Add("00000000000000000000000000000000,XInput Controller,a:b10,b:b11,back:b5,dpdown:b1,dpleft:b2,dpright:b3,dpup:b0,guide:b14,leftshoulder:b8,leftstick:b6,lefttrigger:a4,leftx:a0,lefty:a1,rightshoulder:b9,rightstick:b7,righttrigger:a5,rightx:a3,righty:a2,start:b4,x:b12,y:b13,");
//                //}

//                // Windows - XInput
//                Add("78696e70757400000000000000000000,XInput Controller,a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b10,leftshoulder:b4,leftstick:b8,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b9,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");

//                // Windows
//                Add("341a3608000000000000504944564944,Afterglow PS3 Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,");
//                Add("ffff0000000000000000504944564944,GameStop Gamepad,a:b0,b:b1,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b2,y:b3,");
//                Add("6d0416c2000000000000504944564944,Generic DirectInput Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,");
//                Add("6d0419c2000000000000504944564944,Logitech F710 Gamepad,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,"); // Guide button doesn't seem to be sent in DInput mode.
//                Add("4d6963726f736f66742050432d6a6f79,OUYA Controller,a:b0,b:b3,dpdown:b9,dpleft:b10,dpright:b11,dpup:b8,guide:b14,leftshoulder:b4,leftstick:b6,lefttrigger:b12,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b7,righttrigger:b13,rightx:a5,righty:a4,x:b1,y:b2,");
//                Add("010906a3000000000000504944564944,P880 Controller,a:b2,b:b3,back:b4,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b10,leftshoulder:,leftstick:b8,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:,rightstick:b9,righttrigger:b7,rightx:a3,righty:a2,start:b5,x:b0,y:b1,");
//                Add("88880803000000000000504944564944,PS3 Controller,a:b2,b:b1,back:b8,dpdown:h0.8,dpleft:h0.4,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b9,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:b7,rightx:a3,righty:a4,start:b11,x:b0,y:b3,");
//                Add("4c056802000000000000504944564944,PS3 Controller,a:b14,b:b13,back:b0,dpdown:b6,dpleft:b7,dpright:b5,dpup:b4,guide:b16,leftshoulder:b10,leftstick:b1,lefttrigger:b8,leftx:a0,lefty:a1,rightshoulder:b11,rightstick:b2,righttrigger:b9,rightx:a2,righty:a3,start:b3,x:b15,y:b12,");
//                Add("25090500000000000000504944564944,PS3 DualShock,a:b2,b:b1,back:b9,dpdown:h0.8,dpleft:h0.4,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b6,leftstick:b10,lefttrigger:b4,leftx:a0,lefty:a1,rightshoulder:b7,rightstick:b11,righttrigger:b5,rightx:a2,righty:a3,start:b8,x:b0,y:b3,");
//                Add("4c05c405000000000000504944564944,PS4 Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b10,lefttrigger:a3,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:a4,rightx:a2,righty:a5,start:b9,x:b0,y:b3,");

//                // Mac OS X
//                Add("0500000047532047616d657061640000,GameStop Gamepad,a:b0,b:b1,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b2,y:b3,");
//                Add("6d0400000000000016c2000000000000,Logitech F310 Gamepad (DInput),a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,"); // Guide button doesn't seem to be sent in DInput mode.
//                Add("6d0400000000000018c2000000000000,Logitech F510 Gamepad (DInput),a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,");
//                Add("6d040000000000001fc2000000000000,Logitech F710 Gamepad (XInput),a:b0,b:b1,back:b9,dpdown:b12,dpleft:b13,dpright:b14,dpup:b11,guide:b10,leftshoulder:b4,leftstick:b6,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b7,righttrigger:a5,rightx:a3,righty:a4,start:b8,x:b2,y:b3,");
//                Add("6d0400000000000019c2000000000000,Logitech Wireless Gamepad (DInput),a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,"); // This includes F710 in DInput mode and the "Logitech Cordless RumblePad 2", at the very least.
//                Add("4c050000000000006802000000000000,PS3 Controller,a:b14,b:b13,back:b0,dpdown:b6,dpleft:b7,dpright:b5,dpup:b4,guide:b16,leftshoulder:b10,leftstick:b1,lefttrigger:b8,leftx:a0,lefty:a1,rightshoulder:b11,rightstick:b2,righttrigger:b9,rightx:a2,righty:a3,start:b3,x:b15,y:b12,");
//                Add("4c05000000000000c405000000000000,PS4 Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b10,lefttrigger:a3,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:a4,rightx:a2,righty:a5,start:b9,x:b0,y:b3,");
//                Add("5e040000000000008e02000000000000,X360 Controller,a:b0,b:b1,back:b9,dpdown:b12,dpleft:b13,dpright:b14,dpup:b11,guide:b10,leftshoulder:b4,leftstick:b6,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b7,righttrigger:a5,rightx:a3,righty:a4,start:b8,x:b2,y:b3,");

//                // Linux
//                Add("0500000047532047616d657061640000,GameStop Gamepad,a:b0,b:b1,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b2,y:b3,");
//                Add("03000000ba2200002010000001010000,Jess Technology USB Game Controller,a:b2,b:b1,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b4,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,righttrigger:b7,rightx:a3,righty:a2,start:b9,x:b3,y:b0,");
//                Add("030000006d04000019c2000010010000,Logitech Cordless RumblePad 2,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,");
//                Add("030000006d0400001dc2000014400000,Logitech F310 Gamepad (XInput),a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000006d0400001ec2000020200000,Logitech F510 Gamepad (XInput),a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000006d04000019c2000011010000,Logitech F710 Gamepad (DInput),a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b4,leftstick:b10,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b0,y:b3,"); // Guide button doesn't seem to be sent in DInput mode.
//                Add("030000006d0400001fc2000005030000,Logitech F710 Gamepad (XInput),a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("05000000362000010000000201000000,OUYA Game Controller,a:b0,b:b3,dpdown:b9,dpleft:b10,dpright:b11,dpup:b8,guide:b14,leftshoulder:b4,leftstick:b6,lefttrigger:a2,leftx:a0,lefty:a1,platform:Linux,rightshoulder:b5,rightstick:b7,righttrigger:a5,rightx:a3,righty:a4,x:b1,y:b2,");
//                Add("030000004c0500006802000011010000,PS3 Controller,a:b14,b:b13,back:b0,dpdown:b6,dpleft:b7,dpright:b5,dpup:b4,guide:b16,leftshoulder:b10,leftstick:b1,lefttrigger:b8,leftx:a0,lefty:a1,rightshoulder:b11,rightstick:b2,righttrigger:b9,rightx:a2,righty:a3,start:b3,x:b15,y:b12,");
//                Add("030000004c050000c405000011010000,PS4 Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b10,lefttrigger:a3,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:a4,rightx:a2,righty:a5,start:b9,x:b0,y:b3,");
//                Add("050000004c050000c405000000010000,PS4 Controller,a:b1,b:b2,back:b8,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b12,leftshoulder:b4,leftstick:b10,lefttrigger:a3,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b11,righttrigger:a4,rightx:a2,righty:a5,start:b9,x:b0,y:b3,");
//                Add("03000000de280000ff11000001000000,Valve Streaming Gamepad,a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000005e0400008e02000014010000,X360 Controller,a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000005e0400008e02000010010000,X360 Controller,a:b0,b:b1,back:b6,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000005e0400001907000000010000,X360 Wireless Controller,a:b0,b:b1,back:b6,dpdown:b14,dpleft:b11,dpright:b12,dpup:b13,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("030000005e0400009102000007010000,X360 Wireless Controller,a:b0,b:b1,back:b6,dpdown:b14,dpleft:b11,dpright:b12,dpup:b13,guide:b8,leftshoulder:b4,leftstick:b9,lefttrigger:a2,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b10,righttrigger:a5,rightx:a3,righty:a4,start:b7,x:b2,y:b3,");
//                Add("000000006258786f2033363020576972,X360 Wireless Controller,a:b0,b:b1,back:b8,dpdown:b16,dpleft:b13,dpright:b14,dpup:b15,guide:b10,leftshoulder:b4,leftstick:b11,lefttrigger:b6,leftx:a0,lefty:a1,rightshoulder:b5,rightstick:b12,righttrigger:b7,rightx:a2,righty:a3,start:b9,x:b2,y:b3,");

//                // Android
//                Add("4e564944494120436f72706f72617469,NVIDIA Controller,a:b0,b:b1,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,dpup:h0.1,leftshoulder:b9,leftstick:b7,lefttrigger:a4,leftx:a0,lefty:a1,rightshoulder:b10,rightstick:b8,righttrigger:a5,rightx:a2,righty:a3,start:b6,x:b2,y:b3,");

//            }

//            internal void Add(string config)
//            {
//                Guid guid = new Guid(config.Substring(0, 32));
//                if (!Configurations.ContainsKey(guid))
//                {
//                    Configurations.Add(guid, config);
//                }
//                else
//                {
//                    Configurations[guid] = config;
//                }
//            }

//            internal string this[Guid guid]
//            {
//                get
//                {
//                    if (Configurations.ContainsKey(guid))
//                    {
//                        return Configurations[guid];
//                    }
//                    else
//                    {
//                        return Configurations[new Guid()]; // default configuration
//                    }
//                }
//            }
//        }

//        class MappedGamePadDriver
//        {
//            readonly GamePadConfigurationDatabase database = new GamePadConfigurationDatabase();
//            readonly Dictionary<Guid, GamePadConfiguration> configurations = new Dictionary<Guid, GamePadConfiguration>();

//            public GamePadState GetState(int index)
//            {
//                JoystickState joy = Joystick.GetState(index);
//                GamePadState pad = new GamePadState();

//                if (joy.IsConnected)
//                {
//                    pad.SetConnected(true);
//                    pad.SetPacketNumber(joy.PacketNumber);

//                    GamePadConfiguration configuration = GetConfiguration(Joystick.GetGuid(index));

//                    foreach (GamePadConfigurationItem map in configuration)
//                    {
//                        switch (map.Source.Type)
//                        {
//                            case ConfigurationType.Axis:
//                                {
//                                    // JoystickAxis -> Buttons/GamePadAxes mapping
//                                    JoystickAxis source_axis = map.Source.Axis;
//                                    short value = joy.GetAxisRaw(source_axis);

//                                    switch (map.Target.Type)
//                                    {
//                                        case ConfigurationType.Axis:
//                                            pad.SetAxis(map.Target.Axis, value);
//                                            break;

//                                        case ConfigurationType.Button:
//                                            // Todo: if SDL2 GameController config is ever updated to
//                                            // distinguish between negative/positive axes, then remove
//                                            // Math.Abs below.
//                                            // Button is considered press when the axis is >= 0.5 from center
//                                            pad.SetButton(map.Target.Button, Math.Abs(value) >= short.MaxValue >> 1);
//                                            break;
//                                    }
//                                }
//                                break;

//                            case ConfigurationType.Button:
//                                {
//                                    // JoystickButton -> Buttons/GamePadAxes mapping
//                                    JoystickButton source_button = map.Source.Button;
//                                    bool pressed = joy.GetButton(source_button) == true;

//                                    switch (map.Target.Type)
//                                    {
//                                        case ConfigurationType.Axis:
//                                            // Todo: if SDL2 GameController config is ever updated to
//                                            // distinguish between negative/positive axes, then update
//                                            // the following line to support both.
//                                            short value = pressed ?
//                                                short.MaxValue :
//                                                (map.Target.Axis & (GamePadAxes.LeftTrigger | GamePadAxes.RightTrigger)) != 0 ?
//                                                    short.MinValue :
//                                                    (short)0;
//                                            pad.SetAxis(map.Target.Axis, value);
//                                            break;

//                                        case ConfigurationType.Button:
//                                            pad.SetButton(map.Target.Button, pressed);
//                                            break;
//                                    }
//                                }
//                                break;

//                            case ConfigurationType.Hat:
//                                {
//                                    // JoystickHat -> Buttons/GamePadAxes mapping
//                                    JoystickHat source_hat = map.Source.Hat;
//                                    JoystickHatState state = joy.GetHat(source_hat);

//                                    bool pressed = false;
//                                    switch (map.Source.HatPosition)
//                                    {
//                                        case HatPosition.Down:
//                                            pressed = state.IsDown;
//                                            break;

//                                        case HatPosition.Up:
//                                            pressed = state.IsUp;
//                                            break;

//                                        case HatPosition.Left:
//                                            pressed = state.IsLeft;
//                                            break;

//                                        case HatPosition.Right:
//                                            pressed = state.IsRight;
//                                            break;
//                                    }

//                                    switch (map.Target.Type)
//                                    {
//                                        case ConfigurationType.Axis:
//                                            // Todo: if SDL2 GameController config is ever updated to
//                                            // distinguish between negative/positive axes, then update
//                                            // the following line to support both.
//                                            short value = pressed ?
//                                                short.MaxValue :
//                                                (map.Target.Axis & (GamePadAxes.LeftTrigger | GamePadAxes.RightTrigger)) != 0 ?
//                                                    short.MinValue :
//                                                    (short)0;
//                                            pad.SetAxis(map.Target.Axis, value);
//                                            break;

//                                        case ConfigurationType.Button:
//                                            pad.SetButton(map.Target.Button, pressed);
//                                            break;
//                                    }
//                                }
//                                break;
//                        }
//                    }
//                }

//                return pad;
//            }

//            public GamePadCapabilities GetCapabilities(int index)
//            {
//                JoystickCapabilities joy = Joystick.GetCapabilities(index);
//                GamePadCapabilities pad;
//                if (joy.IsConnected)
//                {
//                    GamePadConfiguration configuration = GetConfiguration(Joystick.GetGuid(index));
//                    GamePadAxes mapped_axes = 0;
//                    Buttons mapped_buttons = 0;

//                    foreach (GamePadConfigurationItem map in configuration)
//                    {
//                        switch (map.Target.Type)
//                        {
//                            case ConfigurationType.Axis:
//                                mapped_axes |= map.Target.Axis;
//                                break;

//                            case ConfigurationType.Button:
//                                mapped_buttons |= map.Target.Button;
//                                break;
//                        }
//                    }

//                    pad = new GamePadCapabilities(
//                        GamePadType.GamePad, // Todo: detect different types
//                        mapped_axes,
//                        mapped_buttons,
//                        joy.IsConnected,
//                        configuration.Name != GamePadConfigurationDatabase.UnmappedName);
//                }
//                else
//                {
//                    pad = new GamePadCapabilities();
//                }
//                return pad;
//            }

//            public string GetName(int index)
//            {
//                JoystickCapabilities joy = Joystick.GetCapabilities(index);
//                string name = String.Empty;
//                if (joy.IsConnected)
//                {
//                    GamePadConfiguration map = GetConfiguration(Joystick.GetGuid(index));
//                    name = map.Name;
//                }
//                return name;
//            }

//            public bool SetVibration(int index, float left, float right)
//            {
//                return false;
//            }

//            #region Private Members

//            GamePadConfiguration GetConfiguration(Guid guid)
//            {
//                if (!configurations.ContainsKey(guid))
//                {
//                    string config = database[guid];
//                    GamePadConfiguration map = new GamePadConfiguration(config);
//                    configurations.Add(guid, map);
//                }
//                return configurations[guid];
//            }

//            bool IsMapped(GamePadConfigurationSource item)
//            {
//                return item.Type != ConfigurationType.Unmapped;
//            }

//            #endregion
//        }

//        #endregion

//        private Vector<float> _leftThumbstickPosition;
//        private Vector<float> _rightThumbstickPosition;

//        static readonly MappedGamePadDriver driver = new MappedGamePadDriver();

//        private GamePad() { }

//        public static GamePadCapabilities GetCapabilities(int index)
//        {
//            if (index < 0)
//                throw new IndexOutOfRangeException();

//            return driver.GetCapabilities(index);
//        }

//        public static GamePadState GetState(int index)
//        {
//            return driver.GetState(index);
//        }

//        public static bool SetVibration(int index, float left, float right)
//        {
//            return driver.SetVibration(index, left, right);
//        }

//        public static string GetName(int index)
//        {
//            return driver.GetName(index);
//        }
//    }
//}
