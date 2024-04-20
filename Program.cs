using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
    private bool isCupFull = true;
    private Icon fullCupIcon;
    private Icon emptyCupIcon;

    public CoffeeApp()
    {
        fullCupIcon = CreateCoffeeCupIcon(true);  // Für volle Tasse
        emptyCupIcon = CreateCoffeeCupIcon(false); // Für leere Tasse

        trayIcon = new NotifyIcon();
        trayIcon.Text = "CoffeeApp";
        trayIcon.Icon = fullCupIcon; // Start with the full cup icon
        trayIcon.Visible = true;

        trayIcon.MouseClick += TrayIcon_MouseClick;

        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
    }

    private Icon CreateCoffeeCupIcon(bool isFull)
    {
        int size = 16;
        Bitmap bmp = new Bitmap(size, size);
        Color handleColor = Color.SaddleBrown;
        Color cupColor = isFull ? Color.SaddleBrown : Color.White; // Dunkelbraun für voll, Weiß für leer
        Color coffeeColor = Color.Black; // Kaffee nur sichtbar, wenn voll

        using (Graphics g = Graphics.FromImage(bmp))
        {
            // Clear the image with white (empty cup)
            g.Clear(Color.White);

            // Draw the cup
            for (int x = 4; x <= 12; x++)
            {
                for (int y = 4; y <= 12; y++)
                {
                    if (x >= 5 && x <= 11 && y >= 5 && y <= 11)
                    {
                        if (isFull && y > 7) // Fill with coffee color if full
                            bmp.SetPixel(x, y, coffeeColor);
                        else
                            bmp.SetPixel(x, y, cupColor);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, cupColor);
                    }
                }
            }

            // Draw the handle
            if (size > 11)
            {
                bmp.SetPixel(13, 8, handleColor);
                bmp.SetPixel(13, 9, handleColor);
                bmp.SetPixel(12, 10, handleColor);
            }
        }

        return Icon.FromHandle(bmp.GetHicon());
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
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }
        else
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        Visible = false; // Hide form window.
        ShowInTaskbar = false; // Remove from taskbar.
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
