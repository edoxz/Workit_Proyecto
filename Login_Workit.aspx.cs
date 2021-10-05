using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace Workit_Proyecto
{
    public partial class Login_Workit : System.Web.UI.Page
    {
        //string oradb = "Data Source = xe ; User ID = workit ; password = 1234;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = 1234 ; USER ID = workit");
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SELECT * FROM USUARIO WHERE NOMBRE_USUARIO = :usuario AND PASSWORD_USUARIO = :password", conexion);

            comando.Parameters.Add(":usuario", txtUsuario.Text);
            comando.Parameters.Add(":password", txtPass.Text);

            OracleDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                if (lector["ROLES_ROL_ID"].ToString()=="1") //admin
                {
                    Server.Transfer("InicioAdmin.aspx");
                    conexion.Close();
                }
                if (lector["ROLES_ROL_ID"].ToString() == "2") //funcionario
                {
                    Server.Transfer("InicioFuncionario.aspx");
                    conexion.Close();
                }
            }
            else
            {
                lblError.Text = "Error de Usuario o Contraseña";
            }
                
                
        }
    }
}