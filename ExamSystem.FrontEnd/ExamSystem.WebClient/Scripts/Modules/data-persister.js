define(['httpRequester', 'Q'], function (httpRequester, Q) {

    function DataPersister(serviceUrl) {
        this.serviceUrl = serviceUrl;
        this.userPersister = new UserPersister(this.serviceUrl);
        this.adminPersister = new AdminPersister(this.serviceUrl + 'post');
    }

    var AdminPersister = (function () {

        function AdminPersister(serviceUrl) {
            this.serviceUrl = serviceUrl;
        }

        AdminPersister.prototype = {
            getPosts: function () {
                return httpRequester.get().getJSON(this.serviceUrl);
            },
            createPost: function (data) {
                return httpRequester.get().postJSON(data);
            }
        }

        return AdminPersister;

    }());

    var UserPersister = (function () {

        function UserPersister(serviceUrl) {
            this.serviceUrl = serviceUrl;
        }

        UserPersister.prototype = {
            getSessionKey: function () {
                return localStorage.getItem("sessionKey");
            },
            getEmail: function () {
                return localStorage.getItem("email");
            },
            setSessionKey: function (value) {
                localStorage.setItem("sessionKey", value);
            },
            setEmail: function (value) {
                this.email = value; 
                localStorage.setItem("email", value);
            },
            clearSessionKey: function () {
                localStorage.removeItem('sessionKey');
            },
            clearEmail: function () {
                localStorage.removeItem('email');
            },
            login: function (email, password) {
                var self = this;
                return httpRequester.get().postJSON(this.serviceUrl + 'Register', {
                    email: email,
                    authCode: (email + password).toString() //maybe cryptojs
                }).then(function (result) {
                    self.setSessionKey(result.sessionKey);
                    self.setEmail(email);
                }, function (err) {
                    console.log(err); // to handle the error better!
                });
            },
            register: function (email, password) {

                var self = this;

                return httpRequester.get().postJSON(this.serviceUrl + 'User', {
                    email: email,
                    authCode: (email + password).toString()
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
                    self.clearemail();
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