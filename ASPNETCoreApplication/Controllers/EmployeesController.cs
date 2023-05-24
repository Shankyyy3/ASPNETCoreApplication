using ASPNETCoreApplication.Models;
using ASPNETCoreApplication.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;



namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly IFileService _uploadService;


        public EmployeesController(IConfiguration configuration, IFileService uploadService)
        {
            _configuration = configuration;
            _uploadService = uploadService;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> employeeList = new List<Employee>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    employee.email = Convert.ToString(dt.Rows[i]["email"]);
                    employee.ePassword = Convert.ToString(dt.Rows[i]["ePassword"]);
                    employeeList.Add(employee);
                }
            }
            if (employeeList.Count > 0)
                return JsonConvert.SerializeObject(employeeList);
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }
        //[EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [Route("GetByEmail/{email}")]
        public string GetEmployees(string email)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
            string query = "SELECT * FROM Employee";
            if (!string.IsNullOrEmpty(email))
            {
                query += $" WHERE email='{email}'";
            }
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> employeeList = new List<Employee>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    employee.email = Convert.ToString(dt.Rows[i]["email"]);
                    employee.ePassword = Convert.ToString(dt.Rows[i]["ePassword"]);
                    employeeList.Add(employee);
                }
            }
            if (employeeList.Count > 0)
                return JsonConvert.SerializeObject(employeeList);
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }
        [HttpPost]
        [Route("AssetMasterForm")]
        public string AssetMaster([FromQuery] int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
            SqlCommand cmd = new SqlCommand("Insert into AssetMaster(assetid) Values(" + id + ")", con);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
                return "Record inserted with value as " + id;
            else
                return "Not Inserted. Try again!";
        }





        [HttpPost]
        [Route("AddAssetMasters")]
        public async Task<IActionResult> AddAssetMasters([FromForm] AssetMaster model)
        {
            if (model.FileDetails == null)
            {
                return BadRequest();
            }
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());

            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddAssetMasters(con, model);
            if (response.assetid > 0)
                await _uploadService.PostFileAsync(model.FileDetails, response.assetid);

            return Ok();
        }
        [HttpGet]
        [Route("AddData")]
        public string GetAllData()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AssetMaster", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<AssetMaster> assetmasterList = new List<AssetMaster>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AssetMaster assetmaster = new AssetMaster();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["assetid"].ToString()))
                        assetmaster.assetid = Convert.ToInt32(dt.Rows[i]["assetid"]);
                    assetmaster.itemType = Convert.ToString(dt.Rows[i]["itemtype"]);
                    assetmaster.itemName = Convert.ToString(dt.Rows[i]["item"]);
                    assetmaster.subItemName = Convert.ToString(dt.Rows[i]["subitem"]);
                    assetmaster.model = Convert.ToString(dt.Rows[i]["model"]);
                    assetmaster.serialno = Convert.ToString(dt.Rows[i]["serialno"]);
                    assetmaster.brand = Convert.ToString(dt.Rows[i]["brand"]);
                    assetmaster.pono = Convert.ToString(dt.Rows[i]["pono"]);
                    // assetmaster.warrantydate= Convert.ToShortDateString(dt.Rows[i]["warrantydate"]);
                    // DateOnly warrantydate = (DateOnly)dt.Rows[i]["warrantydate"];
                    //assetmaster.warrantydate = DateOnly.Parse(warrantydate.ToString("yyyy-MM-dd")); // Change the format specifier to your desired date format
                    if (dt.Rows[i]["warrantydate"] != DBNull.Value)
                    {
                        DateTime warrantyDate = (DateTime)dt.Rows[i]["warrantydate"];
                        assetmaster.warrantydate = DateOnly.Parse(warrantyDate.ToString("yyyy-MM-dd")); // Change the format specifier to your desired date format
                    }
                    if (dt.Rows[i]["isActive"] != DBNull.Value) // Check for DBNull value
                    {
                        assetmaster.isActive = Convert.ToBoolean(dt.Rows[i]["isActive"]);
                    }
                    assetmasterList.Add(assetmaster);
                }
            }
            if (assetmasterList.Count > 0)
                return JsonConvert.SerializeObject(assetmasterList);
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }






        [HttpPost]
        [Route("AddAssetWitFile")]
        public async Task<IActionResult> AddAssetWitFile([FromForm] AssetMaster model)
        {
            if (model.FileDetails == null)
            {
                return BadRequest();
            }
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());

            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddAssetMasters(con, model);
            if (response.assetid > 0)
                await _uploadService.PostFileAsync(model.FileDetails, response.assetid);

            return Ok();
        }
    }
}
