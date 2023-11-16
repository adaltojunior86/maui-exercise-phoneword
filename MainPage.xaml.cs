namespace Phoneword;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private string _translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        var enteredNumber = PhonenumberText.Text;
        _translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);
        if (!string.IsNullOrEmpty(_translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + _translatedNumber;
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
            "Would you like to call " + _translatedNumber + "?",
            "Yes",
            "No");
        if (response)
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                {
                    PhoneDialer.Default.Open(_translatedNumber);
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