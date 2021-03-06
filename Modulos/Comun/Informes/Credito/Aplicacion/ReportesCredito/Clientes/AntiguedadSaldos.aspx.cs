﻿using Dapesa.Comun.Informes.Credito.IU.ReportesCredito.Informes;
using Dapesa.Comun.Informes.Credito.Reglas;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapesa.Seguridad.Entidades;
using System.Web.Security;
using System.IO;
using DevExpress.XtraReports.Web;
using DevExpress.XtraPrinting.InternalAccess;
using System.Globalization;

namespace Dapesa.Comun.Informes.Credito.IU.ReportesCredito.Clientes
{
    public partial class AntiguedadSaldos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                    Response.Redirect(FormsAuthentication.LoginUrl, true);

                    Master.Titulo = "Home::.Dapesa.Comun.Informes.Credito.ReportesCredito.AntigüedadSaldos";
                    Sesion loSesion = (Sesion)Session["Sesion"];
                    Boolean loPermiso = false;
                    foreach (Permiso llpemiso in loSesion.Usuario.Permiso)
                    {
                        if (llpemiso.Clave == 26)
                        {
                            loPermiso = true;
                        }
                    }
                    if (!loPermiso)
                    {
                        Response.Redirect(FormsAuthentication.LoginUrl, true);
                    }
                
            }
            txtClaveCliente.Attributes.Add("onkeypress", "javascript: return ValidarEspacio(event,this)");
            txtDiasPeriodo.Attributes.Add("onkeypress", "javascript: return ValidarEspacio(event,this)");
            txtDiasPeriodo.Attributes.Add("PlaceHolder", "10");
        }

        #region Metodos
        protected void EnlazarDatos()
        {
            try
            {
                InformeClientes loClientesDescuentos = new InformeClientes();
                Sesion loSesion = (Sesion)Session["Sesion"];
                InformeAntiguedadSaldos loAntiguedadSaldos = new InformeAntiguedadSaldos();
                loAntiguedadSaldos.Parameters["Sucursal"].Value = ddlSucursales.SelectedItem.ToString();
                loAntiguedadSaldos.Parameters["FechaCorte"].Value = txtFechaInicio.Text;
                loAntiguedadSaldos.Parameters["Gestor"].Value = ((ddlGestores.SelectedValue.ToString() == string.Empty) ? string.Empty : (ddlGestores.SelectedItem.ToString()));
                loAntiguedadSaldos.Parameters["TipoFecha"].Value = ddlTipoFecha.SelectedItem.ToString();
                loAntiguedadSaldos.Parameters["DiasAdicionales"].Value = (bool)cbDiasAdicionales.Checked;
                loAntiguedadSaldos.Parameters["DiasVencido"].Value = ((string.IsNullOrEmpty(txtDiasPeriodo.Text)) ? 10 : int.Parse(txtDiasPeriodo.Text));
                loAntiguedadSaldos.Parameters["Usuario"].Value = loSesion.Usuario.Nombre;
                loAntiguedadSaldos.DataSource = loClientesDescuentos.ObtenerAntiguedadSaldos(
                                   (Sesion)Session["Sesion"],
                                   int.Parse(ddlSucursales.SelectedValue),
                                   DateTime.Parse(txtFechaInicio.Text),
                                   ((string.IsNullOrEmpty(txtDiasPeriodo.Text)) ? 10 : int.Parse(txtDiasPeriodo.Text)),
                                   int.Parse(ddlTipoFecha.SelectedValue.ToString()),
                                   Convert.ToInt32(cbDiasAdicionales.Checked),
                                   ((ddlGestores.SelectedValue.ToString() == string.Empty) ? null : ddlGestores.SelectedValue),
                                   ((txtClaveCliente.Text == string.Empty) ? null : txtClaveCliente.Text)
                                   ); 
                loAntiguedadSaldos.DataMember = "DataSourceAntiguedadSaldos";

                if (Session["Permiso"] == null)
                {
                    foreach (Permiso loPermiso in loSesion.Usuario.Permiso)
                    {
                        if (loPermiso.Clave == 26)
                        {
                            foreach (Dapesa.Seguridad.Comun.Definiciones.TipoPermiso loTipoEmelento in loPermiso.TipoPermiso)
                            {
                                if (loTipoEmelento.ToString() == "Imprimir")
                                {
                                    #region Eliminar Boton Imprimir
                                    ReportToolbarItem saveItem = null;
                                    foreach (ReportToolbarItem item in xrInforme.ToolbarItems)
                                    {
                                        if (item.ItemKind == ReportToolbarItemKind.PrintReport || item.ItemKind == ReportToolbarItemKind.PrintPage)
                                            saveItem = item;
                                    }
                                    xrInforme.ToolbarItems.Remove(saveItem);
                                    saveItem = null;
                                    foreach (ReportToolbarItem item in xrInforme.ToolbarItems)
                                    {
                                        if (item.ItemKind == ReportToolbarItemKind.PrintPage || item.ItemKind == ReportToolbarItemKind.PrintPage)
                                            saveItem = item;
                                    }
                                    xrInforme.ToolbarItems.Remove(saveItem);
                                    #endregion
                                    xrInforme.ToolbarItems.Add(new ReportToolbarButton(ReportToolbarItemKind.PrintPage, true));
                                    xrInforme.ToolbarItems.Add(new ReportToolbarButton(ReportToolbarItemKind.PrintReport, true));
                                }
                            }
                        }
                        if (loPermiso.Clave == 26)
                        {
                            foreach (Dapesa.Seguridad.Comun.Definiciones.TipoPermiso loTipoEmelento in loPermiso.TipoPermiso)
                            {
                                if (loTipoEmelento.ToString() == "Guardar")
                                {
                                    #region Eliminar Boton Guadar
                                    ReportToolbarItem loItem = null;
                                    foreach (ReportToolbarItem item in xrInforme.ToolbarItems)
                                    {
                                        if (item.ItemKind == ReportToolbarItemKind.SaveToDisk || item.ItemKind == ReportToolbarItemKind.SaveToDisk)
                                            loItem = item;
                                    }
                                    xrInforme.ToolbarItems.Remove(loItem);
                                    #endregion
                                    xrInforme.ToolbarItems.Add(new ReportToolbarButton(ReportToolbarItemKind.SaveToDisk, true));
                                }
                            }
                        }
                    }
                }

                this.xrInforme.Report = loAntiguedadSaldos;
                loAntiguedadSaldos.CreateDocument();
                Page.Session["loInformeAntiguedadSaldos"] = loAntiguedadSaldos;
            }
            catch (Exception ex)
            {
                Session["Excepcion"] = ex;
                Response.Redirect("~/Error.aspx", false);
            }
        }

        #endregion

        #region Eventos
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            EnlazarDatos();
        }
        protected void xrInforme_CacheReportDocument(object sender, DevExpress.XtraReports.Web.CacheReportDocumentEventArgs e)
        {
            e.Key = "loInformeAntiguedadSaldos";
            Page.Session[e.Key] = e.SaveDocumentToMemoryStream();
        }

        protected void xrInforme_RestoreReportDocumentFromCache(object sender, DevExpress.XtraReports.Web.RestoreReportDocumentFromCacheEventArgs e)
        {

            Stream stream = Page.Session[e.Key] as Stream;
            if (stream != null)
                e.RestoreDocumentFromStream(stream);

        }
        #endregion
    }

}