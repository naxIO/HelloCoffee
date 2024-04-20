# HelloCoffee: Keep Your Computer Awake

**HelloCoffee** is a minimalistic Windows application designed to reside quietly in the system tray, represented by a simple coffee cup icon. This application allows users to prevent their computer from entering sleep mode or activating the screensaver with just a simple click. When the coffee cup icon is full, it indicates that sleep and screensaver functions are disabled, keeping the computer awake and active. Clicking the icon empties the cup and allows the computer to engage sleep mode and other power-saving features.

## Key Features

- **Tray-Based Interface:** CoffeeApp does not utilize a traditional user interface but is fully operated from the system tray for convenience and minimal disruption.
- **One-Click Activation:** Toggle the computer's sleep and power-saving settings simply by clicking the coffee cup icon. A full cup means the computer will stay awake, while an empty cup allows it to rest.
- **Visual Feedback:** The icon changes between a full brown coffee cup and an empty white cup, giving clear visual cues about the current mode.
- **Auto-Start:** On launching the application, the coffee cup is automatically full, ensuring that your computer stays awake until you decide otherwise.

## How It Works

CoffeeApp leverages the Windows API to control the system's power state. By setting the execution state to prevent sleep and screensaver activation when the cup is full, it ensures that your system remains active for tasks that require continuous operation.

## Getting Started

To run CoffeeApp, simply start the executable, and a coffee cup icon will appear in your system tray. Click the icon to toggle between keeping your computer awake and allowing it to enter power-saving modes. Right-click the icon and select "Exit" to close the application.

This application is perfect for users who need their computer to remain active for long periods, such as for downloading large files, performing long-running computations, or ensuring uninterrupted access during remote sessions.
