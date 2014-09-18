/// <reference path="data-persister.js" />
define(["DataPersister", "htmlRenderer", "jquery"], function (DataPersister, htmlRenderer) {
    var controller = (function () {
        var persister = DataPersister.getDataPersister('http://localhost:1945/');
        var $main = $('#main');

        function attachEvents() {
            $main.on('click', '#login-user', loginUser);
            $main.on('click', '#login-admin', loginAdmin);
            $main.on('click', '#all-exams', renderAllExams);
            $main.on('click', "#register-btn", registerUser());
        }

        function Controller() {
        }

        function switchToLoginPage() {
            window.location.hash = '#/';
        }

        function registerUser(){
            var $email = $('#email').val();
            var $password = $('#password').val();
            var $confirm = $('#confirm-password').val();
            if ($email === '' || $password === '' || $confirm === '') {
                alert("Username and password are required");
                ev.preventDefault();
                return;
            }
            persister.userPersister.register($email, $password);
            switchToLoginPage();
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
            htmlRenderer.renderUsername(persister.userPersister.getUserName());
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
            attachEvents: attachEvents
        }
    })();

    return controller;
});