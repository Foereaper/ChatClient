
#ifndef _Sniffer_h__
#define _Sniffer_h__

#include <atomic>
#include <thread>
#include <wtypes.h>
#include <psapi.h>

#include "Util.h"
#include "LockedQueue.h"

#define MAX_COMMAND_ARGS 255

#define PKT_VERSION 0x0301
#define SNIFFER_ID  15

typedef struct {
    void* vTable;
    BYTE* buffer;
    DWORD base;
    DWORD alloc;
    DWORD size;
    DWORD read;
} CDataStore;

struct CliCommandHolder
{
    typedef void Print(void*, const char*);
    typedef void CommandFinished();

    void* m_callbackArg;
    std::string m_command;
    char* m_args[MAX_COMMAND_ARGS];
    Print* m_print;

    CommandFinished* m_commandFinished;

    CliCommandHolder(void* callbackArg, char* command[], Print* zprint, CommandFinished* commandFinished)
        : m_callbackArg(callbackArg), m_print(zprint), m_commandFinished(commandFinished)
    {
        m_command = command[0];

        for (unsigned char i = 1; i < MAX_COMMAND_ARGS; ++i)
            m_args[i - 1] = command[i];
    }

    ~CliCommandHolder() { }

private:
    CliCommandHolder(CliCommandHolder const& right) = delete;
    CliCommandHolder& operator=(CliCommandHolder const& right) = delete;
};

struct PacketInfo
{
    PacketInfo() : packetType(0), connectionId(0), opcodeSize(0), dataStore(nullptr) { }
    PacketInfo(DWORD64 PacketType, DWORD ConnectionId, WORD OpcodeSize, CDataStore* DataStore) :
        packetType(PacketType), connectionId(ConnectionId), opcodeSize(OpcodeSize),
        dataStore(DataStore)
    {
    }

    DWORD64 packetType;
    DWORD connectionId;
    WORD opcodeSize;
    CDataStore* dataStore;
};

class Sniffer
{
    public:
        static Sniffer* instance()
        {
            static Sniffer instance;
            return &instance;
        }

        static void Stop() { m_stopEvent = true; }
        static bool IsStopped() { return m_stopEvent; }

        void SetCliThread(std::thread* cliThread) { m_cliThread = cliThread; }
        void ProcessCliCommands();
        void QueueCliCommand(CliCommandHolder* commandHolder) { cliCmdQueue.add(commandHolder); }
        void ShutdownCLIThread();

        void SetSnifferInfo(std::string& DllPath, std::string& Locale, WORD BuildNumber)
        {
            dllPath = DllPath;
            locale = Locale;
            buildNumber = BuildNumber;
            fileDump = 0;
        }
        void DumpPacket(PacketInfo const& info);
        void CloseFileDump()
        {
            if (fileDump)
                fclose(fileDump);
        }

        FILE* GetFileDump() const { return fileDump; }
        const std::string& GetDLLPath() { return dllPath; }

    private:
        Sniffer() { }
        ~Sniffer() { }

        std::string dllPath;
        std::string locale;
        WORD buildNumber;

        unsigned int GetOpcodeFromParam(char* param);

        LockedQueue<CliCommandHolder*> cliCmdQueue;

        static std::atomic<bool> m_stopEvent;
        std::thread* m_cliThread;

        std::mutex dumpMutex;
        FILE* fileDump;
};

#define sSniffer Sniffer::instance()

#endif