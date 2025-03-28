using R2ETuan.NETCore.QuachVanViet.Models;

namespace R2ETuan.NETCore.QuachVanViet.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task UpdateEmployeeAsync(int id, Employee employee);
    }
}