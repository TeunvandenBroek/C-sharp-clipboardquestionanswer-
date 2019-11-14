This application shold run in the background. Becuase of this, a Windows Forms project seems 
overkill for what is accomplished here. Converting this to a form-less application would be 
more lightweight, encasulate seperation of concerns (SOC), and have a lighter memeory footprint.

We will keep the forms here in case we want visual aspects in the project.

Added classes:
ControlContainer - used for the NotifyIcon.
Bootstrap - replaces the Form1 class as the driver for the application.

10/13/19 - Ryan S Thiele