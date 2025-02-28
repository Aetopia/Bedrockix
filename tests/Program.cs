using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(default);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
        Application.Run(new Form());
    }
}