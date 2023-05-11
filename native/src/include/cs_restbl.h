#pragma once
#include <restbl/restbl.h>

#include "native.h"

using namespace oepd;

EXP restbl::RESTBL* FromBinary(u8* src, size_t src_len);
EXP std::vector<u8>* ToBinary(restbl::RESTBL* handle);

EXP restbl::Table<u32>* GetCrcTable(restbl::RESTBL* handle);
EXP restbl::Table<std::string>* GetNameTable(restbl::RESTBL* handle);

EXP bool Free(restbl::RESTBL* handle);