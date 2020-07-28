using Acce.Repositories;
using Dapper.Contrib.Extensions;

namespace Data.Entities
{
    [Table("customers")]
    public class CustomerRecord : RecordBase
    {
        [Key]
        public long CustomerId { get; set; }
        public string Name { get; set; }        
    }
}