#include "cs_name_table.h"

//
// General
size_t NameTableCount(Table<std::string>* table) {
  return table->size();
}

u32 NameTableGet(Table<std::string>* table, char* name) {
  return table->at(name);
}

void NameTableSet(Table<std::string>* table, char* name, u32 size) {
  table->insert_or_assign(name, size);
}

bool NameTableContains(Table<std::string>* table, char* name) {
  return table->contains(name);
}

void NameTableRemove(Table<std::string>* table, char* name) {
  table->erase(name);
}

void NameTableClear(Table<std::string>* table) {
  table->clear();
}

//
// Iterator
void NameTableCurrent(Table<std::string>::iterator* iterator, const char** name, u32* size) {
  auto it = *iterator;
  *name = it->first.c_str();
  *size = it->second;
}

bool NameTableAdvance(Table<std::string>* table, Table<std::string>::iterator* iterator, Table<std::string>::iterator** next) {
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