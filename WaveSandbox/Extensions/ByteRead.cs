using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WaveSandbox.Extensions {
	
	public class ByteRead {

		public static T ByteToType<T>(BinaryReader reader) {
			byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

			GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();

			return theStructure;
		}

		public static UInt32 ReverseBytes(UInt32 value) {
			return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
				(value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
		}
	}
}
