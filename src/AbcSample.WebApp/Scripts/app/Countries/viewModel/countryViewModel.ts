import service = require("../CountryService");
import entities = require("./Country");

export class CountryViewModel {
    isLoading: KnockoutObservable<boolean>;
    dataService: service.CountryService;
    allCountries: KnockoutObservableArray<entities.Country>;
    selectedCountry: KnockoutObservable<entities.Country>;

    constructor(dataService: service.CountryService) {
        this.dataService = dataService;
        this.allCountries = ko.observableArray<entities.Country>();
        this.selectedCountry = ko.observable<entities.Country>();

        this.isLoading = ko.observable<boolean>(false);
    }

    load = () => {
        this.isLoading(false);
        this.dataService.getAllCountryAvatars()
            .done((countryList: ApiEntities.CountryResponse[]) => {
                var temp = ko.utils.arrayMap<ApiEntities.CountryResponse, entities.Country>(countryList,
                    (item: ApiEntities.CountryResponse) => {
                        return new entities.Country(item.Id, item.Description);
                    });
                this.allCountries(temp);
            }).always(() => {
                this.isLoading(false);
            });
    };
}