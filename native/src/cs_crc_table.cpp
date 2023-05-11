#include "cs_crc_table.h"

//
// General
size_t CrcTableCount(Table<u32>* table) {
  return table->size();
}

u32 CrcTableGet(Table<u32>* table, u32 hash) {
  return table->at(hash);
}

void CrcTableSet(Table<u32>* table, u32 hash, u32 size) {
  table->insert_or_assign(hash, size);
}

bool CrcTableContains(Table<u32>* table, u32 hash) {
  return table->contains(hash);
}

void CrcTableRemove(Table<u32>* table, u32 hash) {
  table->erase(hash);
}

void CrcTableClear(Table<u32>* table) {
  table->clear();
}

//
// Iterator
bool CrcTableAdvance(Table<u32>* table, Table<u32>::iterator* iterator, Table<u32>::iterator** next) {
  if (iterator == NULL) {
    *next = new auto{table->begin()};
    return true;
  }

  if (++(*iterator) != table->end()) {
    *next = iterator;
    return true;
  }

  return false;
}

void CrcTableCurrent(Table<u32>::iterator* iterator, u32* hash, u32* size) {
  auto it = *iterator;
  *hash = it->first;
  *size = it->second;
}