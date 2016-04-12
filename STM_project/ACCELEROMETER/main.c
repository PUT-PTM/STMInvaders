#include "tm_stm32f4_delay.h"
#include "tm_stm32f4_disco.h"
#include "accelerometer.h"
#include "defines.h"

int main(void)
{
	/* Initialize system */
	SystemInit();

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
