﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReferenciaBancaria.aspx.cs" Inherits="Dapesa.Comun.Informes.Credito.IU.ReportesCredito.Clientes.ReferenciaBancaria" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.XtraReports.v16.2.Web, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="cEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/HojaEstilosImpresion.css" />
    <style>
        div#ctl00_cphContenidoPrincipal_rvPedidos_AsyncWait {
            background-color: transparent;
            opacity: 0.0;
            position: absolute;
            display: none;
            filter: alpha(opacity=0);
        }
    </style>
</asp:Content>
<asp:Content ID="cContenido" ContentPlaceHolderID="cphContenidoPrincipal" runat="server">
    <div>
        <br/>
        <asp:Panel ID="pnlParametros" runat="server" DefaultButton="btnGenerar">
            <table style="border-spacing:0 3px; border-collapse:separate">
                <tr style="margin-top:10px;">
                    <td style="width:90px">
                        <asp:Label ID="lblSucursales" runat="server" AccessKey="S" AssociatedControlID="ddlSucursales" Text="<u><b>S</b></u>ucursal(es):" />
                    </td>
                    <td colspan="2" >
                        <asp:DropDownList ID="ddlSucursales" runat="server" Width="370px" Height="20px" />
                        <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ControlToValidate="ddlSucursales" ErrorMessage="*Debe seleccionar una sucursal.<br />" ToolTip="Debe seleccionar una sucursal.<br />" ForeColor="Red" Font-Size="8pt" />
                    </td>
                </tr>
                <tr>
                    <td style="width:90px;">
                        <asp:Label ID="lblVendedores" runat="server" AccessKey="V" AssociatedControlID="ddlVendedores" Text="<u><b>V</b></u>endedor(es):" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlVendedores" runat="server" Width="370px" Height="20px" />
                    </td>
                    <td style="padding-left: 25px;"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblClaveCliente" runat="server" Text="<u><b>C</b></u>lave cliente:"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClaveCliente" runat="server" CssClass="upperText" Width="365px" />
                        <asp:RegularExpressionValidator ID="revClaveCliente" runat="server"
                                ControlToValidate="txtClaveCliente" ErrorMessage="*Ingrese valores alfanumericos"
                                ForeColor="Red"
                                ValidationExpression="^([a-zA-Z0-9]{1,20})$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnGenerar" runat="server" OnClick="btnGenerar_Click" Text="Generar"/>
                    </td>
                </tr>      
            </table>
            <asp:CascadingDropDown
                    ID="cddSucursales"
                    runat="server"
                    TargetControlID="ddlSucursales"
                    Category="CLAVE"
                    PromptText="[SELECCIONAR]"
                    ServicePath="~/DropDownAsincrono.asmx"
                    ServiceMethod="ObtenerSucursales" />         
            <asp:CascadingDropDown
                    ID="ccdVendedores"
                    runat="server"
                    TargetControlID="ddlVendedores"
                    ParentControlID="ddlSucursales"
                    Category="CLAVE"
                    PromptText="[SELECCIONAR]"
                    ServicePath="~/DropDownAsincrono.asmx"
                    SelectedValue="-1"
                    ServiceMethod="ObtenerVendedores" />          
        </asp:Panel>
        <script>
            function ValidarEspacio(e, campo) {
                key = e.keyCode ? e.keyCode : e.which;
                if (key == 32) { return false; }
            }
        </script>
         
        <asp:UpdatePanel ID="upPanelContenido" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <asp:UpdatePanel ID="upPanelMostrar" runat="server">
                    <ContentTemplate>
                        <br />
                        <div style="margin: 0; padding: 0;">
                            <table border="0" style="width: 97%; position: absolute; padding-top: 2px;">
                                <tr>
                                    <td>
                                        <dx:ASPxDocumentViewer ID="xrInforme" runat="server"   OnCacheReportDocument="xrInforme_CacheReportDocument"  OnRestoreReportDocumentFromCache="xrInforme_RestoreReportDocumentFromCache">
                                            <ToolbarItems>
                                                <%--<dx:ReportToolbarButton ItemKind="Search" />--%>
                                               <dx:ReportToolbarSeparator />
                                              <%-- <dx:ReportToolbarButton ItemKind="PrintReport" />
                                               <dx:ReportToolbarButton ItemKind="PrintPage" />--%>
                                               <dx:ReportToolbarButton ItemKind="FirstPage" />
                                               <dx:ReportToolbarButton ItemKind="PreviousPage" />
                                               <dx:ReportToolbarLabel ItemKind="Custom" Text="Page" />
                                               <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="60" />
                                               <dx:ReportToolbarLabel ItemKind="Custom" Text="of" />
                                               <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                               <dx:ReportToolbarButton ItemKind="NextPage" />
                                               <dx:ReportToolbarButton ItemKind="LastPage" />
                                               <dx:ReportToolbarSeparator />
                                               <%--<dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                               <dx:ReportToolbarButton ItemKind="SaveToWindow" />--%>
                                               <dx:ReportToolbarSeparator />
                                               <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="100">
                                                   <Elements>
                                                       <dx:ListElement Value="pdf" Text="Pdf" />
                                                       <dx:ListElement Value="xls" Text="Excel" />
                                                   </Elements>
                                               </dx:ReportToolbarComboBox>
                                            </ToolbarItems>
                                        </dx:ASPxDocumentViewer>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalir" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnGenerar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>            
    </div>
</asp:Content>