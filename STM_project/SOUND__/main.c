#include "main.h"
#include "sound0.h"

//NOTKA DLA POTOMNYCH I
//LINIA 68 W CODEC.H: CZÊSTOTLIWOSC TAM EDYTOWA£EM

//NOTKA DLA POTOMNYCH II
//f0 = 0.5 * NOTEFREQUENCY * 48000 (=sample rate)

unsigned int i;

volatile uint32_t sampleCounter = 0;
volatile int16_t sample = 0;

float sound = 0.0;
int up = 0;

/*int main(void) {
	SystemInit();

	// Initialize LED's
	InitGPIO_DC();

	// Initialize codecs
	codec_init();
	codec_ctrl_init();
	I2S_Cmd(CODEC_I2S, ENABLE);

	int cnt = 0;
	const int max = 10538;

    while(1){
    	if (SPI_I2S_GetFlagStatus(CODEC_I2S, SPI_I2S_FLAG_TXE))
    	{
    		sample = (int16_t)(samples[cnt]*500);
    		SPI_I2S_SendData(CODEC_I2S, sample);

    		cnt++;
    	}
    	if(cnt == max){
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
}*/
int main(void) {
    //Fatfs object
    FATFS FatFs;
    //File object
    FIL fil;
    //Free and total space
    uint32_t total, free;

    //Initialize system
    SystemInit();
    //Initialize delays
    TM_DELAY_Init();
	//Initialize LED's
	InitGPIO_DC();

    //Mount drive
    if (f_mount(&FatFs, "", 1) == FR_OK) {
        //Mounted OK, turn on RED LED
        GPIO_SetBits(GPIOD,LED_RED);

        //Try to open file
        if (f_open(&fil, "1stfile.txt", FA_OPEN_ALWAYS | FA_READ | FA_WRITE) == FR_OK) {
            //File opened, turn off RED and turn on GREEN led
        	GPIO_SetBits(GPIOD,LED_GREEN);
        	GPIO_ResetBits(GPIOD,LED_RED);

            //If we put more than 0 characters (everything OK)
            if (f_puts("First string in my file\n", &fil) > 0) {
                if (TM_FATFS_DriveSize(&total, &free) == FR_OK) {
                    //Data for drive size are valid
                }

                //Turn on both leds
                GPIO_SetBits(GPIOD,LED_GREEN | LED_RED);
            }

            //Close file, don't forget this!
            f_close(&fil);
        }

        //Unmount drive, don't forget this!
        f_mount(0, "", 1);
    }
    while (1) {

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
