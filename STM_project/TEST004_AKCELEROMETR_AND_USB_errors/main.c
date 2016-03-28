#define HSE_VALUE ((uint32_t)8000000)

#include "tm_stm32f4_delay.h"
#include "tm_stm32f4_disco.h"
#include "accelerometer.h"
#include "defines.h"
//------
#include "stm32f4xx_conf.h"
#include "stm32f4xx.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_exti.h"
#include "usbd_cdc_core.h"
#include "usbd_usr.h"
#include "usbd_desc.h"
#include "usbd_cdc_vcp.h"
#include "usb_dcd_int.h"

volatile uint32_t ticker, downTicker;

	/*
	 * The USB data must be 4 byte aligned if DMA is enabled. This macro handles
	 * the alignment, if necessary (it's actually magic, but don't tell anyone).
	 */
	__ALIGN_BEGIN USB_OTG_CORE_HANDLE  USB_OTG_dev __ALIGN_END;

	void init();
	void ColorfulRingOfDeath(void);

	/*
	 * Define prototypes for interrupt handlers here. The conditional "extern"
	 * ensures the weak declarations from startup_stm32f4xx.c are overridden.
	 */
	#ifdef __cplusplus
	 extern "C" {
	#endif

	void SysTick_Handler(void);
	void NMI_Handler(void);
	void HardFault_Handler(void);
	void MemManage_Handler(void);
	void BusFault_Handler(void);
	void UsageFault_Handler(void);
	void SVC_Handler(void);
	void DebugMon_Handler(void);
	void PendSV_Handler(void);
	void OTG_FS_IRQHandler(void);
	void OTG_FS_WKUP_IRQHandler(void);

	#ifdef __cplusplus
	}
	#endif
	 //----------------------------

int main(void)
{


	/* Initialize system */
	SystemInit();
	/* Initialize USB, IO, SysTick, and all those other things you do in the morning */
	init();

	/* Initialize button and LEDs */
	TM_DISCO_ButtonInit();
	TM_DISCO_LedInit();

	/* Initialize delay */
	TM_DELAY_Init();



	// Initialize accelerometer
	// 50ms delay for accelerometer calibration
	AccInit();
	Delay(50000);

	unsigned int i;

	while(1){
		/* Updating accelerometer */
		UpdateAccGlobals();

		/* Checking X axis */
		if(!(acc_x < 5 || acc_x > 250)){
			if(acc_x >= 200)		TM_DISCO_LedOn(LED_GREEN);
			else if(acc_x <= 50)	TM_DISCO_LedOn(LED_RED);
		}
		/* Checking Y axis */
		if(!(acc_y < 5 || acc_y > 250)){
			if(acc_y >= 200)		TM_DISCO_LedOn(LED_BLUE);
			else if(acc_y <= 50)	TM_DISCO_LedOn(LED_ORANGE);
		}

		if(TM_DISCO_ButtonPressed()){
			for(i = 0; i < 4; i++){
				TM_DISCO_LedOn(LED_ALL);
				Delay(25000);
				TM_DISCO_LedOff(LED_ALL);
				Delay(25000);
			}
		}
		else Delay(100000);

		//TODO Z value

		TM_DISCO_LedOff(LED_ALL);
	}
}
