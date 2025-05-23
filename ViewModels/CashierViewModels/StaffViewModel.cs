using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class StaffViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Staff> Staffs { get; set; } = new();
        public ObservableCollection<string> Roles { get; set; } = new();
        private int _count = 0;
        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }

        private StaffService staffService = new StaffService();

        // LOAD DANH SACH NHAN VIEN
        public async Task GetAllStaff()
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Roles.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                Staffs.Add(item);
                if (!Roles.Contains(item.Role))
                {
                    Roles.Add(item.Role);
                }
                Count++;
            }
        }
        // LOC NHAN VIEN THEO CHUC VU
        public async Task GetStaffByRole(string role)
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                if (item.Role == role)
                {
                    Staffs.Add(item);
                    Count++;
                }
            }
        }
        // THEM NHAN VIEN
        public async Task AddStaff(Staff staff)
        {
            bool result = await staffService.AddStaffAsync(staff);
            if (result)
            {
                Staffs.Add(staff);
                Count++;
            }
        }
        // CAP NHAT NHAN VIEN
        public async Task UpdateStaff(int id, Staff staff)
        {
            await staffService.UpdateStaffAsync(id, staff);
            
        }
        // XOA NHAN VIEN
        public async Task DeleteStaff(int id)
        {
            bool result = await staffService.DeleteStaffAsync(id);
            if (result)
            {
                var staff = Staffs.FirstOrDefault(s => s.Id == id);
                if (staff != null)
                {
                    Staffs.Remove(staff);
                    Count--;
                }
            }
        }
        // TIM KIEM NHAN VIEN
        public async Task SearchStaff(string name)
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                if (item.Name.ToLower().Contains(name.ToLower()))
                {
                    Staffs.Add(item);
                    Count++;
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
