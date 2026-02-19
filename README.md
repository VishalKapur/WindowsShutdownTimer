**Windows Shutdown Timer**

A simple Windows desktop application that allows users to schedule a system shutdown after a specified time.

The application replaces the need to use Command Prompt or Task Scheduler with a modern, user-friendly interface.

#

**Features:**

- Manual Mode:
  
  - Set a shutdown timer using hours and minutes
 
  - Live countdown display
    
  - Cancel countdown at any time
    
  - Restart the countdown anytime with current or new time


- Automatic Folder Tracking Mode
  
  - Track a specfic folder's (and its subfolders) activity to shutdown when idle for 10 minutes
    
  - Useful for automatically shutting down when a large download finishes
    
  - Reset monioring anytime with current or new folder

#

**Motivation:**


This solution was built to solve a simple problem in Windows: scheduling a system shutdown in a user-friendly way.

An example use case is shutting down after a large download has completed. 

#

**Technologies Used:**

- C#

- XAML

- WPF (.NET 8)

#

**How to Use:**


- Manual Mode:
  
1. Enter the number of hours and minutes for the desired countdown
   
2. Press "Start" to begin the countdown
   
3. Device shuts down when the countdown reaches 00:00:00
   
4. Press "Cancel" at any time to stop the countdown

- Automatic Folder Tracking Mode

1. Select a folder to monitor
    
2. Press "Start" to begin tracking the folder's activity
    
3. When the folder has been inactive for at least 10 minutes, device will shutdown in 60 seconds

4. Press "Cancel" at any time to stop moniotring and to stop a pending shutdown

#

**Disclaimer:**


This application triggers a system shutdown using Windows system commands.

Use with caution and ensure all work is saved before shutdown is activated.

#

<img width="781" height="439" alt="image" src="https://github.com/user-attachments/assets/cc65f9b7-dcdd-4aae-8850-0634a88f025b" />

