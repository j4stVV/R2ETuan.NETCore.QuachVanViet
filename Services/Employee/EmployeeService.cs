using R2ETuan.NETCore.QuachVanViet.Models;
namespace R2ETuan.NETCore.QuachVanViet.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;
        private int _nextId;

        public EmployeeService()
        {
            _employees = new List<Employee>();
            _nextId = 1;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(employee);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            employee.Id = _nextId++;
            _employees.Add(employee);
            await Task.CompletedTask;
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = employee.FullName;
                existingEmployee.Age = employee.Age;
                existingEmployee.Email = employee.Email;
            }
            else
            {
                throw new Exception($"Employee with Id {id} not found");
            }
            await Task.CompletedTask;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
            else
            {
                throw new Exception($"Employee with Id {id} not found");
            }
            await Task.CompletedTask;
        }
    }
}

