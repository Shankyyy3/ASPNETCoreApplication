
    using Microsoft.AspNetCore.Mvc;
    using System.Data.SqlTypes;
    using System.Data.SqlClient;
using System.Globalization;

namespace ASPNETCoreApplication.Models
{
    public class DAL
    {



        public Response AddAssetMasters(SqlConnection connection, AssetMaster model)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("Insert into AssetMaster(itemtype,item,subitem,model,serialno,brand,pono,warrantydate,isActive)" +
                "output INSERTED.assetid Values(@itemType, @itemName, @subItemName, @model, @serialno, @brand, @pono, @warrantydate,@isActive)", connection);

            cmd.Parameters.AddWithValue("@itemType", model.itemType);
            cmd.Parameters.AddWithValue("@itemName", model.itemName);
            cmd.Parameters.AddWithValue("@subItemName", model.subItemName);
            cmd.Parameters.AddWithValue("@model", model.model);
            cmd.Parameters.AddWithValue("@serialno", model.serialno);
            cmd.Parameters.AddWithValue("@brand", model.brand);
            cmd.Parameters.AddWithValue("@pono", model.pono);

            // Convert the warrantydate to string in "dd-MM-yyyy" format
            //string warrantyDateFormatted = model.warrantydate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string warrantyDateFormatted =Convert.ToDateTime(model.warrantydate).ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@warrantydate", warrantyDateFormatted);


            cmd.Parameters.AddWithValue("@isActive", model.isActive);
           



            connection.Open();
            int assetid = (int)cmd.ExecuteScalar();
            //int i = cmd.ExecuteNonQuery();
            connection.Close();



            if (assetid > 0)
            {
                response.StatusCode = 200;
                response.ErrorMessage = "Employee added";
                response.assetid = assetid;
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data inserted";
            }
            return response;
        }

    }

}
    
