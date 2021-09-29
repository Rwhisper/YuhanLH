using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using PClient;

public enum PacketID
{
    CP_Login = 1,
	CS_Login = 2,
	SS_Result = 3,
	SP_Result = 4,
	CP_Chat = 5,
	CS_Chat = 6,
	CP_ScreenRequest = 7,
	CS_ScreenResult = 8,
	SP_ScreenResult = 9,
	
}

interface IPacket
{
	ushort Protocol { get;  }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


class CP_Login : IPacket
{
    public string id;
	public string pwd;    
        
    public ushort Protocol { get { return (ushort)PacketID.CP_Login; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort pwdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.pwd = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, pwdLen);
		count += pwdLen;
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CP_Login), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort pwdLen = (ushort)Encoding.Unicode.GetBytes(this.pwd, 0, this.pwd.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(pwdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += pwdLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class CS_Login : IPacket
{
    public string id;
	public string pwd;    
        
    public ushort Protocol { get { return (ushort)PacketID.CS_Login; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort pwdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.pwd = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, pwdLen);
		count += pwdLen;
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CS_Login), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort pwdLen = (ushort)Encoding.Unicode.GetBytes(this.pwd, 0, this.pwd.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(pwdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += pwdLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class SS_Result : IPacket
{
    public bool result;    
        
    public ushort Protocol { get { return (ushort)PacketID.SS_Result; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        this.result = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
		count += sizeof(bool);
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.SS_Result), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(result), 0, segment.Array, segment.Offset + count, sizeof(bool));
		count += sizeof(bool);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class SP_Result : IPacket
{
    public bool result;    
        
    public ushort Protocol { get { return (ushort)PacketID.SP_Result; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        this.result = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
		count += sizeof(bool);
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.SP_Result), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        Array.Copy(BitConverter.GetBytes(result), 0, segment.Array, segment.Offset + count, sizeof(bool));
		count += sizeof(bool);
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class CP_Chat : IPacket
{
    public string id;
	public string chat;    
        
    public ushort Protocol { get { return (ushort)PacketID.CP_Chat; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort chatLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CP_Chat), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort chatLen = (ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(chatLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += chatLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class CS_Chat : IPacket
{
    public string id;
	public string chat;    
        
    public ushort Protocol { get { return (ushort)PacketID.CS_Chat; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort chatLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, chatLen);
		count += chatLen;
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CS_Chat), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort chatLen = (ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(chatLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += chatLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class CP_ScreenRequest : IPacket
{
    public string id;
	public string student;    
        
    public ushort Protocol { get { return (ushort)PacketID.CP_ScreenRequest; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort studentLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.student = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, studentLen);
		count += studentLen;
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CP_ScreenRequest), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort studentLen = (ushort)Encoding.Unicode.GetBytes(this.student, 0, this.student.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(studentLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += studentLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class CS_ScreenResult : IPacket
{
    public string id;
	public byte[] img;    
        
    public ushort Protocol { get { return (ushort)PacketID.CS_ScreenResult; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort imgLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		ArraySegment<byte> imgArray;
		imgArray = segment.Slice(segment.Offset + count, imgLen);
		this.img = imgArray.ToArray();      
		count += imgLen;
		
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(4096);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.CS_ScreenResult), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort imgLen = (ushort)this.img.Length;
		 Array.Copy(BitConverter.GetBytes(imgLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		 Array.Copy(this.img, 0, segment.Array, segment.Offset + count + sizeof(ushort), imgLen);
		 count += sizeof(ushort);
		 count += imgLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}

class SP_ScreenResult : IPacket
{
    public string id;
	public byte[] img;    
        
    public ushort Protocol { get { return (ushort)PacketID.SP_ScreenResult; } }
    public  void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;
        BitConverter.ToUInt16(segment.Array, segment.Offset + count);
        count += sizeof(ushort);
        count += sizeof(ushort);
        
        ushort idLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.id = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, idLen);
		count += idLen;
		ushort imgLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		ArraySegment<byte> imgArray;
		imgArray = segment.Slice(segment.Offset + count, imgLen);
		this.img = imgArray.ToArray();      
		count += imgLen;
		
       

    }

    public  ArraySegment<byte> Write()
    {
             
        ArraySegment<byte> segment = SendBufferHelper.Open(65535);            
        ushort count = 0;        

        count += sizeof(ushort);
        Array.Copy(BitConverter.GetBytes((ushort)PacketID.SP_ScreenResult), 0, segment.Array, segment.Offset + count, sizeof(ushort));
        count += sizeof(ushort);

        ushort idLen = (ushort)Encoding.Unicode.GetBytes(this.id, 0, this.id.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(idLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += idLen;
		ushort imgLen = (ushort)this.img.Length;
		 Array.Copy(BitConverter.GetBytes(imgLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		 Array.Copy(this.img, 0, segment.Array, segment.Offset + count + sizeof(ushort), imgLen);
		 count += sizeof(ushort);
		 count += imgLen;
        Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

        return SendBufferHelper.Close(count);


    }
}
