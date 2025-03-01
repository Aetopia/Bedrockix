using System;
using System.Drawing;
using Bedrockix.Minecraft;
using System.Windows.Forms;
using System.Threading.Tasks;

sealed class Form : System.Windows.Forms.Form
{
    internal Form()
    {
        Application.ThreadException += (_, e) =>
        {
            var exception = e.Exception;
            while (exception.InnerException != default) exception = exception.InnerException;
            MessageBox.Show(this, exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.ExitThread();
        };

        Text = "Bedrockix Client";
        Font = SystemFonts.MessageBoxFont;
        MinimizeBox = MaximizeBox = default;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;

        OpenFileDialog dialog = new()
        {
            Multiselect = true,
            CheckFileExists = true,
            CheckPathExists = true,
            Filter = "Dynamic Link Libraries (*.dll)|*.dll"
        };

        TableLayoutPanel tableLayoutPanel = new()
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
        };
        Controls.Add(tableLayoutPanel);

        Button button1 = new()
        {
            Text = "Launch",
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        Button button2 = new()
        {
            Text = "Terminate",
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        Button button3 = new()
        {
            Text = "Debug: ❔",
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        Button button4 = new()
        {
            Text = "Load",
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        button1.Click += async (sender, _) =>
        {
            if (!Game.Installed) return;

            if (!(await Task.Run(() =>
            {
                if (!Game.Running) Invoke(() => tableLayoutPanel.Enabled = false);
                return Game.Launch();
            })).HasValue) MessageBox.Show(this, "Minecraft: Bedrock Edition failed to launch!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            tableLayoutPanel.Enabled = true;
        };

        button2.Click += async (_, _) => { if (Game.Installed) await Task.Run(Game.Terminate); };

        bool value = default;
        button3.Click += async (_, _) =>
        {
            if (!Game.Installed) return;

            await Task.Run(() =>
            {
                if (Game.Running && value) return;
                Invoke(() => button3.Text = (value = !value) ? "Debug: ✔️" : "Debug: ❌");
                Game.Debug = value;
            });
        };

        button4.Click += async (_, _) =>
        {
            if (!Game.Installed || dialog.ShowDialog() != DialogResult.OK) return;

            tableLayoutPanel.Enabled = false;

            if (!(await Task.Run(() => Loader.Launch(dialog.FileNames))).HasValue)
                MessageBox.Show(this, "Minecraft: Bedrock Edition failed to launch!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            tableLayoutPanel.Enabled = true;
        };

        tableLayoutPanel.Controls.Add(button1, 0, 0);
        tableLayoutPanel.Controls.Add(button2, 0, 1);
        tableLayoutPanel.Controls.Add(button3, 0, 2);
        tableLayoutPanel.Controls.Add(button4, 0, 3);
        tableLayoutPanel.Controls.Add(new Control() { Dock = DockStyle.Fill }, 0, -1);

        Application.ThreadExit += (_, _) =>
        {
            if (!Game.Installed) return;
            Game.Terminate();
            Game.Debug = default;
        };
    }
}