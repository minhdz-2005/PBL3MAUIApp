
namespace PBL3MAUIApp.Models;
public class ShiftStaffs {
    public int Id { get; set; }
    public int ShiftId { get; set; }
    public int StaffId { get; set; }

    public ShiftStaffs () {}
    public ShiftStaffs (int shiftId, int staffId) {
        ShiftId = shiftId;
        StaffId = staffId;
    }
}