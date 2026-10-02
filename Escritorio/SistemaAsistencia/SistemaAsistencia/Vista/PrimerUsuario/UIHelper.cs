using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaAsistencia.Vista
{
    public static class UIHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        public static void EstablecerPlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox != null && !textBox.IsDisposed && textBox.IsHandleCreated)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 1, placeholder);
            }
        }
    }
}