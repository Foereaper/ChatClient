namespace Client.Chat.Definitions
{
    public enum ChannelNoticeType
    {
        PlayerJoined = 0,           //+ "%s joined channel.";
        PlayerLeft = 1,           //+ "%s left channel.";
        YouJoined = 2,           //+ "Joined Channel: [%s]"; -- You joined
        YouLeft = 3,           //+ "Left Channel: [%s]"; -- You left
        WrongPassword = 4,           //+ "Wrong password for %s.";
        NotMember = 5,           //+ "Not on channel %s.";
        NotModerator = 6,           //+ "Not a moderator of %s.";
        PasswordChanged = 7,           //+ "[%s] Password changed by %s.";
        OwnerChanged = 8,           //+ "[%s] Owner changed to %s.";
        PlayerNotFound = 9,           //+ "[%s] Player %s was not found.";
        NotOwner = 10,           //+ "[%s] You are not the channel owner.";
        OwnerIs = 11,           //+ "[%s] Channel owner is %s.";
        ChangeNotice = 12,           //?
        AnnounceOn = 13,           //+ "[%s] Channel announcements enabled by %s.";
        AnnounceOff = 14,           //+ "[%s] Channel announcements disabled by %s.";
        ModOn = 15,           //+ "[%s] Channel moderation enabled by %s.";
        ModOff = 16,           //+ "[%s] Channel moderation disabled by %s.";
        Muted = 17,           //+ "[%s] You do not have permission to speak.";
        PlayerKicked = 18,           //? "[%s] Player %s kicked by %s.";
        Banned = 19,           //+ "[%s] You are banned from that channel.";
        PlayerBanned = 20,           //? "[%s] Player %s banned by %s.";
        PlayerUnbanned = 21,           //? "[%s] Player %s unbanned by %s.";
        NotBanned = 22,           //+ "[%s] Player %s is not banned.";
        AlreadyMember = 23,           //+ "[%s] Player %s is already on the channel.";
        Invite = 24,           //+ "%2$s has invited you to join the channel '%1$s'.";
        InviteWrongFaction = 25,           //+ "Target is in the wrong alliance for %s.";
        WrongFaction = 26,           //+ "Wrong alliance for %s.";
        InvalidName = 27,           //+ "Invalid channel name";
        NotModerated = 28,           //+ "%s is not moderated";
        YouINvited = 29,           //+ "[%s] You invited %s to join the channel";
        PlayerHasBeenBanned = 30,           //+ "[%s] %s has been banned.";
        Throttled = 31,           //+ "[%s] The number of messages that can be sent to this channel is limited, please wait to send another message.";
        NotArea = 32,           //+ "[%s] You are not in the correct area for this channel."; -- The user is trying to send a chat to a zone specific channel, and they're not physically in that zone.
        NotLFG = 33,           //+ "[%s] You must be queued in looking for group before joining this channel."; -- The user must be in the looking for group system to join LFG chat channels.
        VoiceChatOn = 34,           //+ "[%s] Channel voice enabled by %s.";
        VoiceChatOff = 35            //+ "[%s] Channel voice disabled by %s.";
    }
}