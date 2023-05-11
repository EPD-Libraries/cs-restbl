#include "include/cs_restbl.h"

restbl::RESTBL* FromBinary(u8* src, int src_len) {
  return new auto{restbl::RESTBL{{src, src_len}}};
}

std::vector<u8>* ToBinary(restbl::RESTBL* handle) {
  return new auto{handle->ToBinary()};
}

bool Free(restbl::RESTBL* handle) {
  delete handle;
  return true;
}