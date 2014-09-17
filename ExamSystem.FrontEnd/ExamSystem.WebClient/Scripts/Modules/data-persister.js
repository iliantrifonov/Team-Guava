define(['httpRequester', 'Q', 'cryptoJS'], function (httpRequester, Q) {

    function DataPersister(serviceUrl) {
        this.serviceUrl = serviceUrl;
        this.userPersister = new UserPersister(this.serviceUrl);
        this.visitorPersister = new VisitorPersister(this.serviceUrl + 'post');
    }

    var VisitorPersister = (function () {

        function VisitorPersister(serviceUrl) {
            this.serviceUrl = serviceUrl;
        }

        VisitorPersister.prototype = {
            getPosts: function () {
                return httpRequester.get().getJSON(this.serviceUrl);
            },
            createPost: function (data) {
                return httpRequester.get().postJSON(data);
            }
        }

        return VisitorPersister;

    }());

    var UserPersister = (function () {

        function UserPersister(serviceUrl) {
            this.serviceUrl = serviceUrl;
        }

        UserPersister.prototype = {
            getSessionKey: function () {
                return localStorage.getItem("sessionKey");
            },
            getUsername: function () {
                return localStorage.getItem("username");
            },
            setSessionKey: function (value) {
                localStorage.setItem("sessionKey", value);
            },
            setUsername: function (value) {
                this.username = value; // if not working->comment
                localStorage.setItem("username", value);
            },
            clearSessionKey: function () {
                localStorage.removeItem('sessionKey');
            },
            clearUsername: function () {
                localStorage.removeItem('username');
            },
            login: function (username, password) {
                var self = this;
                //console.log(this.serviceUrl);
                return httpRequester.get().postJSON(this.serviceUrl + 'auth', {
                    username: username,
                    authCode: CryptoJS.SHA1(username + password).toString()
                }).then(function (result) {
                    self.setSessionKey(result.sessionKey);
                    self.setUsername(username);
                }, function (err) {
                    console.log(err); // to handle the error better!
                });
            },
            register: function (username, password) {

                var self = this;

                return httpRequester.get().postJSON(this.serviceUrl + 'user', {
                    username: username,
                    authCode: CryptoJS.SHA1(username + password).toString()
                }).then(function (result) {
                    //console.log(result);
                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
            },
            logout: function () {
                var self = this;
                return httpRequester.get().putJSON(this.serviceUrl + 'user' + '?sessionKey=' + self.getSessionKey(), {
                    'X-SessionKey': JSON.stringify(self.getSessionKey()) // may not be working
                }).then(function (result) {
                    self.clearSessionKey();
                    self.clearUsername();
                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
            },
            isUserLoggedIn: function () {
                return (this.getSessionKey() !== null);
            }
        }

        return UserPersister;

    }());

    return {

        getDataPersister: function (serviceRootUrl) {
            return new DataPersister(serviceRootUrl);
        }
    }

});