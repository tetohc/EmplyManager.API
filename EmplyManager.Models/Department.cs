namespace EmplyManager.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employe { get; set; }
    }
}