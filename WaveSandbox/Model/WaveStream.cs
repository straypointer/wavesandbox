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
			_header = ByteRead.ByteToType<WaveStructHeader>(input);

			this.ValidateHeader();

			this.WaveLength = _header.subchunk2Size;
			this.SampleSizeBytes = _header.bitsPersample / 8;
		}

		private bool ValidateHeader() {
			//_header.chunkId = ByteRead.ReverseBytes(_header.chunkId);				// Contains the letters "RIFF" in ASCII form (0x52494646 big-endian form).
			//_header.format = ByteRead.ReverseBytes(_header.format);					// Contains the letters "WAVE" (0x57415645 big-endian form).
			//_header.subchunk1ID = ByteRead.ReverseBytes(_header.subchunk1ID);		// Contains the letters "fmt " (0x666d7420 big-endian form).
			//_header.subchunk2ID = ByteRead.ReverseBytes(_header.subchunk2ID);		// Contains the letters "data" (0x64617461 big-endian form).
			return true;
		}

		public WaveStructHeader GetHeader() {
			return _header;
		}

		public ulong WaveLength {
			get;
			private set;
		}

		public int SampleSizeBytes {
			get;
			private set;
		}

		public byte[] ReadNextPacket() {
			
			return _reader.ReadBytes(this.SampleSizeBytes);
		}

	}
}
