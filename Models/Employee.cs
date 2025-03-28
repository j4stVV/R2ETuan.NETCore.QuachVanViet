namespace R2ETuan.NETCore.QuachVanViet.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Employee(int id, string fullName, int age, string email)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Email = email;
        }
    }
}
