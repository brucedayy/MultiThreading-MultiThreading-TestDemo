using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestDemo
{
    public static class ControlExtensions
    {
        public static void InvokeIfNeeded(this System.Windows.Threading.DispatcherObject ctl,
            Action doit,
            System.Windows.Threading.DispatcherPriority priority)
        {
            if (System.Threading.Thread.CurrentThread != ctl.Dispatcher.Thread)
            {
                ctl.Dispatcher.Invoke(priority, doit);
            }
            else
            {
                doit();
            }
        }

        public static void InvokeIfNeeded<T>(this System.Windows.Threading.DispatcherObject ctl,
            Action<T> doit,
            T args,
            System.Windows.Threading.DispatcherPriority priority)
        {
            if (System.Threading.Thread.CurrentThread != ctl.Dispatcher.Thread)
            {
                ctl.Dispatcher.Invoke(priority, doit, args);
            }
            else
            {
                doit(args);
            }
        }
    }
}
