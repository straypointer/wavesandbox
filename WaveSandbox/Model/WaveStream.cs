using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSandbox.Extensions;

namespace WaveSandbox.Model {
	
	
	public class WaveStream {

		BinaryReader _reader = null;
		WaveStructHeader _header;

		public WaveStream(BinaryReader input) {
			_reader = input;

			// try to read header
			//_reader.ReadBytes
			_header = ByteRead.ByteToType<WaveStructHeader>(input);

			this.FixEndianHeader();
		}

		private void FixEndianHeader() {
			_header.chunkId = ByteRead.ReverseBytes(_header.chunkId);
			_header.format = ByteRead.ReverseBytes(_header.format);
			_header.subchunk1ID = ByteRead.ReverseBytes(_header.subchunk1ID);
		}

		public ulong WaveLength {
			get;
			private set;
		}

		public byte[] ReadNextPacket() {
			return null;
		}

	}
}
