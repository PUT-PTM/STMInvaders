#include "main.h"

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

    while(1){
    	if (SPI_I2S_GetFlagStatus(CODEC_I2S, SPI_I2S_FLAG_TXE))
    	{
    		SPI_I2S_SendData(CODEC_I2S, sample);
    		//moja wersja
    		if(up == true){
    			sound+=NOTEFREQUENCY;
    			if(sound > 1.0) up = false;
    		} else{
    			sound-=NOTEFREQUENCY;
    			if(sound < -1.0) up = true;
    		}
    		sample = (int16_t)(NOTEAMPLITUDE*sound);
    		sampleCounter++;
    	}
    	if(sampleCounter == 22000){
    		GPIO_ToggleBits(GPIOD, LED_ALL);
    	}
    	else if(sampleCounter == 44000){
    		GPIO_ToggleBits(GPIOD, LED_ALL);
    		sampleCounter = 0;
    	}
    }
	/*while(1)
	{
		GPIO_ToggleBits(GPIOD, LED_ALL);
		for (i=0;i<1000000;i++);
	}*/
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
