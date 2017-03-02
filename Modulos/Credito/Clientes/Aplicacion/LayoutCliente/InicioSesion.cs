﻿using Dapesa.Comun.Entidades;
using Dapesa.Criptografia.Comun;
using Dapesa.Criptografia.Reglas;
using Dapesa.Seguridad.Entidades;
using Dapesa.Seguridad.Reglas;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Credito.Clientes.IU.LayoutCliente
{
	public partial class InicioSesion : Form
	{
		#region Atributos

		private Sesion _oSesion;

		#endregion

		#region Constructor

		public InicioSesion()
		{
			InitializeComponent();
		}

		#endregion

		#region Eventos

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			this.ValidarCredenciales();
		}

		private void InicioSesion_Shown(object sender, EventArgs e)
		{
			this.Invalidate(true);
			this.Refresh(); 
			this.Update();
		}

		private void InicioSesion_VisibleChanged(object sender, EventArgs e)
		{

			if (!((Form)sender).Visible)
			{
				Contenedor loContenedor = new Contenedor() {
					Owner = this
				};

				loContenedor.Show();
			}

			txtContrasenia.Text = string.Empty;
		}

		#endregion

		#region Metodos

		private bool CamposRequeridos()
		{
			bool lbCamposRequeridos = true;

			#region Verificación de campos requeridos

			pbBaseDatosRequerida.Visible = false; 
			pbContraseniaRequerida.Visible = false;
			pbUsuarioRequerido.Visible = false;
			
			if (string.IsNullOrEmpty(txtUsuario.Text.Trim()))
			{
				pbUsuarioRequerido.Visible = true;
				lbCamposRequeridos = false;
			}
				
			if (string.IsNullOrEmpty(txtContrasenia.Text.Trim()))
			{
				pbContraseniaRequerida.Visible = true;
				lbCamposRequeridos = false;
			}

			if (string.IsNullOrEmpty(txtBaseDatos.Text.Trim()))
			{
				pbBaseDatosRequerida.Visible = true;
				lbCamposRequeridos = false;
			}

			#endregion

			return lbCamposRequeridos;
		}

		private void ValidarCredenciales()
		{

		if (!this.CamposRequeridos())
				return;

			try
			{
				#region Autenticación

				Administrador loAdministrador = new Administrador();
				Cifrado loCifrado = new Cifrado(Definiciones.TipoCifrado.AES);
				
				this._oSesion = loAdministrador.ObtenerSesion(new Conexion() {
					Credenciales = new Credenciales() {
						Cifrado = loCifrado,
						Contrasenia = loCifrado.Cifrar(txtContrasenia.Text),
						Usuario = txtUsuario.Text
					},
                    IdAplicacion = ConfigurationManager.AppSettings["ID"],
					IdServicio = txtBaseDatos.Text,
					Puerto = ConfigurationManager.AppSettings["BDPuerto"],
					Servidor = ConfigurationManager.AppSettings["BDServidor"],
					Tipo = Dapesa.Comun.Definiciones.TipoConexion.NoTNSNAMES,
					TipoCliente = Dapesa.Comun.Definiciones.TipoCliente.Oracle
				});

				if (_oSesion.Estatus != Dapesa.Seguridad.Comun.Definiciones.EstatusSesion.Iniciada)
					return;

                lblMensaje.Text = string.Empty;

                bool loPermiso = false;
                foreach (Permiso llpemiso in _oSesion.Usuario.Permiso)
                {
                    if (llpemiso.Aplicacion.KeyId == ConfigurationManager.AppSettings["ID"])
                    {
                        loPermiso = true;
                    }
                }

                if (loPermiso)
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Actualmente no tienes permiso a esta aplicación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasenia.Text = string.Empty;
                    txtUsuario.SelectAll();
                    txtUsuario.Focus();
                    return;
                }

				#endregion
			}
			catch(Exception ex)
			{
				#if DEBUG
					MessageBox.Show(ex.Message + "\r\nFuente: " + ex.Source, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				#else
					ex.Source = string.Empty;
					lblMensaje.Text = "Credenciales no válidas. Intenta nuevamente por favor." + ex.Source;
				#endif
				txtContrasenia.Text = string.Empty;
				txtUsuario.SelectAll();
				txtUsuario.Focus();
			}
		}

		#endregion

		#region Propiedades

		public Sesion Sesion
		{
			get
			{
				return this._oSesion;
			}
		}

		#endregion
	}
}
