using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SDS_Dev.Models;


namespace SDS_Dev.Repository
{
    public class RecyclableItemRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["SDSConnectionString"].ToString();

        //Get all Recyclably Items
        public List<RecyclableItem> GetAllRecyclableItems()
        {
            List<RecyclableItem> recList = new List<RecyclableItem>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllRecyclableItems";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtRecyclableItems = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRecyclableItems);
                connection.Close();

                foreach (DataRow dr in dtRecyclableItems.Rows)
                {
                    recList.Add(new RecyclableItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        RecyclableTypeId = Convert.ToInt32(dr["RecyclableTypeId"]),
                        Weight = Convert.ToDecimal(dr["Weight"]),
                        ComputedRate = Convert.ToDecimal(dr["ComputedRate"]),
                        ItemDescription = Convert.ToString(dr["ItemDescription"]),
                    });
                }
            }
            return recList;
        }

        //Insert Recyclable Item
        public bool InsertRecyclableItem(RecyclableItem recyclableItem)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertRecyclableItem", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RecyclableTypeId", recyclableItem.RecyclableTypeId);
                command.Parameters.AddWithValue("@Weight", recyclableItem.Weight);
                command.Parameters.AddWithValue("@ComputedRate", recyclableItem.ComputedRate);
                command.Parameters.AddWithValue("@ItemDescription", recyclableItem.ItemDescription);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Recyclable Item by Id
        public List<RecyclableItem> GetRecyclableItemById(int id)
        {
            List<RecyclableItem> recList = new List<RecyclableItem>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetRecyclableItemById";
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtRecyclableItems = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRecyclableItems);
                connection.Close();

                foreach (DataRow dr in dtRecyclableItems.Rows)
                {
                    recList.Add(new RecyclableItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        RecyclableTypeId = Convert.ToInt32(dr["RecyclableTypeId"]),
                        Weight = Convert.ToDecimal(dr["Weight"]),
                        ComputedRate = Convert.ToDecimal(dr["ComputedRate"]),
                        ItemDescription = Convert.ToString(dr["ItemDescription"])
                    });
                }
            }
            return recList;
        }

        //Update Recyclable Item
        public bool UpdateRecyclableItem(RecyclableItem recyclableItem)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateRecyclableItem", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", recyclableItem.Id);
                command.Parameters.AddWithValue("@RecyclableTypeId", recyclableItem.RecyclableTypeId);
                command.Parameters.AddWithValue("@Weight", recyclableItem.Weight);
                command.Parameters.AddWithValue("@ComputedRate", recyclableItem.ComputedRate);
                command.Parameters.AddWithValue("@ItemDescription", recyclableItem.ItemDescription);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Recyclable Item
        public string DeleteRecyclableItem(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteRecyclableItem", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@ReturnMessage"].Value.ToString();
                connection.Close();
            }


            return result;
        }


    }
}