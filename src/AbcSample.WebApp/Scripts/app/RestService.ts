export module XCutting {
    declare var g_urlBaseApiServer: string;

    export class RestService {

        protected baseUrl: string;

        constructor() {
            this.baseUrl = g_urlBaseApiServer;
        }

        protected getJson<TResponse>(url: string): JQueryPromise<TResponse> {
            return this.callJson<TResponse, any>("GET", url, null);
        }

        protected postJson<TResponse, TRequest>(url: string, data: TRequest): JQueryPromise<TResponse> {
            return this.callJson("POST", url, data);
        }

        protected putJson<TResponse, TRequest>(url: string, data: TRequest): JQueryPromise<TResponse> {
            return this.callJson("PUT", url, data);
        }

        protected deleteJson<TRequest, TResponse>(url: string, data: TRequest): JQueryPromise<TResponse> {
            return this.callJson("DELETE", url, data);
        }

        private callJson<TResponse, TRequest>(method: string, url: string, data: TRequest): JQueryPromise<TResponse> {

            var deferred = $.Deferred<TResponse>();
            var language = "en";
            var options: JQueryAjaxSettings = {
                type: method,
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            };


            options.headers = { 'Accept-Language': language };

            if (data) {
                options.data = ko.toJSON(data);
            }

            $.ajax(options)
                .done(data => {
                    deferred.resolve(<TResponse>data);
                })
                .fail((jqXHR, textStatus, errorThrown) => {
                    if (jqXHR.status === 404) {
                        toastr.warning("Records not found");
                        deferred.resolve(<TResponse>{});
                    }
                    // toastr.warning(textStatus + ' : ' + errorThrown);

                    if (jqXHR.responseJSON != null && jqXHR.responseJSON.message != null) {
                        toastr.error(jqXHR.responseJSON.message);
                        deferred.reject(jqXHR.responseJSON.message);
                    } else {
                        toastr.error(jqXHR.statusText);
                        deferred.fail(jqXHR.statusText);
                    }
                });
            return deferred.promise();
        }

        protected addDays = (startDate: Date, numberOfDays: number): Date => {
            var returnDate = new Date(
                startDate.getFullYear(),
                startDate.getMonth(),
                startDate.getDate() + numberOfDays,
                startDate.getHours(),
                startDate.getMinutes(),
                startDate.getSeconds());
            return returnDate;
        };
    }

}