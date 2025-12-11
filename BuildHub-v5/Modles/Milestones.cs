// HAMZAH
namespace BuildHubV2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("milestones")]
public class Milestones
{
	[Key]
	public long Id { get; set; }

	[Required]
	public required int Project_Id { get; set; }

	[Required, MaxLength(200)]
	public required string Title { get; set; }

	public DateTime? Due_Date { get; set; }

	[Column(TypeName = "numeric(14,2)")]
	public decimal? Amount_Omr { get; set; }

	[Required, MaxLength(20)]
	public string Status { get; set; } = "planned";

	public int? Order_Index { get; set; }

	public DateTime Created_At { get; set; } = DateTime.Now;
}
