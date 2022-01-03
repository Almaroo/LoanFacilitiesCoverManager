using System.ComponentModel;

namespace LoansFacilities.Domain.Model
{
    public class Bank
    {
        public int Id { get; }
        public string Name { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Bank(string id, string name)
        {
            Id = int.Parse(id);
            Name = name;
        }
        
        public Bank(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}