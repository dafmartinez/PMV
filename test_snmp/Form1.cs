using Aesys.Snmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_snmp
{
    public partial class Form1 : Form
    {
        private SnmpManager snmpMgr ;
        public static Int32 num =0 ;
        public Form1()
        {
            InitializeComponent();

        }
        public void escribe_mensaje() {
            snmpMgr = new SnmpManager();
            snmpMgr.SetIpAddrAndCommunity("192.168.26.34", "public");// se logea con el panel
            int num = snmpMgr.GetAsInt("1.3.6.1.4.1.17377.11.2.1.0");
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.5.8.1.9.3.1", (int)6);// BORRA MENSAJE EN MEMORIA
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.5.8.1.3.3.1", "[g140,1,1][fo9][sc1]PRUEBA  1[/sc][nl5][sc1]PRUEBA 2[/sc][nl5][sc1]PRUEBA 3[/sc][nl5][sc1]PRUEBA 4[/sc]"); //CARGAR EL MENSAJE
            label1.Text += snmpMgr.GetAsStr("1.3.6.1.4.1.1206.4.2.3.5.8.1.3.3.1"); // OBTENER EL MENSAJE
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.5.8.1.4.3.1", "PMV"); //EL USUARIO
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.5.8.1.8.3.1", (int)255);//LA PRIORIDAD
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.5.8.1.9.3.1", (int)7);// CONFIRMAR PUBLICACION
            //ORDEN DE SEÑALIZACION
            snmpMgr.Set("1.3.6.1.4.1.1206.4.2.3.6.3.0", NTCIPUtils.EncodeActivationCode((NtcipMessageTypes)Enum.Parse(typeof(NtcipMessageTypes), "Changeable"), (ushort)1, (ushort)snmpMgr.GetAsInt("1.3.6.1.4.1.1206.4.2.3.5.8.1.5.3.1"), (byte)this.snmpMgr.GetAsInt("1.3.6.1.4.1.1206.4.2.3.5.8.1.8.3.1" )));
            label1.Text += num.ToString();
        }
      
            private void button1_Click(object sender, EventArgs e)
        {
        
            escribe_mensaje();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
