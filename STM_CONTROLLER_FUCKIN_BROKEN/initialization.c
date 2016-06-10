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
//#include "sounds.h"
#include "initialization.h"

uint8_t acc_x = 0;
uint8_t acc_y = 0;
uint8_t acc_z = 0;

void init_GPIO(void){
	GPIO_InitTypeDef GPIO_InitStruct;
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_15 | GPIO_Pin_14 | GPIO_Pin_13 | GPIO_Pin_12;
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStruct.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStruct.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &GPIO_InitStruct);

	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);

	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_0;
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_IN;
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStruct.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStruct.GPIO_PuPd = GPIO_PuPd_DOWN;
	GPIO_Init(GPIOA, &GPIO_InitStruct);
	GPIOD->BSRRL = 0xF000;
	GPIOD->BSRRH = 0xF000;

}
void AccInit(){
	LIS302DL_InitTypeDef AccInitStr;
	AccInitStr.Axes_Enable = LIS302DL_XYZ_ENABLE;
	AccInitStr.Full_Scale = LIS302DL_FULLSCALE_2_3;
	AccInitStr.Power_Mode = LIS302DL_LOWPOWERMODE_ACTIVE;
	AccInitStr.Output_DataRate = LIS302DL_DATARATE_100;
	AccInitStr.Self_Test = LIS302DL_SELFTEST_NORMAL;
	LIS302DL_Init(&AccInitStr);
}

void UpdateAccGlobals(){
	LIS302DL_Read(&acc_x, LIS302DL_OUT_X_ADDR, 1);
	LIS302DL_Read(&acc_y, LIS302DL_OUT_Y_ADDR, 1);
	LIS302DL_Read(&acc_z, LIS302DL_OUT_Z_ADDR, 1);
}

void Timer_Init()
{
	   RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2,ENABLE);
				TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
				TIM_TimeBaseStructure.TIM_Period = 40000;
				TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
				TIM_TimeBaseStructure.TIM_Prescaler = 50;
				TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
				TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
				TIM_TimeBaseInit(TIM2, &TIM_TimeBaseStructure);
				TIM_Cmd(TIM2, ENABLE);
}
void Global_Init(void)
{
	// Initialize system
		SystemInit();

		// Initialize button and LEDs
		init_GPIO();

		// Initialize delay
		TM_DELAY_Init();

		// Init sounds
		InitSounds();

		// Initialize accelerometer
		AccInit();

		// Initialize USB, IO, SysTick, and all those other things you do in the morning
		init();

		//timerINit
		Timer_Init();


}
