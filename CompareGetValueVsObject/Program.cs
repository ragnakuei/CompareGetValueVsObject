using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace CompareGetValueVsObject
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkCases>();
            Console.ReadLine();

            //var test = new TestClass();

            //Print(test.ObjectToBool());
            //Print(test.StreamToBool());
            //Print(test.GetValueToBool());
            //Print(test.ObjectToString());
            //Print(test.StreamToString());
            //Print(test.GetValueToString());
        }

        public static void Print<T>(List<T> list)
        {
            list.ForEach(l => Console.WriteLine(l));
        }
    }

    [MemoryDiagnoser]
    [InliningDiagnoser]
    [TailCallDiagnoser]
    public class BenchmarkCases
    {
        private readonly TestClass _benchClass = new TestClass();

        [Benchmark]
        public void ObjectToBool() => _benchClass.ObjectToBool();

        [Benchmark]
        public void StreamToBool() => _benchClass.StreamToBool();

        [Benchmark]
        public void GetValueToBool() => _benchClass.GetValueToBool();

        [Benchmark]
        public void ObjectToString() => _benchClass.ObjectToString();

        [Benchmark]
        public void StreamToString() => _benchClass.StreamToString();

        [Benchmark]
        public void GetValueToString() => _benchClass.GetValueToString();
    }

    internal class TestClass
    {
        public List<bool> ObjectToBool()
        {
            var result = new List<bool>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(Convert.ToBoolean(dr[1]));
                }
                cn.Close();
            }

            return result;
        }

        public List<bool> StreamToBool()
        {
            var result = new List<bool>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                while (dr.Read())
                {
                    result.Add(dr.GetBoolean(1));
                }
                cn.Close();
            }

            return result;
        }

        public List<bool> GetValueToBool()
        {
            var result = new List<bool>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                while (dr.Read())
                {
                    result.Add(Convert.ToBoolean(dr.GetValue(1)));
                }
                cn.Close();
            }

            return result;
        }

        public List<string> ObjectToString()
        {
            var result = new List<string>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(Convert.ToString(dr[2]));
                }
                cn.Close();
            }

            return result;
        }

        public List<string> StreamToString()
        {
            var result = new List<string>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                while (dr.Read())
                {
                    result.Add(dr.GetString(2));
                }
                cn.Close();
            }

            return result;
        }

        public List<string> GetValueToString()
        {
            var result = new List<string>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=.\mssql2016;Initial Catalog=Test;Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 100 * FROM CompareTable", cn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                while (dr.Read())
                {
                    result.Add(Convert.ToString(dr.GetValue(2)));
                }
                cn.Close();
            }

            return result;
        }
    }
}
