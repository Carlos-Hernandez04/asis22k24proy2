﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;



namespace CapaDiseno
{
    public partial class frm_asignacion_perfiles : Form
    {
        logica logic;
        string TablaPerfilUsuario = "Tbl_asignaciones_perfils_usuario";

        //###################  empieza lo que hizo Karla  Sofia Gómez Tobar #######################
        public frm_asignacion_perfiles(string idUsuario)
        {
            InitializeComponent();

            ToolTip tnuevo = new ToolTip();
            tnuevo.SetToolTip(btn_agregar, "Crea nueva asignación");
            ToolTip tguardar = new ToolTip();
            tguardar.SetToolTip(btn_guardar, "Guardar nueva asignación");
            ToolTip tbuscar = new ToolTip();
            tbuscar.SetToolTip(btn_buscar, "Buscar todas las asignaciones");
            ToolTip tcancelar = new ToolTip();
            tcancelar.SetToolTip(btn_cancelar, "reestablecer");
            ToolTip tremover = new ToolTip();
            tcancelar.SetToolTip(btn_remover, "Quita la nueva asignación");
            ToolTip tsalir = new ToolTip();
            tsalir.SetToolTip(btn_salir, "Salir de asignacion perfil");
            ToolTip tayuda = new ToolTip();
            tayuda.SetToolTip(btn_ayuda, "Ayuda");

            logic = new logica(idUsuario);
            string tablaU = "Tbl_usuarios";
            string campo1U = "Pk_id_usuario";
            string campo2U = "nombre_usuario";

            string tablaP = "Tbl_perfiles";
            string campo1P = "Pk_id_perfil";
            string campo2P = "nombre_perfil";


            string TablaA = "Tbl_asignaciones_perfils_usuario";
            string campo1A= "PK_id_Perfil_Usuario";







            llenarseUsuario(tablaU, campo1U, campo2U);
            llenarsePerfil(tablaP, campo1P, campo2P);



            // Asocia el evento SelectedIndexChanged después de poblar el ComboBox
            cbo_usuario.SelectedIndexChanged += new EventHandler(cbo_usuario_SelectedIndexChanged);
            cbo_perfiles.SelectedIndexChanged += new EventHandler(cbo_perfiles_SelectedIndexChanged2);
            

        }



        public void llenarseUsuario(string tabla, string campo1, string campo2)
        {
            // Obtén los datos para el ComboBox
            var dt2 = logic.enviarUsuario(tabla, campo1, campo2);

            // Limpia el ComboBox antes de llenarlo
            cbo_usuario.Items.Clear();

            foreach (DataRow row in dt2.Rows)
            {
                // Agrega el elemento mostrando el formato "ID-Nombre"
                cbo_usuario.Items.Add(new ComboBoxItemU
                {
                    Value = row[campo1].ToString(),
                    Display = row[campo2].ToString()
                });
            }

            // Configura AutoComplete para el ComboBox con el formato deseado
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            foreach (DataRow row in dt2.Rows)
            {
                coleccion.Add(Convert.ToString(row[campo1]) + "-" + Convert.ToString(row[campo2]));
                coleccion.Add(Convert.ToString(row[campo2]) + "-" + Convert.ToString(row[campo1]));
            }

            cbo_usuario.AutoCompleteCustomSource = coleccion;
            cbo_usuario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbo_usuario.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        // Clase auxiliar para almacenar Value y Display
        public class ComboBoxItemU
        {
            public string Value { get; set; }
            public string Display { get; set; }

            // Sobrescribir el método ToString para mostrar "ID-Nombre" en el ComboBox
            public override string ToString()
            {
                return $"{Value}-{Display}"; // Formato "ID-Nombre"
            }
        }

        private void cbo_usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_usuario.SelectedItem != null)
            {
                
                var selectedItem = (ComboBoxItemU)cbo_usuario.SelectedItem;
                string valorSeleccionado = selectedItem.Value;
                
                MessageBox.Show($"Valor seleccionado: {valorSeleccionado}", "Valor Seleccionado");
            }
        }


// /////////////////////////////////////////////////////////////////////////////////
        public void llenarsePerfil(string tabla, string campo1, string campo2)
        {
            // Obtén los datos para el ComboBox
            var dt2 = logic.enviarPerfiles(tabla, campo1, campo2);

            // Limpia el ComboBox antes de llenarlo
            cbo_perfiles.Items.Clear();

            foreach (DataRow row in dt2.Rows)
            {
                // Agrega el elemento mostrando el formato "ID-Nombre"
                cbo_perfiles.Items.Add(new ComboBoxItemP
                {
                    Value = row[campo1].ToString(),
                    Display = row[campo2].ToString()
                });
            }

            // Configura AutoComplete para el ComboBox con el formato deseado
            AutoCompleteStringCollection coleccion2 = new AutoCompleteStringCollection();
            foreach (DataRow row in dt2.Rows)
            {
                coleccion2.Add(Convert.ToString(row[campo1]) + "-" + Convert.ToString(row[campo2]));
                coleccion2.Add(Convert.ToString(row[campo2]) + "-" + Convert.ToString(row[campo1]));
            }

            cbo_perfiles.AutoCompleteCustomSource = coleccion2;
            cbo_perfiles.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbo_perfiles.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        // Clase auxiliar para almacenar Value y Display
        public class ComboBoxItemP
        {
            public string Value { get; set; }
            public string Display { get; set; }

            // Sobrescribir el método ToString para mostrar "ID-Nombre" en el ComboBox
            public override string ToString()
            {
                return $"{Value}-{Display}"; // Formato "ID-Nombre"
            }
        }

        private void cbo_perfiles_SelectedIndexChanged2(object sender, EventArgs e)
        {
            if (cbo_perfiles.SelectedItem != null)
            {
                // Obtener el valor del ValueMember seleccionado
                var selectedItem = (ComboBoxItemP)cbo_perfiles.SelectedItem;
                string valorSeleccionado = selectedItem.Value;
                // Mostrar el valor en un MessageBox
                MessageBox.Show($"Valor seleccionado: {valorSeleccionado}", "Valor Seleccionado");
            }
        }



        //###################  termina lo que hizo  Karla  Sofia Gómez Tobar #######################

        public frm_asignacion_perfiles()
        {
        }

        private void Frm_asignacion_perfiles_Load(object sender, EventArgs e)
        {
           
        }

        private void Dgv_perfiles_asignados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        void limpieza()
        {
            cbo_usuario.Text = " ";
            cbo_perfiles.Text = " ";
            
            dgv_perfiles_asignados.Columns.Clear();
            

            if (iContadorFila > 0)
            {
                dgv_asignacion_perfiles.Rows.RemoveAt(dgv_asignacion_perfiles.CurrentRow.Index);
                iContadorFila--;
            }
            else
            {
                
            }
        }

        void limpiezaIngreso()
        {
            cbo_usuario.Text = " ";
            cbo_perfiles.Text = " ";
 
        }

        public static int iContadorFila = 0;

        private void Btn_agregar_Click(object sender, EventArgs e)
        {
           
        }

        private void Cbo_usuario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btn_remover_Click(object sender, EventArgs e)
        {

                
        }

        private void Btn_quitar_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_finalizar_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Btn_ayuda_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "C:\\Ayuda_Seguridad\\" + "ayudaAsignacionPerfiles.chm", "Asignacion_Perfil_Usuarios.html");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
          
        }

 
        private void cbo_usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            

            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cbo_usuario.Enabled = true;
            cbo_perfiles.Enabled = true;
            btn_agregar.Enabled = true;
            btn_guardar.Enabled = true;
            btn_cancelar.Enabled = true;
            btn_buscar.Enabled = true;
            btn_remover.Enabled = false;
            groupBox1.Enabled = true;
            limpieza();
        }

        public void actualizardatagriew()
        {
            
            
            try
            {
                // Llamar a la capa lógica para obtener los datos de la asignación (modulo-aplicación)
                DataTable dt = logic.consultaLogicaPerfilesUsuarios();

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Asignar el DataTable al DataGridView
                    dgv_perfiles_asignados.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar los datos: {ex.Message}");
            }
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            
           
        }

        private void btn_ActualizarA_Click(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_agregar_Click_1(object sender, EventArgs e)
        {
            if (cbo_usuario.SelectedItem == null || cbo_perfiles.SelectedItem == null)
            {
                MessageBox.Show("Faltan Datos Por Seleccionar", "Verificación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                bool bUsuario_perfil_existente = false;

                string sUsuario = cbo_usuario.SelectedItem.ToString();
                string sPerfil = cbo_perfiles.SelectedItem.ToString();

                if (iContadorFila == 0)
                {
                    dgv_asignacion_perfiles.Rows.Add(sUsuario, sPerfil);

                    iContadorFila++;

                }
                else
                {
                    foreach (DataGridViewRow Fila in dgv_asignacion_perfiles.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == cbo_usuario.SelectedItem.ToString() && Fila.Cells[1].Value.ToString() == cbo_perfiles.SelectedItem.ToString())
                        {
                            bUsuario_perfil_existente = true;
                        }
                    }

                    if (bUsuario_perfil_existente == true)
                    {
                        MessageBox.Show("Ya existe una relacion del usuario con el perfil");
                    }
                    else
                    {
                        dgv_asignacion_perfiles.Rows.Add(sUsuario, sPerfil);
                        iContadorFila++;
                    }
                    btn_agregar.Enabled = true;
                    btn_remover.Enabled = true;
                    btn_guardar.Enabled = true;
                    btn_buscar.Enabled = false;
                    btn_cancelar.Enabled = false;
                    groupBox1.Enabled = false;
                }

                limpiezaIngreso();
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            // Deshabilitar los ComboBox después de la selección
            cbo_usuario.Enabled = false;
            cbo_perfiles.Enabled = false;

            // Validar si los ComboBox están vacíos
            if (cbo_usuario.SelectedItem == null)
            {
                MessageBox.Show("Falta seleccionar un usuario.");
                btn_agregar.Enabled = false;
                cbo_usuario.Enabled = false;
                cbo_perfiles.Enabled = false;
            }
            else if (cbo_perfiles.SelectedItem == null)
            {
                MessageBox.Show("Falta seleccionar un perfil.");
                btn_agregar.Enabled = false;
                cbo_usuario.Enabled = false;
                cbo_perfiles.Enabled = false;
            }
            else
            {
                // Obtener los valores seleccionados de los ComboBox
                string usuarioSeleccionado = cbo_usuario.SelectedItem.ToString();
                string perfilSeleccionado = cbo_perfiles.SelectedItem.ToString();

                // Insertar en la base de datos utilizando los valores de los ComboBox
                logic.consultaLogicaInsertarPerfilUsuario(usuarioSeleccionado, perfilSeleccionado);

                MessageBox.Show("Asignación de usuario y perfil ingresada correctamente.");
                limpieza();

                // Configuración de botones y controles después de la inserción


                cbo_usuario.Enabled = true;
                cbo_perfiles.Enabled = true;
                btn_agregar.Enabled = true;
                btn_guardar.Enabled = true;
                btn_cancelar.Enabled = true;
                btn_remover.Enabled = true;
                btn_buscar.Enabled = true;
                groupBox1.Enabled = true;
                limpieza();
            }
        }

        private void btn_remover_Click_1(object sender, EventArgs e)
        {
            if (iContadorFila > 0)
            {
                dgv_asignacion_perfiles.Rows.RemoveAt(dgv_asignacion_perfiles.CurrentRow.Index);
                iContadorFila--;
            }
            else
            {
                MessageBox.Show("No hay relaciones que eliminar");
            }
        }

        private void btn_buscar_Click_1(object sender, EventArgs e)
        {
            actualizardatagriew();
            cbo_usuario.Enabled = false;
            cbo_perfiles.Enabled = false;
            btn_agregar.Enabled = false;
            btn_guardar.Enabled = false;
            btn_remover.Enabled = false;
        }

        private void dgv_asignacion_perfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            cbo_usuario.Enabled = true;
            cbo_perfiles.Enabled = true;
            btn_agregar.Enabled = true;
            btn_guardar.Enabled = true;
            btn_cancelar.Enabled = true;
            btn_buscar.Enabled = true;
            btn_remover.Enabled = false;
            groupBox1.Enabled = true;
            limpieza();
        }
    }
}
