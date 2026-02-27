**Windows Shutdown Timer**

A simple Windows desktop application that allows users to schedule a system shutdown after a specified time.

The application replaces the need to use Command Prompt or Task Scheduler with a modern, user-friendly interface.

#

**Features:**

- Manual Mode:
  
  - Set a shutdown timer using hours and minutes
 
  - Live countdown display
    
  - Cancel countdown at any time
    
  - Restart the countdown at anytime with current or new time


- Automatic Folder Tracking Mode
  
  - Track a specfic folder's (and its subfolders) activity to shutdown when it is idle
 
  - Set the threshold for how long to wait to shutdown when a folder is inactive
    
  - Useful for automatically shutting down when a large download finishes
    
  - Reset monioring at anytime with current or new folder

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
  
  - Enter the number of hours and minutes for the desired countdown
   
  - Press "Start" to begin the countdown
   
  - Device shuts down when the countdown reaches 00:00:00
   
  - Press "Cancel" at any time to stop the countdown


- Automatic Folder Tracking Mode

  - Enter the threshold for how many minutes to wait after a folder is inactive to shutdown (10 minutes recommended for large downloads)

  - Select a folder to monitor
    
  - Press "Start" to begin tracking the folder's activity
    
  - When the folder has been inactive for at least 10 minutes, device will shutdown in 60 seconds

  - Press "Cancel" at any time to stop moniotring and to stop a pending shutdown

#

**Disclaimer:**


This application triggers a system shutdown using Windows system commands.

Use with caution and ensure all work is saved before shutdown is activated.

#

<img width="783" height="440" alt="image" src="https://github.com/user-attachments/assets/ac4627d9-2d08-494c-870f-097568ab6f91" />

<img width="782" height="440" alt="image" src="https://github.com/user-attachments/assets/0ffb14bb-38e0-4191-990c-28689d8a43b5" />
