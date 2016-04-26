#ifndef ACCELOMETER_H
#include "stm32f4_discovery_lis302dl.h"

uint8_t acc_x = 0;
uint8_t acc_y = 0;
uint8_t acc_z = 0;

char data[6]={'_','_','_','_','_','X'};

#define AccReadX(void)	LIS302DL_Read(&acc_x, LIS302DL_OUT_X_ADDR, 1)
#define AccReadY(void)	LIS302DL_Read(&acc_y, LIS302DL_OUT_Y_ADDR, 1)
#define AccReadZ(void)	LIS302DL_Read(&acc_z, LIS302DL_OUT_Z_ADDR, 1)

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
	AccReadX();
	AccReadY();
	AccReadZ();
}

#endif
