#include "stm32f4xx_rcc.h"
#include "stm32f4xx_gpio.h"

int main(){
	SystemInit();

	/*Enable GPIO*/
	RCC_AHB1PeriphClockCmd(
			RCC_AHB1Periph_GPIOA | RCC_AHB1Periph_GPIOB |
			RCC_AHB1Periph_GPIOC | RCC_AHB1Periph_GPIOD, ENABLE);
	/*Enable I2C & SPI*/
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_I2C1 | RCC_APB1Periph_SPI3, ENABLE);
	RCC_PLLI2SCmd(ENABLE);


	GPIO_InitTypeDef PinInitStruct;
	PinInitStruct.GPIO_Pin = GPIO_Pin_4;
	PinInitStruct.GPIO_Mode = GPIO_Mode_OUT;
	PinInitStruct.GPIO_OType = GPIO_OType_PP;
	PinInitStruct.GPIO_PuPd = GPIO_PuPd_DOWN;
	PinInitStruct.GPIO_Speed = GPIO_Speed_50MHz;

	GPIO_Init(GPIOD, &PinInitStruct);
}
