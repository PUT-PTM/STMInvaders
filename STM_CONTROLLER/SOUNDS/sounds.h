#ifndef SOUNDS_H
#define SOUNDS_H
#include "main.h"

void InitSounds(){
	// For codec
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
	
	// Initialize codecs
	codec_init();
	codec_ctrl_init();
	I2S_Cmd(CODEC_I2S, ENABLE);


	strSound0.cnt = 0;
	strSound0.max = sound0_max;
	strSound0.tab = &sound0;

	strSound1.cnt = 0;
	strSound1.max = sound1_max;
	strSound1.tab =  &sound1;

	strSound2.cnt = 0;
	strSound2.max = sound2_max;
	strSound2.tab =	&sound2;

	actualSound = &strSound2;
}
void CheckSounds(){
	if(actualSound != NULL){
		if (SPI_I2S_GetFlagStatus(CODEC_I2S, SPI_I2S_FLAG_TXE)){
			//przypisanie próbce aktualnej wartoci z tablicy
			sample = (actualSound->tab[actualSound->cnt] * VOLUME);
			//rzucenie tego do SPI
			SPI_I2S_SendData(CODEC_I2S, sample);
			//zmiana licznika
			actualSound->cnt++;
		}
		if(actualSound->cnt == actualSound->max){
			actualSound->cnt=0;
			actualSound = NULL;
		}
	}
}
void ChangeSound(char x){
	//if(actualSound != NULL) actualSound->cnt = 0;	//wersja: pojawi sie nowy dŸwiêk to zeruj poprzedni
	if(actualSound != NULL) return;					//wersja: odpalaj dŸwiêk póki siê nei skoñczy
	switch(x){
		case '0': actualSound = &strSound0; break;	//shooting
		case '1': actualSound = &strSound1; break;	//explosion
		case '2': actualSound = &strSound2; break;	//dead
		case '_': break;				//no new sound from game
	}
}
/*
void InitGPIO_DC(){
	// For LEDs
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

	GPIO_InitTypeDef  GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = LED_ALL;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &GPIO_InitStructure);
}*/
#endif SOUNDS_H
