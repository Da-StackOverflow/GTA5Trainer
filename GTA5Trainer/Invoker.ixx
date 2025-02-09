export module Invoker;

import "Base.h";
import "Import.h";

export Temp1(T)
inline void NativePush(T val)
{
	ulong val64 = 0;
	*reinterpret_cast<T*>(&val64) = val;
	nativePush64(val64);
}

export inline void PushArgs()
{
}

export Temp1(T)
inline void PushArgs(T arg)
{
	NativePush(arg);
}

export Temp2(T, ... Ts)
inline void PushArgs(T arg, Ts... args)
{
	NativePush(arg);
	PushArgs(args...);
}

export Temp2(R, ... Ts)
inline R Invoke(ulong hash, Ts... args)
{
	nativeInit(hash);
	PushArgs(args...);
	return *reinterpret_cast<R*>(nativeCall());
}

export Temp1(... Ts)
inline void InvokeV(ulong hash, Ts... args)
{
	nativeInit(hash);
	PushArgs(args...);
	nativeCall();
}