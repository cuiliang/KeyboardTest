using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput.Native;

namespace Quicker.Utilities
{
    public static class KeyboardHelper
    {

        /// <summary>
        /// 用于获取字符的virtual key code
        /// https://stackoverflow.com/questions/2934557/convert-character-to-the-corresponding-virtual-key-code
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        struct Helper
        {
            [FieldOffset(0)] public short Value;
            [FieldOffset(0)] public readonly byte Low;
            [FieldOffset(1)] public readonly byte High;
        }






        /// <summary>
        /// 获取按键状态
        /// https://stackoverflow.com/questions/10484085/get-all-keys-that-are-pressed
        /// var array = new byte[256];
        /// GetKeyboardState(array);
        /// </summary>
        /// <param name="lpKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetKeyboardState(byte[] lpKeyState);


        ///// <summary>
        ///// Gets all keys that are currently in the down state.
        ///// </summary>
        ///// <returns>
        ///// A collection of all keys that are currently in the down state.
        ///// </returns>
        //public static IEnumerable<VirtualKeyCode> GetDownKeys()
        //{
        //    var keyboardState = new byte[256];
        //    GetKeyboardState(keyboardState);

        //    var downKeys = new List<VirtualKeyCode>();
        //    for (var index = 0; index < DistinctVirtualKeys.Length; index++)
        //    {
        //        var virtualKey = DistinctVirtualKeys[index];
        //        if ((keyboardState[virtualKey] & 0x80) != 0)
        //        {
        //            downKeys.Add(KeyInterop.KeyFromVirtualKey(virtualKey));
        //        }
        //    }

        //    return downKeys;
        //}


        [DllImport("user32.dll", SetLastError = true)]

        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short GetKeyState(int keyCode);


        /// <summary>
        /// 有任何键按下么？
        /// </summary>
        /// <returns></returns>
        public static bool IsAnyKeyDown()
        {
            for (int i = 7; i <= (int)VirtualKeyCode.OEM_CLEAR; i++)
            {
                if ((GetAsyncKeyState(i) & 0b10000000_00000000) != 0)
                {
                    return true;
                }
            }

            return false;
            //var keyboardState = new byte[256];
            //GetKeyboardState(keyboardState);

            //for (int i = 0; i < 256; i++)
            //{
            //    if ((keyboardState[i] & 0x80) != 0)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        /// <summary>
        /// 某个键是否按下了
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyDown(VirtualKeyCode key)
        {
            return (GetAsyncKeyState((int)key) & 0b10000000_00000000) != 0;
        }

        /// <summary>
        /// 将键值数字、16禁止或键名转换为Keys值
        /// </summary>
        /// <param name="valueOrName"></param>
        /// <returns></returns>
        public static Keys KeyFromValueOrName(string valueOrName)
        {
            if (string.IsNullOrEmpty(valueOrName))
            {
                throw new ArgumentException("键值为空。");
            }

            if (valueOrName.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                int keyV = Convert.ToInt32(valueOrName, 16);
                return (Keys)keyV;
            }
            else if (int.TryParse(valueOrName, out int keyV))
            {
                // 从数字解析
                return (Keys)keyV;
            }
            else
            {
                if (Enum.TryParse(valueOrName, out Keys virtualKeyCode))
                {
                    return virtualKeyCode;
                }
                else
                {
                    throw new ArgumentException("无法识别的键值：" + valueOrName);
                }
            }
        }

       
    }
}
