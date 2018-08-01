using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Materias
{
    public partial class Form1 : Form
    {
        public ListBox A_Eliminar = new ListBox();
        public ListBox Agregados = new ListBox();
        public string[][] Guia;
        public string[][] Oferta;
        string direccion;

        public Form1()
        {
            direccion = Application.StartupPath;
            if (!File.Exists(direccion + "\\PlanDeEstudio.csv"))
                PlanDeEstudio();
            InitializeComponent();
            Archivonotas();
            CargarNotas(null, null);
            Carga2();
            Carga();
        }

        public void PlanDeEstudio()
        {
            File.WriteAllLines(direccion + "\\PlanDeEstudio.csv", new string[] { "Codigo de materia;Descripcion;Correlativas", "901;INGLES NIVEL I;", "902;INGLES NIVEL II; 0901", "903;INGLES NIVEL III; 0902", "904;INGLES NIVEL IV; 0903", "911;COMPUTACION NIVEL I;", "912;COMPUTACION NIVEL II; 0911", "1023;ANALISIS MATEMATICO I;", "1024;ELEMENTOS DE PROGRAMACION;", "1025;SIST DE REPRESENTACION Y DIBUJO TECNICO;", "1026;TECNOLOGIA INGENIERIA Y SOCIEDAD;", "1027;ALGEBRA Y GEOMETRIA ANALITICA I;", "1028;MATEMATICA DISCRETA;", "1029;QUIMICA GENERAL;", "1030;FUNDAMENTOS DE TIC'S;", "1031;FISICA I; 1023", "1032;ALGEBRA Y GEOMETRIA ANALITICA II; 1027", "1033;ANALISIS MATEMATICO II; 1023", "1035;FISICA II; 1031", "1108;REQUERIMIENTOS PARA LA INGENIERIA; 1030", "1109;ARQUITECTURA DE COMPUTADORAS; 1028 1030", "1110;PROGRAMACION; 1024 1028", "1111;PROBABILIDAD Y ESTADISTICA; 1033", "1112;AUDITORIA Y SEGURIDAD INFORMATICA; 1026 1109", "1113;PROGRAMACION AVANZADA; 1023 1110", "1114;BASE DE DATOS; 1028 1033", "1115;SISTEMAS OPERATIVOS; 1109 1110", "1116;ANALISIS DE SISTEMAS; 1028 1108", "1117;CALCULO NUMERICO; 1032 1033", "1118;INGENIERIA DE REQUERIMIENTOS; 1116", "1119;COMUNICACION DE DATOS; 1109 1115", "1120;DISEÑO DE SISTEMAS; 1116", "1121;ANALISIS DE SOFTWARE; 1113", "1122;REDES DE COMPUTADORAS; 1031 1119", "1123;SISTEMAS OPERATIVOS AVANZADOS; 1113 1115", "1124;LENGUAJES Y COMPILADORES; 1110 1115", "1125;GESTION ORGANIZACIONAL; 1026 1116", "1126;INGENIERIA DE SOFTWARE; 1025 1029 1032 1035 1109 1111 1115 1117 1120", "1127;ELEMENTOS DE INTELIGENCIA ARTIFICIAL; 1029 1032 1035 1111 1123", "1128;ELECTIVA I;", "1129;AUTOMATAS Y LENGUAJES FORMALES; 1025 1029 1032 1035 1111 1117 1122", "1130;ELECTIVA II;", "1131;ELECTIVA III;", "1132;PROYECTO DE FIN DE CARRERA; 1023 1024 1025 1026 1027 1028 1029 1030 1031 1032 1033 1035 1108 1109 1110 1111 1112 1113 1114 1115 1116 1117 1118 1119 1120 1121 1122 1123 1124 1125", "1133;PRACTICA PROFESIONAL SUPERVISADA; 1113 1116", "1187;TECNICAS AVANZADAS EN BASES DE DATOS; 1025 1029 1032 1035 1111 1115 1117 1122", "1189;SISTEMAS DE TRANSMISION Y CONMUTACION; 1025 1029 1032 1035 1111 1115 1117 1122", "1190;LAB.DE TELEINFORMATICA(Electiva I-II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1191;SEGURIDAD EN REDES(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1192;LENGUAJES DESCRIPTIVOS DE HARDWARE; 1025 1029 1032 1035 1111 1115 1117 1122", "1193;PROCESO SOFTWARE(Electiva I-II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1194;DATA MINING Y DATA WAREHOUSE(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1195;PROGRAMACION PAGINAS WEB; 1025 1029 1032 1035 1111 1115 1117 1122", "1196;GEST.DE PROY. INFORMATICOS(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1223;CRIPTOGRAFIA(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "1224;FUND.DE E-COMMERCE(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122", "225;GESTION DE RR HH EN PROY.IT(Electiva I - II - III); 1025 1029 1032 1035 1111 1115 1117 1122" });
        }

        private void Archivonotas()
        {
            string NombreVar = Application.ExecutablePath.Substring(Application.StartupPath.Length + 1);
            Text = NombreVar.Remove(NombreVar.IndexOf("."));
            string variable = Environment.GetEnvironmentVariable(NombreVar);
            if (direccion != variable)
            {
                if (File.Exists(variable + "\\Notas.txt"))
                {
                    File.Move(variable + "\\Notas.txt", direccion + "\\Notas.txt");
                    File.SetAttributes(direccion + "\\Notas.txt", FileAttributes.Hidden);
                }
                if (File.Exists(variable + "\\Cronograma.txt"))
                {
                    File.Move(variable + "\\Cronograma.txt", direccion + "\\Cronograma.txt");
                    File.SetAttributes(direccion + "\\Cronograma.txt", FileAttributes.Hidden);
                }
                Environment.SetEnvironmentVariable(NombreVar, direccion);
            }
        }

        public void Carga2()
        {
            NotasL.Items.Clear();
            MateriasL.Items.Clear();
            string[] Materias = File.ReadAllLines(direccion + "\\PlanDeEstudio.csv",Encoding.UTF7);
            foreach (string line in Materias)
            {
                string[] cad = line.Split(';');
                cad[2] = cad[2].Replace("?", "");
                int correlativas = cad[2].Split(' ').Length - 1;
                if (correlativas != 0)
                {
                    foreach (string aux in cad[2].Split(' '))
                    {
                        foreach (string temp in NotasM.Items)
                            if (correlativas != 0 && ((temp.Remove(4).Last() == ' ') ? "0" + temp.Remove(3) : temp.Remove(4)) == aux)
                            {
                                correlativas--;
                            }
                    }
                }
                if (correlativas == 0 && !NotasM.Items.Contains(cad[0] + "  " + cad[1]) && !cad[1].Contains("ELECTI") && !cad[0].Contains("materia"))
                {
                    MateriasL.Items.Add(cad[0] + "  " + cad[1]);
                    NotasL.Items.Add(cad[0] + "  " + cad[1]);
                }
            }
            string dirofe = direccion + "\\Oferta.csv";
            if (File.Exists(dirofe))
            {
                Oferta = new string[File.ReadAllLines(dirofe).Length][];
                string temp = "", temp2 = "";
                int i = 0;
                foreach (string line in File.ReadAllLines(dirofe))
                {
                    string[] cad = line.Replace("�", "").Split(';');
                    if (cad[0] == "")
                    {
                        cad[0] = temp;
                        cad[1] = temp2;
                    }
                    else
                    {
                        temp = cad[0];
                        temp2 = cad[1];
                    }
                    Oferta[i++] = cad;
                }
            }
        }

        public void Carga()
        {
            string[] prof = (new WebClient() { Encoding = Encoding.UTF8 }).DownloadString("https://docs.google.com/spreadsheets/d/1-klLxNq9CvPH4CQbopeWbNJaviMA083FaomM_dChNI4/export?format=csv&id=1-klLxNq9CvPH4CQbopeWbNJaviMA083FaomM_dChNI4&gid=0").Split((new string[] { "\r\n" }), StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            Guia = new string[prof.Length][];
            foreach (string line in prof)
            {
                Guia[i] = line.Split(new Char[] { ',' }, 7);
                if (Guia[i][1].Length > 0)
                {
                    if (i > 1)
                    {
                        //MateriasL.Items.Add(Guia[i][0] + "  " + Guia[i][1]);
                        //NotasL.Items.Add(Guia[i][0] + "  " + Guia[i][1]);
                    }
                }
                else
                {
                    Guia[i][0] = Guia[i - 1][0];
                    Guia[i][1] = Guia[i - 1][1];
                }

                i++;
            }
            if (!File.Exists(direccion + "\\Oferta.csv"))
                Oferta = Guia;
            return;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Carga();
        }

        private void MateriasL_SelectedValueChanged(object sender, EventArgs e)
        {
            ComicionesL.Items.Clear();
            Recomendacion.Text = "";
            foreach (string[] line in Oferta)
            {
                if (line[0] + "  " + line[1] == MateriasL.SelectedItem.ToString())
                {
                    string Comision = line[2] + " " + line[3] + " " + line[4];
                    string reco = RecoT(Comision);
                    ComicionesL.Items.Add(Comision + " " + reco);
                }
            }
        }

        public string RecoT(string text)
        {
            foreach (string[] line in Guia)
            {
                if (line[0] + "  " + line[1] == MateriasL.SelectedItem.ToString() && text == line[2] + " " + line[3] + " " + line[4])
                {
                    return line[5];
                }
            }
            return "";
        }

        private void ComicionesL_SelectedValueChanged(object sender, EventArgs e)
        {
            if (MateriasL.SelectedItem != null && ComicionesL.SelectedItem != null)
                foreach (string[] line in Guia)
                    if (line[0] + "  " + line[1] == MateriasL.SelectedItem.ToString() && line[2] + " " + line[3] + " " + line[4] + " " + line[5] == ComicionesL.SelectedItem.ToString())
                    {
                        line[6] = line[6].Replace(",", "");
                        line[6] = line[6].Replace("\"", "");
                        Recomendacion.Text = line[6];
                        return;
                    }
            Recomendacion.Text = "";
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            if (MateriasL.SelectedItems.Count == 0 || ComicionesL.SelectedItems.Count == 0)
                return;
            A_Eliminar.Items.Clear();
            bool ocupado = false;
            bool repetido = false;
            bool errorextra= false;
            string ocu = "";
            string[] line = (MateriasL.SelectedItem.ToString() + "  ads  " + ComicionesL.SelectedItem.ToString().Split(' ')[1] + "  " + ComicionesL.SelectedItem.ToString().Split(' ')[2]).Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            Comprobar(line, ref ocupado, ref ocu, ref repetido,ref errorextra);
            if (repetido && MessageBox.Show("La materia esta repetida. Desea reemplazar la comision?", "Materia repetida!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Eliminar();
                repetido = false;
            }
            if (ocupado && MessageBox.Show("Los dias " + ocu + "estan ocupados. Desea Reemplazarlo?", "Dia ocupado!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Eliminar();
                ocupado = false;
            }

            if (!ocupado && !repetido && !errorextra)
            {
                ABM(line[3], line[4], line[0] + "  " + line[1] + "  " + ComicionesL.SelectedItem.ToString().Remove(4));
            }
        }


        private void Eliminar()
        {
            foreach (string materia in A_Eliminar.Items)
            {
                foreach (string[] temp in Oferta)
                {
                    if (temp[0] + "  " + temp[1] + "  " + temp[2] == materia)
                    {
                        Agregados.Items.Remove(materia);
                        ABM(temp[3], temp[4], "");
                    }
                }
            }
        }

        private void ABM(string turno, string dia, string valor)
        {
            if (valor.Length > 0)
                Agregados.Items.Add(valor);
            if (turno.Contains("M"))
            {
                if (dia.Contains("Lu"))
                    LunesM.Text = valor;
                if (dia.Contains("Ma"))
                    MartesM.Text = valor;
                if (dia.Contains("Mi"))
                    MiercolesM.Text = valor;
                if (dia.Contains("Ju"))
                    JuevesM.Text = valor;
                if (dia.Contains("Vi"))
                    ViernesM.Text = valor;
                if (dia.Contains("Sa"))
                    SabadoM.Text = valor;
            }
            else if (turno.Contains("T"))
            {
                if (dia.Contains("Lu"))
                    LunesT.Text = valor;
                if (dia.Contains("Ma"))
                    MartesT.Text = valor;
                if (dia.Contains("Mi"))
                    MiercolesT.Text = valor;
                if (dia.Contains("Ju"))
                    JuevesT.Text = valor;
                if (dia.Contains("Vi"))
                    ViernesT.Text = valor;
                if (dia.Contains("Sa"))
                    SabadoT.Text = valor;
            }
            else if (turno.Contains("N"))
            {
                if (dia.Contains("Lu"))
                    LunesN.Text = valor;
                if (dia.Contains("Ma"))
                    MartesN.Text = valor;
                if (dia.Contains("Mi"))
                    MiercolesN.Text = valor;
                if (dia.Contains("Ju"))
                    JuevesN.Text = valor;
                if (dia.Contains("Vi"))
                    ViernesN.Text = valor;
                if (dia.Contains("Sa"))
                    SabadoN.Text = valor;
            }
        }

        private void Comprobar(string[] line, ref bool ocupado, ref string ocu, ref bool repetido,ref bool error)
        {
            if(line[4].Length<5 || line[3].Length==0)
            {
                error = true;
                MessageBox.Show("La materia no tiene dias o turno...");
                return;
            }
            foreach (string asd in Agregados.Items)
                if (asd.Contains(line[0] + "  " + line[1]))
                {
                    repetido = true;
                    A_Eliminar.Items.Add(asd);
                }
            if (line[3].Contains("M"))
            {
                if (line[4].Contains("Lu") && LunesM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(LunesM.Text); ocu += "Lunes "; }
                if (line[4].Contains("Ma") && MartesM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MartesM.Text); ocu += "Martes "; }
                if (line[4].Contains("Mi") && MiercolesM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MiercolesM.Text); ocu += "Miercoles "; }
                if (line[4].Contains("Ju") && JuevesM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(JuevesM.Text); ocu += "Jueves "; }
                if (line[4].Contains("Vi") && ViernesM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(ViernesM.Text); ocu += "Viernes "; }
                if (line[4].Contains("Sa") && SabadoM.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(SabadoM.Text); ocu += "Sabado "; }
            }
            if (line[3].Contains("T"))
            {
                if (line[4].Contains("Lu") && LunesT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(LunesT.Text); ocu += "Lunes "; }
                if (line[4].Contains("Ma") && MartesT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MartesT.Text); ocu += "Martes "; }
                if (line[4].Contains("Mi") && MiercolesT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MiercolesT.Text); ocu += "Miercoles "; }
                if (line[4].Contains("Ju") && JuevesT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(JuevesT.Text); ocu += "Jueves "; }
                if (line[4].Contains("Vi") && ViernesT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(ViernesT.Text); ocu += "Viernes "; }
                if (line[4].Contains("Sa") && SabadoT.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(SabadoT.Text); ocu += "Sabado "; }
            }
            if (line[3].Contains("N"))
            {
                if (line[4].Contains("Lu") && LunesN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(LunesN.Text); ocu += "Lunes "; }
                if (line[4].Contains("Ma") && MartesN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MartesN.Text); ocu += "Martes "; }
                if (line[4].Contains("Mi") && MiercolesN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(MiercolesN.Text); ocu += "Miercoles "; }
                if (line[4].Contains("Ju") && JuevesN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(JuevesN.Text); ocu += "Jueves "; }
                if (line[4].Contains("Vi") && ViernesN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(ViernesN.Text); ocu += "Viernes "; }
                if (line[4].Contains("Sa") && SabadoN.Text.Length > 0) { ocupado = true; A_Eliminar.Items.Add(SabadoN.Text); ocu += "Sabado "; }
            }
        }

        private void Reiniciar(object sender, EventArgs e)
        {
            Agregados.Items.Clear();
            LunesM.Text = "";
            LunesT.Text = "";
            LunesN.Text = "";
            MartesM.Text = "";
            MartesT.Text = "";
            MartesN.Text = "";
            MiercolesM.Text = "";
            MiercolesT.Text = "";
            MiercolesN.Text = "";
            JuevesM.Text = "";
            JuevesT.Text = "";
            JuevesN.Text = "";
            ViernesM.Text = "";
            ViernesT.Text = "";
            ViernesN.Text = "";
            SabadoM.Text = "";
            SabadoT.Text = "";
            SabadoN.Text = "";
        }

        private void Agregar_notas_B(object sender, EventArgs e)
        {
            if (NotasV.Text == "")
                return;
            ComicionesL.Items.Clear();
            string cad = "";
            if (NotasL.SelectedItem != null)
                cad = NotasL.SelectedItem.ToString();
            else
                cad = NotasL.Items[0].ToString();
            string Nota = NotasV.Text;
            Agregar_notas(cad, Nota);
            NotasV.Text = "";
            Carga2();
        }

        private void Agregar_notas(string cad, string Nota)
        {
            int num = 0;
            if (int.TryParse(Nota, out num))
                if (num < 11 && num > 3)
                {
                    NotasM.Items.Add(cad);
                    NotasN.Items.Add(Nota);
                    float promedio = 0;
                    foreach (string valor in NotasN.Items)
                        promedio += int.Parse(valor);
                    promedio /= NotasN.Items.Count;
                    EtiquetaN.Text = "Con un promedio de: " + promedio;
                    EtiquetaM.Text = "LLevas " + NotasM.Items.Count + " Materias";
                }
                else
                    MessageBox.Show("Nota fuera del rango... (4-10");
            if (NotasL.Items.Count > 0)
                NotasL.SetSelected(0, true);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string[] cad = new string[Agregados.Items.Count];
            int i = 0;
            foreach (string line in Agregados.Items)
                cad[i++] = line;

            File.Delete(direccion + "\\Cronograma.txt");
            File.WriteAllLines(direccion + "\\Cronograma.txt", cad);
            File.SetAttributes(direccion + "\\Cronograma.txt", FileAttributes.Hidden);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (File.Exists(direccion + "\\Cronograma.txt"))
            {
                Reiniciar(null, null);
                foreach (string line in File.ReadAllLines(direccion + "\\Cronograma.txt"))
                {
                    foreach (string[] cad in Guia)
                        if (cad[0] + "  " + cad[1] + "  " + cad[2] == line)
                            ABM(cad[3], cad[4], line);
                }
            }
        }

        private void ReiniciarNotas(object sender, EventArgs e)
        {
            EtiquetaN.Text = "";
            EtiquetaM.Text = "";
            NotasM.Items.Clear();
            ComicionesL.Items.Clear();
            NotasN.Items.Clear();
            Carga2();
        }

        private void CargarNotas(object sender, EventArgs e)
        {
            if (File.Exists(direccion + "\\Notas.txt"))
            {
                ComicionesL.Items.Clear();
                NotasN.Items.Clear();
                NotasM.Items.Clear();
                foreach (string line in File.ReadAllLines(direccion + "\\Notas.txt"))
                {
                    string[] cad = line.Split(',');
                    Agregar_notas(cad[0], cad[1]);
                }
                Carga2();
            }
        }

        private void GuardarNotas(object sender, EventArgs e)
        {
            string[] cad = new string[NotasM.Items.Count];
            int i = 0;
            foreach (string line in NotasM.Items)
            {
                cad[i] = line + "," + NotasN.Items[i].ToString();
                i++;
            }
            File.Delete(direccion + "\\Notas.txt");
            File.WriteAllLines(direccion + "\\Notas.txt", cad);
            File.SetAttributes(direccion + "\\Notas.txt", FileAttributes.Hidden);
        }

        private void NotasV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Agregar_notas_B(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            int este = 0;
            if (IngresodeMateria.Focused && IngresodeMateria.Text.Length > 0)
            {
                foreach (string line in NotasL.Items)
                {
                    if (line.ToUpper().Contains(IngresodeMateria.Text.ToUpper()))
                        este = i;
                    i++;
                }
                NotasL.SetSelected(este, true);
            }
        }

        private void NotasL_SelectedValueChanged(object sender, EventArgs e)
        {
            if (NotasL.Focused)
                IngresodeMateria.Text = NotasL.SelectedItem.ToString();
            if (NotasL.SelectedItems.Count == 0)
                NotasL.SetSelected(0, true);
        }
    }
}
