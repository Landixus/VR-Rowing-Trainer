/*******************************************************************************
********************************************************************************

   COPYRIGHT � 2003 Concept 2

   Contains confidential and proprietary information which may not be copied,
   disclosed or used by others except as expressly authorized in writing
   by Concept 2.

   PM3Commands.h	External Interface Definitions for the PM3 Extended CSAFE 
						Command Set.

   About:     This module contains the DLL external interface definitions
				  that allow PC-based applications to communicate with PM3s
              using the PM3 "extended" CSAFE commands.

   Target:        Win32 Compatible

   Written for:   Concept 2
                  105 Industrial Park Drive
                  Morrisville, VT 05661
                  802-888-5225 (Voice)
                  802-888-6331 (Fax)

   Contacts:      Scott Hamilton 
                  802-888-5226 x3062 (Voice)
                  scotth@concept2.com

   Written by:    Andrew Dombek
                  Microprocessor Designs, Inc.
                  65 Longmeadow Drive
                  PO Box 160
                  Shelburne, VT 05482
                  http://www.updesigns.com

   $Revision: 4 $

--------------------------------------------------------------------------------
   $Log: /Concept II/C2PM3/Pc_src/DLLs XPlatform/PM3CsafeCP/PM3CommandsCP.h $
 * 
 * 4     4/23/08 11:25a Adomb
 * PM3CsafeCP V3.14 - Added readMemBlock function.
 * 
 * 3     1/30/08 3:06p Adomb
 * PM3CsafeCP V3.12 - Added function to set the date/time on the PM.
 * 
 * 2     9/14/05 9:04a Adomb
 * Fixed bug in read logCard routine.  Wasn't returning a count of 0 when
 * read error occurred.
 * 
 * 1     7/27/05 8:02a Adomb
 * 
 * 4     7/06/04 9:05a Adomb
 * Added reset erg function.
 * 
 * 3     5/27/04 11:08a Adomb
 * Added get logcard status command to do tickle function.
 * 
 * 2     4/27/04 7:58a Adomb
 * Initial support for single user read/repair/write LogCard.
 * 
 * 1     10/30/03 7:45a Adomb
 * 
 * 2     10/28/03 8:19a Adomb
 * Released a test version of the Race DLL and the DDIMenu application to
 * Concept2.
 * 
 * 
********************************************************************************
*******************************************************************************/
#ifndef _PM3COMMANDS_H
#define _PM3COMMANDS_H


#ifdef _MSC_VER
   #ifdef PM3CSAFECP_EXPORTS
   #define PM3CSAFE_API extern "C" __declspec(dllexport)
   #else
   #define PM3CSAFE_API extern "C" __declspec(dllimport)
   #endif
#else
   #define PM3CSAFE_API
#endif

/*******************************************************************************
 *******************************************************************************
								  Definitions
********************************************************************************
*******************************************************************************/
/*
 * CSAFE PM3-specific command definitions
 */
#define MAX_LOG_CARD_SIZE			32768	// Maximum size of log card
#define LOGCARD_BLOCK_SIZE			64		// #bytes to read/write at a time to/from logcard
#define OLD_LOGCARD_BLOCK_SIZE	32		// #bytes to read/write at a time to/from logcard
#define MEMORY_BLOCK_SIZE			64		// #bytes to read/write at a time to/from memory
#define MAX_LOGGED_WORKOUTS		2048	// Maximum number of logged workouts

#define PM3_CMD_CMD_ID_LOC					0	// Command type location
#define PM3_RSP_CMD_ID_LOC					1	// Response type location
#define PM3_RSP_DATA_LOC					2	// Location of first response data
					
#define PM3_CMDTYPE_BYTE_LOC				0	// First two bytes same for all cmds
#define PM3_CMD_BYTE_LOC					1
#define PM3_CMD_DATA_BYTE_LOC				2	

#define PM3_LOGCARD_CMD_ADDRESS_LOC		2	// Log card command frame
#define PM3_LOGCARD_CMD_BLOCK_SIZE_LOC	3
#define PM3_LOGCARD_CMD_DATA_SIZE		4

#define PM3_LOGCARD_CMD_WRITE_DATA_LOC	4

#define PM3_LOGCARD_RSP_CMDTYPE_LOC		0	// Log card response frame
#define PM3_LOGCARD_RSP_CMD_LOC			1	
#define PM3_LOGCARD_RSP_DATA_COUNT_LOC	2	
#define PM3_LOGCARD_RSP_DATA_LOC			3	

#define PM3_GETMEM_CMD_MEMTYPE_LOC		2	// Get memory command frame
#define PM3_GETMEM_CMD_ADDRESS_LOC		3	
#define PM3_GETMEM_CMD_BLOCK_SIZE_LOC	4
#define PM3_GETMEM_CMD_DATA_SIZE			5

#define PM3_GETMEM_RSP_CMDTYPE_LOC		0	// Get memory response frame
#define PM3_GETMEM_RSP_CMD_LOC			1	
#define PM3_GETMEM_RSP_DATA_COUNT_LOC	2	
#define PM3_GETMEM_RSP_DATA_LOC			3	


#define PM3_SETERG_HW_ADDRESS_LOC		2	// Set Erg# command frame
#define PM3_SETERG_ERG_ADDRESS_LOC		3
#define PM3_SETERG_CMD_DATA_SIZE			4

#define PM3_GETERG_HW_ADDRESS_LOC		2	// Get Erg# command frame
#define PM3_GETERG_ERG_ADDRESS_LOC		3
#define PM3_GETERG_CMD_DATA_SIZE			3	 
#define PM3_GETERG_RSP_DATA_LOC			2	

#define PM3_RESETERG_CMD_DATA_SIZE		2

#define PM3_GET_HW_ADDRESS_CMD_DATA_SIZE	2		// Get serial# command
#define PM3_GET_HW_ADDRESS_RSP_DATA_LOC	2

#define PM3_GET_FW_VERSION_CMD_DATA_SIZE	2		// Get version# command
#define PM3_GET_FW_VERSION_RSP_DATA_LOC	2

#define PM3_GET_TICK_TIMEBASE_CMD_DATA_SIZE	2	// Get tick timebase
#define PM3_GET_TICK_TIMEBASE_RSP_DATA_LOC	2

#define PM3_GET_TICK_TIME_CMD_DATA_SIZE	2		// Get latched tick time
#define PM3_GET_TICK_TIME_RSP_DATA_LOC		2

#define PM3_SET_TICK_TIME_CMD_DATA_SIZE	3		// Set tick time
#define PM3_SET_TICK_TIME_TICK_PARAM_LOC	2

#define PM3_LATCH_TICK_TIME_CMD_DATA_SIZE	2		// Latch tick time

#define PM3_SETBAUD_CMD_DATA_SIZE		3			// Set baud rate
#define PM3_SETBAUD_BAUDADDRESS_LOC		2

#define PM3_SET_RACE_OP_CMD_DATA_SIZE	3			// Set race operation type
#define PM3_SET_RACE_OP_TYPE_PARAM_LOC	2

#define PM3_SET_WORKOUT_TYPE_CMD_DATA_SIZE	3	// Set workout type
#define PM3_SET_WORKOUT_TYPE_PARAM_LOC	2

#define PM3_SET_SCREENSTATE_SCREENTYPE_LOC	2	// Set screen state command frame
#define PM3_SET_SCREENSTATE_SCREENVALUE_LOC	3
#define PM3_SET_SCREENSTATE_CMD_DATA_SIZE		4

enum SET_DATE_TIME_DATA {	                  // Set date and time command frame
	PM3_SET_DATE_TIME_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_TIME_HOURS_LOC,
	PM3_SET_TIME_MIN_LOC,
	PM3_SET_TIME_AMPM_LOC,
	PM3_SET_TIME_MONTH_LOC,
	PM3_SET_TIME_DAY_LOC,
	PM3_SET_TIME_YEAR_LOC,
   PM3_SET_DATE_TIME_CMD_DATA_SIZE
};

#define PM3_SET_RACE_SAMPLE_TICKS_PARAM_LOC			2	// Set race data sample ticks command frame
#define PM3_SET_RACE_SAMPLE_TICKS_CMD_DATA_SIZE		3

#define PM3_SET_RACE_DISPLAY_TICKS_PARAM_LOC			2	// Set race data display ticks command frame
#define PM3_SET_RACE_DISPLAY_TICKS_CMD_DATA_SIZE	3

#define PM3_SET_WORKOUT_DURATION_TYPE_LOC		2	// Set workout duration command frame
#define PM3_SET_WORKOUT_DURATION_LOC			3
#define PM3_SET_WORKOUT_DURATION_CMD_DATA_SIZE		4

#define PM3_SET_SPLIT_DURATION_TYPE_LOC		2	// Set workout duration command frame
#define PM3_SET_SPLIT_DURATION_LOC				3
#define PM3_SET_SPLIT_DURATION_CMD_DATA_SIZE		4

#define PM3_CONFIGURE_WORKOUT_CMD_DATA_SIZE		3	// Configure workout command frame
#define PM3_CONFIGURE_WORKOUT_TYPE_PARAM_LOC	2

#define PM3_SET_AUTHEN_PW_HW_ADR_LOC			2	// Set authentication password command frame
#define PM3_SET_AUTHEN_PW_MSB_AUTHEN_PW_LOC	3
#define PM3_SET_AUTHEN_PW_LSB_AUTHEN_PW_LOC	4
#define PM3_SET_AUTHEN_PW_CMD_DATA_SIZE		5
#define PM3_SET_AUTHEN_PW_RSP_DATA_LOC			2


#define PM3_SET_RACESTARTPARAMS_CMD_DATA_SIZE	7	// Set race start params command frame
#define PM3_SET_RACESTARTPARAMS_TYPE_PARAM_LOC	2
#define PM3_SET_RACESTARTPARAMS_COUNT_PARAM_LOC	3
#define PM3_SET_RACESTARTPARAMS_READY_PARAM_LOC	4
#define PM3_SET_RACESTARTPARAMS_ATTENTION_PARAM_LOC	5
#define PM3_SET_RACESTARTPARAMS_ROW_PARAM_LOC	6

enum GET_RACE_STATUS_DATA {	// Get race data status RESPONSE frame
	PM3_GET_RACE_DATA_RSP_THIS_TIME_LOC = PM3_RSP_DATA_LOC+1,	// 3 (2 commands in response)
	PM3_GET_RACE_DATA_RSP_THIS_METERS_LOC,
	PM3_GET_RACE_DATA_RSP_THIS_500M_PACE_LOC,
	PM3_GET_RACE_DATA_RSP_THIS_ELAPSED_TIME_LOC,
	PM3_GET_RACE_DATA_RSP_THIS_SPM_LOC,
	PM3_GET_RACE_DATA_RSP_THIS_STATE_LOC,
  	PM3_GET_RACE_DATA_RSP_THIS_BATTERY_STATUS_LOC,
   PM3_GET_RACE_DATA_RSP_THIS_SPARE,
	PM3_GET_RACE_DATA_RSP_LAST_TIME_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_METERS_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_500M_PACE_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_ELAPSED_TIME_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_SPM_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_STATE_LOC,
	PM3_GET_RACE_DATA_RSP_LAST_BATTERY_STATUS_LOC,
   PM3_GET_RACE_DATA_RSP_LAST_SPARE,
	PM3_GET_RACE_DATA_RSP_DATA_SIZE
};

enum GET_SET_RACE_STATUS_DATA {	// Get and set race data status command frame
	PM3_GET_RACESTATUS_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_RACESTATUS_FIRST_ID_LOC,	
	PM3_SET_RACESTATUS_FIRST_POS_LOC,	
	PM3_SET_RACESTATUS_FIRST_DELTA_LOC,
	PM3_SET_RACESTATUS_SECOND_ID_LOC,
	PM3_SET_RACESTATUS_SECOND_POS_LOC,	
	PM3_SET_RACESTATUS_SECOND_DELTA_LOC,
	PM3_SET_RACESTATUS_THIRD_ID_LOC,
	PM3_SET_RACESTATUS_THIRD_POS_LOC,	
	PM3_SET_RACESTATUS_THIRD_DELTA_LOC,
	PM3_SET_RACESTATUS_FOURTH_ID_LOC,
	PM3_SET_RACESTATUS_FOURTH_POS_LOC,	
	PM3_SET_RACESTATUS_FOURTH_DELTA_LOC,
	PM3_SET_RACESTATUS_MODE_LOC,	
	PM3_GET_SET_RACESTATUS_CMD_DATA_SIZE
};

enum SET_RACE_PARTICIPANT_DATA {	// Set race participant data command frame
	PM3_SET_RACEPARTICIPANT_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_RACEPARTICIPANT_RACER_ID_LOC,	
	PM3_SET_RACEPARTICIPANT_RACER_NAME_LOC,	
	PM3_SET_RACEPARTICIPANT_CMD_DATA_SIZE
};

enum SET_RACE_LANE_SETUP_DATA {	// Set race lane setup command frame
	PM3_SET_RACE_LANE_SETUP_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_RACE_LANE_SETUP_PHYSICAL_ADDRESS_LOC,	
	PM3_SET_RACE_LANE_SETUP_LANE_NUMBER_LOC,	
	PM3_SET_RACE_LANE_SETUP_CMD_DATA_SIZE
};

enum GET_RACE_LANE_REQUEST_DATA {	// Get race lane request command frame
	PM3_GET_RACELANE_REQUEST_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_RACELANE_REQUEST_CMD_DATA_SIZE
};
#define PM3_GET_RACELANE_REQUEST_RSP_DATA_LOC		2

enum GET_RACE_PARTICIPANT_COUNT_DATA {	// Get race participant data command frame
	PM3_GET_RACEPARTICIPANT_COUNT_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_RACEPARTICIPANT_COUNT_CMD_DATA_SIZE
};
#define PM3_GET_RACEPARTICIPANT_COUNT_RSP_DATA_LOC		2

enum GET_RACE_COMMUNICATION_STATE_DATA {	// Get communication state command frame
	PM3_GET_COMMUNICATION_STATE_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_COMMUNICATION_STATE_CMD_DATA_SIZE
};
#define PM3_GET_COMMUNICATION_STATE_RSP_DATA_LOC		2

enum GET_LOGCARD_STATUS_DATA {	// Get logcard status command frame
	PM3_GET_LOGCARD_STATUS_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_LOGCARD_STATUS_CMD_DATA_SIZE
};
#define PM3_GET_LOGCARD_STATUS_RSP_DATA_LOC		2

enum GET_LOGCARD_STATE_DATA {	   // Get logcard state command frame
	PM3_GET_LOGCARD_STATE_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_LOGCARD_STATE_CMD_DATA_SIZE
};
#define PM3_GET_LOGCARD_STATE_RSP_DATA_LOC		2

enum GET_UPDATE_STATUS_DATA {	   // Get update status command frame
	PM3_GET_UPDATE_STATUS_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_UPDATE_STATUS_CMD_DATA_SIZE
};
#define PM3_GET_UPDATE_STATUS_RSP_DATA_LOC		2

enum GET_RACE_EPM_STATUS_DATA {				// Get epm status command frame
	PM3_GET_EPM_STATUS_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_EPM_STATUS_CMD_DATA_SIZE
};
#define PM3_GET_EPM_STATUS_RSP_DATA_LOC		2

enum SET_DISPLAY_STRING_DATA {	                  // Set display string command frame
	PM3_SET_DISPLAY_STRING_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_DISPLAY_STRING_DATA_LOC,
   PM3_SET_DISPLAY_STRING_CMD_DATA_SIZE
};
#define PM3_NUM_DISPLAY_STRING_CHARS   32

enum GET_ERG_INFO_DATA {	// Get erg info cmd frame (3 commands)
	PM3_GET_SERIAL_NUM_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
   PM3_GET_ERG_INFO_CMD_DATA_SIZE
};

enum GET_ERG_INFO_RSP_DATA {	// Get get erg info RESPONSE frame
	PM3_SERIAL_NUM_RSP_LOC = PM3_RSP_DATA_LOC,	// 2 
   PM3_OPERATION_TYPE_RSP_LOC,
   PM3_RACE_STATE_RSP_LOC,
   PM3_RACE_START_STATE_RSP_LOC,
   PM3_ROWING_STATE_RSP_LOC,
   PM3_EPM_STATUS_RSP_LOC,
   PM3_BATTERY_LEVEL_RSP_LOC,
   PM3_ERG_INFO_RSP_DATA_SIZE
};

enum GET_PRODUCT_CONFIG_CMD_DATA {	// Get product configuration data command frame
	PM3_GET_PRODUCT_CONFIG_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_PRODUCT_CONFIG_CMD_DATA_SIZE
};

enum GET_PRODUCT_CONFIG_RSP_DATA {	// Get product configuration data response frame
	PM3_GET_PRODUCT_CONFIG_PM3_HW_REV_RSP_LOC  = PM3_RSP_DATA_LOC,	// 2
   PM3_GET_PRODUCT_CONFIG_PM3_SW_REV_RSP_LOC,
	PM3_GET_PRODUCT_CONFIG_EXP_CARD_STATUS_LOC,
	PM3_GET_PRODUCT_CONFIG_EXP_CARD_CF_STATUS_LOC,
	PM3_GET_PRODUCT_CONFIG_EXP_CARD_HW_REV_LOC,
	PM3_GET_PRODUCT_CONFIG_EXP_CARD_SW_REV_LOC,
	PM3_GET_PRODUCT_CONFIG_RSP_DATA_SIZE
};

enum SET_LANGUAGE_TYPE_DATA {	                  // Set language type command frame
	PM3_SET_LANGUAGE_TYPE_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_SET_LANGUAGE_TYPE_DATA_LOC,
   PM3_SET_LANGUAGE_TYPE_CMD_DATA_SIZE
};

enum GET_LANGUAGE_TYPE_DATA {	// Get language type command frame
	PM3_GET_LANGUAGE_TYPE_CMD_LOC  = PM3_CMD_BYTE_LOC,	// 1
	PM3_GET_LANGUAGE_TYPE_CMD_DATA_SIZE
};
#define PM3_GET_LANGUAGE_TYPE_RSP_DATA_LOC		2

// Unit addressing
#define PM3_UNDEFINED_UNIT_ADDRESS		65535

/*******************************************************************************
 *******************************************************************************
								  TypeDefs
********************************************************************************
*******************************************************************************/
#ifdef Q_OS_MAC
#ifndef MPD_TYPES
#define MPD_TYPES
#include <CoreFoundation.h>
typedef UInt8       UINT8_T;
typedef UInt16      UINT16_T;
typedef UInt32      UINT32_T;
typedef char        INT8_T;
typedef SInt16       INT16_T;
typedef SInt32       INT32_T;
typedef UInt8       BOOLEAN_T;
typedef Float32     FLOAT32_T;
typedef Float64     FP64_T;
typedef INT16_T     ERRCODE_T;
typedef INT8_T      (*FUNC_PTR_T)(void); 
#endif
#else
/* MPD Standard data type definitions */
#ifndef MPD_TYPES
#define MPD_TYPES
typedef unsigned char  UINT8_T;
typedef unsigned short UINT16_T;
typedef unsigned long  UINT32_T;
typedef char           INT8_T;
typedef short          INT16_T;
typedef long           INT32_T;
typedef unsigned char  BOOLEAN_T;
typedef float          FLOAT32_T;
typedef INT16_T        ERRCODE_T;
#endif
#endif
/*
 * Individual race status data coming from PM3 to PC
 */
typedef struct {
	UINT32_T	this_time_stamp;	//	Tick time stamp of current sample
	UINT32_T	last_time_stamp;	//	Tick time stamp of previous sample
	UINT16_T	this_meters;		//	Current sample total meters
	UINT16_T	last_meters;		//	Previous sample total meters
	UINT8_T	this_spm;			// Current sample stroke rate
	UINT8_T	last_spm;			// Previous sample stroke rate
	UINT16_T	this_500m_pace;	// Current sample 500m pace
	UINT16_T	last_500m_pace;	// Previous sample 500m pace
	UINT8_T	this_state;			// Current sample race state
	UINT8_T	last_state;			// Previous sample race state
	UINT8_T	this_battery_level_pcnt;	// Percent battery level
  	UINT8_T	last_battery_level_pcnt;	// Percent battery level
   UINT32_T this_elapsed_time; // Race elapsed time of current sample (tenths)
   UINT32_T last_elapsed_time; // Race elapsed time of previous sample
} TKCMDSET_PM3_RACE_DATA;

/*
 * Overall race status data coming from PC to PM3
 */
typedef struct {
	UINT8_T first_racer_id;			// ID of racer in first display line
	UINT8_T first_racer_position;	// Position of racer in first display line
	INT32_T first_racer_delta;		// Delta distance of racer in first display line
	UINT8_T second_racer_id;		// ID of racer in second display line
	UINT8_T second_racer_position;// Position of racer in second display line
	INT32_T second_racer_delta;	// Delta distance of racer in second display line
	UINT8_T third_racer_id;			// ID of racer in third display line
	UINT8_T third_racer_position;	// Position of racer in third display line
	INT32_T third_racer_delta;		// Delta distance of racer in third display line
	UINT8_T fourth_racer_id;		// ID of racer in fourth display line
	UINT8_T fourth_racer_position;// Position of racer in fourth display line
	INT32_T fourth_racer_delta;	// Delta distance of racer in fourth display line
	UINT8_T mode;		            // Bit-mapped flags 	
} TKCMDSET_OVERALL_RACE_DATA;

/********************************************************************************
   Prototypes for Command Set functions
********************************************************************************/

/****************************************************************************
   tkcmdsetCSAFE_poll_erg_num

   About:      Poll an erg by requesting it's serial number. 
					Update the device map if successful.

   Inputs:     UINT8_T		media_type		Media type to check
					UINT16_T		media_port		Identifying number to media interface

   Outputs:    UINT16_T *	unit_address_ptr	Logical unit address erg
					BOOLEAN_T *	status_changed_ptr	Change of status flag

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_poll_erg_num(UINT8_T media_type, UINT16_T media_port, 
																	UINT16_T *unit_address_ptr,
																	BOOLEAN_T *	status_changed_ptr);

/****************************************************************************
   tkcmdsetCSAFE_reset_erg_num

   About:      Reset the Erg number on a PM3. 
					PM3 resets to CSAFE_DESTINATION_ADDR_ERG_DEFAULT

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_reset_erg_num(UINT16_T unit_address);

/****************************************************************************
 tkcmdsetCSAFE_get_update_status
 
 Get the firmware update status of the PM.
 
 Inputs:    UINT16_T    unit_address	Address of PM device
            UINT16_T *updateType -  Pointer to update type
            UINT16_T * updateInfo - Pointer to update info
 
 Outputs:   None - updates updateType and updateInfo
 
 Returns:   ERRCODE_T   ecode				Zero if successful
            Error code otherwise
 
 ****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_update_status(UINT16_T unit_address,
                                                       UINT16_T *updateType, UINT16_T *updateInfo);

/****************************************************************************
   tkcmdsetCSAFE_set_erg_num

   About:      Set the Erg number on a PM3

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		erg_num			Erg number to set

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_erg_num(UINT16_T unit_address, 
																 UINT8_T erg_num);

/****************************************************************************
   tkcmdsetCSAFE_get_erg_info

   About:      Get information from a PM3, including the serial number,
               race state, rowing state, epm status and battery status.

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    UINT32_T		*serial_num		   Serial number
               UINT8_T     *battery_percent  Battery level percent
               UINT8_T     *epm_status       Misc erg status flags
               UINT8_T     *state            Race state
               UINT8_T     *start_state      Race start state
               UINT8_T     *rowing_state     Race rowing state
               UINT8_T     *operation_type   Race operation type


   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_erg_info(UINT16_T unit_address, 
                           UINT32_T *serial_num, UINT8_T *battery_percent,
                           UINT8_T *epm_status, UINT8_T *state,
                           UINT8_T *start_state, UINT8_T *rowing_state,
                           UINT8_T *operation_type);

/****************************************************************************
   tkcmdsetCSAFE_get_serial_num

   About:      Get the serial number from a PM3

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    UINT32_T		*serial_num		serial number

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_serial_num(UINT16_T unit_address, 
																	 UINT32_T *serial_num);

/****************************************************************************
   tkcmdsetCSAFE_set_baud_rate

   About:      Set the baud rate on the PM3 serial interface.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT32_T		baud				Baud rate to set

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_baud_rate(UINT16_T unit_address, 
																	 UINT32_T baud);

/****************************************************************************
   tkcmdsetCSAFE_sync_PC_and_PM3_clocks

   About:      Sync the PC and PM3 clocks.  This is done by setting the 
					PM3 tick time to be the same as the PC tick time.
					A conversion is done between the two since they are
					running at different frequencys and resolutions.
					After setting the PM3 tick time, the time is read back
					from the PM3 and compared to the PC's calculated time.
					If the error is within a tolerance, this routine
					returns success.
					Note that if this is a broadcast, the error tolerance
					is not checked.

   Inputs:     UINT16_T    unit_address	Address of PM device
					FLOAT32_T	*gPM3_freq		timebase of PM3 (ticks/sec)
					FLOAT32_T	*time_mux		PC/PM3 timebase conversion multiplier
					UINT32_T		*tolerance		error tolerance

	Outputs:		FLOAT32_T	*actual_error	actual error

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_sync_PC_and_PM3_clocks(UINT16_T unit_address, 
	FLOAT32_T *gPM3_freq, FLOAT32_T *time_mux, UINT32_T *tolerance, FLOAT32_T *actual_error);

/****************************************************************************
   tkcmdsetCSAFE_get_PC_and_PM3_sync_params

   About:      Get the respective timebases of the PC high performance timer
					and the PC, and calculate the timebase conversion multiplier.

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    FLOAT32_T	*gPM3_freq		timebase of PM3 (ticks/sec)
					LARGE_INTEGER *gPC_freq		timebase of PC (ticks/sec)
					FLOAT32_T	*time_mux		timebase conversion multiplier

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
//PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_PC_and_PM3_sync_params(UINT16_T unit_address, 
//	FLOAT32_T *gPM3_freq, LARGE_INTEGER *gPC_freq, FLOAT32_T *time_mux);

/****************************************************************************
   tkcmdsetCSAFE_get_tick_timebase

   About:      Get the tick timebase from the PM3

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    FLOAT32_T	*timebase		timebase

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_tick_timebase(UINT16_T unit_address, 
																	 FLOAT32_T *timebase);

/****************************************************************************
   tkcmdsetCSAFE_set_tick_time

   About:      Set the tick time on the PM3

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT32_T		tick_time		tick time

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_tick_time(UINT16_T unit_address, 
																	 UINT32_T tick_time);

/****************************************************************************
   tkcmdsetCSAFE_set_tick_time_offset

   About:      Set the tick time offset on the PM3. Used in races to synchronize
					PM3 tick time.

   Inputs:     UINT16_T    unit_address	Address of PM device
					INT32_T		tick_time		tick time

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_tick_time_offset(UINT16_T unit_address, 
																	 INT32_T tick_time);

/****************************************************************************
   tkcmdsetCSAFE_latch_tick_time

   About:      Latch the tick time on the PM3 for race synchronization.

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_latch_tick_time(UINT16_T unit_address);

/****************************************************************************
   tkcmdsetCSAFE_get_latched_tick_time

   About:      Get the latched tick time from the PM3.
					Takes two CSAFE commands - CSAFE_PM_GET_TICKTIME & 
														CSAFE_PM_GET_SYNC_DATA

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    FLOAT32_T	*tick_time		tick time

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_latched_tick_time(UINT16_T unit_address, 
																	 FLOAT32_T *tick_time);

/****************************************************************************
   tkcmdsetCSAFE_get_communication_state

   About:      Retrieves from the PM the communication state byte, which
					contains various PM status bits.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T*		pCommState		Pointer to communication state

   Outputs:    Updates data pointed to by pCommState

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_communication_state(UINT16_T unit_address, 
													 UINT8_T * pCommState);

/****************************************************************************
   tkcmdsetCSAFE_get_epm_status

   About:      Retrieves from the PM the epm status byte, which
					contains various PM status bits.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T*		pEpmStatus		Pointer to epm status

   Outputs:    Updates data pointed to by pEpmStatus

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_epm_status(UINT16_T unit_address, 
													 UINT8_T * pEpmStatus);

/****************************************************************************
   tkcmdsetCSAFE_set_race_operation_type

   About:      Set the race operation type on the PM3. (Sets the unit
					to one of the race states).

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		type				Race operation type

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_operation_type(UINT16_T unit_address, 
																	 UINT8_T type);

/****************************************************************************
   tkcmdsetCSAFE_get_race_lane_request

   About:      Retrieves the PM's physical address.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T*		pPhysicalAddress	Pointer to erg physical address

   Outputs:    Updates data pointed to by pPhysicalAddress

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_race_lane_request(UINT16_T unit_address, 
													 UINT8_T * pPhysicalAddress);

/****************************************************************************
   tkcmdsetCSAFE_set_race_lane_setup

   About:      Sends a suggested physical address and lane number to the PM.
					(PM will not actually set the physical address until the user
					subsequently presses a key).

   Inputs:     UINT16_T    unit_address		Address of PM device
					UINT16_T		physical_address	Erg physical address
					UINT16_T		lane_number			Lane number

   Outputs:    Updates data pointed to by pPhysicalAddress

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_lane_setup(UINT16_T unit_address, 
													 UINT16_T physical_Address, UINT16_T lane_number);

/****************************************************************************
   tkcmdsetCSAFE_set_race_participant

   About:      Sends racer ID and racer name information to the PM3. (Allowing
					the PM3 to build a list of IDs and names for the race display).

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT16_T		racer_id			ID of racer
					INT8_T*		racer_name_ptr	Pointer to racer name string

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_participant(UINT16_T unit_address, 
													 UINT16_T racer_id, INT8_T * racer_name_ptr);

/****************************************************************************
   tkcmdsetCSAFE_get_race_participant_count

   About:      Retrieves from the PM the count of the number of participants
					in a race.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T*		pNumParticipants	Pointer to number of participants

   Outputs:    Updates data pointed to by pNumParticipants

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_get_race_participant_count(UINT16_T unit_address, 
													 UINT8_T * pNumParticipants);

/****************************************************************************
   tkcmdsetCSAFE_set_race_data_sample_ticks

   About:      Sets the race data sampling interval

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT32_T		sample_ticks	Sampling interval

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_data_sample_ticks(UINT16_T unit_address, 
													UINT32_T sample_ticks);

/****************************************************************************
   tkcmdsetCSAFE_set_race_status_display_ticks

   About:      Sets the race data display update interval.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT32_T		display_ticks	display update interval

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_status_display_ticks(UINT16_T unit_address, 
													UINT32_T display_ticks);

/****************************************************************************
   tkcmdsetCSAFE_set_workout_type

   About:      Set the workout type on the PM3.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		type				workout type

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_workout_type(UINT16_T unit_address, 
																	 UINT8_T type);

/****************************************************************************
   tkcmdsetCSAFE_set_screen_state

   About:      Set the screen state on a PM3

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		screen_type		Screen type
					UINT8_T		screen_value	Screen value

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_screen_state(UINT16_T unit_address, 
													UINT8_T screen_type, UINT8_T screen_value);

/****************************************************************************
   tkcmdsetCSAFE_set_datetime

   About:      Set time and date on the erg.

   Inputs:     UINT16_T    unit_address
               UINT8_T     Time in hours (1-12)
					UINT8_T     Time in minutes (0-59)
					UINT8_T		Time Meridiem (0=AM, 1=PM)
					UINT8_T		Date Month (1-12)
					UINT8_T     Date Day (1-31)
					UINT16_T    Date Year 

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_datetime(UINT16_T unit_address,
   UINT8_T hours, UINT8_T minutes, UINT8_T meridiem, UINT8_T month, 
   UINT8_T day, UINT16_T year);

/****************************************************************************
   tkcmdsetCSAFE_reset_erg_num

   About:      Reset the Erg number on a PM3. 
					PM3 resets to CSAFE_DESTINATION_ADDR_ERG_DEFAULT

   Inputs:     UINT16_T    unit_address	Address of PM device

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_reset_erg_num(UINT16_T unit_address);

/****************************************************************************
   tkcmdsetCSAFE_set_workout_duration

   About:      Set the time/distane workout duration on a PM3

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		type				Time or Distance
					UINT32_T		duration			Duration

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_workout_duration(UINT16_T unit_address, 
													UINT8_T type, UINT32_T duration);

/****************************************************************************
   tkcmdsetCSAFE_set_split_duration

   About:      Set the time/distane split duration on a PM3

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		type				Time or Distance
					UINT32_T		duration			Duration

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_split_duration(UINT16_T unit_address, 
													UINT8_T type, UINT32_T duration);

/****************************************************************************
   tkcmdsetCSAFE_configure_workout

   About:      Enable/disable the programming mode for configuring workouts 
					on the PM3.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		mode				Programming mode (enabled or disabled)

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_configure_workout(UINT16_T unit_address, 
																	 UINT8_T mode);

/****************************************************************************
   tkcmdsetCSAFE_set_race_start_params

   About:      Set the race start parameters.  There are two start types,
					random or countdown.  If countdown, we utilize two parameters:
					a starting count and a countdown tick.  The countdown tick is
					actually the delta between ready-attention and attention-row.
					In random type, we utilize three parameters: ready tick, attention
					tick and row tick.
					In all cases the ticks are relative to when the start command
					is received.

   Inputs:     UINT16_T    unit_address	Address of PM device
					UINT8_T		start_type		Start type - random or countdown
					UINT8_T		count_start		For countdown type, number to countdown from
					UINT32_T		ready_or_countdown_tick		For countdown type, countdown
																			ticks/number
																		For random type, ready tick
					UINT32_T		attention_tick	Attention tick count
					UINT32_T		row_tick			Row tick count

   Outputs:    Nothing

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_race_start_params(UINT16_T unit_address, 
		UINT8_T start_type, UINT8_T count_start, UINT32_T ready_or_countdown_tick,
		UINT32_T attention_tick, UINT32_T row_tick);

/****************************************************************************
   tkcmdsetCSAFE_update_race_data

   About:      Update race data between a PM and the PC.  
					This creates a CSAFE frame with two commands:
					SET_RACESTATUS to update the PM with the current overall race data,
					and GET_RACEDATA to request the PM to send it's current race
					information.

   Inputs:     TKCMDSET_PM3_RACE_DATA *pm_race_data	Ptr to PM race data structure
					TKCMDSET_OVERALL_RACE_DATA *overall_race_data	Ptr to overall race data

   Outputs:    Updates pm_race_data with current race data information

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_update_race_data(UINT16_T unit_address, 
		TKCMDSET_PM3_RACE_DATA *pm_race_data, 
		TKCMDSET_OVERALL_RACE_DATA *overall_race_data);

/****************************************************************************
   tkcmdsetCSAFE_set_display_string

   About:      Display a string of characters on a PM3.

   Inputs:     UINT16_T    unit_address	Address of PM device
					INT8_T*		display_string String to display

   Outputs:    None

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_set_display_string(UINT16_T unit_address, 
													INT8_T* display_string);

/****************************************************************************
   tkcmdsetCSAFE_read_logblk8

   About:      Reads an 8-bit block of data from a logcard memory location.

   Inputs:     UINT16_T    unit_address   Address of PM device
               UINT32_T    logcard_address	Address of logcard memory to read
               UINT32_T    byte_len			Number of bytes to be read
														(0 returns all)
               

   Outputs:    UINT8_T *  val_ptr8			Location to store block of data read
					UINT32_T * num_read			Actual number of bytes read

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_read_logblk8(UINT16_T unit_address, UINT32_T logcard_address, 
																	  UINT32_T byte_len, UINT8_T *val_ptr8, 
																	  UINT32_T *num_read);

/****************************************************************************
   tkcmdsetCSAFE_read_memblk8

   About:      Reads an 8-bit block of data from a memory location.

   Inputs:     UINT16_T    unit_address   Address of PM device
					UINT16_T		memory_type		Memory type to read
               UINT32_T    read_address	Address of logcard memory to read
               UINT32_T    byte_len			Number of bytes to be read
														(0 returns all)
               

   Outputs:    UINT8_T *  val_ptr8			Location to store block of data read
					UINT32_T * num_read			Actual number of bytes read

   Returns:    ERRCODE_T   ecode				Zero if successful
														Error code otherwise

****************************************************************************/
PM3CSAFE_API ERRCODE_T tkcmdsetCSAFE_read_memblk8(UINT16_T unit_address, UINT16_T memory_type,
																  UINT32_T read_address, UINT32_T byte_len, 
																  UINT8_T *val_ptr8, UINT32_T *num_read);

#endif /* _PM3COMMANDS_H */

