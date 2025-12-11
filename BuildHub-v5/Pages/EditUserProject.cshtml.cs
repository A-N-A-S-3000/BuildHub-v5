using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace BuildHub_v5.Pages
{
    public class EditUserProjectModel : PageModel
    {
        public void OnGet(int id)
        {
            ViewData["IdA"] = id;
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";

            try
            {
                c.Open();
                SqlCommand q = new SqlCommand($"SELECT * FROM UserProjects WHERE Id = {id}", c);


                SqlDataReader read = q.ExecuteReader();

                if (read.Read())
                {
                    ViewData["Id"] = read["Id"];
                    ViewData["Location"] = read["Location"];
                    ViewData["Status"] = read["Status"];
                    ViewData["Floors"] = read["Floors"];
                    ViewData["KrokiNumber"] = read["KrokiNumber"];
                    ViewData["UserId"] = read["UserId"];
                }

                read.Close();
                c.Close();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error: " + ex.Message;
            }
        }

        public IActionResult OnPost(int Id, string Location, string Status, int Floors, string KrokiNumber, int UserId)
        {
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";

            try
            {
                c.Open();

                SqlCommand q = new SqlCommand(@"
            UPDATE UserProjects
            SET Location=@Location,
                Status=@Status,
                Floors=@Floors,
                KrokiNumber=@KrokiNumber,
                UserId=@UserId
            WHERE Id=@Id
        ", c);

                q.Parameters.AddWithValue("@Id", Id);
                q.Parameters.AddWithValue("@Location", Location);
                q.Parameters.AddWithValue("@Status", Status);
                q.Parameters.AddWithValue("@Floors", Floors);
                q.Parameters.AddWithValue("@KrokiNumber", KrokiNumber);
                q.Parameters.AddWithValue("@UserId", UserId);

                q.ExecuteNonQuery();
                c.Close();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error updating: " + ex.Message;
            }

            return RedirectToPage("/UserProjects");
        }



    }
}
