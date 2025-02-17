using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetColorMode(SystemColorMode.System);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetCompatibleTextRenderingDefault(default);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
        Application.Run(new Form());
    }

}