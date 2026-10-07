using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Vista.Comun
{
    /// <summary>
    /// Resultado elegido por el usuario en el cuadro de confirmación.
    /// </summary>
    public enum ResultadoEliminacion
    {
        Cancelado = 0,
        Baja = 1,
        Definitiva = 2
    }

    /// <summary>
    /// Cuadro de confirmación antes de dar de baja o eliminar un registro.
    /// Muestra qué se va a borrar y los registros vinculados (dependencias).
    /// El botón de eliminación definitiva solo aparece para el rol Administrador.
    /// </summary>
    public class FrmConfirmarEliminar : Form
    {
        private readonly Button btnDarDeBaja;
        private readonly Button btnEliminarDefinitiva;
        private readonly Button btnCancelar;

        /// <summary>
        /// Resultado elegido por el usuario al cerrar el cuadro.
        /// </summary>
        public ResultadoEliminacion Resultado { get; private set; } = ResultadoEliminacion.Cancelado;

        public FrmConfirmarEliminar(string descripcionDelRegistro,
            List<Dependencia> dependencias)
        {
            Text = "Eliminar registro";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(540, 400);
            MinimumSize = new Size(540, 400);

            // ---------- Estructura: 4 filas ----------

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(20, 14, 20, 14)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // encabezado
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // dependencias
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // nota
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // botones

            // ---------- Encabezado ----------

            Label lblTitulo = new Label
            {
                Text = "¿Qué desea hacer con este registro?",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold),
                Dock = DockStyle.Top
            };

            Label lblRegistro = new Label
            {
                Text = descripcionDelRegistro,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.75F),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Top,
                MaximumSize = new Size(480, 60),
                Padding = new Padding(0, 4, 0, 0)
            };

            Panel pnlEncabezado = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            pnlEncabezado.Controls.Add(lblRegistro);
            pnlEncabezado.Controls.Add(lblTitulo);

            layout.Controls.Add(pnlEncabezado, 0, 0);

            // ---------- Dependencias (con scroll si la lista es larga) ----------

            string textoDependencias;
            bool hayDependencias = false;

            if (dependencias != null)
            {
                foreach (Dependencia dep in dependencias)
                {
                    if (dep != null && dep.Cantidad > 0)
                    {
                        hayDependencias = true;
                        break;
                    }
                }
            }

            if (hayDependencias)
            {
                textoDependencias = "Registros vinculados (se eliminarán también con la eliminación definitiva):";

                foreach (Dependencia dep in dependencias)
                {
                    if (dep != null && dep.Cantidad > 0)
                    {
                        textoDependencias += Environment.NewLine + "  • " +
                            dep.Cantidad + " " + dep.Nombre;
                    }
                }
            }
            else
            {
                textoDependencias = "Este registro no tiene registros vinculados.";
            }

            Label lblDependencias = new Label
            {
                Text = textoDependencias,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                MaximumSize = new Size(480, 0)
            };

            Panel pnlDependencias = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 8, 0, 8)
            };
            pnlDependencias.Controls.Add(lblDependencias);

            layout.Controls.Add(pnlDependencias, 0, 1);

            // ---------- Nota ----------

            Label lblNota = new Label
            {
                Text = "Nota: \"Dar de baja\" oculta el registro conservando su" +
                    " histórico. No se puede deshacer una eliminación definitiva.",
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Dock = DockStyle.Top,
                MaximumSize = new Size(480, 0)
            };

            layout.Controls.Add(lblNota, 0, 2);

            // ---------- Botones (alineados a la derecha, sin solaparse) ----------

            btnDarDeBaja = new Button
            {
                Text = "Dar de baja",
                Width = 130,
                Height = 34,
                Margin = new Padding(6, 8, 0, 0)
            };
            btnDarDeBaja.Click += (s, e) => Elegir(ResultadoEliminacion.Baja);

            btnEliminarDefinitiva = new Button
            {
                Text = "Eliminar definitivamente",
                Width = 200,
                Height = 34,
                Margin = new Padding(6, 8, 0, 0)
            };
            btnEliminarDefinitiva.Click += (s, e) => Elegir(ResultadoEliminacion.Definitiva);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 110,
                Height = 34,
                Margin = new Padding(6, 8, 0, 0)
            };
            btnCancelar.Click += (s, e) => CerrarCancelando();

            FlowLayoutPanel pnlBotones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false
            };

            // En RightToLeft el primero agregado queda a la derecha.
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnEliminarDefinitiva);
            pnlBotones.Controls.Add(btnDarDeBaja);

            layout.Controls.Add(pnlBotones, 0, 3);

            Controls.Add(layout);

            // El botón de eliminación definitiva solo para el Administrador.
            // Dar de baja solo para Administrador y Directivo.
            btnEliminarDefinitiva.Visible = Sesion.UsuarioActualRolAdministrador();
            btnDarDeBaja.Visible = Sesion.PuedeGestionarActivo();

            AcceptButton = btnDarDeBaja;
            CancelButton = btnCancelar;
        }

        private void Elegir(ResultadoEliminacion resultado)
        {
            Resultado = resultado;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CerrarCancelando()
        {
            Resultado = ResultadoEliminacion.Cancelado;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}