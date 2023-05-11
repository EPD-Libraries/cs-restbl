#pragma once
#include <cstdint>
#include <string>
#include <vector>

#if _WIN32
#define CEAD __declspec(dllexport)
#else
#define CEAD
#endif