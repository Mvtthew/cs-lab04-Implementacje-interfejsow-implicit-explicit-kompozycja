using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace BezKlasyCopier
{
	class Copier
	{
		public IPrinter Printer = new Printer();
		public IScanner Scanner = new Scanner();

		public int Counter => Printer.Counter;

		public void PowerOn() {
			Printer.PowerOn();
			Scanner.PowerOn();
		}

		public void PowerOff()
		{
			Printer.PowerOff();
			Scanner.PowerOff();
		}

		public IDevice.State GetState() {
			return Printer.GetState();
		}

		public void Print(in IDocument document) {
			Printer.Print(document);
		}

		internal void Scan(out IDocument doc1)
		{
			Scanner.Scan(out doc1, IDocument.FormatType.JPG);
		}
	}
}
