using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class StudentContext
    {
        string connectionString = "Data Source=(LocalDB)\\.;Initial Catalog=STUDENTDB;Persist Security Info=False;User ID=;password=;";
       //Get All
        public IEnumerable<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(dr["Id"].ToString());
                    student.Name = dr["Name"].ToString();
                    student.Gender = dr["Gender"].ToString();
                    student.Department = dr["Department"].ToString();

                    students.Add(student);
                }
                con.Close();
            }
            return students;
        }

        // Insert Student
        public void AddStudent(Student student)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Department", student.Department);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Update Student
        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Department", student.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Delete Student
        public void DeleteStudent(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get Student By Id
        public Student GetStudentById(int? id)
        {
            Student student = new Student();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    student.Id = Convert.ToInt32(dr["Id"].ToString());
                    student.Name = dr["Name"].ToString();
                    student.Gender = dr["Gender"].ToString();
                    student.Department = dr["Department"].ToString();

                }
                con.Close();
            }
            return student;
        }
    }
}
