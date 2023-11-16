namespace Phoneword;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    string translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        var enteredNumber = PhonenumberText.Text;
        translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);
        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    private async void OnCall(object sender, EventArgs e)
    {
        var response = await DisplayAlert("Dial a Number",
            "Would you like to call " + translatedNumber + "?",
            "Yes",
            "No");
        if (response)
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                {
                    PhoneDialer.Default.Open(translatedNumber);
                }
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}