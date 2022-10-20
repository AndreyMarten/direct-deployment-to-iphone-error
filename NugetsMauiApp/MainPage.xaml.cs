using System.Collections.ObjectModel;
using DevExpress.Maui.Scheduler;

namespace NugetsMauiApp;

public partial class MainPage : ContentPage {
    public MainPage() {
        BindingContext = new VM();
        InitializeComponent();

        sch.DataStorage = new DevExpress.Maui.Scheduler.SchedulerDataStorage()
        {

        };
        sch.DataStorage.AppointmentItems.Add(new DevExpress.Maui.Scheduler.AppointmentItem()
        {
            Subject = "Apt1",
            Start = DateTime.Today.AddHours(9),
            End = DateTime.Today.AddHours(10),
            StatusId = 0
        });
        sch.DataStorage.AppointmentItems.Add(new DevExpress.Maui.Scheduler.AppointmentItem()
        {
            Subject = "Apt2",
            Start = DateTime.Today.AddHours(10),
            End = DateTime.Today.AddHours(11),
            StatusId = 1
        });
        sch.DataStorage.AppointmentItems.Add(new DevExpress.Maui.Scheduler.AppointmentItem()
        {
            Subject = "Apt3",
            Start = DateTime.Today.AddHours(11),
            End = DateTime.Today.AddHours(12),
            StatusId = 2
        });
        sch.DataStorage.AppointmentItems.Add(new DevExpress.Maui.Scheduler.AppointmentItem()
        {
            Subject = "Apt4",
            Start = DateTime.Today.AddHours(12),
            End = DateTime.Today.AddHours(13),
            StatusId = 3
        });
        sch.Tap += DayView_OnTap;
    }

    async void DayView_OnTap(object sender, SchedulerGestureEventArgs e)
    {
        //if (this.inNavigation)
        //    return;
        Page appointmentPage = sch.DataStorage.CreateAppointmentPageOnTap(e, true);
        if (appointmentPage != null)
        {
            //inNavigation = true;
            await NavigationService.NavigateToPage(appointmentPage);
        }
    }
}

public class VM {
    public ObservableCollection<Item> Items { get; }
    public VM() {
        Items = new ObservableCollection<Item>();
        for (int i = 0; i < 20; i++)
            Items.Add(new Item(i));
    }
}
public class Item {
    public int Id { get;  }
    public string Text { get; }
    public Item(int id) {
        Id = id;
        Text = $"Item{(char)((int)'A' + id)}";
    }
}

public static class NavigationService
{
    static void SetDemoPageTitleView(Page page, string titleText)
    {
        Label label = new()
        {
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            FontFamily = "Univia-Pro Medium",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            Text = titleText,
            LineBreakMode = Microsoft.Maui.LineBreakMode.NoWrap
        };
        label.SetDynamicResource(Label.TextColorProperty, "TextThemeColor");

        Grid container = new();
        container.Add(label);

        Shell.SetTitleView(page, container);
    }

    public static async Task NavigateToPage(Page page)
    {
        string titleText = page.Title;
        await NavigateToPage(page, titleText);
    }

    public static async Task NavigateToPage(Page page, string titleText)
    {
        if (Shell.GetTitleView(page) == null)
            SetDemoPageTitleView(page, titleText);

        await Shell.Current.Navigation.PushAsync(page);
    }
}


