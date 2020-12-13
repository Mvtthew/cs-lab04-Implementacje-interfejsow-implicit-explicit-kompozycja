using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace BezKlasyCopier
{
	class Printer : IPrinter
	{

		IDevice.State DeviceState = IDevice.State.off;
		private int OnCounter = 0;
		private int Printed = 0;

		public int Counter => OnCounter;

		public int PrintCounter => Printed;

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

		public void Print(in IDocument document)
		{
			if (DeviceState == IDevice.State.off)
			{
				return;
			}
			var now = DateTime.Now;
			Printed++;
			Console.WriteLine($"{now.ToString("MM.dd.yyyy HH:mm:ss")} Print: {document.GetFileName()}.{document.GetFormatType()}");
		}
	}
}
