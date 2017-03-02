using System;

namespace Dapesa.AccesoDatos.Comun
{
	public class Excepcion : ApplicationException 
	{
		/// <summary>
		/// Lanza una excepci�n espec�fica de la capa de acceso a datos
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		/// <param name="poExcepcionOriginal">Excepci�n original</param>
		public Excepcion(string psMensaje, Exception poExcepcionOriginal)
			: base(psMensaje, poExcepcionOriginal) 
		{ 
		
		}

		/// <summary>
		/// Lanza una excepci�n espec�fica de la capa de acceso a datos
		/// </summary>
		/// <param name="psMensaje">Mensaje de error</param>
		public Excepcion(string psMensaje)
			: base(psMensaje) 
		{ 
		
		}
	}
}
