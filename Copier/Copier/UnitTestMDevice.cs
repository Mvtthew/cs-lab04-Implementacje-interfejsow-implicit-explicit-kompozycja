using Microsoft.VisualStudio.TestTools.UnitTesting;
using CopierProjekt;
using System;
using System.IO;
using CopierProjektTests;

namespace MDeviceProjektTests
{


    [TestClass]
    public class UnitTestMDevice
    {
        [TestMethod]
        public void ultifunctionalDevice_GetState_StateOff()
        {
            var md = new MultifunctionalDevice();
            md.PowerOff();

            Assert.AreEqual(IDevice.State.off, md.GetState()); 
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            Assert.AreEqual(IDevice.State.on, md.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                md.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);   
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var md = new MultifunctionalDevice();
            md.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                md.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var md = new MultifunctionalDevice();
            md.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                md.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                md.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                md.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                md.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                md.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                md.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var md = new MultifunctionalDevice();
            md.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                md.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                md.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
            }
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            md.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            md.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            md.Print(in doc3);

            md.PowerOff();
            md.Print(in doc3);
            md.Scan(out doc1);
            md.PowerOn();

            md.ScanAndPrint();
            md.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, md.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();

            IDocument doc1;
            md.Scan(out doc1);
            IDocument doc2;
            md.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            md.Print(in doc3);

            md.PowerOff();
            md.Print(in doc3);
            md.Scan(out doc1);
            md.PowerOn();

            md.ScanAndPrint();
            md.ScanAndPrint();

            // 3 skany, gdy urządzenie włączone
            Assert.AreEqual(3, md.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var md = new MultifunctionalDevice();
            md.PowerOn();
            md.PowerOn();
            md.PowerOn();

            IDocument doc1;
            md.Scan(out doc1);
            IDocument doc2;
            md.Scan(out doc2);

            md.PowerOff();
            md.PowerOff();
            md.PowerOff();
            md.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            md.Print(in doc3);

            md.PowerOff();
            md.Print(in doc3);
            md.Scan(out doc1);
            md.PowerOn();

            md.ScanAndPrint();
            md.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, md.Counter);
        }

    }


}
