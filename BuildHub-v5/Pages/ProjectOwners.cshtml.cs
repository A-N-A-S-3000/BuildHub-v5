using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace BuildHub_v5.Pages
{
    public class ProjectOwnersModel : PageModel
    {
        public void OnGet()
        {
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";

            string html = "";
            ViewData["Message"] = "";

            try
            {
                c.Open();

                SqlCommand q = new SqlCommand();
                q.Connection = c;

                q.CommandText = "SELECT * FROM ProjectOwners";

                SqlDataReader read = q.ExecuteReader();

                html += @"
                        <table class='table table-striped table-bordered table-hover mt-3'>
                            <thead class='table-dark'>
                                <tr>
                                    <th>Project Id</th>
                                    <th>Location</th>
                                    <th>Status</th>
                                    <th>Floors</th>
                                    <th>Kroki</th>
                                    <th>Project Created</th>
                                    <th>Owner Id</th>
                                    <th>Owner Email</th>
                                    <th>User Type</th>
                                    <th>User Created</th>
                                </tr>
                            </thead>
                            <tbody>
                        ";

                while (read.Read())
                {
                    html += $@"
                            <tr>
                                <td>{read["ProjectId"]}</td>
                                <td>{read["Location"]}</td>
                                <td>{read["Status"]}</td>
                                <td>{read["Floors"]}</td>
                                <td>{read["KrokiNumber"]}</td>
                                <td>{read["ProjectCreatedAt"]}</td>
                                <td>{read["OwnerId"]}</td>
                                <td>{read["OwnerEmail"]}</td>
                                <td>{read["UserType"]}</td>
                                <td>{read["UserCreatedAt"]}</td>
                            </tr>";
                }

                html += @"
                            </tbody>
                        </table>";

                read.Close();
                c.Close();

                ViewData["Message"] = html;
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error: " + ex.Message;
            }
        }
    }
}
