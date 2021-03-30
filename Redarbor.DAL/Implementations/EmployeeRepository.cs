using Redarbor.DAL.DataAccessObjects;
using Redarbor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Redarbor.DAL.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ILogger _logger;
        IConfiguration _configuration;
        private string connectionString;
        public EmployeeRepository(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("RedarborAPIDb");

        }

        public List<EmployeeDAO> GetEmployeeList()
        {
            List<EmployeeDAO> employeeList = new List<EmployeeDAO>();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    var command = "Select * from Employee";
                    SqlCommand cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employeeList.Add(MapEmployeeFromDb(reader));
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return employeeList;
        }

        
        public EmployeeDAO GetEmployeeById(int id)
        {
            EmployeeDAO employee = new EmployeeDAO();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Employee " +
                                                    "WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employee = MapEmployeeFromDb(reader);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return employee;
        }

        public EmployeeDAO InsertEmployee(EmployeeDAO employee)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Employee " +
                                                    "(CompanyId, CreatedOn, DeletedOn, Email, Fax, Name, Lastlogin, Password, PortalId, RoleId, StatusId, Telephone, UpdatedOn, Username) output INSERTED.ID " +
                                                    "VALUES (@CompanyId, @CreatedOn, @DeletedOn, @Email, @Fax, @Name, @Lastlogin, @Password, @PortalId, @RoleId, @StatusId, @Telephone, @UpdatedOn, @Username)", con);
                    cmd.Parameters.AddWithValue("@CompanyId", employee.CompanyId);
                    cmd.Parameters.AddWithValue("@CreatedOn", employee.CreatedOn);
                    cmd.Parameters.AddWithValue("@DeletedOn", employee.DeletedOn);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Fax", employee.Fax);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Lastlogin", employee.Lastlogin);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);
                    cmd.Parameters.AddWithValue("@PortalId", employee.PortalId);
                    cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    cmd.Parameters.AddWithValue("@StatusId", employee.StatusId);
                    cmd.Parameters.AddWithValue("@Telephone", employee.Telephone);
                    cmd.Parameters.AddWithValue("@UpdatedOn", employee.UpdatedOn);
                    cmd.Parameters.AddWithValue("@Username", employee.Username);

                    con.Open();
                    var insertedId = (Int32)cmd.ExecuteScalar();
                    con.Close();

                    if (insertedId > 0) employee.Id = insertedId;
                    return employee;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return employee;
            }

            //return employee;
        }

        public void UpdateEmployee(EmployeeDAO employee)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Employee " +
                                                    "SET CompanyId = @CompanyId, CreatedOn = @CreatedOn, DeletedOn = @DeletedOn, Email = @Email, Fax = @Fax, Name = @Name, " +
                                                    "Lastlogin = @Lastlogin, Password = @Password, PortalId = @PortalId, RoleId = @RoleId, StatusId = @StatusId, Telephone = @Telephone, " +
                                                    "UpdatedOn = @UpdatedOn, Username = @Username " +
                                                    "WHERE id = @Id", con);
                    cmd.Parameters.AddWithValue("@CompanyId", employee.CompanyId);
                    cmd.Parameters.AddWithValue("@CreatedOn", employee.CreatedOn);
                    cmd.Parameters.AddWithValue("@DeletedOn", employee.DeletedOn);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Fax", employee.Fax);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Lastlogin", employee.Lastlogin);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);
                    cmd.Parameters.AddWithValue("@PortalId", employee.PortalId);
                    cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    cmd.Parameters.AddWithValue("@StatusId", employee.StatusId);
                    cmd.Parameters.AddWithValue("@Telephone", employee.Telephone);
                    cmd.Parameters.AddWithValue("@UpdatedOn", employee.UpdatedOn);
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Id", employee.Id);

                    con.Open();
                    var result = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public int DeleteEmployee(int id)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Employee " +
                                                    "WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return -1;
            }
        }

        private EmployeeDAO MapEmployeeFromDb(SqlDataReader reader)
        {
            EmployeeDAO employee = new EmployeeDAO();

            employee.Id = Convert.ToInt32(reader["Id"]);
            employee.CompanyId = Convert.ToInt32(reader["CompanyId"]);
            employee.CreatedOn = DateTime.Parse(reader["CreatedOn"].ToString());
            employee.DeletedOn = DateTime.Parse(reader["DeletedOn"].ToString());
            employee.Email = reader["Email"].ToString();
            employee.Fax = reader["Fax"].ToString();
            employee.Name = reader["Name"].ToString();
            employee.Lastlogin = DateTime.Parse(reader["Lastlogin"].ToString());
            employee.Password = reader["Password"].ToString();
            employee.PortalId = Convert.ToInt32(reader["PortalId"]);
            employee.RoleId = Convert.ToInt32(reader["RoleId"]);
            employee.StatusId = Convert.ToInt32(reader["StatusId"]);
            employee.CompanyId = Convert.ToInt32(reader["CompanyId"]);
            employee.Telephone = reader["Telephone"].ToString();
            employee.UpdatedOn = DateTime.Parse(reader["UpdatedOn"].ToString());
            employee.Username = reader["Username"].ToString();

            return employee;
        }

        
    }
}
