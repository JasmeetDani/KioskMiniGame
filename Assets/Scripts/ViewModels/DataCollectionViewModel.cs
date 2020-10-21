using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityWeld.Binding;

[Binding]
public class DataCollectionViewModel : ViewModelBase<DataCollectionViewModel, DataCollectionController>
{
    public InputField Name;
    public InputField Email;

    public AutoCompleteComboBox Countries;

    public void Awake()
    {
        Countries.AvailableOptions = Options.ToList();
    }

    public string[] Options { get; } = new string[]
    {
        "Afghanistan",
        "Albania",
        "Algeria",
        "Andorra",
        "Angola",
        "Antigua and Barbuda",
        "Argentina",
        "Armenia",
        "Australia",
        "Austria",
        "Azerbaijan",
        "Bahamas",
        "Bahrain",
        "Bangladesh",
        "Barbados",
        "Belarus",
        "Belgium",
        "Belize",
        "Benin",
        "Bhutan",
        "Bolivia",
        "Bosnia and Herzegovina",
        "Botswana",
        "Brazil",
        "Brunei",
        "Bulgaria",
        "Burkina Faso",
        "Burundi",
        "Côte d'Ivoire",
        "Cabo Verd",
        "Cambodia",
        "Cameroon",
        "Canada",
        "Central African Republic",
        "Chad",
        "Chile",
        "China",
        "Colombia",
        "Comoros",
        "Congo (Congo-Brazzaville)",
        "Costa Rica",
        "Croatia",
        "Cuba",
        "Cyprus",
        "Czechia (Czech Republic)",
        "Democratic Republic of the Congo",
        "Denmark",
        "Djibouti",
        "Dominica",
        "Dominican Republic",
        "Ecuador",
        "Egypt",
        "El Salvador",
        "Equatorial Guinea",
        "Eritrea",
        "Estonia",
        "Eswatini (fmr. Swaziland)",
        "Ethiopia",
        "Fiji",
        "Finland",
        "France",
        "Gabon",
        "Gambia",
        "Georgia",
        "Germany",
        "Ghana",
        "Greece",
        "Grenada",
        "Guatemala",
        "Guinea",
        "Guinea-Bissau",
        "Guyana",
        "Haiti",
        "Holy See",
        "Honduras",
        "Hungary",
        "Iceland",
        "India",
        "Indonesia",
        "Iran",
        "Iraq",
        "Ireland",
        "Israel",
        "Italy",
        "Jamaica",
        "Japan",
        "Jordan",
        "Kazakhstan",
        "Kenya",
        "Kiribati",
        "Kuwait",
        "Kyrgyzstan",
        "Laos",
        "Latvia",
        "Lebanon",
        "Lesotho",
        "Liberia",
        "Libya",
        "Liechtenstein",
        "Lithuania",
        "Luxembourg",
        "Madagascar",
        "Malawi",
        "Malaysia",
        "Maldives",
        "Mali",
        "Malta",
        "Marshall Islands",
        "Mauritania",
        "Mauritius",
        "Mexico",
        "Micronesia",
        "Moldova",
        "Monaco",
        "Mongolia",
        "Montenegro",
        "Morocco",
        "Mozambique",
        "Myanmar",
        "Namibia",
        "Nauru",
        "Nepal",
        "Netherlands",
        "New Zealand",
        "Nicaragua",
        "Niger",
        "Nigeria",
        "North Korea",
        "North Macedonia",
        "Norway",
        "Oman",
        "Pakistan",
        "Palau",
        "Palestine State",
        "Panama",
        "Papua New Guinea",
        "Paraguay",
        "Peru",
        "Philippines",
        "Poland",
        "Portugal",
        "Qatar",
        "Romania",
        "Russia",
        "Rwanda",
        "Saint Kitts and Nevis",
        "Saint Lucia",
        "Saint Vincent and the Grenadines",
        "Samoa",
        "San Marino",
        "Sao Tome and Principe",
        "Saudi Arabia",
        "Senegal",
        "Serbia",
        "Seychelles",
        "Sierra Leone",
        "Singapore",
        "Slovakia",
        "Slovenia",
        "Solomon Islands",
        "Somalia",
        "South Africa",
        "South Korea",
        "South Sudan",
        "Spain",
        "Sri Lanka",
        "Sudan",
        "Suriname",
        "Sweden",
        "Switzerland",
        "Syria",
        "Tajikistan",
        "Tanzania",
        "Thailand",
        "Timor-Leste",
        "Togo",
        "Tonga",
        "Trinidad",
        "Tunisia",
        "Turkey",
        "Turkmenistan",
        "Tuvalu",
        "Uganda",
        "Ukraine",
        "United Arab Emirates",
        "United Kingdom",
        "United States of America",
        "Uruguay",
        "Uzbekistan",
        "Vanuatu",
        "Venezuela",
        "Vietnam",
        "Yemen",
        "Zambia",
        "Zimbabwe"
    };

    public Toggle OptOut;

    private bool isNameValid = true;
    [Binding]
    public bool IsNameValid
    {
        get => isNameValid;
        set
        {
            if (isNameValid == value) return;
            isNameValid = value;
            OnPropertyChanged();
        }
    }

    private bool isEmailValid = true;
    [Binding]
    public bool IsEmailValid
    {
        get => isEmailValid;
        set
        {
            if (isEmailValid == value) return;
            isEmailValid = value;
            OnPropertyChanged();
        }
    }

    private bool isCountryValid = true;
    [Binding]
    public bool IsCountryValid
    {
        get => isCountryValid;
        set
        {
            if (isCountryValid == value) return;
            isCountryValid = value;
            OnPropertyChanged();
        }
    }

    private string nameErrorMessage = "";
    [Binding]
    public string NameErrorMessage
    {
        get => nameErrorMessage;
        set
        {
            if (nameErrorMessage == value) return;
            nameErrorMessage = value;
            OnPropertyChanged();
        }
    }

    private string emailErrorMessage = "";
    [Binding]
    public string EmailErrorMessage
    {
        get => emailErrorMessage;
        set
        {
            if (emailErrorMessage == value) return;
            emailErrorMessage = value;
            OnPropertyChanged();
        }
    }

    private string countryErrorMessage = "";
    [Binding]
    public string CountryErrorMessage
    {
        get => countryErrorMessage;
        set
        {
            if (countryErrorMessage == value) return;
            countryErrorMessage = value;
            OnPropertyChanged();
        }
    }

    public bool HasErrors()
    {
        if (Name.text == string.Empty)
        {
            NameErrorMessage = "Name cannot be empty...";
            IsNameValid = false;
        }
        else IsNameValid = true;
        if (Email.text == string.Empty)
        {
            EmailErrorMessage = "Email cannot be empty...";
            IsEmailValid = false;
        }
        else
        {
            if (!controller.IsValidEmail(Email.text))
            {
                EmailErrorMessage = "Not a valid email address...";
                IsEmailValid = false;
            }
            else IsEmailValid = true;
        }
        if (Countries.Text == string.Empty)
        {
            CountryErrorMessage = "Country cannot be empty...";
            IsCountryValid = false;
        }
        else
        {
            if (!controller.IsValidCountry(Countries.Text))
            {
                CountryErrorMessage = "Not a valid Country...";
                IsCountryValid = false;
            }
            else IsCountryValid = true;
        }

        return !isNameValid || !isEmailValid || !isCountryValid;
    }

    [Binding]
    public void OnNext()
    {
        controller.OnNext();
    }

    public void SetFocusOnName()
    {
        StartCoroutine(SetFocusOnNameField());
    }

    IEnumerator SetFocusOnNameField()
    {
        yield return new WaitForEndOfFrame();
        Name.Select();
    }
}