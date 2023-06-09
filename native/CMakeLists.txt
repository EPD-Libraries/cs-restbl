cmake_minimum_required(VERSION 3.10)
project(cs_restbl VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)
set(CMAKE_SHARED_LIBRARY_PREFIX "")

if(WIN32)
    set(CMAKE_CXX_STANDARD_LIBRARIES "-static -static-libgcc -static-libstdc++")
endif()

include(CTest)
enable_testing()

add_library(cs_restbl SHARED
    ./src/include/native.h

    ./src/include/cs_restbl.h
    ./src/cs_restbl.cpp

    ./src/include/cs_name_table.h
    ./src/cs_name_table.cpp

    ./src/include/cs_crc_table.h
    ./src/cs_crc_table.cpp
)

add_subdirectory(${PROJECT_SOURCE_DIR}/lib/restbl)
target_link_libraries(cs_restbl PRIVATE restbl)
target_include_directories(cs_restbl PRIVATE ./src/include)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
