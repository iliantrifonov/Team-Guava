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

            $('#exams-container').html(examUl);
           
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