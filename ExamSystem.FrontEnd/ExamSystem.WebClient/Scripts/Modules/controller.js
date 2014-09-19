define(["Mustache", "jquery"], function (Mustache) {
    var htmlRenderer = (function () {
        function renderUsername(username) {
            $('span#username').html = username;
        }

        function renderAllExams(data) {
            var examUl = $('<ul>');
            var template = '<li><a class="exam-name" href="#">{{{Name}}}</a><p>StartTime: {{{StartTime}}}</p><p>EndTime: {{{EndTime}}}</p>' +
                '<a class="exam-id" href="#">{{{Id}}}</a><button id="add-comment" class="btn">Add Comment</button></li>';

            for (var i = 0; i < data.length; i++) {
                examUl.append(Mustache.to_html(template, data[i]));
                console.log(data[i].Id);
        }

            $('#exams-container').html(examUl);
            }

        function renderAllComments(data){
            var examUl = $('<ul>');
            var template = '<li><p>{{{Text}}}</p><p>Date: {{{Date}}}</p>' +
                '<a href="#">{{{Id}}}</a></li>';

            for (var i = 0; i < data.length; i++) {
                examUl.append(Mustache.to_html(template, data[i]));
            }

            $.ajax({url: "http://localhost:1945/Token",
                type: "POST",
                data: "userName=" + $email + "&password=" + $password + "&grant_type=password",
                success: function (data) {
                    localStorage.setItem("token", data.access_token);
                    window.location.hash = '#/UserHomepage';
                    renderAllExams();
                },
                error: function (errorData) {
                }
            });

//            persister.userPersister.login($email, $password).then(function () {
//                window.location.hash = '#/UserHomepage';
//                htmlRenderer.renderUsername(persister.userPersister.getUserName());
//                renderAllExams();
//            });
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
            $.ajax({url: "http://localhost:1945/api/Problems/Add",
                type: "POST",
                data: "name=" + $name + "&examId=" + $examId,
                contentType:"application/x-www-form-urlencoded",
                success: function (data) {
                    alert('Problem Added');
                    window.location.hash = '#/UserHomepage';
                    renderAllExams();
                },
                error: function (errorData) {
                }
            })
        }

        function showProblems(){
            var examId = $('#exams-container > ul > li').children('a.exam-id').html();
            persister.userPersister.getAllProblems(examId);
        }

        }

        function renderAddComment(){
            var $text = $('<textarea id="text" />');
            var $button = $('<button id="send" class="btn">SEND</button>');
            $('#exams-container').append($text);
            $('#exams-container').append($button);
        }

        return {
            renderUsername: renderUsername,
            renderAllExam: renderAllExams,
            renderAllComments: renderAllComments,
            renderAddComment: renderAddComment
        }
    })();
    return htmlRenderer;
});