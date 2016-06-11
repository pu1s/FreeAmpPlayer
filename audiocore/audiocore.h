// audiocore.h

#pragma once

const int majorversion = 0;
const int minorversion = 1;
#include <stdio.h>
#include <Windows.h>
#include "winaudiodevices.h"


using namespace System;
#pragma managed
namespace audiocore {

    public ref class systemaudiodevices
	{

	public: static System::String^ GetCurrentVersion()
		{
			return System::String::Format("audiocore: {0}:{1}", majorversion, minorversion);
		}
	};
}
namespace audiocore2 {
	public ref class ObjectClass
	{
	public:
		ObjectClass();
		~ObjectClass();

	private:


	};

	ObjectClass::ObjectClass()
	{
	}

	ObjectClass::~ObjectClass()
	{
	}
}
