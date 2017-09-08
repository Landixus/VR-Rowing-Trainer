/*
 * Author: Grant Burgess
 * Date: 20/08/17
 * Purpose: Script to save training summary to xml or excel spreadsheet
 *			Currently unable to complete due to issues using the interop.excel dll with unity
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
//using Excel = Microsoft.Office.Interop.Excel;
using UnityEngine;

public class Stats_To_XML : MonoBehaviour {

	/*
	Excel.Application xlApp;
	Excel._Workbook xlWB;
	Excel._Worksheet xlWS;
	Excel.Range xlRange;

	// Use this for initialization
	void Start() {
		//Save_Data();

	}

	// Update is called once per frame
	void Update() {

	}

	public void Save_Data() {
		xlApp = new Microsoft.Office.Interop.Excel.Application();
		xlApp.Visible = true;

		xlWB = (Microsoft.Office.Interop.Excel._Workbook)(xlApp.Workbooks.Add(""));
		xlWS = (Microsoft.Office.Interop.Excel._Worksheet)xlWB.ActiveSheet;

		xlWS.Cells[1, 1] = "Date";
		xlApp.Visible = false;
		xlApp.UserControl = false;
		xlWB.SaveAs("d:\\test.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
		false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
		Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		xlWB.Close();
	}
	*/
}
	