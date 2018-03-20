using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Client.Authentication
{
    public class WorldServerList : IEnumerable<WorldServerInfo>
    {
        public int Count { get; private set; }
        private WorldServerInfo[] serverList;

        public WorldServerList(BinaryReader reader)
        {
            reader.ReadUInt32();

            Count = reader.ReadUInt16();
            serverList = new WorldServerInfo[Count];

            for (var i = 0; i < Count; ++i)
                serverList[i] = new WorldServerInfo(reader);
        }

        public WorldServerInfo this[int index] => serverList[index];

        #region IEnumerable<WorldServerInfo> Members

        public IEnumerator<WorldServerInfo> GetEnumerator()
        {
            foreach (var server in serverList)
                yield return server;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var realm in serverList)
                yield return realm;
        }

        #endregion
    }
}
