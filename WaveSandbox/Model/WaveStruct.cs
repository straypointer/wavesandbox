using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSandbox.Model {

	// from: http://soundfile.sapp.org/doc/WaveFormat/
	
	struct WaveStructHeader {

		public UInt32 chunkId;		// big endian
		public UInt32 chunkSize;	// little endian
		public UInt32 format;		// big endian

		public UInt32 subchunk1ID;		// big
		public UInt32 subchunk1Size;	// little
		public UInt16 audioFormat;		// little
		public UInt16 numChannels;		// little
		public UInt32 sampleRate;		// little
		public UInt32 byteRate;			// little
		public UInt16 blockAlign;		// little
		public UInt16 bitsPersample;	// little

	}
}
