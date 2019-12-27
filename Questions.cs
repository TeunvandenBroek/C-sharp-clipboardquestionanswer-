using System;
using System.Collections.Generic;
using System.Linq;

namespace it
{
    internal class Questions
    {
        private static IReadOnlyDictionary<string, string> questionDict = new Dictionary<string, string>(750,StringComparer.Ordinal)
        {
            //Add question/answer to list
            //hoofdstuk 3 it
            ["When a computer is being assembled, which action can be taken to help eliminate cable clutter within a computer case?"]
                = "Install a modular power supply.*",

            ["What is the best way to apply thermal compound when reseating a CPU?"] =
                "Clean the CPU and the base of the heat sink with isopropyl alcohol before applying the thermal compound.*",

            ["A technician is installing additional memory in a computer. How can the technician guarantee that the memory is correctly aligned?"]
                = "A notch in the memory module should be aligned with a notch in the memory slot.*",

            ["What is used to prevent the motherboard from touching metal portions of the computer case?"] =
                "standoffs",

            ["Which statement describes the purpose of an I/O connector plate?"] =
                "It makes the I/O ports of the motherboard available for connection in a variety of computer cases.*",

            ["What are three important considerations when installing a CPU on a motherboard? (Choose three.)"] =
                "Antistatic precautions are taken.* The CPU is correctly aligned and placed in the socket.* The CPU heat sink and fan assembly are correctly installed.*",

            ["Which type of drive is typically installed in a 5.25 inch (13.34 cm) bay?"] = "optical drive",

            ["Which type of motherboard expansion slot has four types ranging from x1 to x16 with each type having a different length of expansion slot?"]
                = "PCIe*",

            ["A technician is installing a new high-end video adapter card into an expansion slot on a motherboard. What may be needed to operate this video adapter card?"]
                = "Two 8-pin power connectors*",

            ["When assembling a PC, how is pin 1 identified on the front panel cables so that it can be aligned properly with pin 1 on the motherboard panel connector?"]
                = "by a small arrow or notch*",

            ["A technician has just finished assembling a new computer. When the computer is powered up for the first time, the POST discovers a problem. How does the POST indicate the error?"]
                = "It issues a number of short beeps.*",

            ["What is a function of the BIOS?"] = "performs a check on all internal components",

            ["A technician has assembled a new computer and must now configure the BIOS. At which point must a key be pressed to start the BIOS setup program?"]
                = "during the post",

            ["What data is stored in the CMOS memory chip?"] = "BIOS settintgs",

            ["What is the purpose of LoJack?"] =
                "It allows the owner of a device to remotely locate, lock, or delete all files from the device.*",

            ["Which is a BIOS security feature that can prevent data from being read from a hard drive even if the hard drive is moved to another computer?"]
                = "drive encryption",

            ["What is one purpose of adjusting the clock speed within the BIOS configuration settings?"] =
                "to allow the computer run cooler",

            ["A technician is assembling a new computer. Which two components are often easier to install before mounting the motherboard in the case? (Choose two.)"]
                = "CPU* memory*",

            ["Which two components are commonly replaced when a computer system with a newer motherboard is being upgraded? (Choose two.)"]
                = "RAM* CPU*",

            ["A user reports that every morning when a particular computer is turned on, the configuration settings on that computer have to be reset. What action should be taken to remedy this situation?"]
                = "Replace the CMOS battery*",

            ["What are two reasons for installing a second hard disk drive inside an existing computer? (Choose two.)"]
                = "to support a RAID array* to store the system swap file*",

            ["When should a technician use unsigned drivers for newly installed computer hardware?"] =
                "if the source of the drivers is trusted by the technician*",

            ["What computer component is often required to allow a single computer to use two monitors simultaneously?"]
                = "a second video adapter card*",

            ["A technician notices that half of the memory slots on a motherboard are colored differently from the other half. Which technology is probably implemented on this system?"]
                = "dual chanelling",

            // hoofdstuk 4 it essentials
            ["Which negative environmental factor does cleaning the inside of a computer reduce?"] = "dust*",

            ["Which component can be easily damaged by the direct spray of compressed air when cleaning the inside of the computer case?"]
                = "fan*",

            ["On the production floor, a furniture plant has laptops for process monitoring and reporting. The production floor environment is around 80 degrees Fahrenheit (27 degrees Celsius). The humidity level is fairly high around 70 percent. Fans are mounted in the ceiling for air circulation. Wood dust is prevalent. Which condition is most likely to adversely affect a laptop that is used in this environment? "]
                = "the dust",

            ["A vegetable canning plant uses laptops for monitoring of assembly lines. The production environment has an ambient temperature around 75 degrees Fahrenheit (24 degrees Celsius). The humidity level is around 30 percent. Noise levels are high due to the canning machinery. The laptops are in a wooden box that tightly surrounds the laptop on three sides. Which factor is most likely to adversely affect a laptop that is used in this environment? "]
                = "the laptop container",

            ["A scientific expedition team is using laptops for their work. The temperatures where the scientists are working range from -13 degrees Fahrenheit (-25 degree Celsius) to 80 degrees Fahrenheit (27 degrees Celsius). The humidity level is around 40 percent. Noise levels are low, but the terrain is rough and winds can reach 45 miles per hour (72 kilometers per hour). When needed, the scientists stop walking and enter the data using the laptop. Which condition is most likely to adversely affect a laptop that is used in this environment? "]
                = "the temprature",

            ["What is part of creating a preventive maintenance plan?"] =
                "documenting the details and frequency of each maintenance task*",

            ["A technician is performing hardware maintenance of PCs at a construction site. What task should the technician perform as part of a preventive maintenance plan?"]
                = "Remove dust from intake fans.*",

            ["Which task should be part of a hardware maintenance routine?"] = "Check for and secure any loose cables.",

            ["During the process of testing a theory of several probable causes to a problem, which should be tested first?"]
                = "the easiest and most obvious*",

            ["Which two items could be used to help establish a plan of action when resolving a computer problem? (Choose two.)"]
                = "the computer manual*the computer repair history log * ",

            ["A customer reports that recently several files cannot be accessed. The service technician decides to check the hard disk status and the file system structure. The technician asks the customer if a backup has been performed on the disk and the customer replies that the backup was done a week ago to a different logical partition on the disk. What should the technician do before performing diagnostic procedures on the disk?"]
                = "Back up the user data to a removable drive.*",

            ["What is the next step after a possible solution is implemented during a troubleshooting process?"] =
                "Verify the full system functionality and apply maintenance procedures. *",

            ["An employee reports that the antivirus software cannot obtain updates. The support technician notices that the license for the software has expired. The technician adds a new license to the software and completes the update service. What should the technician do next?"]
                = "Run a full virus scan on the computer.*",

            ["What task should be completed before escalating a problem to a higher-level technician?"] =
                "Document each test that was tried.*",

            ["Which step of the six-step troubleshooting process is where a technician would ask the computer user to print a document on a newly installed printer?"]
                = "Verify full system functionality and, if applicable, implement preventive measures. *",

            ["After a technician tests a theory of probable causes, what two actions should the technician take if the testing did not identify an exact cause? (Choose two.)"]
                = "Establish a new theory of probable causes.* Document each test tried that did not correct the problem.*",

            ["What is the preferred method to remove a disc from an optical drive that fails to eject the disc?"] =
                "Insert a pin into the small hole on the front of the drive.*",

            ["A technician is called to an office where the computer is randomly rebooting. Which of the given components would most likely cause this issue?"]
                = "power supply",

            ["What would happen if a PC that contains a power supply that does not automatically adjust for input voltage is set to 230 volts and attaches to an outlet in the United States?"]
                = "The PC would not turn on.*",

            ["During the troubleshooting of a PC that will not boot, it is suspected that the problem is with the RAM modules. The RAM modules are removed and put into another PC, which successfully powers on. The RAM modules are then put back into the original PC and it now successfully powers on as well. What was the most likely cause of the problem?"]
                = "The RAM modules were not seated firmly.*",

            ["After a new PCIe video card is added, the computer seems to boot successfully but it will not display any video. The computer was working properly before the new video card was installed. What is the most likely cause of the problem?"]
                = "The saved CMOS settings are set to use the built-in video adapter.*",

            ["A user has opened a ticket that indicates that the computer clock keeps losing the correct time. What is the most likely cause of the problem?"]
                = "The CMOS battery is loose or failing.*",

            ["An employee reports that each time a workstation is started it locks up after about 5 minutes of use. What is the most likely cause of the problem?"]
                = "The CPU is overheating.*",

            ["A user reports that images on a computer display are distorted. The technician sends an intern to look at the problem. What could the technician tell the intern to check first that does not involve hardware replacement or disassembly?"]
                = "display settings*",

            ["A user has connected an external monitor to a laptop VGA port. What is the next step the user should take?"]
                = "Use a Fn key along with a multi-purpose key to send video to the external display.*",

            ["What are two functions of an operating system? (Choose two.)"] =
                "controlling hardware access *managing applications *",

            ["Which two methods should a technician use to update a device driver? (Choose two.)"] =
                "Use the media that came with the device.* Download the driver from the website of the manufacturer.*",

            ["What is the term for the ability of a computer to run multiple applications at the same time?"] =
                "multitasking",

            ["What is provided by an operating system that has multiprocessing capability?"] =
                "support for two or more processors*",

            ["What is used by an operating system to communicate with hardware components?"] = "device driver*",

            ["Which application interface is comprised of multiple APIs that support multimedia tasks in Windows operating systems?"]
                = "DirectX*",

            ["Which version of Windows introduced the Metro user interface, which can be used on desktops, laptops, mobile phones, and tablets?"]
                = "Windows 8",

            ["Where can a technician look to determine if specific computer hardware or software has been tested to work with Windows 7?"]
                = "Microsoft Compatibility Center*",

            ["What is critical to consider when determining the version of Windows to install on a desktop computer?"] =
                "customized applications installed *",

            ["A user downloads and uses the Windows 7 Upgrade Advisor to generate a report on a PC that is being considered for an operating system upgrade. What information would the report contain?"]
                = "recommended changes to hardware*",

            ["What are two features of the active partition of a hard drive that is using MBR? (Choose two.)"] =
                "The active partition must be a primary partition.* The operating system uses the active partition to boot the system.*",

            ["A computer with the Windows 7 operating system fails to boot when the system is powered on. The technician suspects that the operating system has been attacked by a virus that rendered the system inoperable. What measure could be taken to restore system functionality?"]
                = "Use a system image that was created prior to the failure to restore the system.*",

            ["What is the purpose of a recovery partition?"] = "to restore the computer to its factory state*",

            ["What Windows 8 tool allows the computer to be rolled back to its factory state without losing any user files?"]
                = "refresh your pc",

            ["A Windows 8 computer locks up with a stop error during startup and then automatically reboots. The automatic restart setting is making it difficult to see any error messages. What can be done so that the error messages can be viewed?"]
                = "Access the Advanced Startup options menu before the OS loads to disable the auto restart function. *",

            ["A user installs a new video driver on a computer that was working properly. After the new driver is installed and the computer is restarted, the computer fails to boot. How can the user quickly return the computer to a working state?"]
                = "Press F8 while the computer is booting and choose the Last Known Good Configuration option.*",

            ["Where are the settings that are chosen during the installation process stored?"] = "in the Registry *",

            ["Why is it important to register a DLL file in Windows?"] =
                "so that any program needing to use that specific program code can locate the file*",

            ["A Windows 8.1 system is unable to boot properly. What is the procedure to boot this system into Safe Mode?"]
                = "Reboot the system, press and hold F8 until a menu is displayed, and then choose Safe Mode. *",

            ["A technician has installed a new video driver on a Windows 8.1 computer and now the monitor shows distorted images. The technician needs to install a different video driver that is available on a network server. What startup mode should the technician use to access the driver on the network?"]
                = "Safe Mode with Networking*",

            ["A technician is considering implementing RAID 5 before installing Windows. Which technology does RAID 5 use?"]
                = "striping with parity*",

            ["What is a characteristic of a spanned volume?"] =
                "Data is seen as one volume, but is stored across two or more disks.*",

            ["Which Windows tool is used to determine if a dynamic disk is corrupted?"] = "disk management",

            ["A user has upgraded a PC to Windows 7 but still needs to use an application that is created for the earlier version of Windows. Which mode of operation within Windows 7 can be used to run the application?"]
                = "compatibility mode*",

            ["Within which folder would a user of 64-bit Windows 8 find 32-bit programs?"] = "C:Program Files (x86)*",
            //hoofdstuk 6//

            ["What would be a reason for a computer user to use the Task Manager Performance tab?"] =
                "to check the CPU usage of the PC *",

            ["Which feature in Windows 7 and 8 allows a user to temporarily view the desktop that is behind open windows by moving the mouse over the right edge of the taskbar?"]
                = "Peek",

            ["What is the minimum amount of RAM and hard drive space required to install 64-bit Windows 8 on a PC?"] =
                "2 GB RAM and 20 GB hard disk space*",

            ["After upgrading a computer to Windows 7, a user notices that the UAC (User Account Control) panel appears more frequently. How can the user reduce the frequency with which the UAC appears?"]
                = "Lower the UAC setting in the Change User Account Control settings dialog box of the User Accounts control panel.*",

            ["Which Windows utility allows Windows 7 and 8 users to quickly and easily share files and folders?"] =
                "HomeGroup",

            ["Which Windows administrative tool displays the usage of a number of computer resources simultaneously and can help a technician decide if an upgrade is needed?"]
                = "performace monitor",

            ["Which type of startup must be selected for a service that should run each time the computer is booted?"] =
                "automatic",

            ["Which Windows tool allows an administrator to organize computer management tools in one location for convenient use?"]
                = "Microsoft Management console *",

            ["To which category of hypervisor does the Microsoft Virtual PC belong?"] = "Type 2",

            ["What are two advantages of using PC virtualization? (Choose two.)"] =
                "It allows multiple operating systems to run on a single PC simultaneously.* It can provide cost savings.*",

            ["A college uses virtualization technology to deploy information security courses. Some of the lab exercises involve studying the characteristics of computer viruses and worms. What is an advantage of conducting the lab exercises inside the virtualized environment as opposed to using actual PCs?"]
                = "Virus and worm attacks are more easily controlled in a virtualized environment, thus helping to protect the college network and its devices from attack.*",

            ["A software engineer is involved in the development of an application. For usability tests, the engineer needs to make sure that the application will work in both Windows 7 and Windows 8 environments. The features and functions must be verified in the actual OS environment. The engineer is using a Windows 7 workstation. What two technologies can help the engineer achieve the usability tests? (Choose two.)"]
                = "dual boot* client-side virtualization*",

            ["A technician needs to use an application that is not supported by Windows operating systems on the PC. How can the technician make this application run on the PC?"]
                = "Create a virtual machine with an operating system that supports the application.*",

            ["What preventive maintenance action should be taken to help improve system security?"] =
                "Automate any antivirus scanners.*",

            ["Which Windows 7 feature would an administrator use to configure a computer to delete temporary files from the hard drive at 3:00 AM each day?"]
                = "task scheduler*",

            ["When troubleshooting a printer problem, a technician finds that the operating system was automatically updated with a corrupt device driver. Which solution would resolve this issue?"]
                = "Roll back the driver.*",

            ["Which two Windows utilities can be used to help maintain hard disks on computers that have had long periods of normal use? (Choose two.)"]
                = "Disk cleanup * Disk defragmenter*",

            ["What is a common step that a technician can take to determine the cause of an operating system problem?"]
                = "Boot into Safe Mode to determine if the problem is driver related.*",

            ["Which question is an open ended question that could be used when helping a customer troubleshoot a Windows problem?"]
                = "What programs have you installed recently ? *",

            ["A user reports that logging into the workstation fails after a display driver has been updated. The user insists that the password is typed in correctly. What is the most likely cause of the problem?"]
                = "The Caps Lock key is set to on.*",

            ["A user reports that a video editing program does not work properly after a sound mixing program is installed. The technician suspects that a compatibility issue might be the cause of the problem. What should the technician do to verify this theory?”"]
                = "Uninstall the sound mixing software.*",

            ["A user reports to a technician that his computer freezes without any error messages. What are two probable causes? (Choose two.)"]
                = "An update has corrupted the operating system.*The power supply is failing.*",

            ["A technician is trying to fix a Windows 7 computer that displays an “Invalid Boot Disk” error after POST. What is a possible cause of the error?"]
                = "The boot order is not set correctly in the BIOS.*",
            //chapter 7 it essentials//

            ["How many devices can a Bluetooth device connect to simultaneously?"] = "7*",

            ["A device has an IPv6 address of 2001:0DB8:75a3:0214:0607:1234:aa10:ba01 /64. What is the host identifier of the device?"]
                = "0607:1234:aa10: ba01 *",

            ["Which layer of the OSI model is responsible for logical addressing and routing?"] = "network*",

            ["When would a printer be considered a network host?"] = "when it is connected to a switch*",

            ["Which device provides wireless connectivity to users as its primary function?"] = "acces point",

            ["Which technology is most often used to connect devices to a PAN?"] = "Bluetooth",

            ["Which three factors are reasons for a company to choose a client/server model for a network instead of peer-to-peer? (Choose three.)"]
                = "The company network requires secure access to confidential information.*The users need a central database to store inventory and sales information.*The data gathered by the employees is critical and should be backed up on a regular basis.*",

            ["What is a characteristic of a WAN?"] =
                "It connects multiple networks that are geographically separated.*",

            ["Which three layers of the OSI model map to the application layer of the TCP/IP model? (Choose three.)"] =
                "application *presentation *session * ",

            ["What is the correct order of the layers of the TCP/IP model from the top layer to the bottom?"] =
                "application, transport, internet, network access *",

            ["What TCP/IP model layer is responsible for MAC addressing?"] = "network acces",

            ["What PDU is associated with the network layer of the OSI model?"] = "packet",

            ["Which two layers of the OSI model correspond to the functions of the TCP/IP model network access layer? (Choose two.)"]
                = "physical *data link * ",

            ["What is identified by the 100 in the 100BASE-TX standard?"] = "the maximum bandwidth in Mb / s *",

            ["Which two characteristics describe Ethernet technology? (Choose two.)"] =
                "It is supported by IEEE 802.3 standards.*It uses the CSMA / CD access control method.*",

            ["Which IEEE standard operates at wireless frequencies in both the 5 GHz and 2.4 GHz ranges?"] =
                "802.11n *",

            ["A customer is considering a multipurpose device to create a home network. Which three devices are usually integrated into a multipurpose network device? (Choose three.)"]
                = "switch*router * wireless access point*  ",

            ["Which network device makes forwarding decisions based on the destination MAC address that is contained in the frame?"]
                = "switch",

            ["A routeruses IP addresses to forward traffic from one network to other networks."] = "router",

            ["A network specialist has been hired to install a network in a company that assembles airplane engines. Because of the nature of the business, the area is highly affected by electromagnetic interference. Which type of network media should be recommended so that the data communication will not be affected by EMI?"]
                = "fiber optic",

            ["Which term describes a type of coaxial cable?"] = "RG-6",

            ["Which pairs of wires change termination order between the 568A and 568B standards?"] = "green and orange",

            ["Which two types of signal interference are reduced more by STP than by UTP? (Choose two.)"] = "RFI* EMI*",

            ["How many host addresses are available on a network with a subnet mask of 255.255.0.0?"] = "65,534*",

            ["A technician is troubleshooting a problem where the user claims access to the Internet is not working, but there was access to the Internet the day before. Upon investigation, the technician determines that the user cannot access the network printer in the office either. The network printer is on the same network as the computer. The computer has 169.254.100.88 assigned as an IP address. What is the most likely problem?"]
                = "The computer cannot communicate with a DHCP server.*",

            ["What is a characteristic of the UDP protocol?"] = "low overhead*",
            //hoofdstuk 8 it essentials//

            ["A user notices that the data transfer rate for the gigabit NIC in the user computer is much slower than expected. What is a possible cause for the problem?"]
                = "The NIC duplex settings have somehow been set to half-duplex.*",

            ["Two LEDs are usually present on a NIC. What are the two primary uses for these LEDs? (Choose two.)"] =
                "to indicate the presence of a connection*to indicate that data transfer activity is present *",

            ["What is the purpose of the network profiles that are used to establish a new network connection on a Windows PC?"]
                = "to provide an easy way to configure or apply network functions based on the type of network to be joined *",

            ["A wireless router is displaying the IP address of 192.168.0.1. What could this mean?"] =
                "The wireless router still has the factory default IP address.*",

            ["A technician is configuring the channel on a wireless router to either 1, 6, or 11. What is the purpose of adjusting the channel?"]
                = "to avoid interference from nearby wireless devices *",

            ["Which combination of user account and network location profile allows a user to become a member of a homegroup?"]
                = "a standard user account with a network location profile of Home*",

            ["A business traveler connects to a wireless network with open authentication. What should the traveler do to secure confidential data when connecting to the business services?"]
                = "Connect with a vpn",

            ["A technician has been asked to configure a broadband connection for a teleworker. The technician has been instructed that all uploads and downloads for the connection must use existing phone lines. Which broadband technology should be used?"]
                = "DSL",

            ["Which type of connection to the Internet is capable of the fastest transfer rates?"] = "fiber",

            ["Which technology would be recommended for a business that requires workers to access the Internet while visiting customers at many different locations?"]
                = "cellular",

            ["A small company is considering moving many of its data center functions to the cloud. What are three advantages of this plan? (Choose three.)"]
                = "The company only needs to pay for the amount of processing and storage capacity that it uses.* The company can increase processing and storage capacity as needed and then decrease capacity when it is no longer needed.* As the amount of data that the company uses increases, it becomes impractical for the data to be stored and processed in a single-tenant data center.*",

            ["Which Cloud computing service would be best for an organization that does not have the technical knowledge to host and maintain applications at their local site?"]
                = "SaaS*",

            ["A network client in a corporate environment reboots. Which type of server would most likely be used first?"]
                = "DHCP",

            ["What is a common function of a proxy server?"] =
                "to store frequently accessed web pages on the internal network*",

            ["ABC Company requires preventive maintenance for all local network cabling once a year. Which task should be included in the preventive maintenance program?"]
                = "Inspect all patch cables for breaks.*",

            ["What is a benefit of performing preventative maintenance at regular intervals?"] =
                "reduction in network downtime*",

            ["A user cannot access the network. While the technician is checking the computer, the other users on the same network report that they are having the same problem. Further investigation shows that the LED lights on each NIC are not lit. What should the technician do next?"]
                = "Report the problem to the network administrator.*",

            ["A user can print to a printer that is on the same network, but the traffic of the user cannot reach the Internet. What is a possible cause of the problem?"]
                = "The PC default gateway address is missing or incorrect.*",

            ["A technician is investigating a report that a computer is unable to access network resources. The technician discovers that the computer has an IP address of 169.254.27.168. What is a logical first step in diagnosing this problem?"]
                = "Check the NIC LED lights.*",

            ["A Windows 7 computer is unable to reach a mapped drive on a file server that is on another network within the organization. Further investigation shows that the user is able to use a printer that is connected to the same network as the computer. Which action should the technician perform next?"]
                = "Check Network Connection Details in the Windows GUI for the appropriate network connection.*",

            ["A user has taken a personal laptop to work. The laptop is unable to discover the name of the office wireless network. What are two potential causes of this problem? (Choose two.)"]
                = "The wireless router is not broadcasting the SSID.* The network does not support the wireless protocol in use by the laptop.*",

            ["A network technician attempts to ping www.example.net from a customer computer, but the ping fails. Access to mapped network drives and a shared printer are working correctly. What are two potential causes for this problem? (Choose two.)"]
                = "The target web server is down.* DNS service is unavailable on the customer network.*",

            ["A technician uses the nbtstat -A 192.168.0.3 command. What does the technician expect to view in the command output?"]
                = "current connections and statistics*",

            ["Fill in the blank What is the acronym for the protocol that is used when securely communicating with a web server ?"]
                = "HTTPS",

            ["Match the definition to the of cloud."] =
                "build to meet a specific need, services made available to the general population, made up two or more clouds connected via a single architecture, intended for a specific organization or entity , such as the goverment",
            //Hoofdstuk 9 it essentials//

            ["Which two statements are true of a laptop CPU when compared to a desktop CPU? (Choose two.)"] =
                "Laptop CPUs use smaller cooling devices.* Laptop CPUs are designed to produce less heat.",

            ["Which laptop component makes use of throttling to reduce power consumption and heat?"] = "CPU",

            ["Why are SODIMMs well suited for laptops?"] = "They have a small form factor.",

            ["What is a characteristic of laptop motherboards?"] = "Laptop motherboards are proprietary.",

            ["Which laptop component converts DC power to AC power?"] = "inverter",

            ["Where is a Wi-Fi antenna commonly located on a laptop?"] = "above the laptop screen",

            ["Which two methods can be used to configure laptop ACPI power options? (Choose two.)"] =
                "BIOS settings* Windows Power Options utility",

            ["A user needs to connect a Bluetooth device to a laptop. Which type of cable is needed to accomplish this?"]
                = "None. Bluetooth connections are wireless.",

            ["A technician has installed a wireless Ethernet card in a Windows 7 laptop. Where would the technician configure a new wireless connection?"]
                = "Control Panel > Networking and Sharing Center > Set up a new connection or network",

            ["Which technology provides laptops the ability to function on a cellular network?"] = "mobile hotspot",

            ["A technician is trying to remove a SODIMM module from a laptop. What is the correct way to do this?"] =
                "Press outward on the clips that hold the sides of the SODIMM.",

            ["When replacing or adding memory to a laptop, where can a technician find the maximum amount of RAM each memory slot can support?"]
                = "website of manufacturer ",

            ["What should be done immediately after a CPU is latched in and screwed down to a laptop motherboard?"] =
                "The locked down CPU should be protected with thermal paste. ",

            ["Which technology allows a mobile device to establish wireless communication with another mobile device by touching them together?"]
                = "NFC",

            ["Which technology will allow a mobile device to share an Internet connection with other devices via tethering?"]
                = "Bluetooth",

            ["What are two differences between an e-reader and a tablet device? (Choose two.)"] =
                "An e-reader commonly has longer battery life than a tablet device.* A tablet device screen may be harder to read in direct sunlight than an e-reader screen.",

            ["What type of device is commonly used to measure and collect daily activity data for achieving personal goals?"]
                = "fitness monitor",

            ["A technician has been asked to decide which laptop components should be cleaned on a monthly basis as part of a maintenance routine. What are two examples of components that should be included? (Choose two.)"]
                = "exterior casekeyboard",

            ["To clean laptops, which two products are recommended? (Choose two.)"] =
                "cotton swabs, mild cleaning solution",

            ["What two steps must be taken before performing a cleaning procedure on a laptop computer? (Choose two.)"]
                = "Disconnect the laptop from the electrical outlet. * Remove all installed batteries.",

            ["What is the first step in the process of troubleshooting a laptop?"] = "Identify the problem.",

            ["What is the next step in the troubleshooting process after a solution has been implemented on a laptop?"]
                = "Verify the solution.",

            ["While troubleshooting a mobile device, a technician determines that the battery is not holding a charge. What is the next step in the troubleshooting process?"]
                = "Determine the exact cause.",

            ["A technician notices that a laptop display appears stretched and pixelated. What is a probable cause of this?"]
                = "The display properties are incorrectly set.",

            ["A user with a company-issued smartphone notices that there is a gap at the end of the device. A technician examines the smartphone and discovers that the battery is swollen. What should the technician do next?"]
                = "Replace the battery.",
            //Hoofdstuk 10 It//

            ["What are two ways that iOS differs from Android? (Choose two.)"] =
                "iOS has a physical Home button, but Android uses navigation icons.*In iOS, the icon for an app represents the app itself. Deleting the icon in iOS deletes the app. In Android, the icon on the Home screen is a shortcut to the app.*",

            ["What is a good source for safely downloading Android apps?"] = "Google play",

            ["Refer to the exhibit. What two statements are true about the operating system screen shown? (Choose two.)"]
                = "The area that is highlighted contains navigation icons. This is an Android screen.*",

            ["A small number has appeared on one of the apps on a user’s iOS home screen. What does this mean?"] =
                "It is an alert badge that indicates the number of items requiring attention for that app.*",

            ["What represents apps in the Windows Phone interface?"] = "tiles",

            ["What is the name of the Windows Phone 8.1 digital, or virtual, assistant?"] = "Cortana",

            ["What is Wi-Fi calling?"] = "a way to make mobile phone calls over a wireless data network*",

            ["What is a benefit of the WEA system?"] =
                "It can save lives by sending emergency text messages to mobile phones.*",

            ["What are two purposes of the passcode lock feature on mobile devices? (Choose two.)"] =
                "to help prevent theft of private information* to prevent unauthorized use of the device*",

            ["Which two conditions must be met for mobile device security measures such as remote lock and remote wipe to function? (Choose two.)"]
                = "The device must be powered on.* The device must be connected to a network.*",

            ["Which two location data sources can locator apps use to determine the position of a mobile device? (Choose two.)"]
                = "cellular towers* WiFi hotspots*",

            ["What are two potential user benefits of rooting or jailbreaking a mobile device? (Choose two.)"] =
                "The user interface can be extensively customized.* The operating system can be fine-tuned to improve the speed of the device.*",

            ["Which statement is true about wireless connectivity on an Android mobile device?"] =
                "When the device roams out of the range of any Wi-Fi networks, it can connect to the cellular data network if this feature is enabled.*",

            ["In the context of mobile devices, what does the term tethering involve?"] =
                "connecting a mobile device to another mobile device or computer to share a network connection*",

            ["Which statement is true about industry standards for cellular networks?"] =
                "Cell phones that use a single standard can often only be used in specific geographic areas.*",

            ["What will allow someone to use a cell phone for entertainment without connecting to any networks?"] =
                "Airplane Mode*",

            ["What technology enables a cell phone to be used as a hands-free device?"] = "Bluetooth",

            ["A technician is configuring email on a mobile device. The user wants to be able to keep the original email on the server, organize it into folders, and synchronize the folders between the mobile device and the server. Which email protocol should the technician use?"]
                = "IMAP",

            ["What is the code name used for the OS X 10.10 operating system?"] = "Yosemite",

            ["What is a purpose of the boot manager program?"] =
                "It allows the user to select the OS to use to boot the device.*",

            ["What is the purpose of signature files used to secure mobile devices and operating systems?"] =
                "They contain sample code from known viruses and malware that is used by security software to identify malicious software. *",

            ["A file called new_resume has the following file permissions: rw-r-x–x What two facts can be determined from these permissions? (Choose two.)"]
                = "Members of the group have read and execute access to the file.* The user is able to read and modify the file.*",

            ["What is the result of doing a factory reset on a mobile device?"] =
                "All user data and settings will be deleted.*",

            ["What tool can be used on a mobile device to display available cellular networks, location of networks, and signal strength?"]
                = "cell tower analizer",

            ["An administrator is re-imaging a large number of Mac OS X machines. What built-in tool or command can be used to remotely boot the computers?"]
                = "Netboot",

            ["What tool or command is used in the Mac OS X to navigate the file system?"] = "Finder",

            ["What command is used to open a text editor in Unix-like systems?"] = "vi",
            //hoofdstuk 11 it//
            ["Which factor affects the speed of an inkjet printer?"] = "the desired quality of the image*",

            ["What are two cables that are used to connect a computer to a printer? (Choose two.)"] =
                "serial* FireWire",

            ["What type of connection would be used to connect a printer directly to the network?"] = "Ethernet",

            ["What is a characteristic of thermal inkjet nozzles?"] =
                "The heat creates a bubble of steam in the chamber.*",

            ["In laser printing, what is the name of the process of applying toner to the latent image on the drum?"] =
                "developing",

            ["What mechanism is used in a laser printer to permanently fuse the toner to the paper?"] = "heat",

            ["Which statement describes a printer driver?"] =
                "It is software that converts a document into the format that a printer can understand.*",

            ["A technician is installing a printer that will be directly connected to a computer. Why does the technician not connect the printer initially during the installation process?"]
                = "The printer driver might need to be installed first before the printer is connected.*",

            ["A Windows 7 computer has several printers configured in the Control Panel Devices and Printers window. Which printer will the computer choose to be the first option for printing?"]
                = "the printer that is set as the default printer*",

            ["What is a characteristic of global and per-document options in print settings?"] =
                "Per-document options override global options.*",

            ["A user discovers that an inkjet color printer is printing different colors from those that are shown on the screen. What can be done to solve this problem?"]
                = "Calibrate the printer.*",

            ["The users on a LAN are reporting that computers respond slowly whenever high resolution photographs are being printed on the color laser printer. What would be the cause of this problem?"]
                = "The printer does not have enough memory to buffer an entire photograph.*",

            ["What is the purpose of the Additional Drivers button in the Sharing tab of the Printer Properties?"] =
                "to add additional drivers for other operating systems*",

            ["What are two methods to connect to a printer wirelessly? (Choose two.)"] =
                "IEEE 802.11 standards*, Bluetooth*",

            ["In Windows 8, what must be configured to enable one user to share a USB-connected printer with another user on the same network?"]
                = "File and printer sharing*",

            ["Which type of print server provides the most functions and capabilities?"] =
                "a dedicated PC print server",

            ["What are two functions of a print server? (Choose two.)"] =
                "provide print resources to all connected client computers, store print jobs in a queue until the printer is ready",

            ["Which action supports an effective printer preventive maintenance program?"] =
                "Reset the printer page counters if available.",

            ["How can the life of a thermal printer be extended?"] =
                "Clean the heating element regularly with isopropyl alcohol.",

            ["After applying a solution to a printer problem, a technician restarts the printer and prints a test page. Which step of the troubleshooting process is the technician applying?"]
                = "verifying the solution and system functionality",

            ["A technician recorded that a new fuser roller unit was installed in a laser printer to solve a printing problem. Which step in the troubleshooting process did the technician just perform?"]
                = "documenting findings, actions, and outcomes",

            ["What are two probable causes for printer paper jams?"] = "high humidity, the wrong type of paper",

            ["What corrective action should be taken if a printer is printing faded images?"] =
                "replace the toner cartridge",

            ["What corrective action would a technician take in response to a print spooler error?"] =
                "restart the print spooler",

            ["What would cause an inkjet printer to fail to print any pages?"] = "The ink cartridge is empty.",

            ["A user tells a technician that the printer does not respond to attempts to print a document. The technician attempts to print a document and the printer does not output any pages. The technician notices that the printer LCD display is blank and unlit. What is most likely the problem?"]
                = "The printer is not turned on.",

            ["Match the common printer configuration options to the correct descriptions. (Not all options are used.)"]
                = "collate, paper orientation, draft, normal, or photo, grayscale printing, print layout, duplex",
            //chapter 12 it//

            ["Which two security precautions will help protect a workplace against social engineering? (Choose two.)"] =
                "registering and escorting all visitors to the premises, ensuring that each use of an access card allows access to only one user at the time",

            ["Which two characteristics describe a worm? (Choose two.)"] =
                "is self-replicating, travels to new computers without any intervention or knowledge of the user",

            ["Which type of security threat uses email that appears to be from a legitimate sender and asks the email recipient to visit a website to enter confidential information?"]
                = "phishing",

            ["What is the primary goal of a DoS attack?"] =
                "to prevent the target server from being able to handle additional requests",

            ["Which type of attack involves the misdirection of a user from a legitimate web site to a fake web site?"]
                = "DNS poisoning",

            ["Which password is the strongest?"] = "Gd^7123e!",

            ["Which three questions should be addressed by organizations developing a security policy? (Choose three.)"]
                = "What assets require protection?, What is to be done in the case of a security breach?, What are the possible threats to the assets of the organization? ",

            ["The XYZ company has decided to upgrade some of its older PCs. What precaution should the company take before the disposal of the remaining older computers?"]
                = "Data wipe the hard drive.",

            ["Which two file-level permissions allow a user to delete a file? (Choose two.)"] = "Modify, Full control",

            ["What is the name given to the programming-code patterns of viruses?"] = "signatures",

            ["What is the most effective way of securing wireless traffic?"] = "WPA2",

            ["Port triggering has been configured on a wireless router. Port 25 has been defined as the trigger port and port 113 as an open port. What effect does this have on network traffic?"]
                = "All traffic that is sent out port 25 will open port 113 to allow inbound traffic into the internal network through port 113.",

            ["What are two physical security precautions that a business can take to protect its computers and systems? (Choose two.)"]
                = "Implement biometric authentication. , Lock doors to telecommunications rooms.",

            ["What is the minimum level of Windows security required to allow a local user to restore backed up files?"]
                = "Write",

            ["What is the purpose of the user account idle timeout setting?"] =
                "to log a user out of a computer after a specified amount of time",

            ["Which two security procedures are best practices for managing user accounts? (Choose two.)"] =
                "Limit the number of failed login attempts. , Restrict the time of day that users can log into a computer.",

            ["Which Windows Firewall option allows the user to manually allow access to the ports required for an application to be allowed to run?"]
                = "Manage Security Settings",

            ["Which two Windows default groups are allowed to back up and restore all files, folders, and subfolders regardless of what permissions are assigned to those files and folders? (Choose two.)"]
                = "Administrators, Backup Operators",

            ["A manager approaches a PC repair person with the issue that users are coming in to the company in the middle of the night to play games on their computers. What might the PC repair person do to help in this situation?"]
                = "Limit the login times.",

            ["Which question would be an example of an open-ended question that a technician might ask when troubleshooting a security issue?"]
                = "What symptoms are you experiencing?",

            ["Which action would help a technician to determine if a denial of service attack is being caused by malware on a host?"]
                = "Disconnect the host from the network.",

            ["A technician is troubleshooting a computer security issue. The computer was compromised by an attacker as a result of the user having a weak password. Which action should the technician take as a preventive measure against this type of attack happening in the future?"]
                = "Ensure the security policy is being enforced.",

            ["It has been noted that the computers of employees who use removable flash drives are being infected with viruses and other malware. Which two actions can help prevent this problem in the future? (Choose two.)"]
                = "Set virus protection software to scan removable media when data is accessed.   - Disable the autorun feature in the operating system.",

            ["A virus has infected several computers in a small office. It is determined that the virus was spread by a USB drive that was shared by users. What can be done to prevent this problem?"]
                = "Set the antivirus software to scan removable media.",

            ["A user is browsing the Internet when a rogue pop-up warning message appears indicating that malware has infected the machine. The warning message window is unfamiliar, and the user knows that the computer is already protected by antimalware software. What should the user do in this situation?"]
                = "Close the browser tab or window.",

            ["In what situation will a file on a computer using Windows 8.1 keep its original access permissions?"] =
                "when it is moved to the same volume",

            ["What security measure can be used to encrypt the entire volume of a removable drive?"] =
                "BitLocker To Go",

            ["A user calls the help desk reporting that a laptop is not performing as expected. Upon checking the laptop, a technician notices that some system files have been renamed and file permissions have changed. What could cause these problems?"]
                = "The laptop is infected by a virus.",
            //it essentials hoofdstuk 13//

            ["What is the correct way to conduct a telephone call to troubleshoot a computer problem?"] =
                "Maintain professional behavior at all times. ",

            ["What is a recommended technique for a technician who is both troubleshooting a problem and also trying to help a customer relax?"]
                = "Establish a good rapport with the customer.",

            ["A customer is upset and wants to speak to a specific technician to resolve a problem immediately. The requested technician is away from the office for the next hour. What is the best way to handle this call?"]
                = "Make an offer to help the customer immediately, and advise the customer that otherwise the requested technician will call the customer back within two hours.",

            ["What two actions should a call center technician avoid when dealing with an angry customer? (Choose two.)"]
                = "spending time explaining what caused the problem,      putting the customer on hold or transferring the call",

            ["A call center technician is on a call with a customer when a colleague indicates that there is something to discuss. What should the technician do?"]
                = "Signal to the colleague to wait.",

            ["A technician receives a call from a customer who is too talkative. How should the technician handle the call?"]
                = "Allow the customer to speak for one minute and then try to refocus the conversation.",

            ["What are two examples of displaying professional communication skills while talking to a customer? (Choose two.)"]
                = "the use of active listening, with occasional interjections such as “I see” or “I understand”,         clarifying what customers say after they have finished their explanations",

            ["Which statement describes a best practice related to time management?"] =
                "The technician should make sure to call the customer back as close to the callback time as possible. ",

            ["Which two rules should be followed by call center employees to ensure customer satisfaction? (Choose two.)"]
                = "Offer different repair or replacement options if possible.,      Communicate the repair status with explanations of any delays.",

            ["Fill in the blank."] =
                "The   SLA       is a contract defining the agreed-on level of support between a customer and a service vendor.",

            ["During the process of troubleshooting, a technician gains access to customer private information. What is expected that the technician do with this information?"]
                = "Keep it confidential.",

            ["Which statement is characteristic of most personal software licenses?"] =
                "A user is allowed to install the software on only one computer. ",

            ["What is the definition of cyber law?"] =
                "the collection of international, country, and local laws that affect computer security professionals",

            ["Which two actions should a technician take if illegal content, such as child pornography, is discovered on the hard drive of a customer computer? (Choose two.)"]
                = "Contact a first responder.          Document as much information as possible.",

            ["When performing computer forensics, what can be prevented with a properly and carefully documented chain of custody?"]
                = "evidence tampering",

            ["When performing computer forensics what is required to prove a chain of custody?"] =
                "proper documentation procedures",

            ["A technician has found possible illegal content on the hard drive of a customer computer. When should a computer forensics expert be brought in?"]
                = "after the content is classified as illegal ",

            ["A technician is analyzing a computer that may have been used for illegal activity. What are two examples of volatile data? (Choose two.)"]
                = "network connections that are open             users who are logged in to the computer",

            ["What should a level two technician do immediately after receiving an escalated work order from a level one technician?"]
                = "Call the customer back to ask any additional questions and resolve the problem.",

            ["What is a reason to escalate a problem from a level one technician to a level two technician?"] =
                "when drivers, applications, or operating systems need to be installed",

            ["What are three pieces of information a level one technician should gather from a customer? (Choose three.)"]
                = "contact information          description of the problem             details of any recent changes to the computer",

            ["Which situation would require that a support desk call be given the highest priority?"] =
                "The company cannot operate because of a system failure.",

            ["When does a level one technician prepare an escalated work order?"] =
                "when a problem cannot be resolved within a predetermined amount of time",

            ["Which statement best describes a call center?"] =
                "It is a place that exists within a company and provides computer support to both employees and customers of the company.",
            //hoofdstuk 14 it//

            ["A technician discovers that RAID has stopped working. Which two situations could cause this issue? (Choose two.)"]
                = "The external RAID controller loses power.         The RAID controller fails.",

            ["A technician is upgrading an older PC with a dual core CPU. When the PC restarts, it is slower than it was before the upgrade. The Performance tab from the Task Manager displays only one CPU graph. What is the most probable solution to this problem?"]
                = "Update the BIOS firmware to support the dual core CPU.",

            ["What is a symptom of a printer fuser that needs to be replaced?"] = "Toner is coming off printed pages.",

            ["What is a probable cause of a printer producing pages with ghost images?"] = "a worn drum wiper",

            ["What corrective action should be taken on a printer that prints unknown characters?"] =
                "Reinstall the driver.",

            ["Users in an office complain that they are receiving “Document failed to print” messages when trying to print to a network printer. What is a likely problem?"]
                = "The printer is configured with an incorrect IP address.",

            ["A user has not updated an application for over two years and has just updated to the newest release on the workstation. The user notices, however, that the software with the newest release is operating very slowly. The other applications on the workstation are operating normally. What is a possible cause?"]
                = "The computer does not have enough RAM.",

            ["A technician adds a new optical drive to a computer but the optical drive is not recognized by the computer. The technician thinks that the BIOS firmware needs to be updated and updates the CMOS. However, the computer fails to start. What is a possible solution?"]
                = "Contact the motherboard manufacturer to obtain a new CMOS chip.",

            ["A computer repeatedly locks without any error message. Which two conditions may be the cause of the problem? (Choose two.)"]
                = "The computer has a virus.             An update has corrupted the operating system.",

            ["What usually causes the BSOD in the Windows OS?"] = "device driver compatibility errors",

            ["A user reports that WiFi is not working on a laptop. A technician checks the laptop and notices that the wireless networking icon is missing from the notification area of the task bar. The technician tries to turn the wireless switch on the laptop on and off. However, the wireless NIC is still not displayed. What should be done next to troubleshoot this issue?"]
                = "Activate the NIC in the BIOS or UEFI settings.",

            ["What are two reasons that a workstation would begin to lock up frequently? (Choose two.)"] =
                "failing RAM         an overheating CPU",

            ["A PC is not able to connect to a wired network. Pinging the loopback address is successful, but the gateway cannot be reached. On the network switch all the interface lights are on, except for the interface connected to the PC. The LED on the network card is off. What is the most likely cause of this problem?"]
                = "The network cable is faulty.",

            ["Users in a recently installed wireless network are complaining of slow data transfer and frequent loss of connectivity. The technician checks that the wireless security is correctly implemented, and there is no evidence of unauthorized users on the network. Which two problems might the technician suspect? (Choose two.)"]
                = "There is interference from outside sources.           The wireless signal is too weak.",

            ["A user can send email to other people in the office successful but is unable to receive any email. What is a possible cause of this issue?"]
                = "The computer has incorrect POP3 or IMAP settings.",

            ["A group of users is unable to connect to the network. When testing several of the PCs and issuing the command ipconfig, the technician notices that all of them have an IP address in the 169.254.x.x range. What is the most likely cause of this problem?"]
                = "The DHCP server is not operational.",

            ["Which network server is malfunctioning if a user can ping the IP address of a web server but cannot ping the web server host name?"]
                = "the DNS server",

            ["What command can a technician use on a computer to see if DNS is functioning properly?"] = "nslookup",

            ["An administrator deploys wireless access points across the office to provide wireless network connectivity to users. Each workstation receives an IP address via DHCP. After a file server with a static IP is connected to the wired network, the administrator receives an IP address conflict message. What is a possible solution?"]
                = "Change the static IP configured on the file server.",

            ["A computer displays this message when the computer boots: “MBR has been changed or modified.” What could cause this problem?"]
                = "A boot sector virus has altered the master boot record.",

            ["A technician is troubleshooting a Windows 7 laptop infected with a virus that has damaged the master boot record. The technician has booted the laptop using the installation media and is attempting to repair the laptop from the command line interface. Which two commands can the technician use to repair the corrupt master boot record? (Choose two.)"]
                = "bootrec /fixboot       bootrec /fixmbr",

            ["A user reports that all of the software applications operate very slowly every day around 9:30 a.m. A couple of hours later, the computer operates normally for the rest of the day. What is the most likely cause?"]
                = "The computer is being scanned by the antivirus software on a specified schedule.",
            //it essentials checkpoint exam//

            ["What three voltages are commonly provided by the power supply to the various components inside the computer? (Choose three.)"]
                = "3.3 volts           5 volts            12 volts",

            ["Hard drives in a grocery warehouse keep failing because of the adverse warehouse environment. What would be a possible solution for this high failure rate?"]
                = "Install an SSD drive in each computer.",

            ["A server administrator needs to set up a new server with disk fault tolerance technology. The administrator decides to deploy RAID 0+1 technology. What is the minimum number of disks needed to deploy the disk array setting?"]
                = "4",

            ["Which type of interface should a customer choose if connecting a single cable from a Microsoft Windows computer to output both audio and video to a high definition television?"]
                = "HDMI*",

            ["Refer to the exhibit. To what input device would the cables attach?"] = "KVM switch*",

            ["A desktop user has requested the ability to easily transfer photos from a camera SD card to a computer. Which device can be installed on the desktop computer to fulfill this request?"]
                = "media reader*",

            ["Which is the correct procedure to put out a fire with a fire extinguisher after the pin is pulled?"] =
                "Aim at the base of the flame, squeeze the lever, and sweep from side to side.*",

            ["Which measure can help control RFI effects on wireless networks?"] =
                "Ensure the wireless network is on a different frequency than the offending source.*",

            ["What can be tested with a digital multimeter?"] = "quality of electricity in computer components*",

            ["Which disk management tool scans the critical files of an operating system and replaces the corrupt files?"]
                = "System File Checker*",

            ["Which action can reduce the risk of ESD damage when computer equipment is being worked on?"] =
                "working on a grounded antistatic mat*",

            ["When a used CPU is being installed, what two items should be used to clean the top of the CPU and the base of the heat sink before applying thermal compound? (Choose two.)"]
                = "isopropyl alcohol*            lint-free cloth",

            ["A technician is troubleshooting a problem where many websites return a “certificate not valid” error. The technician notices that the date and time are not current and resets these to the correct settings. The previously inaccessible websites are now accessible. However, when the computer is restarted the same problem reoccurs. What can the technician do to prevent the date and time from resetting?"]
                = "Replace the CMOS battery.*",

            ["Which two hardware features would a technician monitor within the BIOS if a computer was suspected of overheating? (Choose two.)"]
                = "CPU clock speed*            CPU fan speed*",

            ["What component can be replaced and upgraded on a PC to increase the data processing speed?"] = "CPU",

            ["What are two effects of not having a preventive maintenance plan for users and organizations? (Choose two.)"]
                = "increased repair costs*   increased downtime*",

            ["Which three actions should be part of a regular computer preventive maintenance plan? (Choose three.)"] =
                "removing dust from fan intakes, the power supply, and peripherals*    installing appropriate security updates*          removing unused or unwanted programs*",

            ["If a technician is unable to create a backup of data on a customer computer, what three pieces of information should the technician include on the liability release form signed by the customer before beginning work? (Choose three.)"]
                = "permission to work on the computer without a current backup available*            a release from liability if data is lost or corrupted*       a description of the work to be performed*",

            ["What is the first step in the troubleshooting process?"] =
                "Ask the user direct questions about the issue.*",

            ["After performing a preventive maintenance on a PC, the technician powers on the PC only to find that nothing happens. What is the most likely problem?"]
                = "The computer is not plugged into the AC outlet.*",

            ["Which statement describes a characteristic of a network operating system?"] =
                "It runs multiuser applications.*",

            ["What technology was created to replace the BIOS program on modern personal computer motherboards?"] = "",

            ["On which two occasions is it most likely that a technician will have to perform a clean operating system installation if a backup has not been performed? (Choose two.)"]
                = "The existing operating system is corrupted.*    A new replacement hard drive is installed in a computer.*",

            ["Which key or key sequence pressed during the boot process will allow a user to start a Windows PC using the last known good configuration?"]
                = "F8",

            ["In which folder are application files for 32-bit programs typically located on a computer that is running a 64-bit edition of Windows 7?"]
                = "C:Program Files (x86)*",

            ["A technician notices that an application is not responding to commands and that the computer seems to respond slowly when applications are opened. What is the best administrative tool to force the release of system resources from the unresponsive application?"]
                = "Task Manager*",

            ["What does virtual memory provide to an operating system?"] =
                "a temporary storage of data until enough RAM is available to process it*",

            ["A PC technician is having trouble with a Windows-based system and wants to perform a system restore from the command prompt. What command would start this process?"]
                = "rstrui*",

            ["A technician is attempting to create virtual machines on a PC but wants to ensure that the physical system resources used by the virtual machines are directly managed by them and not by the host operating system. How can the technician accomplish this?"]
                = "Set up the virtual machines using a native hypervisor.*",

            ["A company has a preventive maintenance policy that requires every computer have a new restore point created every six months. For computers using the Windows 7 operating system, how can a technician create the restore point?"]
                = "Choose Start > All Programs > Accessories > System Tools > System Restore > Next.*",

            ["A user notices that some of the programs that are installed before upgrading to Windows 7 no longer function properly. What can the user do to fix this problem?"]
                = "Reinstall the programs in Compatibility Mode.*",

            ["A network administrator has finished migrating from a peer-to-peer network to a new client-server network configuration. What are two advantages of the new configuration? (Choose two.)"]
                = "Data is more secure in the event of hardware failures.*       Data resources and access are centrally controlled.*",

            ["At which TCP/IP model layer would a MAC address be found?"] = "network acces",

            ["A technician needs to add a new wireless device to a small WLAN. The WLAN is a mixture of old and newer 802.11b and 802.11g devices. What choice for the new device would provide the most interoperability for present and future growth?"]
                = "Add a new 802.11n device.*",

            ["A customer who wants to enhance home network security is shopping for devices. The sales representative recommends a device that has a hardware firewall. Which device can provide this service?"]
                = "integrated router*",

            ["Copper network cables consist of pairs of wires that are twisted together. Which type of signal interference is reduced or prevented by this?"]
                = "crosstalk*",

            ["A computer is assigned an IP address of 169.254.33.16. What can be said about the computer, based on the assigned address?"]
                = "It cannot communicate outside its own network.*",

            ["A user is configuring a wireless access point and wants to prevent any neighbors from discovering the network. What action does the user need to take?"]
                = "Disable SSID broadcast.*",

            ["A home user was successfully browsing the Internet earlier in the day but is now unable to connect. A ping command from a wireless laptop to a wired PC in the LAN is successful, but the nslookup command fails to resolve a website address. The user decides to analyze the LEDs on the Linksys router to verify connectivity. Which LED should be of main concern in this situation?"]
                = "Internet LED*",

            ["Why would an administrator use Windows Remote Desktop and Windows Remote Assistant?"] =
                "to connect to a remote computer over the network to control its applications and data*",

            ["A corporation has expanded to include multiple remote offices around the globe. Which technology should be used to allow the remote offices to communicate and share network resources privately?"]
                = "VPN*",

            ["What physical layer medium is used by DSL to provide high-speed data communications?"] =
                "telephone lines*",

            ["The current IP configuration of a small company is done manually and is time-consuming. Because of increased network growth, a technician needs a simpler way for IP configuration of workstations. Which service would simplify the workstation IP configuration task?"]
                = "DHCP*",

            ["What is the function of the DNS server?"] = "It maps a hostname to a corresponding IP address.*",

            ["A computer can access devices on the same network but cannot access devices on other networks. What is the probable cause of this problem?"]
                = "The computer has an invalid default gateway address.*",
            // rekenen domein 2//

            ["De eigenaar van een warenhuis heeft het gebouw na laten maken op"] = "2,16",

            ["De zandbak is 70cm diep."] = "141,12",

            ["Wat was de gemiddelde snelheid van Filho tijdens zijn olympische race"] = "8,45",

            ["Marit huurt een kleine kluis op vrijdag om 14:16 uur."] = "50",

            ["Hoeveel middelgrote zoutkisten kun je vullen met 2,4 ton strooizout?"] = "8",

            ["Aike geeft zijn kat een antibioticakuur van 30 dagen."] = "150",

            ["De breedte van de grote doos is twee keer zo groot als de breedte van"] = "16,2",

            ["De eerste workshopronde begint om 10 uur."] = "15:10",

            ["Uit een kraan komt 15 liter water per minuut."] = "2100",

            ["Berend fietst vanaf de paarse stip tot de witte stip een stuk door de"] = "21",

            ["Ruud koopt tegels voor het terras in zijn tuin."] = "215,70",

            ["Wat is de oppervlakte van de woonkamer in vierkante meter?"] = "26,11",

            ["Joep rijdt deze route 2 dagen per week met zijn auto heen en terug."] = "5,67",

            ["Van welke figuur is dit een uitslag?"] = "C",

            ["Patrick gaat met de auto naar zijn werk."] = "8",

            ["36 g - 200 mg"] = "35,8",

            ["Kristel legt plakplinten langs de muur van haar woonkamer."] = "23,70",

            ["straat"] = "b (het meest rechtsonder"
        };



        internal static IReadOnlyList<Question> LoadQuestions()
        {
            return questionDict.Select(item => new Question(item.Key, item.Value)).ToList();
        }
    }
}