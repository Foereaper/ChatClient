#include "Sniffer.h"
#include "OpcodeMgr.h"
#include "CommandMgr.h"

std::atomic<bool> Sniffer::m_stopEvent(false);

void Sniffer::ProcessCliCommands()
{
    CliCommandHolder::Print* zprint = NULL;
    void* callbackArg = NULL;
    CliCommandHolder* command = NULL;
    while (cliCmdQueue.next(command))
    {
        zprint = command->m_print;
        callbackArg = command->m_callbackArg;

        if (!sCommandMgr->HandleCommand(command->m_command, command->m_args))
            printf("Invalid command. Type 'help' for a list of commands\n");

        if (command->m_commandFinished)
            command->m_commandFinished();
        delete command;
    }
}

void Sniffer::DumpPacket(PacketInfo const& info)
{
    DWORD packetOpcode = info.opcodeSize == 4
        ? *(DWORD*)info.dataStore->buffer
        : *(WORD*)info.dataStore->buffer;

    if (!sOpcodeMgr->ShouldShowOpcode(packetOpcode, info.packetType))
        return;

    dumpMutex.lock();
    // gets the time
    time_t rawTime;
    time(&rawTime);

    DWORD tickCount = GetTickCount();

    DWORD optionalHeaderLength = 0;

    if (!fileDump)
    {
        tm* date = localtime(&rawTime);
        // basic file name format:
        char fileName[MAX_PATH];
        // removes the DLL name from the path
        PathRemoveFileSpec(const_cast<char *>(dllPath.c_str()));
        // fills the basic file name format
        _snprintf(fileName, MAX_PATH,
            "wowsniff_%s_%u_%d-%02d-%02d_%02d-%02d-%02d.pkt",
            locale.c_str(), buildNumber,
            date->tm_year + 1900,
            date->tm_mon + 1,
            date->tm_mday,
            date->tm_hour,
            date->tm_min,
            date->tm_sec);

        // some info
        printf("Sniff dump: %s\n\n", fileName);

        char fullFileName[MAX_PATH];
        _snprintf(fullFileName, MAX_PATH, "%s\\%s", dllPath.c_str(), fileName);

        WORD pkt_version    = PKT_VERSION;
        BYTE sniffer_id     = SNIFFER_ID;
        BYTE sessionKey[40] = { 0 };

        fileDump = fopen(fullFileName, "wb");
        // PKT 3.1 header
        fwrite("PKT",                           3, 1, fileDump);  // magic
        fwrite((WORD*)&pkt_version,             2, 1, fileDump);  // major.minor version
        fwrite((BYTE*)&sniffer_id,              1, 1, fileDump);  // sniffer id
        fwrite((DWORD*)&buildNumber,            4, 1, fileDump);  // client build
        fwrite(locale.c_str(),                  4, 1, fileDump);  // client lang
        fwrite(sessionKey,                     40, 1, fileDump);  // session key
        fwrite((DWORD*)&rawTime,                4, 1, fileDump);  // started time
        fwrite((DWORD*)&tickCount,              4, 1, fileDump);  // started tick's
        fwrite((DWORD*)&optionalHeaderLength,   4, 1, fileDump);  // opional header length

        fflush(fileDump);
    }

    BYTE* packetData     = info.dataStore->buffer + info.opcodeSize;
    DWORD packetDataSize = info.dataStore->size   - info.opcodeSize;

    fwrite((DWORD*)&info.packetType,            4, 1, fileDump);  // direction of the packet
    fwrite((DWORD*)&info.connectionId,          4, 1, fileDump);  // connection id
    fwrite((DWORD*)&tickCount,                  4, 1, fileDump);  // timestamp of the packet
    fwrite((DWORD*)&optionalHeaderLength,       4, 1, fileDump);  // connection id
    fwrite((DWORD*)&info.dataStore->size,       4, 1, fileDump);  // size of the packet + opcode lenght
    fwrite((DWORD*)&packetOpcode,               4, 1, fileDump);  // opcode

    fwrite(packetData, packetDataSize,          1, fileDump);  // data

    printf("%s Size: %u\n", sOpcodeMgr->GetOpcodeNameForLogging(packetOpcode, info.packetType != CMSG).c_str(), packetDataSize);

    fflush(fileDump);

    dumpMutex.unlock();
}

void Sniffer::ShutdownCLIThread()
{
    if (m_cliThread != nullptr)
    {
        // First try to cancel any I/O in the CLI thread
        if (!CancelSynchronousIo(m_cliThread->native_handle()))
        {
            // if CancelSynchronousIo() fails, print the error and try with old way
            DWORD errorCode = GetLastError();
            LPSTR errorBuffer;

            DWORD formatReturnCode = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_IGNORE_INSERTS,
                                                   nullptr, errorCode, 0, (LPTSTR)&errorBuffer, 0, nullptr);
            if (!formatReturnCode)
                errorBuffer = "Unknown error";

            LocalFree(errorBuffer);

            // send keyboard input to safely unblock the CLI thread
            INPUT_RECORD b[4];
            HANDLE hStdIn = GetStdHandle(STD_INPUT_HANDLE);
            b[0].EventType = KEY_EVENT;
            b[0].Event.KeyEvent.bKeyDown = TRUE;
            b[0].Event.KeyEvent.uChar.AsciiChar = 'X';
            b[0].Event.KeyEvent.wVirtualKeyCode = 'X';
            b[0].Event.KeyEvent.wRepeatCount = 1;

            b[1].EventType = KEY_EVENT;
            b[1].Event.KeyEvent.bKeyDown = FALSE;
            b[1].Event.KeyEvent.uChar.AsciiChar = 'X';
            b[1].Event.KeyEvent.wVirtualKeyCode = 'X';
            b[1].Event.KeyEvent.wRepeatCount = 1;

            b[2].EventType = KEY_EVENT;
            b[2].Event.KeyEvent.bKeyDown = TRUE;
            b[2].Event.KeyEvent.dwControlKeyState = 0;
            b[2].Event.KeyEvent.uChar.AsciiChar = '\r';
            b[2].Event.KeyEvent.wVirtualKeyCode = VK_RETURN;
            b[2].Event.KeyEvent.wRepeatCount = 1;
            b[2].Event.KeyEvent.wVirtualScanCode = 0x1c;

            b[3].EventType = KEY_EVENT;
            b[3].Event.KeyEvent.bKeyDown = FALSE;
            b[3].Event.KeyEvent.dwControlKeyState = 0;
            b[3].Event.KeyEvent.uChar.AsciiChar = '\r';
            b[3].Event.KeyEvent.wVirtualKeyCode = VK_RETURN;
            b[3].Event.KeyEvent.wVirtualScanCode = 0x1c;
            b[3].Event.KeyEvent.wRepeatCount = 1;
            DWORD numb;
            WriteConsoleInput(hStdIn, b, 4, &numb);
        }

        m_cliThread->join();
        delete m_cliThread;
    }
}
