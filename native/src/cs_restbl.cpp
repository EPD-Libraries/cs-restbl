#include "include/cs_restbl.h"

restbl::RESTBL* FromBinary(u8* src, size_t src_len) {
  return new auto{restbl::RESTBL{{src, src_len}}};
}

std::vector<u8>* ToBinary(restbl::RESTBL* handle) {
  return new auto{handle->ToBinary()};
}

restbl::Table<u32>* GetCrcTable(restbl::RESTBL* handle) {
  return &handle->m_crc_table;
}

void SetCrcTable(restbl::RESTBL* handle, restbl::Table<u32>* table) {
  handle->m_crc_table = *table;
}

restbl::Table<std::string>* GetNameTable(restbl::RESTBL* handle) {
  return &handle->m_name_table;
}

void SetNameTable(restbl::RESTBL* handle, restbl::Table<std::string>* table) {
  handle->m_name_table = *table;
}

bool Free(restbl::RESTBL* handle) {
  delete handle;
  return true;
}