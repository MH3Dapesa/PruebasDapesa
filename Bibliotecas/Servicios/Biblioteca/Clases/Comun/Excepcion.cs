﻿using System;

namespace Dapesa.Servicios.Comun
{
	public class Excepcion : ApplicationException
	{
		/// <summary>
		/// Lanza una excepción específica de la capa de servicios
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		/// <param name="poExcepcionOriginal">Excepción original</param>
		public Excepcion(string psMensaje, Exception poExcepcionOriginal)
			: base(psMensaje, poExcepcionOriginal)
		{

		}

		/// <summary>
		/// Lanza una excepción específica de la capa de servicios
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		public Excepcion(string psMensaje)
			: base(psMensaje)
		{

		}
	}
}
