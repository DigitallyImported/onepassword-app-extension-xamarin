# onepassword-app-extension-xamarin
Xamarin.iOS binding for the AgileBits 1Password App Extension library

## Usage

Download the [OnePasswordExtension.dll](https://github.com/ianlevesque/onepassword-app-extension-xamarin/releases) and add
a reference to it to your project.

Create a button and add it to your password UITextField:

```C#
if(OnePasswordExtension.SharedExtension.IsAppExtensionAvailable)
{
    var passwordFillButton = new UIButton(UIButtonType.Custom)
        {
            AccessibilityLabel = "Fill Password",
            ContentMode = UIViewContentMode.Left,
            ContentEdgeInsets = new UIEdgeInsets(0, 8, 0, 8)
        };
    var buttonImage = UIImage.FromBundle("onepassword-button");
    passwordFillButton.SetImage(buttonImage, UIControlState.Normal);
    passwordFillButton.SizeToFit();
    passwordFillButton.TouchUpInside += PasswordFillButton_TouchUpInside;

    passwordField.RightView = passwordFillButton;
    passwordField.RightViewMode = UITextFieldViewMode.Always;
}
```

Handle logging in:

```C#
private void PasswordFillButton_TouchUpInside (object sender, EventArgs e)
{
    OnePasswordExtension.SharedExtension.FindLoginForURLString("https://mywebsite", this, (NSObject)sender, (loginDetails, error) =>
        {
            if(loginDetails == null)
            {
                if(error != null && error.Domain == AppExtension.ErrorDomain && error.Code == AppExtension.ErrorCodeCancelledByUser)
                {
                    return;
                }

                Console.WriteLine("Couldn't fill login: {0}", error != null ? error.Description : string.Empty);

                return;
            }

            if(loginDetails.ContainsKey(AppExtension.UsernameKey))
            {
                usernameTextField.Text = (NSString)loginDetails[AppExtension.UsernameKey];
            }

            if(loginDetails.ContainsKey(AppExtension.PasswordKey))
            {
                passwordField.Text = (NSString)loginDetails[AppExtension.PasswordKey];
            }
        });
}
```

Handle signing up:

```C#
private void PasswordFillButton_TouchUpInside (object sender, EventArgs e)
{
    var newLoginDetails = new NSMutableDictionary
        {
            { AppExtension.TitleKey, (NSString)Config.NetworkName },
            { AppExtension.UsernameKey, (NSString)(string.IsNullOrWhiteSpace(usernameTextField.Text) ? string.Empty : usernameTextField.Text) },
            { AppExtension.PasswordKey, (NSString)(string.IsNullOrWhiteSpace(passwordField.Text) ? string.Empty : passwordField.Text) }
        };

    var generatorOptions = new NSMutableDictionary
        {
            { AppExtension.GeneratedPasswordMinLengthKey, new NSNumber(6) },
            { AppExtension.GeneratedPasswordMinLengthKey, new NSNumber(64) }
        };

    OnePasswordExtension.SharedExtension.StoreLoginForURLString("https://mywebsite", newLoginDetails, generatorOptions, this, (NSObject)sender, (loginDetails, error) =>
        {
            if(loginDetails == null)
            {
                if(error != null && error.Domain == AppExtension.ErrorDomain && error.Code == AppExtension.ErrorCodeCancelledByUser)
                {
                    return;
                }

                Console.WriteLine("Couldn't generate login: {0}", error != null ? error.Description : string.Empty);

                return;
            }

            if(loginDetails.ContainsKey(AppExtension.UsernameKey))
            {
                usernameTextField.Text = (NSString)loginDetails[AppExtension.UsernameKey];
            }

            if(loginDetails.ContainsKey(AppExtension.PasswordKey))
            {
                passwordField.Text = (NSString)loginDetails[AppExtension.PasswordKey];
            }
        });
}
```

Good luck!
