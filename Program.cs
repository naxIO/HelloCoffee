using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

public class CoffeeApp : Form
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    private NotifyIcon trayIcon;
    private ContextMenuStrip trayMenu;
    private bool isCupFull = true;
    private Icon fullCupIcon;
    private Icon emptyCupIcon;

    public CoffeeApp()
    {
        fullCupIcon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("HelloCoffee.coffee-7-16.ico"));
        emptyCupIcon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("HelloCoffee.coffee-8-16.ico"));

        trayIcon = new NotifyIcon();
        trayIcon.Text = "CoffeeApp";
        trayIcon.Icon = fullCupIcon;
        trayIcon.Visible = true;

        trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Exit", null, OnExit);
        trayIcon.ContextMenuStrip = trayMenu;

        trayIcon.MouseClick += TrayIcon_MouseClick;
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
    }

    private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ToggleCup();
        }
    }

    private void ToggleCup()
    {
        isCupFull = !isCupFull;
        trayIcon.Icon = isCupFull ? fullCupIcon : emptyCupIcon;
        if (isCupFull)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        }
        else
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    private void OnExit(object sender, EventArgs e)
    {
        Application.Exit();
    }

    protected override void OnLoad(EventArgs e)
    {
        Visible = false;
        ShowInTaskbar = false;
        base.OnLoad(e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            trayIcon.Dispose();
            fullCupIcon.Dispose();
            emptyCupIcon.Dispose();
        }
        base.Dispose(disposing);
    }

    [STAThread]
    static void Main()
    {
        Application.Run(new CoffeeApp());
    }
}
