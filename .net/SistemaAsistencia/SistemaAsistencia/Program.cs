using System;
using System.Windows.Forms;
using SistemaAsistencia.Controlador;
using SistemaAsistencia.Utilidades;
using SistemaAsistencia.Vista;
using SistemaAsistencia.Vista.Login;
using SistemaAsistencia.Vista.PrimerUsuario;

namespace SistemaAsistencia
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                UsuarioController usuarioController = new UsuarioController();

                if (!usuarioController.ExistenUsuarios())
                {
                    using (var primer = new FrmPrimerUsuario())
                    {
                        if (primer.ShowDialog() != DialogResult.OK)
                            return; // cerró/salió sin crear admin, terminar
                    }
                }

                Application.Run(new FrmLogin());
            }
            catch (Exception ex)
            {
                Logger.Error("Arranque del sistema", ex);
                MessageBox.Show(
                    "No se pudo iniciar el sistema.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}