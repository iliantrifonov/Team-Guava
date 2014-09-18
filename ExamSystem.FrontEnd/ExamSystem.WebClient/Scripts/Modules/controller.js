/// <reference path="data-persister.js" />
define(["DataPersister", "htmlRenderer", "jquery"], function (DataPersister, htmlRenderer) {
    var controller = (function () {
        var persister = DataPersister.getDataPersister('http://localhost:1945/api/');
        var $main = $('#main');

        function attachEvents() {
            $main.on('click', '#login-user', loginUser);
            $main.on('click', '#login-admin', loginAdmin);
            $main.on('click', '#all-exams', renderAllExams);
            $main.on('click', "#register-btn", switchToLoginPage);
        }

        function Controller() {
        }

        function switchToLoginPage() {
            window.location.hash = '#/';
        }

        function loginUser(ev) {
            var $email = $('#email').val();
            var $password = $('#password').val();
            if ($email === '' || $password === '') {
                alert("Username and password are required");
                ev.preventDefault();
                return;
            }

            persister.userPersister.login($email, $password);
            window.location.hash = '#/UserHomepage';
            htmlRenderer.renderUsername(persister.userPersister.getSessionKey());
            renderAllExams();
        }

        function loginAdmin(ev) {
            var $email = $('#email').val();
            var $password = $('#password').val();
            if ($email === '' || $password === '') {
                alert("Username and password are required");
                ev.preventDefault();
                return;
            }

            persister.adminPersister.login($email, $password);
            window.location.hash = '#/AdminHomepage';
            htmlRenderer.renderUsername(persister.adminPersister.getSessionKey());
            renderAllExams();
        }

        function renderAllExams() {
            var examsData = persister.getAllExams();
            htmlRenderer.renderAllExam(examsData);
        }

        return {
            loginUser: loginUser,
            loginAdmin: loginAdmin,
            attachEvents: attachEvents
        }
    })();

    return controller;
});