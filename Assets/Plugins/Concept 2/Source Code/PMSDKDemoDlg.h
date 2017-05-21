// PMSDKDemoDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "compiler.h"
#include "PM3DDICP.h"
#include "PM3CsafeCP.h"
#include "pc_error.h"
#include "MMSystem.h"
#include "tk_time.h"
#include "csafe.h"

#pragma pack(push, before_firmware, 1)
#include "logmap.h"
#pragma pack(pop, before_firmware)

// CPMSDKDemoDlg dialog
class CPMSDKDemoDlg : public CDialog
{
// Construction
public:
	CPMSDKDemoDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_PMSDKDEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
   unsigned short OnReset(void);

	afx_msg LRESULT OnProgressMessage(WPARAM wParam, LPARAM lParam);

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

   UINT16_T discoverDevices();
   UINT16_T numCommunicating;

public:
   afx_msg void OnCbnSelchangeUnitId();
   afx_msg void OnFilesave();
   afx_msg void OnSend();
   afx_msg void OnResetButton(void);
   afx_msg void OnSelchangeUnitId();
   afx_msg void OnSelchangeCommand();
	afx_msg void OnClose();

   CButton m_Repeat;
   CEdit m_Rate;
   CButton m_FileSave;
   CButton m_Reset;
   CEdit m_Time;
   CButton m_Send;
   CEdit m_Data;
   CComboBox m_Unit_ID;
   CEdit m_Serial_Num;
   CEdit m_Response;
   CComboBox m_Command;
   CString m_DataPrompt;

	FILE *stream;
   char filename[MAX_PATH];
   UINT32_T lo_address;
   UINT32_T hi_address;
	UINT16_T num_units;
	UINT16_T nNumChars;
};
