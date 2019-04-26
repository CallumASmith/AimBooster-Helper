using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AimBooster_Helper
{
    public class KeyboardHook
    {

        public static void Start()
        {
            ApplicationContext msgLoop = new ApplicationContext();
            Action startCheating = () => { Settings.status = true; };
            Action stopCheating = () => { Settings.status = false; };
            var assignment = new Dictionary<Combination, Action>
            {
                { Combination.FromString("Control+C"), startCheating },
                { Combination.FromString("Control+V"), stopCheating}
            };
            Hook.GlobalEvents().OnCombination(assignment);
            Application.Run(msgLoop);
        }
    }
}
