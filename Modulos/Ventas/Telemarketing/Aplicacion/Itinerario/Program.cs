﻿using System;
using System.Windows.Forms;

namespace Dapesa.Ventas.Telemarketing.IU.Itinerario
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new InicioSesion());
		}
	}
}
