using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSandbox.Model {
	
	public class WaveBox {

		public WaveBox() {

		}

		public string File1Path { get; set; }
		public string File2Path { get; set; }
		public string OutputPath { get; set; }

		public bool CombineFiles() {
			return true;
		}


	}
}
