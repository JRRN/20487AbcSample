import { CountryService } from "./CountryService";
import { CountryViewModel } from "./viewModel/countryViewModel";

var dataService = new CountryService();
var countryVm = new CountryViewModel(dataService);

ko.applyBindings(countryVm);
