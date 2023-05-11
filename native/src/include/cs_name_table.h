#pragma once
#include <restbl/restbl.h>

#include "native.h"

using namespace oepd::restbl;

//
// General
EXP size_t NameTableCount(Table<std::string>* table);
EXP u32 NameTableGet(Table<std::string>* table, char* name);
EXP void NameTableSet(Table<std::string>* table, char* name, u32 size);
EXP bool NameTableContains(Table<std::string>* table, char* name);
EXP void NameTableRemove(Table<std::string>* table, char* name);
EXP void NameTableClear(Table<std::string>* table);

//
// Iterator
EXP bool NameTableAdvance(Table<std::string>* table, Table<std::string>::iterator* iterator, Table<std::string>::iterator** next);
EXP void NameTableCurrent(Table<std::string>::iterator* it, const char** name, u32* size);