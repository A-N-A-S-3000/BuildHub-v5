using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace BuildHub_v5.Pages
{
    public class AddUserProjectModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string Location, string Status, int Floors, string KrokiNumber, int UserId)
        {
            SqlConnection c = new SqlConnection();
            c.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BuildHubDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";
            try
            {
                c.Open();
                SqlCommand q = new SqlCommand(@"
            INSERT INTO UserProjects (Location, Status, Floors, KrokiNumber, UserId, CreatedAt)
            VALUES (@Location, @Status, @Floors, @KrokiNumber, @UserId, GETDATE())"
            , c);
                q.Parameters.AddWithValue("@Location", Location);
                q.Parameters.AddWithValue("@Status", Status);
                q.Parameters.AddWithValue("@Floors", Floors);
                q.Parameters.AddWithValue("@KrokiNumber", KrokiNumber);
                q.Parameters.AddWithValue("@UserId", UserId);
                q.ExecuteNonQuery();
                c.Close();
                return RedirectToPage("UserProjects");
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error: " + ex.Message;
                return Page();
            }
        }
    }
}
