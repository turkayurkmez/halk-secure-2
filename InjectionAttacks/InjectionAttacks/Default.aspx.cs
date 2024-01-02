using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=Northwind;Integrated Security=True");

        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Employees WHERE FirstName=@name AND LastName=@pass", sqlConnection);

        sqlCommand.Parameters.AddWithValue("@name", TextBoxUserName.Text);
        sqlCommand.Parameters.AddWithValue("@pass", TextBoxPassword.Text);

        sqlConnection.Open();
        var reader = sqlCommand.ExecuteReader();
        if (reader.Read())
        {
            LabelResult.Text = "Giriş Başarılı";
        }
        else
        {
            LabelResult.Text = "Giriş Başarısız";
        }

        sqlConnection.Close();

    }
}