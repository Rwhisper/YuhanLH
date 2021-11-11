using PClient;
using System;
using System.Collections.Generic;

public class PacketManager
{
    

    public PacketManager()
    {
        Register();
    }

    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
    Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
    public void Register()
    {
  
        _onRecv.Add((ushort)PacketID.SP_Result, MakePacket<SP_Result>);
        _handler.Add((ushort)PacketID.SP_Result, PacketHandler.SP_ResultHandler);
  
        _onRecv.Add((ushort)PacketID.SP_ScreenResult, MakePacket<SP_ScreenResult>);
        _handler.Add((ushort)PacketID.SP_ScreenResult, PacketHandler.SP_ScreenResultHandler);

    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        int count = 0;

        int size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += sizeof(int);
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;


        Action<PacketSession, ArraySegment<byte>> action = null;
        if (_onRecv.TryGetValue(id, out action))
            action.Invoke(session, buffer);
    }

    void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
    {
        T pkt = new T();
        pkt.Read(buffer);

        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(pkt.Protocol, out action))
            action.Invoke(session, pkt);
    }
}