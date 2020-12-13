using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace BezKlasyCopier
{
	class Scanner : IScanner
	{

		IDevice.State DeviceState = IDevice.State.off;
		private int OnCounter = 0;
		private int Scanned = 0;

		public int Counter => OnCounter;

		public int ScanCounter => Scanned;

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
			if (DeviceState == IDevice.State.on)
			{
				DeviceState = IDevice.State.off;
				Console.WriteLine("... Device is off !");
			}
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
			}
			else
			{
				Console.WriteLine($"Printer is off!");
				document = new PDFDocument("");
			}
		}
	}
}
