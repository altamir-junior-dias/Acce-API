using Acce.Repositories;
using Dapper.Contrib.Extensions;

[Table("sample")]
public class SampleRecord : RecordBase { 
    [Key]
    public long Id { get; set; }
}