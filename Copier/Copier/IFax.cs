using System;
using System.Collections.Generic;
using System.Text;

namespace CopierProjekt
{
	interface IFax
	{
		void Send(in IDocument document) { }
	}
}
