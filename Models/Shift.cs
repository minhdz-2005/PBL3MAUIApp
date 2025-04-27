
namespace PBL3MAUIApp.Models;
public class Shift {
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Shift () {}
    public Shift (DateTime start, DateTime end) {
        StartTime = start;
        EndTime = end;
    }
}