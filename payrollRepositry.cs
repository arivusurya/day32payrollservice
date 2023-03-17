using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeePayroleService;
namespace EmployeePayroleServiceRepo{
    public  class EmployeeRepository{

            public static string connectionString = "server=localhost;user=root;password=arivumathi;database=employeepayrool;";
            MySqlConnection connection = new MySqlConnection(connectionString);


                //Display all employees
    public void GetAllEmployees()
    {
        try
        {
            List<Payrole> payroles = new List<Payrole>();
            using (this.connection)
            {
                this.connection.Open();
                
                MySqlCommand command = new MySqlCommand("`employeepayrool`.`getemployee`", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Payrole service = new Payrole();
                        service.EmpId = dr.GetInt32(0);
                        service.EmpName = dr.GetString(1);
                        service.EmpDept = dr.GetString(2);
                        service.EmpSalary = dr.GetInt64(3);
                        payroles.Add(service);
                    }
                }
                foreach (var data in payroles)
                {
                    Console.WriteLine(data.EmpId + "  " + data.EmpName + " " + data.EmpDept + " " + data.EmpSalary);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //To Add Employee details    
    public void AddEmployee(Payrole obj)
    {
        this.connection.Open();
        MySqlCommand com = new MySqlCommand("AddNewEmpDetails", this.connection);

        com.CommandType = System.Data.CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@EmpsName", obj.EmpName);
        com.Parameters.AddWithValue("@EmpsDept", obj.EmpDept);
        com.Parameters.AddWithValue("@EmpsSalary", obj.EmpSalary);
        int i = com.ExecuteNonQuery();
        this.connection.Close();
        if (i >= 1)
            Console.WriteLine("Added successfully.");
        else
            Console.WriteLine("Retry. Emp id : " + obj.EmpId + " is not found ");
    }

    //To Update Employee details    
    public void UpdateEmployee(Payrole obj)
    {
        this.connection.Open();
        MySqlCommand com = new MySqlCommand("UpdateEmpDetails", this.connection);

        com.CommandType = System.Data.CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@EmpsId", obj.EmpId);
        com.Parameters.AddWithValue("@EmpsName", obj.EmpName);
        com.Parameters.AddWithValue("@EmpsDept", obj.EmpDept);
        com.Parameters.AddWithValue("@EmpsSalary", obj.EmpSalary);
        int i = com.ExecuteNonQuery();
        this.connection.Close();
        if (i >= 1)
            Console.WriteLine("Updated successfully.");
        else
            Console.WriteLine("Retry. Emp id : " + obj.EmpId + " is not found ");
    }

    //To delete Employee details    
    public void DeleteEmployee(int Id)
    {
        try
        {
            using (this.connection)
            {
                this.connection.Open();
                MySqlCommand com = new MySqlCommand("DeleteEmpDetails", this.connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmpsId", Id);
                int i = com.ExecuteNonQuery();
                this.connection.Close();
                if (i >= 1)
                    Console.WriteLine("Deleted successfully.");
                else
                    Console.WriteLine("Retry. Emp id : " + Id + " is not found ");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
}
