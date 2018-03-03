#ifndef _Util_h__
#define _Util_h__

#include <wtypes.h>
#include <psapi.h>
#include <Shlwapi.h>
#include <io.h>
#include <cctype>

#include "uft8.h"

#define WOW_MOP_16135 16135

// hook entry structure
// stores the offsets which are will be hooked
// every different client version should has different offsets
typedef struct {
    // offset of NetClient::Send2 to sniff client packets
    DWORD send_2;
    // offset of NetClient::ProcessMessage to sniff server packets
    DWORD receive;
    // offset of client locale "xxXX"
    DWORD locale;
} HookEntry;


bool Utf8toWStr(char const* utf8str, size_t csize, wchar_t* wstr, size_t& wsize);
inline bool Utf8toWStr(const std::string& utf8str, wchar_t* wstr, size_t& wsize)
{
    return Utf8toWStr(utf8str.c_str(), utf8str.size(), wstr, wsize);
}

inline bool char_isspace(char c)
{
    return std::isspace(static_cast<unsigned char>(c)) != 0;
}

inline void ctolower(char* s)
{
    int len = strlen(s);
    for (int i = 0; i < len; ++i)
        s[i] = tolower(s[i]);
}

bool WStrToUtf8(std::wstring const& wstr, std::string& utf8str);
bool consoleToUtf8(const std::string& conStr, std::string& utf8str);

WORD GetBuildNumberFromProcess(HANDLE hProcess = NULL);
bool GetOffsets(const HINSTANCE moduleHandle, const WORD build, HookEntry* entry);
bool IsHookEntryExists(const HINSTANCE moduleHandle, WORD buildNumber);

#endif


