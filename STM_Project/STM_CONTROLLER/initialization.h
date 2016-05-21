#include "main.h"

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
Global_Init(void)
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
