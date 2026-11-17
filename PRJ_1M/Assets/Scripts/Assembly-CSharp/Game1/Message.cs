using Game1.Mod;

namespace Game1
{
	public class Message
	{
        public int readItemOptionId1()
        {
            if (MinhAnhMod.MoGioiHanOptionFull2)
            {
                return dis.readInt();
            }
            return dis.readUnsignedByte();
        }
        public int readItemOptionParam1()
        {
            if (MinhAnhMod.MoGioiHanOptionFull2)
            {
                return dis.readInt();
            }
            return dis.readUnsignedShort();
        }
        public int readItemOptionId()
        {
			if (MinhAnhMod.MoGioiHanOptionFull1)
			{
				return dis.readInt();
			}
            return dis.readUnsignedByte();
        }
        public int readItemOptionParam()
        {
            if (MinhAnhMod.MoGioiHanOptionFull1)
            {
                return dis.readInt();
            }
            return dis.readUnsignedShort();
        }

        public sbyte command;

		private myReader dis;

		private myWriter dos;

		public Message(int command)
		{
			this.command = (sbyte)command;
			dos = new myWriter();
		}

		public Message(sbyte command)
		{
			this.command = command;
			dos = new myWriter();
		}

		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			dis = new myReader(data);
		}

		public sbyte[] getData()
		{
			return dos.getData();
		}

		public myReader reader()
		{
			return dis;
		}

		public myWriter writer()
		{
			return dos;
		}

		public int readInt3Byte()
		{
			return dis.readInt();
		}



        public void cleanup()
		{
		}

		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return dis.readInt();
			}
			return dis.readLong();
		}

        public long readLuong()
        {
            if (MinhAnhMod.ngocLong)
            {
                return dis.readLong();
            }
            return dis.readInt();
        }
    }
}
