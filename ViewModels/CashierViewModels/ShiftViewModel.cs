using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Services;
using PBL3MAUIApp.Models;
using System.Diagnostics;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class ShiftViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DateTime> Day { get; set; } = new();
        public ObservableCollection<Shift> DayShifts { get; set; } = new();
        public ObservableCollection<Staff> Staffs { get; set; } = new();
        public ObservableCollection<Staff?> TotalStaffInShift { get; set; } = new();
        public ObservableCollection<Staff?> TotalStaffOutShift { get; set; } = new();

        public ObservableCollection<Staff?> StaffCa1 { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa1_ThuNgan { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa1_PhaChe { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa1_PhucVu { get; set; } = new();

        public ObservableCollection<Staff?> StaffCa2 { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa2_ThuNgan { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa2_PhaChe { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa2_PhucVu { get; set; } = new();

        public ObservableCollection<Staff?> StaffCa3 { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa3_ThuNgan { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa3_PhaChe { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa3_PhucVu { get; set; } = new();

        private DateTime _selectedDay = new DateTime();
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (value != _selectedDay)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                }
            }
        }
        private int _selectedShift = 0;
        public int SelectedShift
        {
            get => _selectedShift;
            set
            {
                if (value != _selectedShift)
                {
                    _selectedShift = value;
                    OnPropertyChanged(nameof(SelectedShift));
                }
            }
        }
        private int _selectedTotal = 0;
        public int SelectedTotal
        {
            get => _selectedTotal;
            set
            {
                if (value != _selectedTotal)
                {
                    _selectedTotal = value;
                    OnPropertyChanged(nameof(SelectedTotal));
                }
            }
        }

        private int _totalNvCa1;
        private int _totalNvCa2;
        private int _totalNvCa3;

        public int TotalNvCa1
        {
            get => _totalNvCa1;
            set
            {
                if (value != _totalNvCa1)
                {
                    _totalNvCa1 = value;
                    OnPropertyChanged(nameof(TotalNvCa1));
                }
            }
        }
        public int TotalNvCa2
        {
            get => _totalNvCa2;
            set
            {
                if (value != _totalNvCa2)
                {
                    _totalNvCa2 = value;
                    OnPropertyChanged(nameof(TotalNvCa2));
                }
            }
        }
        public int TotalNvCa3
        {
            get => _totalNvCa3;
            set
            {
                if (value != _totalNvCa3)
                {
                    _totalNvCa3 = value;
                    OnPropertyChanged(nameof(TotalNvCa3));
                }
            }
        }


        private int _ca1ThuNgan;
        private int _ca2ThuNgan;
        private int _ca3ThuNgan;

        public int Ca1ThuNgan
        {
            get => _ca1ThuNgan;
            set
            {
                if (value != _ca1ThuNgan)
                {
                    _ca1ThuNgan = value;
                    OnPropertyChanged(nameof(Ca1ThuNgan));
                }
            }
        }
        public int Ca2ThuNgan
        {
            get => _ca2ThuNgan;
            set
            {
                if (value != _ca2ThuNgan)
                {
                    _ca2ThuNgan = value;
                    OnPropertyChanged(nameof(Ca2ThuNgan));
                }
            }
        }
        public int Ca3ThuNgan
        {
            get => _ca3ThuNgan;
            set
            {
                if (value != _ca3ThuNgan)
                {
                    _ca3ThuNgan = value;
                    OnPropertyChanged(nameof(Ca3ThuNgan));
                }
            }
        }


        private int _ca1PhucVu;
        private int _ca2PhucVu;
        private int _ca3PhucVu;

        public int Ca1PhucVu
        {
            get => _ca1PhucVu;
            set
            {
                if (value != _ca1PhucVu)
                {
                    _ca1PhucVu = value;
                    OnPropertyChanged(nameof(Ca1PhucVu));
                }
            }
        }
        public int Ca2PhucVu
        {
            get => _ca2PhucVu;
            set
            {
                if (value != _ca2PhucVu)
                {
                    _ca2PhucVu = value;
                    OnPropertyChanged(nameof(Ca2PhucVu));
                }
            }
        }
        public int Ca3PhucVu
        {
            get => _ca3PhucVu;
            set
            {
                if (value != _ca3PhucVu)
                {
                    _ca3PhucVu = value;
                    OnPropertyChanged(nameof(Ca3PhucVu));
                }
            }
        }

        private int _ca1PhaChe;
        private int _ca2PhaChe;
        private int _ca3PhaChe;

        public int Ca1PhaChe
        {
            get => _ca1PhaChe;
            set
            {
                if (value != _ca1PhaChe)
                {
                    _ca1PhaChe = value;
                    OnPropertyChanged(nameof(Ca1PhaChe));
                }
            }
        }
        public int Ca2PhaChe
        {
            get => _ca2PhaChe;
            set
            {
                if (value != _ca2PhaChe)
                {
                    _ca2PhaChe = value;
                    OnPropertyChanged(nameof(Ca2PhaChe));
                }
            }
        }
        public int Ca3PhaChe
        {
            get => _ca3PhaChe;
            set
            {
                if (value != _ca3PhaChe)
                {
                    _ca3PhaChe = value;
                    OnPropertyChanged(nameof(Ca3PhaChe));
                }
            }
        }


        private ShiftService shiftService = new ShiftService();
        private StaffService staffService = new StaffService();
        private ShiftStaffsService shiftStaffsService = new ShiftStaffsService();

        // LAY TAT CA DANH SACH NGAY LAM
        public async Task GetAllDay()
        {
            var shifts = await shiftService.GetShiftsAsync();
            Day.Clear();
            foreach (var shift in shifts)
            {
                if (!Day.Contains(shift.StartTime.Date))
                    Day.Add(shift.StartTime.Date);
            }
        }
        // LAY CAC CA LAM THEO NGAY
        public async Task GetShiftsByDay(DateTime date)
        {
            var dayShifts = await shiftService.GetShiftsByDateAsync(date);
            DayShifts.Clear();
            foreach (var shift in dayShifts)
            {
                DayShifts.Add(shift);
            }
        }
        // TINH SO NHAN VIEN TRONG NGAY
        public async Task StaffInShift(DateTime date)
        {
            int idCa1 = 0, idCa2 = 0, idCa3 = 0;
            var dayShifts = await shiftService.GetShiftsByDateAsync(date);
            DayShifts.Clear();
            foreach (var shift in dayShifts)
            {
                DayShifts.Add(shift);

            }
            foreach (var shift in DayShifts)
            {
                if (shift.EndTime.Hour < 12) idCa1 = shift.Id;
                if (shift.StartTime.Hour >= 12 && shift.EndTime.Hour < 18) idCa2 = shift.Id;
                if (shift.StartTime.Hour >= 18) idCa3 = shift.Id;
                // Debug.WriteLine($"{shift.Id}");
            }

            //Debug.WriteLine($"{idCa1}, {idCa2}, {idCa3}");

            // Reset nhan vien trong ca
            TotalNvCa1 = 0;
            TotalNvCa2 = 0;
            TotalNvCa3 = 0;

            Ca1ThuNgan = 0;
            Ca2ThuNgan = 0;
            Ca3ThuNgan = 0;

            Ca1PhucVu = 0;
            Ca2PhucVu = 0;
            Ca3PhucVu = 0;

            Ca1PhaChe = 0;
            Ca2PhaChe = 0;
            Ca3PhaChe = 0;

            StaffCa1.Clear();
            StaffCa2.Clear();
            StaffCa3.Clear();

            StaffCa1_ThuNgan.Clear();
            StaffCa1_PhaChe.Clear();
            StaffCa1_PhucVu.Clear();

            StaffCa2_ThuNgan.Clear();
            StaffCa2_PhaChe.Clear();
            StaffCa2_PhucVu.Clear();

            StaffCa3_ThuNgan.Clear();
            StaffCa3_PhaChe.Clear();
            StaffCa3_PhucVu.Clear();

            // CA 1
            var ca1 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa1);
            foreach (var ca in ca1)
            {
                //Debug.WriteLine("1");
                //Debug.WriteLine(ca.StaffId);

                TotalNvCa1 = ca1.Count;
                if (TotalNvCa1 > 0)
                {
                    StaffCa1.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
                }
            }
            foreach (var staff in StaffCa1)
            {
                if (staff != null)
                {
                    if (staff.Role == "Thu ngân")
                    {
                        Ca1ThuNgan++;
                        StaffCa1_ThuNgan.Add(staff);
                    }
                    else if (staff.Role == "Phục vụ")
                    {
                        Ca1PhucVu++;
                        StaffCa1_PhucVu.Add(staff);
                    }
                    else if (staff.Role == "Pha chế")
                    {
                        Ca1PhaChe++;
                        StaffCa1_PhaChe.Add(staff);
                    }
                }
            }

            // CA 2
            var ca2 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa2);
            foreach (var ca in ca2)
            {
                //Debug.WriteLine("2");
                //Debug.WriteLine(ca.StaffId);

                TotalNvCa2 = ca2.Count;
                if (TotalNvCa2 > 0)
                {
                    StaffCa2.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
                }
            }
            foreach (var staff in StaffCa2)
            {
                if (staff != null)
                {
                    if (staff.Role == "Thu ngân")
                    {
                        Ca2ThuNgan++;
                        StaffCa2_ThuNgan.Add(staff);
                    }
                    else if (staff.Role == "Phục vụ")
                    {
                        Ca2PhucVu++;
                        StaffCa2_PhucVu.Add(staff);
                    }
                    else if (staff.Role == "Pha chế")
                    {
                        Ca2PhaChe++;
                        StaffCa2_PhaChe.Add(staff);
                    }
                }
            }


            // CA 3
            var ca3 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa3);
            foreach (var ca in ca3)
            {
                //Debug.WriteLine("3");
                //Debug.WriteLine(ca.StaffId);

                TotalNvCa3 = ca3.Count;
                if (TotalNvCa3 > 0)
                {
                    StaffCa3.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
                }
            }
            foreach (var staff in StaffCa3)
            {
                if (staff != null)
                {
                    if (staff.Role == "Thu ngân")
                    {
                        Ca3ThuNgan++;
                        StaffCa3_ThuNgan.Add(staff);
                    }
                    else if (staff.Role == "Phục vụ")
                    {
                        Ca3PhucVu++;
                        StaffCa3_PhucVu.Add(staff);
                    }
                    else if (staff.Role == "Pha chế")
                    {
                        Ca3PhaChe++;
                        StaffCa3_PhaChe.Add(staff);
                    }
                }
            }

        }
        // THEM NGAY LAM
        public async Task AddDayShift()
        {
            DateTime lastDay = Day.Last();
            Debug.WriteLine($"{lastDay:dd/MM/yyyy}");

            lastDay = lastDay.AddDays(1);

            DateTime ca1Start = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 6, 0, 0);
            DateTime ca1End = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 11, 59, 59);
            DateTime ca2Start = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 12, 0, 0);
            DateTime ca2End = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 17, 59, 59);
            DateTime ca3Start = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 18, 0, 0);
            DateTime ca3End = new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59);

            Day.Add(ca1Start);

            await Shell.Current.DisplayAlert("Thành công",$"Đã thêm ngày làm {ca1Start:dd/MM/yyyy} !","OK");

            // THEM VAO DB
            await shiftService.AddShiftAsync(new Shift(ca1Start, ca1End));
            await shiftService.AddShiftAsync(new Shift(ca2Start, ca2End));
            await shiftService.AddShiftAsync(new Shift(ca3Start, ca3End));
        }
        // NHAN VIEN TRONG CA
        public async Task ShowStaffShift (DateTime date, int shift)
        {
            TotalStaffInShift.Clear();
            TotalStaffOutShift.Clear();

            SelectedDay = date;
            SelectedShift = shift;
            if (SelectedShift == 1)
            {
                SelectedTotal = TotalNvCa1;
                
                foreach (var staff in StaffCa1)
                {
                    if (staff != null)
                    {
                        TotalStaffInShift.Add(staff);
                    }
                }
            }
            else if (SelectedShift == 2)
            {
                SelectedTotal = TotalNvCa2;

                foreach (var staff in StaffCa2)
                {
                    if (staff != null)
                    {
                        TotalStaffInShift.Add(staff);
                    }
                }
            }
            else if (SelectedShift == 3)
            {
                SelectedTotal = TotalNvCa3;

                foreach (var staff in StaffCa3)
                {
                    if (staff != null)
                    {
                        TotalStaffInShift.Add(staff);
                    }
                }
            }

            
            foreach (var staff in TotalStaffInShift)
            {
                if (staff != null)
                {
                    Debug.WriteLine($"{staff.Name}");
                }
            }

            //Debug.WriteLine($"ca1 {StaffCa1.Count}");
            //Debug.WriteLine($"ca2 {StaffCa2.Count}");
            //Debug.WriteLine($"ca3 {StaffCa3.Count}");

            var listStaff = await staffService.GetStaffsAsync();

            foreach (var staff in listStaff)
            {
                bool isIn = false;
                foreach (var s in TotalStaffInShift)
                {
                    if (staff != null && s != null && staff.Id == s.Id)
                    {
                        isIn = true;
                        break; // Da co trong ca
                    }
                }
                if (!isIn)
                {
                    TotalStaffOutShift.Add(staff);
                }
            }
        }
        // Them nhan vien vao ca
        public async Task AddStaffToShift(Staff staff)
        {
            if (staff != null)
            {
                TotalStaffInShift.Add(staff);
                TotalStaffOutShift.Remove(staff);

                // LUU VAO DB
                int count = 1;
                foreach (var shift in DayShifts)
                {
                    if (count == SelectedShift)
                    {
                        await shiftStaffsService.AddShiftStaffsAsync(new ShiftStaffs(shift.Id, staff.Id));

                        if (count == 1)
                        {
                            TotalNvCa1++;
                            if (staff.Role == "Thu ngân")
                            {
                                Ca1ThuNgan++;
                                StaffCa1_ThuNgan.Add(staff);
                                if (!StaffCa1.Contains(staff))
                                {
                                    StaffCa1.Add(staff);
                                }
                            }
                            else if (staff.Role == "Phục vụ")
                            {
                                Ca1PhucVu++;
                                StaffCa1_PhucVu.Add(staff);
                                if (!StaffCa1.Contains(staff))
                                {
                                    StaffCa1.Add(staff);
                                }
                            }
                            else if (staff.Role == "Pha chế")
                            {
                                Ca1PhaChe++;
                                StaffCa1_PhaChe.Add(staff);
                                if (!StaffCa1.Contains(staff))
                                {
                                    StaffCa1.Add(staff);
                                }
                            }
                        }
                        else if (count == 2)
                        {
                            TotalNvCa2++;
                            if (staff.Role == "Thu ngân")
                            {
                                Ca2ThuNgan++;
                                StaffCa2_ThuNgan.Add(staff);
                                if (!StaffCa2.Contains(staff))
                                {
                                    StaffCa2.Add(staff);
                                }
                            }
                            else if (staff.Role == "Phục vụ")
                            {
                                Ca2PhucVu++;
                                StaffCa2_PhucVu.Add(staff);
                                if (!StaffCa2.Contains(staff))
                                {
                                    StaffCa2.Add(staff);
                                }
                            }
                            else if (staff.Role == "Pha chế")
                            {
                                Ca2PhaChe++;
                                StaffCa2_PhaChe.Add(staff);
                                if (!StaffCa2.Contains(staff))
                                {
                                    StaffCa2.Add(staff);
                                }
                            }

                        }
                        else if (count == 3)
                        {
                            TotalNvCa3++;
                            if (staff.Role == "Thu ngân")
                            {
                                Ca3ThuNgan++;
                                StaffCa3_ThuNgan.Add(staff);
                                if (!StaffCa3.Contains(staff))
                                {
                                    StaffCa3.Add(staff);
                                }
                            }
                            else if (staff.Role == "Phục vụ")
                            {
                                Ca3PhucVu++;
                                StaffCa3_PhucVu.Add(staff);
                                if (!StaffCa3.Contains(staff))
                                {
                                    StaffCa3.Add(staff);
                                }
                            }
                            else if (staff.Role == "Pha chế")
                            {
                                Ca3PhaChe++;
                                StaffCa3_PhaChe.Add(staff);
                                if (!StaffCa3.Contains(staff))
                                {
                                    StaffCa3.Add(staff);
                                }
                            }

                        }
                    }
                    count++;
                }
            }
        }
        // Xoa nhan vien trong ca
        public async Task RemoveStaffFromShift(Staff staff)
        {
            if (staff != null)
            {
                TotalStaffInShift.Remove(staff);
                TotalStaffOutShift.Add(staff);

                // XOA KHOI DB
                int count = 1;
                foreach (var shift in DayShifts)
                {
                    if (count == SelectedShift)
                    {
                        var listShiftStaff = await shiftStaffsService.GetShiftStaffssAsync();
                        foreach (var ss in  listShiftStaff)
                        {
                            if (ss != null && ss.ShiftId == shift.Id && ss.StaffId == staff.Id)
                            {
                                await shiftStaffsService.DeleteShiftStaffsAsync(ss.Id);

                                if (count == 1)
                                {
                                    TotalNvCa1--;
                                    if (staff.Role == "Thu ngân")
                                    {
                                        Ca1ThuNgan--;
                                        StaffCa1_ThuNgan.Remove(staff);
                                        StaffCa1.Remove(staff);
                                    }
                                    else if (staff.Role == "Phục vụ")
                                    {
                                        Ca1PhucVu--;
                                        StaffCa1_PhucVu.Remove(staff);
                                        StaffCa1.Remove(staff);
                                    }
                                    else if (staff.Role == "Pha chế")
                                    {
                                        Ca1PhaChe--;
                                        StaffCa1_PhaChe.Remove(staff);
                                        StaffCa1.Remove(staff);
                                    }

                                }
                                else if (count == 2)
                                {
                                    TotalNvCa2--;
                                    if (staff.Role == "Thu ngân")
                                    {
                                        Ca2ThuNgan--;
                                        StaffCa2_ThuNgan.Remove(staff);
                                        StaffCa2.Remove(staff);
                                    }
                                    else if (staff.Role == "Phục vụ")
                                    {
                                        Ca2PhucVu--;
                                        StaffCa2_PhucVu.Remove(staff);
                                        StaffCa2.Remove(staff);
                                    }
                                    else if (staff.Role == "Pha chế")
                                    {
                                        Ca2PhaChe--;
                                        StaffCa2_PhaChe.Remove(staff);
                                        StaffCa2.Remove(staff);
                                    }

                                }
                                else if (count == 3)
                                {
                                    TotalNvCa3--;
                                    if (staff.Role == "Thu ngân")
                                    {
                                        Ca3ThuNgan--;
                                        StaffCa3_ThuNgan.Remove(staff);
                                        StaffCa3.Remove(staff);
                                    }
                                    else if (staff.Role == "Phục vụ")
                                    {
                                        Ca3PhucVu--;
                                        StaffCa3_PhucVu.Remove(staff);
                                        StaffCa3.Remove(staff);
                                    }
                                    else if (staff.Role == "Pha chế")
                                    {
                                        Ca3PhaChe--;
                                        StaffCa3_PhaChe.Remove(staff);
                                        StaffCa3.Remove(staff);
                                    }

                                }

                                break; // Chi can xoa 1 lan
                            }
                        }
                    }
                    count++;
                }
            }
        }
        // LOC NHAN VIEN THEO ROLE
        public async Task FilterStaffByRole(string role)
        {
            // LUU VAO BIEN TAM
            await ShowStaffShift(SelectedDay, SelectedShift);

            var listStaffInShift = new ObservableCollection<Staff?>(TotalStaffInShift);
            var listStaffOutShift = new ObservableCollection<Staff?>(TotalStaffOutShift);
            
            // Xoa toan bo nhan vien trong ca
            TotalStaffInShift.Clear();
            TotalStaffOutShift.Clear();

            // LOC NHAN VIEN
            if (role == "Tất cả")
            {
                foreach (var staff in listStaffInShift)
                {
                    if (staff != null)
                    {
                        TotalStaffInShift.Add(staff);
                    }
                }
                foreach (var staff in listStaffOutShift)
                {
                    if (staff != null)
                    {
                        TotalStaffOutShift.Add(staff);
                    }
                }
            }
            else
            {
                foreach (var staff in listStaffInShift)
                {
                    if (staff != null && staff.Role == role)
                    {
                        TotalStaffInShift.Add(staff);
                    }
                }
                foreach (var staff in listStaffOutShift)
                {
                    if (staff != null && staff.Role == role)
                    {
                        TotalStaffOutShift.Add(staff);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
