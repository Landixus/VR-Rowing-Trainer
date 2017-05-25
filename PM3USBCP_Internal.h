#ifndef _PM3USBCP_INTERNAL_
#define _PM3USBCP_INTERNAL_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "compiler.h"
#include "pc_error.h"

#ifndef NULL
	#define NULL 0
#endif

#ifndef MAX_UINT16_T
	#define MAX_UINT16_T 0xFFFF
#endif

#define TKCMDSET_PM3_CONNECT		1
#define TKCMDSET_PM3_DISCONNECT		2
#define TKCMDSET_VOLUME_CONNECT		3
#define TKCMDSET_VOLUME_DISCONNECT	4


//  PM3 Event callback definition
#ifndef PM3PROC_TYPES
#define PM3PROC_TYPES
   #ifdef WIN32  
      // Use StdCall on Windows for VB6/.NET support 
      #define PM3CALLBACKAPI __stdcall 
   #else  
      // No StdCall on Mac/Linux  
      #define PM3CALLBACKAPI 
   #endif  

   typedef void (PM3CALLBACKAPI *PM3EVENT_PROC)(unsigned short, unsigned char);
   typedef void (PM3CALLBACKAPI *MPDEVENT_PROC)(unsigned short, unsigned char, char *, char *);
   typedef void (PM3CALLBACKAPI *PM3ASYNC_PROC)(unsigned short, unsigned char *, unsigned short, short, short);
	typedef void (PM3CALLBACKAPI *PM3ASYNC_CSAFE_PROC)(unsigned short, unsigned long *, unsigned short, short, short);
#endif


#endif // _PM3USBCP_INTERNAL_