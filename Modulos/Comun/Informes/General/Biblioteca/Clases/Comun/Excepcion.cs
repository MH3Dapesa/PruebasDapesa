﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dapesa.Comun.Informes.General.Comun
{

    public class Excepcion : ApplicationException
    {
        /// <summary>
        /// Lanza una excepción específica del proceso de despliegue de informes
        /// </summary>
        /// <param name="psMensaje">Mensaje de error</param>
        /// <param name="poExcepcionOriginal">Excepción original</param>
        public Excepcion(string psMensaje, Exception poExcepcionOriginal)
            : base(psMensaje, poExcepcionOriginal)
        {

        }

        /// <summary>
        /// Lanza una excepción específica del proceso de despliegue de informes
        /// </summary>
        /// <param name="psMensaje">Mensaje de error</param>
        public Excepcion(string psMensaje)
            : base(psMensaje)
        {

        }
    }


}