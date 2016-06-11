#pragma once

#include <mmdeviceapi.h>

//using namespace System;
// Определение девайса
#pragma unmanaged
public struct win_a_dev
{
	LPWSTR win_a_dev_name;
	LPCWSTR win_a_dev_id;
	LPWSTR win_a_dev_prop;
	LPWSTR win_a_dev_GUID;
} typedef WIN_A_DEV;

#pragma unmanaged
class winaudiodevices
{
public:
	winaudiodevices();
	
//__declspec(dllexport) 
int GetCustomDev();
private:
	WIN_A_DEV device;
};

