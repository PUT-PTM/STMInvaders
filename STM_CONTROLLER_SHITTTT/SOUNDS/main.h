#ifndef MAIN_H
#define MAIN_H

#include "stm32f4xx_conf.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_spi.h"
#include "stm32f4xx_i2c.h"
#include "stm32f4xx_dac.h"
// library with SPI/I2C from MIND-DUMP.net
#include "codec.h"

#include "sound0.h"	//shooting
#include "sound1.h"	//explosion
#include "sound2.h"	//dead

#define LED_GREEN	GPIO_Pin_12
#define LED_ORANGE	GPIO_Pin_13
#define LED_RED		GPIO_Pin_14
#define LED_BLUE	GPIO_Pin_15
#define LED_ALL		LED_BLUE|LED_GREEN|LED_ORANGE|LED_RED
#define true	1
#define false	0


unsigned int i;
unsigned int sample;
unsigned int VOLUME = 500;	//500 default

//void InitGPIO_DC();

/*
	cnt - counter
	max - number of samples in sound
	tab - reference to sound table
*/
typedef struct{
	uint32_t cnt;
	uint16_t max;
	uint16_t* tab;
} Sound;
Sound strSound0;
Sound strSound1;
Sound strSound2;

Sound* actualSound;

#endif
