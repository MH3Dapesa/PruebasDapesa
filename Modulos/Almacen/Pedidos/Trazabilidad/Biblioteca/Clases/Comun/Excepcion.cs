﻿using System;

namespace Dapesa.Almacen.Pedidos.Trazabilidad.Comun
{
	public class Excepcion : ApplicationException
	{
		/// <summary>
		/// Lanza una excepción específica del proceso de empaque para el seguimiento de pedidos en almacén
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		/// <param name="poExcepcionOriginal">Excepción original</param>
		public Excepcion(string psMensaje, Exception poExcepcionOriginal)
			: base(psMensaje, poExcepcionOriginal)
		{

		}

		/// <summary>
		/// Lanza una excepción específica del proceso de empaque para el seguimiento de pedidos en almacén
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		public Excepcion(string psMensaje)
			: base(psMensaje)
		{

		}
	}
}
