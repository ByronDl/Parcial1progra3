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
        //List<Inscripcion> inscripciones = new List<Inscripcion>();
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

    }
}
