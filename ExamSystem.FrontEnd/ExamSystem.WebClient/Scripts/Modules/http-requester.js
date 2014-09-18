define(["Q", "jquery"], function (Q) {

    var HttpRequester;

    HttpRequester = (function () {
        function HttpRequester() {
        }

        var AjaxRequest = function (url, type, data, headers) {
            var deferred = Q.defer();

            data = data || {};
            headers = headers || {};

            $.ajax({
                url: url,
                type: type,
                data: data,
                contentType: "application/json",
                headers: headers,
                success: function (responseData) {
                    deferred.resolve(responseData);
                },
                error: function (errorData) {
                    deferred.reject(errorData);
                }
            });

            return deferred.promise;
        }

        HttpRequester.prototype = {
            postJSON: function (url, data) {
                return AjaxRequest(url, "POST", data);
            },
            getJSON: function (url) {
                return AjaxRequest(url, "GET");
            },
            putJSON: function (url, headers) {
                return AjaxRequest(url, "PUT", headers); s

                var deferred = Q.defer();

                $.ajax({
                    url: url,
                    type: "PUT",
                    data: data,
                    contentType: "application/json",
                    headers: headers, 
                    success: function (responseData) {
                        deferred.resolve(responseData);
                    },
                    error: function (errorData) {
                        deferred.reject(errorData);
                    }
                });

                return deferred.promise;
            }

        }

        return HttpRequester;
    }());

    return {
        get: function () {
            return new HttpRequester();
        }
    };

});