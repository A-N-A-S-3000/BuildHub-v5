using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace BuildHub_v5.Pages
{
    public class UserProjectsModel : PageModel
    {

        //to show the table
        public void OnGet()
        {
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";
            ViewData["Message"] = "";
            string html = "";
            try
            {
                c.Open();
                SqlCommand q = new SqlCommand();
                q.Connection = c;
                q.CommandText = "Select * from UserProjects";
                SqlDataReader read = q.ExecuteReader();
                html += @"
                        <table class='table table-striped table-bordered table-hover mt-3'>
                            <thead class='table-dark'>
                                <tr>
                                    <th>Id</th>
                                    <th>Location</th>
                                    <th>Status</th>
                                    <th>Floors</th>
                                    <th>Kroki</th>
                                    <th>UserId</th>
                                    <th>Created At</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                        ";

                while (read.Read())
                {
                    html += $@"
                            <tr>
                                <td>{read["Id"]}</td>
                                <td>{read["Location"]}</td>
                                <td>{read["Status"]}</td>
                                <td>{read["Floors"]}</td>
                                <td>{read["KrokiNumber"]}</td>
                                <td>{read["UserId"]}</td>
                                <td>{read["CreatedAt"]}</td>
                                <td>
                                    <a class='btn btn-primary btn-sm' href='/EditUserProject?id={read["Id"]}'>Edit</a>
                                    <a class='btn btn-danger btn-sm' href='/UserProjects?handler=Delete&id={read["Id"]}''>
                                        Delete
                                    </a>
                                </td>
                            </tr>";

                }
                html += "</table>";
                read.Close();
                c.Close();
                ViewData["Message"] = html;
            }
            catch (Exception ee) { ViewData["Message"] = $"Error: {ee.Message}"; }
        }

        public IActionResult OnGetDelete(int id)
        {
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";

            try
            {
                c.Open();

                SqlCommand q = new SqlCommand();
                q.Connection = c;
                q.CommandText = $"DELETE FROM UserProjects WHERE Id = {id}";   // or UserProjects


                q.ExecuteNonQuery();

                c.Close();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error deleting: " + ex.Message;
            }

            // reload the list after delete
            return RedirectToPage("/UserProjects");
        }



    }
}
