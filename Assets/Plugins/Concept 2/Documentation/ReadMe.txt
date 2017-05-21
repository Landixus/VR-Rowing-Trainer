Concept2 PM3/PM4/PM5 SDK 2.02 Release Notes:

Note to Developers
The PM3/PM4/PM5 SDK documentation provides sufficient information to allow a software developer to create custom PC applications for the PM3/PM4/PM5, using the provided Data Link Libraries (DLLs).  
This documentation assumes the developer has experience writing applications that interface to DLLs.

Another option available to developers is a software tool called Performance Monitor Interface or PMI, written by Chris Brett.  The PMI is aimed at simplying the task of developing applications that are capable of processing data from the PM3/PM4/PM5.  

The PMI is an ActiveX control designed for communicating with the Performance Monitor. ActiveX Controls are effectively self contained packages that, once installed, can be used within any Microsoft Office Application.

Once the PMI control has been referenced (or loaded) by the Office application the user has access to a suite of features and functions enabling simple and versatile access to the data provided by the Performance Monitors. By using these features the user can quickly build applications which enable communication to and from the Concept2 PMs through a standard USB cable.

The free PMI software and documentation is available for download here: 
http://www.concept2.co.uk/software/pmi.php


Contents
----------------------------------------------------------------------
The SDK 2.02 release consists of the following:

1. Install Folder -
Demo application, DLLs and .lib files

2. Documentation Folder -
Concept2 PM Communication Interface Definition.pdf
ReadMe.txt
ReadMe.rtf

3. Source Code Folder -
Source code for demo application


PM3/PM4/PM5 Firmware Update
----------------------------------------------------------------------
Before exercising the PM3/PM4/PM5 communication interface defined for "public CSAFE" users, check that your PM3/PM4/PM5 is updated to the most recent firmware.  The firmware updater application can be found on Concept2's web site: http://www.concept2.com/firmware/

----------------------------------------------------------------------
In order to verify proper PC-to-PM3/PM4/PM5 communications run the install "PMSDK Setup.exe" and then launch "PMSDKDemo.exe". 
If the PM3/PM4/PM5 is recognized (e.g., discovered) by the application you'll see the "PMs discovered: 1" message in the "Response" area at the bottom of the application window.


PM3/PM4/PM5 API Demo Application
----------------------------------------------------------------------
The demo application provides a sample command interface to the PM3/PM4.  It was written using Visual Studio 2005.  Several predefined commands along with generic CSAFE and USB command capabilities are provided.  The CSAFE slave status and 'command response data is also displayed.  All predefined commands, usages and prototypes are defined in "Concept2 PM Communication Interface Definition.pdf". Note that all commands and command data are entered as two digit hexidecimal values in the command dialog. Similarly, all response information is displayed as two digit hexidecimal.

The "Reset" button causes the demo application to "re-discover" PMs in case the PM3/PM4 has been disconneted from the PC and reconnected. Exiting and restarting the application would also work.


