using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Progra3Parcial1by
{
    public partial class Form1 : Form
    {

        List<Doctor> doctores = new List<Doctor>();
        List<Paciente> pacientes = new List<Paciente>();
        List<Cita> citas = new List<Cita>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
            CargarDoctores();
            CargarPacientes();
            LlenarCombos();
        }
        

        void CargarPacientes()
        {
            string ruta = Application.StartupPath + "\\pacientes.txt";

            string[] lineas = File.ReadAllLines(ruta);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                Paciente p = new Paciente
                {
                    DPI = datos[0],
                    Nombre = datos[1],
                    Telefono = datos[2]
                };

                pacientes.Add(p);
            }
        }

        void CargarDoctores()
        {
            string ruta = Application.StartupPath + "\\doctores.txt";

            string[] lineas = File.ReadAllLines(ruta);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                Doctor d = new Doctor
                {
                    ID = datos[0],
                    Nombre = datos[1],
                    Especialidad = datos[2]
                };

                doctores.Add(d);
            }
        }
        void LlenarCombos()
        {
            comboDoctor.DataSource = doctores;
            comboDoctor.DisplayMember = "Nombre";

            comboPaciente.DataSource = pacientes;
            comboPaciente.DisplayMember = "Nombre";
        }
        private void btnGuardarCita_Click(object sender, EventArgs e)
        {
            Doctor doc = (Doctor)comboDoctor.SelectedItem;
            Paciente pac = (Paciente)comboPaciente.SelectedItem;

            DateTime fechaHora = dateFecha.Value.Date + dateHora.Value.TimeOfDay;

            Cita nueva = new Cita
            {
                IDDoctor = doc.ID,
                DPIPaciente = pac.DPI,
                FechaHora = fechaHora
            };

            citas.Add(nueva);

            string ruta = Application.StartupPath + "\\citas.txt";

            File.AppendAllText(ruta,
                nueva.IDDoctor + "," +
                nueva.DPIPaciente + "," +
                nueva.FechaHora +
                Environment.NewLine);

            MessageBox.Show("Cita registrada");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Doctor doctor = (Doctor)comboDoctor.SelectedItem;
            Paciente paciente = (Paciente)comboPaciente.SelectedItem;

            DateTime fechaHora = dateFecha.Value.Date + dateHora.Value.TimeOfDay;

            Cita nueva = new Cita();
            nueva.IDDoctor = doctor.ID;
            nueva.DPIPaciente = paciente.DPI;
            nueva.FechaHora = fechaHora;

            citas.Add(nueva);

            string ruta = Application.StartupPath + "\\citas.txt";

            File.AppendAllText(ruta,
                nueva.IDDoctor + "," +
                nueva.DPIPaciente + "," +
                nueva.FechaHora +
                Environment.NewLine);

            MessageBox.Show("Cita guardada correctamente");
        }

        private void btnMostrarCitas_Click(object sender, EventArgs e)
        {
   
        
            listBoxCitas.Items.Clear();

            foreach (Cita cita in citas)
            {
                Doctor doctor = doctores.Find(d => d.ID == cita.IDDoctor);
                Paciente paciente = pacientes.Find(p => p.DPI == cita.DPIPaciente);

                string texto = doctor.Nombre + " - " +
                               doctor.Especialidad + " - " +
                               paciente.Nombre + " - " +
                               cita.FechaHora.ToString("dd/MM/yyyy HH:mm");

                listBoxCitas.Items.Add(texto);
            }
        }

        private void btnOrdenarFecha_Click(object sender, EventArgs e)
        {
            var citasOrdenadas = citas.OrderBy(c => c.FechaHora).ToList();

            listBoxCitas.Items.Clear();

            foreach (Cita cita in citasOrdenadas)
            {
                Doctor doctor = doctores.Find(d => d.ID == cita.IDDoctor);
                Paciente paciente = pacientes.Find(p => p.DPI == cita.DPIPaciente);

                string texto = doctor.Nombre + " - " +
                               doctor.Especialidad + " - " +
                               paciente.Nombre + " - " +
                               cita.FechaHora.ToString("dd/MM/yyyy HH:mm");

                listBoxCitas.Items.Add(texto);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var citasOrdenadas = citas.OrderBy(c =>
      doctores.Find(d => d.ID == c.IDDoctor).Nombre).ToList();

            listBoxCitas.Items.Clear();

            foreach (Cita cita in citasOrdenadas)
            {
                Doctor doctor = doctores.Find(d => d.ID == cita.IDDoctor);
                Paciente paciente = pacientes.Find(p => p.DPI == cita.DPIPaciente);

                string texto = doctor.Nombre + " - " +
                               doctor.Especialidad + " - " +
                               paciente.Nombre + " - " +
                               cita.FechaHora.ToString("dd/MM/yyyy HH:mm");

                listBoxCitas.Items.Add(texto);
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            int totalCitas = citas.Count;

            string mensaje = "Total de citas registradas: " + totalCitas + "\n\n";

            foreach (Doctor doctor in doctores)
            {
                int cantidad = citas.Count(c => c.IDDoctor == doctor.ID);

                mensaje += doctor.Nombre + " (" + doctor.Especialidad + ") : "
                           + cantidad + " citas\n";
            }

            MessageBox.Show(mensaje);

        }
    }
   }

