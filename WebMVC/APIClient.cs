using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using WebAPI.Models;

namespace WebMVC {
    public class APIClient {
        private static HttpClient client = new HttpClient();

        public APIClient() {
            client.BaseAddress = new Uri("http://localhost:56948");
        }

        public List<Employees> ListAllEmployees() {
          
            HttpResponseMessage resp = client.GetAsync("api/employee").Result;
            resp.EnsureSuccessStatusCode();

            return resp.Content.ReadAsAsync<IEnumerable<Employees>>().Result.ToList();
        }

        public Employees ListEmployee(int id) {
            var resp = client.GetAsync(string.Format("api/Employee/{0}", id)).Result;
            resp.EnsureSuccessStatusCode();

            return resp.Content.ReadAsAsync<Employees>().Result;
        }

        //static void ListEmployees(string category) {
        //    Console.WriteLine("Employees in '{0}':", category);

        //    string query = string.Format("api/Employees?category={0}", category);

        //    var resp = client.GetAsync(query).Result;
        //    resp.EnsureSuccessStatusCode();

        //    var employees = resp.Content.ReadAsAsync<IEnumerable<Employees>>().Result;
        //    foreach (var emp in employees) {
        //        Console.WriteLine(emp.FirstName + emp.LastName);
        //    }
        //}
    }


}