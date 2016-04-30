using System;
using System.Windows.Forms;

namespace SiteParser
{
    public static class ControlHelper
    {
        public static void InvokeEx(this Control control, Delegate method, params object[] param)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(method, param);
            }
            else
            {
                method.DynamicInvoke(param);
            }
        }
    }
}