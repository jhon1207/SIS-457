using CadFinalJazm;
using ClnFinalJazm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpJazm
{
    public partial class FrmUsuario : Form
    {
        public bool esNuevo { get; private set; }

        public FrmUsuario()
        {
            InitializeComponent();
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Size = new Size(1039, 423);
            listar();
        }

        private void listar()
        {
            var usuarios = UsuarioCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = usuarios;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["usuario"].HeaderText = "Usuario";
            dgvLista.Columns["clave"].HeaderText = "Clave";
            dgvLista.Columns["rol"].HeaderText = "Rol";
            btnEditar.Enabled = usuarios.Count > 0;
            btnEliminar.Enabled = usuarios.Count > 0;
            if (usuarios.Count > 0) dgvLista.Rows[0].Cells["usuario"].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(1039, 578);
            txtUsuario.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1039, 578);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var usuarios = UsuarioCln.get(id);

            txtUsuario.Text = usuarios.usuario;
            txtClave.Text = usuarios.clave; ;
            txtRol.Text = usuarios.rol; ;
            //nudDuracion.Value = serie.duracion;
            txtUsuario.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(1039, 423);
            limpiar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpTitulo.SetError(txtUsuario, "");
            erpSinopsis.SetError(txtClave, "");
            erpDirector.SetError(txtRol, "");
            //erpDuracion.SetError(nudDuracion, "");

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                erpTitulo.SetError(txtUsuario, "El campo Usuario es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                erpSinopsis.SetError(txtClave, "El campo Clave es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtRol.Text))
            {
                erpSinopsis.SetError(txtClave, "El campo Rol es obligatorio");
                esValido = false;
            }
            //if (string.IsNullOrEmpty(nudDuracion.Text))
            //{
            //erpDuracion.SetError(nudDuracion, "El campo Saldo es obligatorio");
            //esValido = false;
            //}
            return esValido;
        }

        private void limpiar()
        {
            txtUsuario.Text = String.Empty;
            txtClave.Text = String.Empty;
            txtRol.Text = String.Empty;
            //nudDuracion.Value = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var usuarios = new Usuario();
                usuarios.registroActivo = true;
                usuarios.usuario = txtUsuario.Text.Trim();
                usuarios.clave = Util.Encrypt("SIS457");
                usuarios.rol = txtRol.Text.Trim();
                UsuarioCln.insertar(usuarios);
                

                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Usuario guardado correctamente", "::: Mensaje - FinalJazm :::", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string usuario = dgvLista.Rows[index].Cells["usuario"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el usuario con el nombre {usuario}?", "::: Pregunta - FinalJazm :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                SerieCln.eliminar(id);
                listar();
                MessageBox.Show("El usuario a sido dado de baja correctamente", "::: Mensaje - FinalJazm :::", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
