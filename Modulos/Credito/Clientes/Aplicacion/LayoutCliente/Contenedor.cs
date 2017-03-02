﻿using System;
using System.Windows.Forms;

namespace Credito.Clientes.IU.LayoutCliente
{
    public partial class Contenedor : Form
    {
        #region Constructor

        public Contenedor()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void Contenedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Invalidate(true);
            this.Owner.Refresh();
            this.Owner.Update();
            this.Owner.Show();
        }

        private void Contenedor_Load(object sender, EventArgs e)
        {
            tsslCredenciales.Text += ((InicioSesion)this.Owner).Sesion.Usuario.Nombre.ToUpper();
            tsslSucursal.Text += ((InicioSesion)this.Owner).Sesion.Usuario.Sucursal[0].Descripcion.ToUpper();
        }

        private void tsmiTemporal_Click(object sender, EventArgs e)
        {
            if (!Dapesa.Comun.Utilerias.IU.ExisteFormulario(typeof(Temporal), this.MdiChildren))
            {
                Temporal loGestor = new Temporal()
                {
                    ControlBox = true,
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable,
                    MdiParent = this,
                    MinimizeBox = true,
                    ShowIcon = true,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Normal
                };

                loGestor.Show();
            }
        }

        private void tsmiPermanente_Click(object sender, EventArgs e)
        {

            if (!Dapesa.Comun.Utilerias.IU.ExisteFormulario(typeof(Permanente), this.MdiChildren))
            {
                Permanente loGestor = new Permanente()
                {
                    ControlBox = true,
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable,
                    MdiParent = this,
                    MinimizeBox = true,
                    ShowIcon = true,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Normal
                };

                loGestor.Show();
            }
        }

        private void tsmiSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void tsslEstatus_Click(object sender, EventArgs e)
        {

        }

    }
}
