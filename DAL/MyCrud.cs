﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public class MyCrud
    {
        public static async Task<string> CRUD(string StoreProcedure, params SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(StoreProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);

                        // Add the output parameter
                        var outputParameter = new SqlParameter("@outputMessage", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParameter);

                        await cmd.ExecuteNonQueryAsync();
                        await con.CloseAsync();

                        // Get the value of the output parameter
                        
                       string outputMessage = outputParameter.Value != DBNull.Value ? outputParameter.Value.ToString() : null;
                        Console.WriteLine($"{ outputMessage} CRUD");
                        if(outputMessage != null )
                        {
                            Console.WriteLine($"OUT MESSAGE{outputMessage}");

                            return outputMessage.ToString(); // WIll return UserName already exist
                        }
                        else
                        {
                            return "NO OUTPUT REQUIRED";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as needed
                Console.WriteLine(ex.Message,"12313");
                return null;
            }
        }
    }
}
