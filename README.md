# **Windows Shutdown Timer**

<img width="220" height="252" alt="WST logo" src="https://github.com/user-attachments/assets/a23726dc-3197-436b-9e01-2bb6dd2576dc" />

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
    
  - Also tracks disk write speed
 
  - Set the threshold for how long to wait to shutdown when a folder is inactive and disk speed slows
    
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
    
  - When the folder has been inactive and the disk write speed is slow (< 5 MB/s) for at least 10 minutes, device will shutdown in 60 seconds

  - Press "Cancel" at any time to stop moniotring and to stop a pending shutdown

#

**Disclaimer:**


- This application triggers a system shutdown using Windows system commands.

- Use with caution and ensure all work is saved before shutdown is activated.

#

<img width="784" height="440" alt="image" src="https://github.com/user-attachments/assets/edadefbd-2874-4780-ad02-9e07b0f3a03e" />

<img width="782" height="439" alt="image" src="https://github.com/user-attachments/assets/a87da7ab-ba8c-4c7b-b804-95206f68d4af" />

