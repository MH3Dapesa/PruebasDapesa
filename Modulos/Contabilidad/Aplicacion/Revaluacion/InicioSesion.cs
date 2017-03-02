﻿using Dapesa.Comun.Entidades;
using Dapesa.Criptografia.Comun;
using Dapesa.Criptografia.Reglas;
using Dapesa.Seguridad.Entidades;
using Dapesa.Seguridad.Reglas;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;


namespace Contabilidad.IU.Revaluacion
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
            //MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //builder.Server = "192.168.0.189";
            //builder.UserID = "root";
            //builder.Password = "0000";
            //builder.Database = "prueba_costos";


            //MySqlConnection conn = new MySqlConnection(builder.ToString());
            //MySqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "select * from muestra limit 1";

            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        private void InicioSesion_Shown(object sender, EventArgs e)
        {
            this.Invalidate(true);
            this.Refresh();
            this.Update();
            txtUsuario.SelectionStart = txtUsuario.Text.Length;
        }

        private void InicioSesion_VisibleChanged(object sender, EventArgs e)
        {

            if (!((Form)sender).Visible)
            {
                Contenedor loMdi = new Contenedor()
                {
                    Owner = this
                };

                loMdi.Show();
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

                this._oSesion = loAdministrador.ObtenerSesion(new Conexion()
                {
                    Credenciales = new Credenciales()
                    {                       
                        Cifrado = loCifrado,
                        Contrasenia = loCifrado.Cifrar(txtContrasenia.Text),
                        Usuario = txtUsuario.Text
                    },
                    IdServicio = txtBaseDatos.Text,
                    Puerto = ConfigurationManager.AppSettings["BDPuerto"],
                    Servidor = ConfigurationManager.AppSettings["BDServidor"],
                    Tipo = Dapesa.Comun.Definiciones.TipoConexion.CredencialesExplicitas,
                    TipoCliente = Dapesa.Comun.Definiciones.TipoCliente.MySql
                });

                if (_oSesion.Estatus != Dapesa.Seguridad.Comun.Definiciones.EstatusSesion.Iniciada)
                    return;

                lblMensaje.Text = string.Empty;
                this.Hide();
                
                #endregion
            }
            catch (Exception ex)
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

        internal Sesion Sesion
        {
            get
            {
                return this._oSesion;
            }
        }

        #endregion

        private void txtBaseDatos_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
