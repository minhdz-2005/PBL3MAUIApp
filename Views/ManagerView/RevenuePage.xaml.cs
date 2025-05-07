using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class RevenuePage : ContentPage
{
    public RevenuePage()
    {
        InitializeComponent();
        BindingContext = new RevenuePageViewModel();
    }
}

public class RevenuePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Bộ lọc thời gian
    private string _selectedTimeRange = "Ngày";
    public string SelectedTimeRange
    {
        get => _selectedTimeRange;
        set
        {
            _selectedTimeRange = value;
            IsDateVisible = value == "Ngày"; // Chỉ hiển thị DatePicker khi chọn "Ngày"
            OnPropertyChanged();
            FilterCommand.Execute(SelectedDate);
        }
    }

    private DateTime _selectedDate = DateTime.Now;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            _selectedDate = value;
            OnPropertyChanged();
        }
    }

    private bool _isDateVisible = true;
    public bool IsDateVisible
    {
        get => _isDateVisible;
        set { _isDateVisible = value; OnPropertyChanged(); }
    }

    // Tab selection
    private bool _isTotalRevenueSelected = true;
    public bool IsTotalRevenueSelected
    {
        get => _isTotalRevenueSelected;
        set { _isTotalRevenueSelected = value; OnPropertyChanged(); }
    }

    private bool _isHourlyRevenueSelected;
    public bool IsHourlyRevenueSelected
    {
        get => _isHourlyRevenueSelected;
        set { _isHourlyRevenueSelected = value; OnPropertyChanged(); }
    }

    private bool _isProductRevenueSelected;
    public bool IsProductRevenueSelected
    {
        get => _isProductRevenueSelected;
        set { _isProductRevenueSelected = value; OnPropertyChanged(); }
    }

    private bool _isEmployeeRevenueSelected;
    public bool IsEmployeeRevenueSelected
    {
        get => _isEmployeeRevenueSelected;
        set { _isEmployeeRevenueSelected = value; OnPropertyChanged(); }
    }

    private bool _isCancellationRateSelected;
    public bool IsCancellationRateSelected
    {
        get => _isCancellationRateSelected;
        set { _isCancellationRateSelected = value; OnPropertyChanged(); }
    }

    private bool _isDiscountSelected;
    public bool IsDiscountSelected
    {
        get => _isDiscountSelected;
        set { _isDiscountSelected = value; OnPropertyChanged(); }
    }

    private bool _isProfitMarginSelected;
    public bool IsProfitMarginSelected
    {
        get => _isProfitMarginSelected;
        set { _isProfitMarginSelected = value; OnPropertyChanged(); }
    }

    // Dữ liệu thống kê
    public string TotalRevenueText => $"Tổng doanh thu: {CalculateTotalRevenue():N0} VNĐ (So sánh: +5% so với tuần trước)";
    public string HourlyRevenueText => "Giờ cao điểm: 12:00-14:00 (500k VNĐ), Giờ thấp điểm: 04:00-06:00 (50k VNĐ)";
    public ObservableCollection<ProductRevenue> ProductRevenueList { get; } = new ObservableCollection<ProductRevenue>
    {
        new ProductRevenue { ProductName = "Cà phê đen", Revenue = 300000, Profit = 150000 },
        new ProductRevenue { ProductName = "Trà sữa", Revenue = 200000, Profit = 100000 }
    };
    public ObservableCollection<EmployeeRevenue> EmployeeRevenueList { get; } = new ObservableCollection<EmployeeRevenue>
    {
        new EmployeeRevenue { EmployeeName = "Nhân viên A", OrdersCount = 15, AverageOrderValue = 40000 },
        new EmployeeRevenue { EmployeeName = "Nhân viên B", OrdersCount = 10, AverageOrderValue = 35000 }
    };
    public string CancellationRateText => "Tỉ lệ hủy đơn: 2%, Đổi trả: 1%";
    public string DiscountText => "Tổng chiết khấu: 50.000 VNĐ (Ảnh hưởng: -2% doanh thu)";
    public string ProfitMarginText => "Tỷ suất lợi nhuận: 25% (Sau chi phí)";

    private double CalculateTotalRevenue()
    {
        // Logic giả lập tính tổng doanh thu
        return 1000000; // Giá trị mẫu
    }

    public ICommand FilterCommand { get; }
    public ICommand SelectTabCommand { get; }

    public RevenuePageViewModel()
    {
        FilterCommand = new Command<DateTime>(date =>
        {
            // Logic lọc theo ngày (giả lập)
            OnPropertyChanged(nameof(TotalRevenueText));
            OnPropertyChanged(nameof(HourlyRevenueText));
            OnPropertyChanged(nameof(ProductRevenueList));
            OnPropertyChanged(nameof(EmployeeRevenueList));
            OnPropertyChanged(nameof(CancellationRateText));
            OnPropertyChanged(nameof(DiscountText));
            OnPropertyChanged(nameof(ProfitMarginText));
        });

        SelectTabCommand = new Command<string>(tab =>
        {
            IsTotalRevenueSelected = tab == "TotalRevenue";
            IsHourlyRevenueSelected = tab == "HourlyRevenue";
            IsProductRevenueSelected = tab == "ProductRevenue";
            IsEmployeeRevenueSelected = tab == "EmployeeRevenue";
            IsCancellationRateSelected = tab == "CancellationRate";
            IsDiscountSelected = tab == "Discount";
            IsProfitMarginSelected = tab == "ProfitMargin";
        });

        // Đảm bảo khởi tạo mặc định
        OnPropertyChanged(nameof(IsDateVisible));
    }
}

// Model cho Doanh thu theo sản phẩm
public class ProductRevenue : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _productName = string.Empty;
    private double _revenue;
    private double _profit;

    public string ProductName
    {
        get => _productName;
        set { _productName = value; OnPropertyChanged(); }
    }

    public double Revenue
    {
        get => _revenue;
        set { _revenue = value; OnPropertyChanged(); }
    }

    public double Profit
    {
        get => _profit;
        set { _profit = value; OnPropertyChanged(); }
    }

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Model cho Doanh thu theo nhân viên
public class EmployeeRevenue : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _employeeName = string.Empty;
    private int _ordersCount;
    private double _averageOrderValue;

    public string EmployeeName
    {
        get => _employeeName;
        set { _employeeName = value; OnPropertyChanged(); }
    }

    public int OrdersCount
    {
        get => _ordersCount;
        set { _ordersCount = value; OnPropertyChanged(); }
    }

    public double AverageOrderValue
    {
        get => _averageOrderValue;
        set { _averageOrderValue = value; OnPropertyChanged(); }
    }

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}