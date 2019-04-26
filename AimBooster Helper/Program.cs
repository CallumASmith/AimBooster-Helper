using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AimBooster_Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(new ThreadStart(KeyboardHook.Start)).Start();
            while (true)
            {
                if (Settings.status == true)
                {
                    var target = Detect.FindBest(Detect.FindPixels(Detect.GetPixels()));
                    if (target.HasValue)
                    {
                        Click.Point(target.Value);
                    }
                }
                else
                {
                    Thread.Sleep(55);
                }
            }

        }
    }
}
