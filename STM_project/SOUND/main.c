#include "main.h"
#include "sound0.h"	//shooting
#include "sound1.h"	//explosion
#include "sound2.h"	//dead

//NOTKA DLA POTOMNYCH I
//LINIA 68 W CODEC.H: CZÊSTOTLIWOSC TAM EDYTOWA£EM

//NOTKA DLA POTOMNYCH II
//f0 = 0.5 * NOTEFREQUENCY * 48000 (=sample rate)

int main(void) {
	SystemInit();

	/* Initialize LEDs */
	InitGPIO_DC();

	/* Initialize codecs */
	codec_init();
	codec_ctrl_init();
	I2S_Cmd(CODEC_I2S, ENABLE);

	int cnt = 0;

    while(1){
    	if (SPI_I2S_GetFlagStatus(CODEC_I2S, SPI_I2S_FLAG_TXE))
    	{
    		sample = (int16_t)(sound2[cnt]*500);
    		SPI_I2S_SendData(CODEC_I2S, sample);

    		cnt++;
    	}
    	if(cnt == sound2_max){
    		cnt=0;
    	}
    	if(sampleCounter == 22000){
    		GPIO_ToggleBits(GPIOD, LED_ALL);
    	}
    	else if(sampleCounter == 44000){
    		GPIO_ToggleBits(GPIOD, LED_ALL);
    		sampleCounter = 0;
    	}
    }
}

void InitGPIO_DC(){
	// For LEDs
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);
	// For codec
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);

	GPIO_InitTypeDef  GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = LED_ALL;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &GPIO_InitStructure);
}
