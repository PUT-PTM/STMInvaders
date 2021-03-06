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
#include "tm_stm32f4_delay.h"
#include "defines.h"
#include "stm32f4xx_tim.h"
#include "stm32f4_discovery_lis302dl.h"
#include "sounds.h"
#include "initialization.h"

volatile uint32_t ticker;//, downTicker;
uint8_t acc_x;
uint8_t acc_y;
uint8_t acc_z;
char data[6]={'_','_','_','_','_','X'};
char sound = '_';
char lastSound = '_';
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




int main(void)
{
	unsigned int i;
	char buffer[10];

	Global_Init();

	while (1){
		// Updating accelerometer
		UpdateAccGlobals();
		
		// Update sounds
		CheckSounds();

		// Checking X axis (vertical move)
		if(!(acc_x < 5 || acc_x > 245)){
			if(acc_x >= 190){
				data[0]='_';
				data[1]='S';
				GPIOD->BSRRL = 0x1000; //GREEN LED
			}
			else if(acc_x <= 60){
				data[0]='W';
				data[1]='_';
				GPIOD->BSRRL = 0x4000;// RED LED
			}
		}
		// Checking Y axis (horizontal move)
		if(!(acc_y < 5 || acc_y > 245)){
			if(acc_y >= 190){
				data[2]='_';
				data[3]='D';
				GPIOD->BSRRL = 0x8000; //BLUE LED
			}
			else if(acc_y <= 60){
				data[2]='A';
				data[3]='_';
				GPIOD->BSRRL = 0x2000; //ORANGE LED
			}
		}
		// Checking button
		if(GPIOA->IDR & 0x0001){
			data[4]='B';
			//ChangeSound('0');

		}
		if(TIM_GetFlagStatus(TIM2,TIM_FLAG_Update) == 1){
			//sending data
			VCP_send_buffer(&data,6);
			for(i=0; i<5; i++){
				data[i]='_';
			}
			//reading data
			VCP_get_char(buffer);
			sound = buffer[0];
			//if(sound != lastSound){
			//	lastSound = sound;
				ChangeSound(sound);
			//}
			GPIOD->BSRRH = 0x4000; //turn of of leds
			GPIOD->BSRRH = 0x1000;
			GPIOD->BSRRH = 0x2000;
			GPIOD->BSRRH = 0x8000;

			TIM_ClearFlag(TIM2,TIM_FLAG_Update);
		}
	}
	return 0;
}

void init(){
	// STM32F4 discovery LEDs
	GPIO_InitTypeDef LED_Config;

	// Always remember to turn on the peripheral clock...  If not, you may be up till 3am debugging...
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);
	LED_Config.GPIO_Pin = LED_ALL;
	LED_Config.GPIO_Mode = GPIO_Mode_OUT;
	LED_Config.GPIO_OType = GPIO_OType_PP;
	LED_Config.GPIO_Speed = GPIO_Speed_25MHz;
	LED_Config.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &LED_Config);

	// Setup SysTick or CROD!
	if (SysTick_Config(SystemCoreClock / 1000)){
		ColorfulRingOfDeath();
	}

	// Setup USB
	USBD_Init(
		&USB_OTG_dev,
		USB_OTG_FS_CORE_ID,
		&USR_desc,
		&USBD_CDC_cb,
		&USR_cb
	);
}

void ColorfulRingOfDeath(void)
{
	uint16_t ring = 1;
	while (1)
	{
		uint32_t count = 0;
		while (count++ < 500000);

		GPIOD->BSRRH = (ring << 12);
		ring = ring << 1;
		if (ring >= 1<<4)
		{
			ring = 1;
		}
		GPIOD->BSRRL = (ring << 12);
	}
}


void NMI_Handler(void)       {}
void HardFault_Handler(void) { ColorfulRingOfDeath(); }
void MemManage_Handler(void) { ColorfulRingOfDeath(); }
void BusFault_Handler(void)  { ColorfulRingOfDeath(); }
void UsageFault_Handler(void){ ColorfulRingOfDeath(); }
void SVC_Handler(void)       {}
void DebugMon_Handler(void)  {}
void PendSV_Handler(void)    {}

void OTG_FS_IRQHandler(void)
{
  USBD_OTG_ISR_Handler (&USB_OTG_dev);
}

void OTG_FS_WKUP_IRQHandler(void)
{
  if(USB_OTG_dev.cfg.low_power)
  {
    *(uint32_t *)(0xE000ED10) &= 0xFFFFFFF9 ;
    SystemInit();
    USB_OTG_UngateClock(&USB_OTG_dev);
  }
  EXTI_ClearITPendingBit(EXTI_Line18);
}



