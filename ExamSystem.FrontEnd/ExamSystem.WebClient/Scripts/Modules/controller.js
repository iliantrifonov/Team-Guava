/// <reference path="data-persister.js" />
define(["DataPersister", "jquery"], function (DataPersister) {
    var controller = (function () {
        var persister = DataPersister.getDataPersister('url');
        var $main = $('#main');

        function attachEvents() {
            $main.on('click', '#login-user', login);
        }

        function Controller() {
        }

        function login() {
            var $email = $('#email').val();
            var $password = $('#password').val();
            persister.userPersister.login($email, $password);
            window.location.hash = '#/UserLogIn';
        }

        function renderAllExams() {
            $main.on('click', '#all-exams')
        }

        return {
            login: login,
            attachEvents: attachEvents
        }
    })();
    return controller;
});