define(['httpRequester', 'Q', 'htmlRenderer'], function (httpRequester, Q, htmlRenderer) {

    function DataPersister(serviceUrl) {
        this.serviceUrl = serviceUrl;
        this.userPersister = new UserPersister(this.serviceUrl);
        this.adminPersister = new AdminPersister(this.serviceUrl);
    }

    var AdminPersister = (function () {

        function AdminPersister(serviceUrl) {
            this.serviceUrl = serviceUrl;
        }

        AdminPersister.prototype = {
            getSessionKey: function () {
                return localStorage.getItem("sessionKey");
            },
            getUserName: function () {
                return localStorage.getItem("userName");
            },
            setSessionKey: function (value) {
                localStorage.setItem("sessionKey", value);
            },
            setUserName: function (value) {
                this.email = value;
                localStorage.setItem("userName", value);
            },
            clearSessionKey: function () {
                localStorage.removeItem('sessionKey');
            },
            clearUserName: function () {
                localStorage.removeItem('email');
            },
            login: function (userName, password) {
                var self = this;
                return httpRequester.get().postJSON(this.serviceUrl + 'Token', {
                    userName: userName,
                    password: password,
                    grant_type: "password"
                }).then(function (result) {
                    self.setSessionKey(result.access_token);
                    self.setUserName(userName);
                }, function (err) {
                    console.log(err); // to handle the error better!
                });
            },
            register: function (email, password, confirmPassword) {

                var self = this;

                return httpRequester.get().postJSON(this.serviceUrl + 'api/Account/Register', {
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmPassword
                }).then(function (result) {

                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
            },
            logout: function () { //TODO: Logout
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
            },
            getAllExams: function () {
                return httpRequester.get().getJSON(this.serviceUrl + 'problems/all?examid=xxx').then(function (data) {
                    return data;
                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
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
            getUserName: function () {
                return localStorage.getItem("userName");
            },
            setSessionKey: function (value) {
                localStorage.setItem("sessionKey", value);
            },
            setUserName: function (value) {
                this.email = value;
                localStorage.setItem("userName", value);
            },
            clearSessionKey: function () {
                localStorage.removeItem('sessionKey');
            },
            clearUserName: function () {
                localStorage.removeItem('email');
            },
            login: function (userName, password) {
                var self = this;
                return httpRequester.get().postJSON(this.serviceUrl + 'Token', {
                    userName: userName,
                    password: password,
                    grant_type: "password"
                }).then(function (result) {
                    self.setSessionKey(result.access_token);
                    self.setUserName(userName);
                }, function (err) {
                    alert('Invalid username or password');
                    console.log(err); 
                });
            },
            register: function (email, password, confirmPassword) {

                var self = this;

                return httpRequester.get().postJSON(this.serviceUrl + 'api/Account/Register', {
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmPassword
                }).then(function (result) {
                    //console.log(result);
                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
            },
            logout: function () { //TODO: Logout
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
            },
            getAllExams: function () {
                return httpRequester.get().getJSON(this.serviceUrl + 'api/Exams/All').then(function (data) {
                    htmlRenderer.renderAllExam(data);
                }, function (err) {
                    console.log(err.responseText); // to handle the error better!
                });
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