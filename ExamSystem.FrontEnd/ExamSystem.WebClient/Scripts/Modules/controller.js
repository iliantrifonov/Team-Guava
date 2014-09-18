/// <reference path="data-persister.js" />
define(["DataPersister", "htmlRenderer", "jquery"], function (DataPersister, htmlRenderer) {
    var controller = (function () {
        var persister = DataPersister.getDataPersister('http://localhost:1945/');
        var $main = $('#main');

        function attachEvents() {
            $main.on('click', '#login-user', loginUser);
            $main.on('click', '#login-admin', loginAdmin);
            $main.on('click', '#all-exams', renderAllExams);
            $main.on('click', "#register-btn", registerUser);
            $main.on('click', "#add-exam", addExam);
            $main.on('click', "#add-problem", addProblem);
            $main.on('click', "ul li a", showComments);
            $main.on('click', "#add-exam", addExam);

        }

        function Controller() {
        }

        function switchToLoginPage() {
            window.location.hash = '#/';
        }

        function registerUser(ev) {
            var $email = $('#email').val();
            var $password = $('#password').val();
            var $confirm = $('#confirm-password').val();
            if ($email === '' || $password === '' || $confirm === '') {
                alert("Username and password are required");
                ev.preventDefault();
                return;
            }
            persister.userPersister.register($email, $password, $confirm);
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

            persister.userPersister.login($email, $password).then(function () {
                window.location.hash = '#/UserHomepage';
                htmlRenderer.renderUsername(persister.userPersister.getUserName());
                renderAllExams();
            });
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
            htmlRenderer.renderUsername(persister.adminPersister.getUserName());
            renderAllExams();
        }

        function addExam(){
            var $name = $('#exam-name').val();
            var $start = $('#start-time').val();
            var $end = $('#end-time').val();
            persister.userPersister.addExam($name, $start, $end).then(function(){
                renderAllExams();
            })
        }

        function addProblem(){
            var $name = $('#problem-name').val();
            var $examId = $('#exam-id').val();
            persister.userPersister.addProblem($name, $examId).then(function(){
                renderAllExams();
            })
        }

        function showComments(){
            var examId = $('.exam-id').html();
            persister.userPersister.getComments(examId);
        }

        function renderAllExams() {
            var examsData = persister.userPersister.getAllExams();
            htmlRenderer.renderAllExam(examsData);
        }

        return {
            attachEvents: attachEvents
        }
    })();

    return controller;
});