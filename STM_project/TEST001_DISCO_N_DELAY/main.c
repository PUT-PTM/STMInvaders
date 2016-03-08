#include "tm_stm32f4_disco.h"
#include "tm_stm32f4_delay.h"

/* DUMB-NOTE: Delay works in microseconds... */

int main(void)
{
	/* Initialize system */
	SystemInit();

	/* Initialize button and LEDs */
	TM_DISCO_ButtonInit();
	TM_DISCO_LedInit();

	/* Initialize delay */
	TM_DELAY_Init();

	unsigned int i;
	while(1){
		TM_DISCO_LedOn(LED_ALL);
		Delay(1000000);
		TM_DISCO_LedOff(LED_ALL);
		Delay(1000000);
	}
}
