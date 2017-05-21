// PMSDKDemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PMSDKDemo.h"
#include "PMSDKDemoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


enum COMMAND_LIST {
   COMMAND_INIT,
   COMMAND_DISCOVER,
   COMMAND_GET_FW_VERSION,
   COMMAND_GET_HW_VERSION,
   COMMAND_GET_SERIAL_NUM,
   COMMAND_INIT_PROTOCOL,
   COMMAND_CSAFE,
   COMMAND_RAW_USB
};

UINT16_T g_DefaultDLLTimeout = 70;
UINT16_T g_DLLTimeout;

// The Settings section and entries
CString  szKeySectionPreferences = _T("Settings");
CString  szKeyEntryTimeout = _T("DLL Timeout");

#define RACE_DLL_RETRIES   1
#define RACE_DLL_TIMEOUT   70

#define DEFAULT_FILENAME			"Cmd_Rsp Data.txt"
#define TKUSB_MAX_PACKET_SIZE    500
#define DEFAULT_REPEAT_RATE_MS   1     // Default message Send repeat rate
#define PM_SERIALNUMSTRING_LEN   16
#define PM_FWIDSTRING_LEN        20
#define PM_PNSTRING_LEN          20

// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CPMSDKDemoDlg dialog




CPMSDKDemoDlg::CPMSDKDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPMSDKDemoDlg::IDD, pParent)
   , m_DataPrompt(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CPMSDKDemoDlg::DoDataExchange(CDataExchange* pDX)
{
   CDialog::DoDataExchange(pDX);
   DDX_Control(pDX, IDC_REPEAT, m_Repeat);
   DDX_Control(pDX, IDC_RATE, m_Rate);
   DDX_Control(pDX, IDC_FILESAVE, m_FileSave);
   DDX_Control(pDX, IDC_RESET, m_Reset);
   DDX_Control(pDX, IDC_TIME, m_Time);
   DDX_Control(pDX, IDC_SEND, m_Send);
   DDX_Control(pDX, IDC_DATA, m_Data);
   DDX_Control(pDX, IDC_UNIT_ID, m_Unit_ID);
   DDX_Control(pDX, IDC_SERIAL_NUM, m_Serial_Num);
   DDX_Control(pDX, IDC_RESPONSE, m_Response);
   DDX_Control(pDX, IDC_COMMAND, m_Command);
   DDX_Text(pDX, IDC_DATA_PROMPT_STATIC, m_DataPrompt);
	DDV_MaxChars(pDX, m_DataPrompt, 256);
}

BEGIN_MESSAGE_MAP(CPMSDKDemoDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
   ON_CBN_SELCHANGE(IDC_UNIT_ID, &CPMSDKDemoDlg::OnSelchangeUnitId)
   ON_BN_CLICKED(IDC_FILESAVE, &CPMSDKDemoDlg::OnFilesave)
   ON_BN_CLICKED(IDC_SEND, &CPMSDKDemoDlg::OnSend)
   ON_BN_CLICKED(IDC_RESET, &CPMSDKDemoDlg::OnResetButton)
   ON_MESSAGE(TKCMDSET_PROGRESS_MESSAGE, OnProgressMessage)
   ON_CBN_SELCHANGE(IDC_COMMAND, &CPMSDKDemoDlg::OnSelchangeCommand)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


UINT16_T GetAppVersion(void);

/****************************************************************************
   GetAppVersion()

   Returns the current version number of this software.

   Inputs:     none

   Outputs: UINT16_T ver_info       High byte is major version number
                                    Low byte is minor version number
****************************************************************************/
UINT16_T GetAppVersion(void) 
{

   CString appname = _T("PMSDKDemo.exe");
   UINT16_T retval = 0;
   DWORD dummy;
   VS_FIXEDFILEINFO * FixedInfo;
   unsigned int FixedInfoLen;

   // Get the size of the version information
   int VersionInfoSize = GetFileVersionInfoSize(appname,&dummy);
   if (!VersionInfoSize)
      return 0;

   // Allocate memory to hold version information
   char *VersionBuf = new char[VersionInfoSize];
   if (!VersionBuf)
      return 0;

   // Load version information into buffer
   if (GetFileVersionInfo(appname,dummy,VersionInfoSize,VersionBuf)) {

      // Extract file version information from buffer
      VerQueryValue(VersionBuf,TEXT("\\"),(void **) &FixedInfo,&FixedInfoLen);

      // Encode version info into UINT16_T
      retval = (UINT16_T) FixedInfo->dwFileVersionMS;
      retval <<= 8;
      retval |= (FixedInfo->dwFileVersionLS & 0xFF);
   }

   // Free allocated memory
   delete VersionBuf;

   // Return version number
   return retval;
}

// CPMSDKDemoDlg message handlers

BOOL CPMSDKDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

   ERRCODE_T ecode = 0;
   static char rspbuf[32];
	m_Unit_ID.SetCurSel(0);
	m_Command.SetCurSel(0);
	m_Time.SetWindowText(CString(""));
	m_FileSave.SetCheck(FALSE)	;
	stream = NULL;

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// Get version information
	UINT16_T ver_num = GetAppVersion();

   /*
    * Recover the preferences from the registry.
    */
   // Set the registry key under which our settings are stored.
   CWinApp* pApp = AfxGetApp();

   // DLL timeout
   g_DLLTimeout = pApp->GetProfileInt(szKeySectionPreferences, 
                                    szKeyEntryTimeout, g_DefaultDLLTimeout);

   // Point to active window
	CWnd * hpAppWindow;
	hpAppWindow = AfxGetApp()->m_pMainWnd;

   OnSelchangeCommand();

   // Initialize PM interface
	ecode = OnReset();

   if (!ecode)
   {
      ecode = tkcmdsetDDI_serial_number(0,rspbuf, PM_SERIALNUMSTRING_LEN);
      // Display the results
      if (!ecode)
         m_Serial_Num.SetWindowText(CString(rspbuf));
   }

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPMSDKDemoDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CPMSDKDemoDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CPMSDKDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CPMSDKDemoDlg::OnCbnSelchangeUnitId()
{
   // TODO: Add your control notification handler code here
}

void CPMSDKDemoDlg::OnFilesave()
{
	ERRCODE_T ecode = 0;
	char name[40];
	char text[200];
	char memo[250];

	// If FileSave selected
	if (m_FileSave.GetCheck())
	{
		// Create filename string
		strcpy(filename, DEFAULT_FILENAME);
		
		// Open file for saving cmd/rsp data
		stream = fopen(filename, "wt");

		// Check for open failed.
		if (stream == NULL)
			ecode = TKLOGCARD_FILE_OPEN_ERROR;

	}
	else
	{
		// Close the stream
		fclose(stream);
	}

   // Format the error 
   tkcmdsetDDI_get_error_name(ecode,name,sizeof(name));
   tkcmdsetDDI_get_error_text(ecode,text,sizeof(text));
      
   sprintf(memo,"Error (%d):  %s\r\n%s",ecode,name,text);
}

void CPMSDKDemoDlg::OnSend()
{
   static char rspbuf[64000];
   ERRCODE_T ecode = 0;
   UINT32_T StartTime;
   UINT32_T ElapsedTime;
   UINT8_T port;
   char *token;
   UINT8_T  cval1;
   UINT16_T sval1;
   UINT32_T lval1;
   unsigned char slaveStatus;

   UINT16_T index;
   char *ss;
   char CmdData[10000];
   CString cmdDataString();
   char RspData[200];
   char CmdData1[200];
   char CmdData2[200];
   char CmdData3[200];
   char CmdData4[200];
   char CmdData5[200];
   char CmdData6[200];
   const char delims[] = " ,";
   UINT16_T i,j;
   static char cmdbuf[512];
   char *tokenData;
   UINT32_T cmd_data[64];
   UINT16_T cmd_data_size;
   UINT32_T rsp_data[64];
   UINT16_T rsp_data_size;

   /* Disable the Send button */
   m_Send.EnableWindow(FALSE);

   /* Get identifier for communication port */
   port = m_Unit_ID.GetCurSel();

   // Read the text in the edit component for command parameters
   m_Data.GetWindowText(CmdData,sizeof CmdData);

   /* Initialize the parameter buffers */
   CmdData1[0] =
   CmdData2[0] = 
   CmdData3[0] = 
   CmdData4[0] = 
   CmdData5[0] = 
   CmdData6[0] = NULL;

   /* Parse the first parameter */
   token = strtok((char *)CmdData,delims);
	if (token) 
   {
      strcpy(CmdData1,token);

      /* Parse the second parameter */
      token = strtok(NULL,delims);
      if (token) 
      {
         strcpy(CmdData2,token);

         /* Parse the third parameter */
         token = strtok(NULL,delims);
         if (token) 
         {
            strcpy(CmdData3,token);

            /* Parse the fourth parameter */
            token = strtok(NULL,delims);
            if (token)
            {
               strcpy(CmdData4,token);

               /* Parse the fifth parameter */
               token = strtok(NULL,delims);
               if (token) 
               {
                  strcpy(CmdData5,token);

                  /* Parse the sixth parameter */
                  token = strtok(NULL,delims);
                  if (token) 
                  {
                     strcpy(CmdData6,token);
                  }
               }
            }
         }
      }
   }

   rspbuf[0] = 0;

   index = m_Command.GetCurSel();

   TKTIME_DELAY_MS_M(1);
   
   /* Store the start time */
   StartTime = TKTIME_GETTIME_M();

   // Send the appropriate command
   switch (index)
   {
   case CB_ERR:
      break;

   case COMMAND_INIT:  // Init
	   ecode = tkcmdsetDDI_init();	
      break;
   case COMMAND_DISCOVER:  // Discover
		num_units=0;
      cval1 = (UINT8_T) strtoul(CmdData1,&ss,16);
      
      num_units = discoverDevices();

      if (num_units)
		{
			j=0;
         j += sprintf(rspbuf,"PM's discovered: %u",num_units);
      }
      else
      {
   	   j=0;
         j += sprintf(rspbuf,"No PM's discovered");
      }
      break;
   case COMMAND_GET_FW_VERSION:  // Firmware Version
      ecode = tkcmdsetDDI_fw_version(port, rspbuf, PM_FWIDSTRING_LEN);

      break;
   case COMMAND_GET_HW_VERSION:  // Hardware Version
      ecode = tkcmdsetDDI_hw_version(port, rspbuf, PM_PNSTRING_LEN);
      break;
   case COMMAND_GET_SERIAL_NUM:  // Serial Number
      ecode = tkcmdsetDDI_serial_number(port, rspbuf, PM_SERIALNUMSTRING_LEN);
      break;
   case COMMAND_INIT_PROTOCOL:  // Init CSAFE protocol
      sval1 = (UINT16_T) strtoul(CmdData1,&ss,16);
      ecode = tkcmdsetCSAFE_init_protocol(sval1);
      break;
   case COMMAND_CSAFE:  // Send CSAFE command
      // Re-read the text in the edit component for command parameters
      m_Data.GetWindowText(CmdData,sizeof CmdData);

      /* Parse the first data value */
      i = 0;
	   j = 0;
      tokenData = (char *) CmdData;
      token = strtok(tokenData,delims);

      /* Convert and load all the data into a buffer */
      while (token && (i < sizeof(cmd_data)))
      {
         cmd_data[i] = (UINT32_T) strtoul(token,&ss,16);
         token = strtok(NULL,delims);
         j += sprintf(cmdbuf+j,"%02XH ",cmd_data[i]);
		   i++;
	  }
      cmd_data_size = i;

      /* Re-store the start time since we parsed a bunch of data */
      StartTime = TKTIME_GETTIME_M();

      if (cmd_data_size)
      {
         rsp_data_size = 100;
         ecode = tkcmdsetCSAFE_command(port, cmd_data_size, cmd_data,
                                       &rsp_data_size, rsp_data);
         if (!ecode)
         {
            j=0;
            for (i=0; i<rsp_data_size; i++)
               j += sprintf(rspbuf+j,"%08XH ",rsp_data[i]);
         }
      }

      // Save to file if SaveToFile is selected
      if (m_FileSave.GetCheck())
      {
		fprintf(stream, " %s %s\n\r", cmdbuf, rspbuf);

      }
      break;

   case COMMAND_RAW_USB:   // Talk to USB command/response engine directly
      UINT8_T cmd_buffer[128];
      UINT16_T cmd_buffer_size;
      UINT16_T rsp_buffer_size;
      UINT8_T rsp_buffer[128];

      // Re-read the text in the edit component for command parameters
      m_Data.GetWindowText(CmdData,sizeof CmdData);

      /* Parse the first data value */
      i = 0;
	   j = 0;
      token = strtok((char *)CmdData,delims);

      /* Convert and load all the data into a buffer */
      while (token && (i < sizeof(cmd_buffer)))
      {
         cmd_buffer[i] = (UINT8_T) strtoul(token,&ss,16);
         token = strtok(NULL,delims);
         j += sprintf(cmdbuf+j,"%02XH ",cmd_buffer[i]);
         i++;
      }
      cmd_buffer_size = i;

      if (cmd_buffer_size)
      {
         rsp_buffer_size = 100;

         // Go through the DDI layer so that we get the correct media port
         ecode = tkcmdsetDDI_do_command(port, cmd_buffer, cmd_buffer_size,
            rsp_buffer, &rsp_buffer_size, g_DLLTimeout);

         if (!ecode)
         {
            j=0;
            for (i=0; i<TKUSB_MAX_PACKET_SIZE; i++)
            {
               j += sprintf(rspbuf+j,"%02X ",rsp_buffer[i]);
               if (rsp_buffer[i] == CSAFE_FRAME_END_BYTE)
			      {
				     // Save to file if SaveToFile is selected
				     if (m_FileSave.GetCheck())
				     {
					   fprintf(stream, " %s %s\n\r", cmdbuf, rspbuf);

				     }
                 break;
			      }
            }
            // Get status byte
            if (rsp_buffer[0] == CSAFE_EXT_FRAME_START_BYTE)
            {
               // Extended frame
               slaveStatus = rsp_buffer[3];
            }
            else
            {
               // Standard frame
               slaveStatus = rsp_buffer[1];
            }
         }
      }
      break;

  default:
      sprintf(rspbuf,"Command not supported");
      break;
   }

   ElapsedTime = TKTIME_ELAPSED_TIME_MS_M(StartTime);
   /* Display the execution time */
   sprintf(RspData,"%d",ElapsedTime);
   m_Time.SetWindowText(CString(RspData));

   // Display slave status byte
   unsigned char tempByte;
   CString tempText;

   if (index != COMMAND_RAW_USB)
   {
      slaveStatus = tkcmdsetCSAFE_get_status();
   }

   // Frame count
   tempByte = GET_FRAME_COUNT(slaveStatus);
   tempText.Format(TEXT("%d"), tempByte);
   GetDlgItem(IDC_FRAME_COUNT_STATIC)->SetWindowText(CString(tempText));

   // Slave state
   tempByte = GET_SLAVE_STATE(slaveStatus);
   switch (tempByte)
   {
      case STATE_ERROR:
      default:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Error"));
         break;
      case STATE_READY:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Ready"));
         break;
      case STATE_IDLE:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Idle"));
         break;
      case STATE_HAVEID:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("HaveID"));
         break;
      case STATE_INUSE:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("InUse"));
         break;
      case STATE_PAUSED:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Paused"));
         break;
      case STATE_FINISHED:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Finished"));
         break;
      case STATE_MANUAL:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Manual"));
         break;
      case STATE_OFFLINE:
         GetDlgItem(IDC_STATE_STATIC)->SetWindowText(_T("Offline"));
         break;   
   }

   // Slave frame status
   tempByte = GET_FRAME_STATUS(slaveStatus);
   switch (tempByte)
   {
      case STATUS_OK:
      default:
         GetDlgItem(IDC_PREV_STATUS_STATIC)->SetWindowText(_T("Prev OK"));
         break;
      case STATUS_PREV_REJECT:
         GetDlgItem(IDC_PREV_STATUS_STATIC)->SetWindowText(_T("Prev Reject"));
         break;
      case STATUS_PREV_BAD:
         GetDlgItem(IDC_PREV_STATUS_STATIC)->SetWindowText(_T("Prev Bad"));
         break;
      case STATUS_PREV_NOT_READY:
         GetDlgItem(IDC_PREV_STATUS_STATIC)->SetWindowText(_T("Prev Not Ready"));
         break;
   }

   if (ecode) 
   {   // An error has occured
      char name[40];
      char text[200];
      char memo[250];

      // Format the error by looking up info in INI file
      tkcmdsetDDI_get_error_name(ecode,name,sizeof(name));
      tkcmdsetDDI_get_error_text(ecode,text,sizeof(text));
      
      sprintf(memo,"Error (%d):  %s\r\n%s",ecode,name,text);

      // Display the error information
      m_Response.SetWindowText(CString(memo));

      // Stop sending commands
      m_Send.EnableWindow(TRUE);
      m_Send.SetFocus();
   }
   else 
   {         // No error
      // Display the results
      m_Response.SetWindowText(CString(rspbuf));

	  // Press the "Send" button again if repeat is selected
      if (m_Repeat.GetCheck())
      {
         // Get our repeat rate from the user
         m_Rate.GetWindowText(CmdData,sizeof CmdData);

         // Parse the data 
         if (*CmdData != NULL)
			{
				token = strtok((char *)CmdData,delims);
				lval1 = (UINT32_T) strtoul(token,&ss,10);
			}
			else
				lval1 = 0;

         // If a value set, use it.  Otherwise, set at default
         if (lval1)
            TKTIME_DELAY_MS_M(lval1);
         else
            TKTIME_DELAY_MS_M(DEFAULT_REPEAT_RATE_MS);

         PostMessage(WM_COMMAND, (BN_CLICKED << 16) | IDC_SEND, NULL);
      }
    // Otherwise, reenable the "Send" button
      else 
      {
         m_Send.EnableWindow(TRUE);
         m_Send.SetFocus();
      }
   }	

}

void CPMSDKDemoDlg::OnResetButton()
{
   OnReset();
}
unsigned short CPMSDKDemoDlg::OnReset()
{
   ERRCODE_T ecode = 0;
   char name[40];
   char text[200];
   char memo[250];
//	int i;
	int j=0;

	nNumChars = 0;


	sprintf(memo,"Initializing...");
	m_Response.SetWindowText(CString(memo));
   num_units = 0;

   // Init DDI DLL
   ecode = tkcmdsetDDI_init();
   if (!ecode)
   {
      // Init CSAFE protocol
      ecode = tkcmdsetCSAFE_init_protocol(100);


      // Discover PM's
		if (!ecode)
		{
         num_units = discoverDevices();
		}
	}
	if (ecode)
   {   // An error has occured
      // Format the error by looking up info in INI file
      tkcmdsetDDI_get_error_name(ecode,name,sizeof(name));
      tkcmdsetDDI_get_error_text(ecode,text,sizeof(text));

      sprintf(memo,"Error (%d):  %s\r\n%s",ecode,name,text);
   }
   else
   {
      OnSelchangeUnitId();
      if (num_units)
			j += sprintf(memo+j,"PM's discovered: %u",num_units);
		else
			j += sprintf(memo+j,"Reset OK - No PM's discovered.");

   }


   // Display the information
   m_Response.SetWindowText(CString(memo));

   // Stop sending commands
   m_Send.EnableWindow(TRUE);
   m_Send.SetFocus();

	return(ecode);
}

void CPMSDKDemoDlg::OnSelchangeUnitId()
{
   static char rspbuf[32];
   UINT16_T port;
   ERRCODE_T ecode = 0;
   
      /* Get identifier for communication port */
   port = m_Unit_ID.GetCurSel();
   m_Serial_Num.SetWindowText(CString(""));
   ecode = tkcmdsetDDI_serial_number(port,rspbuf, PM_SERIALNUMSTRING_LEN);
      // Display the results
   if (!ecode)
      m_Serial_Num.SetWindowText(CString(rspbuf));
}

void CPMSDKDemoDlg::OnSelchangeCommand()
{
	// Command pulldown menu selection has changed

   UINT16_T index;
   CString dataPrompt = _T("No data required for the selected command");
   CString dataString = _T("");
   
   // Clear the data field and prompt user
   m_Data.SetWindowText((LPCTSTR) dataString);

   index = m_Command.GetCurSel();

   // Send the appropriate prompt based upon the selection
   switch (index)
   {
      case COMMAND_INIT_PROTOCOL:  // Init protocol
         dataPrompt = _T("Enter protocol timeout in ms or leave blank for default");
         break;

      case COMMAND_CSAFE:     // Send CSAFE command
         dataPrompt = _T("Enter CSAFE command - Format: cmd1, [data1, data2, dataX], [cmd2, data1, dataX]");
         break;

      case COMMAND_RAW_USB:   // Talk to USB command/response engine directly
         dataPrompt = _T("Enter command -  Format: F0 or F1, [FD, 00,] cmd1, [data1, dataX], [cmd2, dataX], checksum, F2");
//         m_Data.SetWindowText(_T("F0 FD 00 "));
         break;

      default:
         break;
   }

   // Set the data field prompt text
	GetDlgItem(IDC_DATA_PROMPT_STATIC)->SetWindowText(dataPrompt);
}

LRESULT CPMSDKDemoDlg::OnProgressMessage(WPARAM wParam, LPARAM lParam)
{
   LRESULT lResult = 0;
   char temp[150];

   static int i = 0;

   switch (wParam)
   {
      case TKCMDSET_READ_LOG_DATA:
      case TKCMDSET_WRITE_LOG_DATA:
      case TKCMDSET_READ_RAM_DATA:
      {
         int percent_complete = (((UINT32_T) lParam - lo_address) * 100) / 
            (hi_address - lo_address);

         if (wParam == TKCMDSET_READ_LOG_DATA)
				sprintf(temp,"Reading Log Card...\r\nCurrent address:\t%08XH\r\nPercent Complete:\t%d%%",lParam,percent_complete);
			else if (wParam == TKCMDSET_WRITE_LOG_DATA)
				sprintf(temp,"Writing Log Card...\r\nCurrent address:\t%08XH\r\nPercent Complete:\t%d%%",lParam,percent_complete);
			else if (wParam == TKCMDSET_READ_RAM_DATA)
				sprintf(temp,"Reading ESRAM...\r\nCurrent address:\t%08XH\r\nPercent Complete:\t%d%%",lParam,percent_complete);

         m_Response.SetWindowText(CString(temp));
         m_Response.UpdateWindow();

         break;
      }
      default:
         break;
   }

   return lResult;
}

UINT16_T CPMSDKDemoDlg::discoverDevices()
{
   UINT16_T i;
   UINT16_T j;
   UINT16_T numOldPM3Devices = 0;
   UINT16_T numNewPM3Devices = 0;
   UINT16_T numPM4Devices = 0;
   UINT16_T numPM5Devices = 0;
   ERRCODE_T ecode = 0;
      
   numCommunicating = 0;

   // Look for PM3 devices
   ecode = tkcmdsetDDI_discover_pm3s(TKCMDSET_PM3_PRODUCT_NAME2, 0, &numCommunicating);
  
   if (!ecode && numCommunicating)
   {
      // We discovered one or more PM3's
      numNewPM3Devices = numCommunicating;
   }

   // Look for old style PM3 devices, starting numbering after the previous
   ecode = tkcmdsetDDI_discover_pm3s(TKCMDSET_PM3_PRODUCT_NAME, numCommunicating, &numCommunicating);
  
   if (!ecode && numCommunicating)
   {
      // We discovered one or more old PM3's
      numOldPM3Devices = numCommunicating - numNewPM3Devices;
   }

   // Look for PM4 devices
   ecode = tkcmdsetDDI_discover_pm3s(TKCMDSET_PM4_PRODUCT_NAME, numCommunicating, &numCommunicating);

   if (!ecode && numCommunicating)
   {
      // We discovered one or more PM4's
      numPM4Devices = numCommunicating - numNewPM3Devices - numOldPM3Devices;
   }

   // Look for PM5 devices
   ecode = tkcmdsetDDI_discover_pm3s(TKCMDSET_PM5_PRODUCT_NAME, numCommunicating, &numCommunicating);

   if (!ecode && numCommunicating)
   {
      // We discovered one or more PM5's
      numPM5Devices = numCommunicating - numNewPM3Devices - numOldPM3Devices - numPM4Devices;
   }

   // Initialize each of the PMs discovered
   j = numCommunicating;
   for (i=0; i<j; i++)
   {

   }

   return numCommunicating;
}

void CPMSDKDemoDlg::OnClose() 
{
   // Shut down the command set toolkit functions 
   tkcmdsetDDI_shutdown_all();

   CDialog::OnClose();
}