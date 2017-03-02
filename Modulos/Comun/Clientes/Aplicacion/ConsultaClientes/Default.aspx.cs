﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comun.Clientes.ConsultaClientes
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!Request.IsAuthenticated)
                    Response.Redirect(FormsAuthentication.LoginUrl, true);

                Master.Titulo = "Home::.Dapesa.Almacén.Pedidos.Mensajero";
            }
        }
    }
}