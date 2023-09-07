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
    public class RecyclableTypeRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["SDSConnectionString"].ToString();

        //Get All Recyclable Types
        public List<RecyclableType> GetAllRecyclableTypes()
        {
            List<RecyclableType> recList = new List<RecyclableType>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllRecyclableTypes";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtRecyclableTypes = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRecyclableTypes);
                connection.Close();

                foreach (DataRow dr in dtRecyclableTypes.Rows)
                {
                    recList.Add(new RecyclableType
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Type = Convert.ToString(dr["Type"]),
                        Rate = Convert.ToDecimal(dr["Rate"]),
                        MinKg = Convert.ToDecimal(dr["MinKg"]),
                        MaxKg = Convert.ToDecimal(dr["MaxKg"])
                    });
                }
            }
            return recList;
        }

        //Insert Recyclable Type
        public bool InsertRecyclableType(RecyclableType recyclableType)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertRecyclableType", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Type", recyclableType.Type);
                command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

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

        //Get Recyclable Type by Id
        public List<RecyclableType> GetRecyclableTypeById(int id)
        {
            List<RecyclableType> recList = new List<RecyclableType>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetRecyclableTypeById";
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtRecyclableTypes = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRecyclableTypes);
                connection.Close();

                foreach (DataRow dr in dtRecyclableTypes.Rows)
                {
                    recList.Add(new RecyclableType
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Type = Convert.ToString(dr["Type"]),
                        Rate = Convert.ToDecimal(dr["Rate"]),
                        MinKg = Convert.ToDecimal(dr["MinKg"]),
                        MaxKg = Convert.ToDecimal(dr["MaxKg"])
                    });
                }
            }
            return recList;
        }

        //Update Recyclable Type
        public bool UpdateRecyclableType(RecyclableType recyclableType)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateRecyclableType", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", recyclableType.Id);
                command.Parameters.AddWithValue("@Type", recyclableType.Type);
                command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

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

        //Delete Recyclable Type
        public string DeleteRecyclableType(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteRecyclableType", connection);
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

        // Get all recyclable types options
        public List<RecyclableTypeOptions> GetAllRecyclableTypesOptions()
        {
            List<RecyclableTypeOptions> recList = new List<RecyclableTypeOptions>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetOptionsOfRecyclableTypes";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtRecyclableTypeOptions = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRecyclableTypeOptions);
                connection.Close();

                foreach (DataRow dr in dtRecyclableTypeOptions.Rows)
                {
                    recList.Add(new RecyclableTypeOptions
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Type = Convert.ToString(dr["Type"]),
                        Rate = Convert.ToDecimal(dr["Rate"])
                    });
                }
            }
            return recList;
        }
    }

}