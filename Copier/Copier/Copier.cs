using System;
using System.Collections.Generic;
using System.Text;

namespace CopierProjekt
{
	class Copier : IPrinter, IScanner
	{
		IDevice.State DeviceState = IDevice.State.off;
		private int OnCounter = 0;
		private int Printed = 0;
		private int Scanned = 1;

		public int Counter => OnCounter;
		public int PrintCounter => Printed;
		public int ScanCounter => Scanned;

		int IDevice.Counter => throw new NotImplementedException();


		public void Print(in IDocument document)
		{
			if (DeviceState == IDevice.State.off) {
				return;
			}
			var now = DateTime.Now;
			Printed++;
			Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Print: {document.GetFileName()}.{document.GetFormatType()}");
		}

		public void Scan(out IDocument document, IDocument.FormatType formatType)
		{
			if (DeviceState == IDevice.State.on)
			{
				var now = DateTime.Now;
				string fileName;
				switch (formatType)
				{
					case IDocument.FormatType.PDF:
						fileName = $"PDFScan{Scanned}";
						Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Scan: {fileName}.pdf");
						document = new PDFDocument(fileName);
						break;
					case IDocument.FormatType.JPG:
						fileName = $"ImageScan{Scanned}";
						Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Scan: {fileName}.jpg");
						document = new ImageDocument(fileName);
						break;
					case IDocument.FormatType.TXT:
						fileName = $"TextScan{Scanned}";
						Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Scan: {fileName}.txt");
						document = new TextDocument(fileName);
						break;
					default:
						throw new Exception();
				}
			} else {
				Console.WriteLine($"Printer is off!");
				document = new PDFDocument("");
			}
		}

		public IDevice.State GetState()
		{
			return DeviceState;
		}

		public void PowerOn()
		{
			if (DeviceState == IDevice.State.off)
			{
				OnCounter++;
				DeviceState = IDevice.State.on;
				Console.WriteLine("Device is on ...");
			}
		}

		public void PowerOff()
		{
			if (DeviceState == IDevice.State.on) {
				DeviceState = IDevice.State.off;
				Console.WriteLine("... Device is off !");
			}	
		}

		public void ScanAndPrint() 
		{
			if (DeviceState == IDevice.State.on) { 
				var now = DateTime.Now;
				string fileName = $"ImageScan{Scanned}";
				Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Scan: {fileName}.jpg");
				var document = new ImageDocument(fileName);
				Print(document);
			}
		}

		internal void Scan(out IDocument doc1)
		{
			if (DeviceState == IDevice.State.on) Scanned++;
			Scan(out doc1, IDocument.FormatType.JPG);
			
		}
	}
}
