﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Launcher

{

    public partial class Main : Form
    {
        private Thread TStatus_Server;
        private Thread TAtt;

        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd,

        int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]

        public static extern bool ReleaseCapture();
        public Main()
        {
            InitializeComponent();
            Globals.pForm = this;

        }

        
        private void Status_server()
        {
          
                try
                {
                    lbstatus.Text = "Login server: " + Funções.Online("66.70.203.161", 44405) + " | GameServer: " + Funções.Online("66.70.203.161", 55300);
                }catch
                {
                    lbstatus.Text = "Login server: Falha | GameServer: Falha";
                }
         
        }
        private void Attualizar()
        {
          
                try
                {
                    Networking.CheckNetwork();
                    
                }
                catch
                {
                    Status.Text = "Falha de atualização"; 
                }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            Attualizar();
            Status_server();
            navegador.Navigate(Globals.sitelauncher);
            if (Common.IsGameRunning()) { Common.EnableStart(); }
            
            /*   if (Globals.logado)
               {
                   btconta.Text = "Bem Vindo " + Globals.login;
               }
               */
        }

        private void btsair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btregistro_Click(object sender, EventArgs e)
        {
            Globals.pForm.Hide();
            Registro reg = new Registro();
            reg.Show();
        }

        private void btjogar_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.jogo.StartInfo.FileName = Globals.BinaryName;
                Globals.jogo.StartInfo.WorkingDirectory = Globals.directory;
                Globals.jogo.StartInfo.Arguments = Globals.startparam;
                Globals.jogo.Start();
                
             
                //System.Diagnostics.Process.Start(Globals.BinaryName, Globals.startparam);
            }
            catch 
            {
                MessageBox.Show("Erro ao inicar o jogo.","Erro!");
            }
        }

        private void btconta_Click(object sender, EventArgs e)
        {
            if (!Globals.logado)
            {
                Acc gerenciar = new Acc();
                gerenciar.ShowDialog();
            }
            else
            {
                MinhaConta my = new MinhaConta();
                my.ShowDialog();
            }
        }

        private void btopcoes_Click(object sender, EventArgs e)
        {
            Opcoes opt = new Opcoes();
            opt.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)

            {

                ReleaseCapture();

                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ON_Close(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.TStatus_Server.Abort();
                this.TAtt.Abort();
                Globals.jogo.Kill();

            }
            catch
            {

            }
        }

        private void onfocus(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(246,66,66);
        }
        private void ondefocus(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(246, 46, 46);
        }

    }

}
