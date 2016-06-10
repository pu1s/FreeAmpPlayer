// ֳכאגםûי DLL-פאיכ.

#include "stdafx.h"

#include "core.audio.h"
#include <mmdeviceapi.h>

__declspec(dllexport) HRESULT WINAPI GetAudioDevices();
int main()
{
	
}

HRESULT WINAPI GetAudioDevices()
{
	HRESULT res = S_OK;
	IMMDevice *prop;
	return res;
}
