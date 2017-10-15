import common = require("../RestService");

export class CountryService extends common.RestService {
    apiUrlBase: string = null;

    constructor() {
        super();
        this.apiUrlBase = this.baseUrl + "country";
    }

    getAllCountryAvatars = (): JQueryPromise<ApiEntities.CountryResponse[]> => {
        return this.getJson<ApiEntities.CountryResponse[]>(this.apiUrlBase);
    }
}