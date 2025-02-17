using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bedrockix.Minecraft;

sealed class Form : System.Windows.Forms.Form
{
    internal Form()
    {
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
            AutoSizeMode = AutoSizeMode.GrowAndShrink
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
            Text = "Debug: Off",
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
            await Task.Run(() =>
            {
                if (!Game.Running) Invoke(() => tableLayoutPanel.Enabled = false);
                Game.Launch();
            });
            tableLayoutPanel.Enabled = true;
        };

        button2.Click += async (_, _) => await Task.Run(Game.Terminate);

        bool value = default;
        button3.Click += async (_, _) => await Task.Run(() =>
        {
            if (Game.Running && value) return;
            Invoke(() => button3.Text = (value = !value) ? "Debug: On" : "Debug: Off");
            Game.Debug = value;
        });

        button4.Click += (_, _) =>
        {
            if (dialog.ShowDialog() != DialogResult.OK) return;
        };


        tableLayoutPanel.Controls.Add(button1, 0, 0);
        tableLayoutPanel.Controls.Add(button2, 0, 1);
        tableLayoutPanel.Controls.Add(button3, 0, 2);
        tableLayoutPanel.Controls.Add(button4, 0, 3);
        tableLayoutPanel.Controls.Add(new Control() { Dock = DockStyle.Fill }, 0, -1);

        Application.ThreadExit += (_, _) =>
        {
            Game.Terminate();
            Game.Debug = default;
        };
    }
}