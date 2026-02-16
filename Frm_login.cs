using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoCSharp_BaseDados
{
    public partial class Frm_login : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;

        public Frm_login()
        {
            InitializeComponent();
            string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ToString();
            con = new MySqlConnection(minhaConString);
        }


        private void btn_login_Click(object sender, EventArgs e)
        {

            try
            {

                string user = txt_utilizador.Text;
                string pwd = txt_senha.Text;

                string query = "Select * from login where lg_nome= @user and lg_pass = @pass";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pwd);

                        con.Open();

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Login efetuado com sucesso!\n" + "Bom vindo " +dr[1],
                                    "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //GerirForms.TrocarForms(this, new Frm_crud());
                            }
                            else
                            {
                                MessageBox.Show("Utilizador ou senha inválidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Ocorreu um erro! Tente mais tarde!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*catch(MySqlException ex) when (exNumber==0)  //nao foi possivel ligar ao servidor
            {
                MessageBox.Show("Não foi possivel ligar ao servidor");
            }*/
        }
    }
}


            
            