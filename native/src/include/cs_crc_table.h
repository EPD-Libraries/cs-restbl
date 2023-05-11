#pragma once
#include <restbl/restbl.h>

#include "native.h"

using namespace oepd::restbl;

//
// General
EXP size_t CrcTableCount(Table<u32>* table);
EXP u32 CrcTableGet(Table<u32>* table, u32 hash);
EXP void CrcTableSet(Table<u32>* table, u32 hash, u32 size);
EXP bool CrcTableContains(Table<u32>* table, u32 hash);
EXP void CrcTableRemove(Table<u32>* table, u32 hash);
EXP void CrcTableClear(Table<u32>* table);

//
// Iterator
EXP void CrcTableCurrent(Table<u32>::iterator* it, const u32* hash, u32* size);
EXP bool CrcTableAdvance(Table<u32>* table, Table<u32>::iterator* iterator, Table<u32>::iterator** next);