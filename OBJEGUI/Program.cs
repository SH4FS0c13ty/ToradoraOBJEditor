using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OBJEGUI {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OBJEGUI obj = new OBJEGUI();
            if (args.Length != 0) {
                obj.arg1 = args[0];
                obj.arg2 = args[1];
                obj.arg3 = args[2];
                obj.Hide();
            }
            Application.Run(obj);
        }
    }
}
