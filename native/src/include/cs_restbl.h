#pragma once
#include <restbl/restbl.h>

#if _WIN32
#define EXP extern "C" __declspec(dllexport)
#else
#define EXP extern "C"
#endif

using namespace oepd;

EXP restbl::RESTBL* FromBinary(u8* src, int src_len);
EXP std::vector<u8>* ToBinary(restbl::RESTBL* handle);

EXP bool Free(restbl::RESTBL* handle);