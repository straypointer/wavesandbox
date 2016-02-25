using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



			return true;
		}




	}
}
