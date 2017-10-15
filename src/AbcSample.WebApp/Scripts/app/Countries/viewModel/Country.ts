export class Country {
    id: KnockoutObservable<string>;

    description: KnockoutObservable<string>;

    constructor(id: string, description:string) {
        this.id = ko.observable<string>(id);
        this.description = ko.observable<string>(description);
    }
}