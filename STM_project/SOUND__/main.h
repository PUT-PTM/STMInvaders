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
// others
#include "tm_stm32f4_fatfs.h"
#include <stdio.h>
#include <string.h>


//#define HSE_VALUE	((uint32_t)8000000)	//redefine clock
#define LED_GREEN	GPIO_Pin_12
#define LED_ORANGE	GPIO_Pin_13
#define LED_RED		GPIO_Pin_14
#define LED_BLUE	GPIO_Pin_15
#define LED_ALL		LED_BLUE|LED_GREEN|LED_ORANGE|LED_RED
#define true	1
#define false	0


//SD LIB CONFIG
/* Use SPI communication with SDCard */
#define	FATFS_USE_SDIO				0
/* Select your SPI settings */
#define FATFS_SPI				SPI2
#define FATFS_SPI_PINSPACK		TM_SPI_PinsPack_2



//unsigned int i;

void InitGPIO_DC();

/*volatile uint32_t sampleCounter = 0;
volatile int16_t sample = 0;

float sound = 0.0;
int up = 0;*/

#endif
