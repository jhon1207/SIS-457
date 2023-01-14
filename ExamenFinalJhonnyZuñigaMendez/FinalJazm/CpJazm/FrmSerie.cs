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
    public partial class FrmSerie : Form
    {
        bool esNuevo = true;
        public FrmSerie()
        {
            InitializeComponent();
        }

        private void FrmSerie_Load(object sender, EventArgs e)
        {
            Size = new Size(1039, 423);
            listar();
        }

        private void listar()
        {
            var series = SerieCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = series;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["titulo"].HeaderText = "Titulo";
            dgvLista.Columns["sinopsis"].HeaderText = "Sinopsis";
            dgvLista.Columns["director"].HeaderText = "Director";
            dgvLista.Columns["duracion"].HeaderText = "Duracion";
            btnEditar.Enabled = series.Count > 0;
            btnEliminar.Enabled = series.Count > 0;
            if (series.Count > 0) dgvLista.Rows[0].Cells["titulo"].Selected = true;
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
            txtTitulo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1039, 578);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var serie = SerieCln.get(id);

            txtTitulo.Text = serie.titulo;
            txtSinopsis.Text = serie.sinopsis; ;
            txtDirector.Text = serie.director; ;
            //nudDuracion.Value = serie.duracion;
            txtTitulo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(1039, 423);
            limpiar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpTitulo.SetError(txtTitulo, "");
            erpSinopsis.SetError(txtSinopsis, "");
            erpDirector.SetError(txtDirector, "");
            //erpDuracion.SetError(nudDuracion, "");

            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                erpTitulo.SetError(txtTitulo, "El campo titulo es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtSinopsis.Text))
            {
                erpSinopsis.SetError(txtSinopsis, "El campo Sinopsis es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDirector.Text))
            {
                erpSinopsis.SetError(txtSinopsis, "El campo Direcccion es obligatorio");
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
            txtTitulo.Text = String.Empty;
            txtSinopsis.Text = String.Empty;
            txtDirector.Text = String.Empty;
            //nudDuracion.Value = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var serie = new Serie();
                serie.titulo = txtTitulo.Text.Trim();
                serie.sinopsis = txtSinopsis.Text.Trim();
                serie.director = txtDirector.Text;
                //serie.duracion = nudDuracion.Value;
                serie.fechaEstreno = DateTime.Now;

                if (esNuevo)
                {
                    serie.registroActivo = true;
                    SerieCln.insertar(serie);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    SerieCln.actualizar(serie);
                }

                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Serie guardado correctamente", "::: Mensaje - FinalJazm :::", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string titulo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja la serie con el titulo {titulo}?", "::: Pregunta - FinalJazm :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                SerieCln.eliminar(id);
                listar();
                MessageBox.Show("La serie a sido dado de baja correctamente", "::: Mensaje - FinalJazm :::", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
