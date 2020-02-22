using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication
{
	/// <summary>
	/// Descripción breve de WebService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	// [System.Web.Script.Services.ScriptService]
	public class WebService : System.Web.Services.WebService
	{

		[WebMethod]
		public Result Login(string userName, string userPass)
		{
			SqlConnection conn = new SqlConnection(new DBConnection().ConnectionString);
			Result result = new Result();

			try
			{
				SqlCommand cmd = new SqlCommand("");

				cmd.Parameters.AddWithValue("username", userName);
				cmd.Parameters.AddWithValue("password", userPass);
				cmd.Connection = conn;
				if (conn.State == System.Data.ConnectionState.Closed)
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					result.ValidUser = true;
					return result;
				}
				else
				{
					result.ValidUser = false;
				}
			}

			catch (Exception ex)
			{
				result.Error = ex.ToString();
			}

			finally
			{
				conn.Close();
			}

			return result;
		}
	}
}
