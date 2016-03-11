using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSandbox.Extensions;

namespace WaveSandbox.Model {
	
	public class WaveBox {

		public WaveBox() {

		}

		public string File1Path { get; set; }
		public string File2Path { get; set; }
		public string OutputPath { get; private set; }
		public string LastError { get; private set; }

		public bool CombineFiles() {

			string error;
			bool result;

			// double-check that the files exist
			if (!File.Exists(this.File1Path)) {
				this.LastError = "file 1 not found: " + this.File1Path;
				return false;
			}

			if (!File.Exists(this.File2Path)) {
				this.LastError = "file 2 not found: " + this.File2Path;
				return false;
			}

			this.OutputPath = Path.Combine(Path.GetDirectoryName(this.File1Path), "output.wav");

			// open streams
			using (var input1 = new BinaryReader(File.Open(this.File1Path, FileMode.Open))) {
				using (var input2 = new BinaryReader(File.Open(this.File2Path, FileMode.Open))) {
					using (var output = new BinaryWriter(File.Open(this.OutputPath, FileMode.Create))) {

						result = this.ProcessFiles(input1, input2, output, out error);
					}
				}
			}

			if (!result) {
				this.LastError = error;
			}

			return result;
		}

		private bool ProcessFiles(BinaryReader input1, BinaryReader input2, BinaryWriter output, out string error) {
			error = string.Empty;

			var wave1 = new WaveStream(input1);
			var wave2 = new WaveStream(input2);

			var minSize = Math.Min(wave1.WaveLength, wave2.WaveLength);
			
			WaveStructHeader outputheader;
			BinaryReader shortStream = null;

			if (wave1.SampleSizeBytes != wave2.SampleSizeBytes) {
				Debug.Assert(false);
				return false;
			}

			if (wave1.WaveLength < wave2.WaveLength) {
				outputheader = wave1.GetHeader();
				shortStream = input1;
			} else {
				outputheader = wave2.GetHeader();
				shortStream = input2;
			}

			output.Write(ByteRead.GetBytes<WaveStructHeader>(outputheader));
			output.Flush();

			while (shortStream.BaseStream.Position != shortStream.BaseStream.Length) {
				var wave1packet = wave1.ReadNextPacket();
				short w1 = this.ConvertPacket(wave1packet);

				var wave2packet = wave2.ReadNextPacket();
				short w2 = this.ConvertPacket(wave2packet);

				if (wave1packet.Length < wave1.SampleSizeBytes || wave2packet.Length < wave2.SampleSizeBytes) {
					break;
				}

				output.Write(this.AddStreams(w1, w2));
			}

			output.Flush();

			return true;
		}

		private short AddStreams(short w1, short w2) {
			return (short) (w1 / 2 + w2 / 2);
		}

		private short ConvertPacket(byte[] bytes) {
			if (bytes.Length < sizeof(short)) {
				return 0;
			} else {
				return BitConverter.ToInt16(bytes, 0);
			}
		}

	}
}
