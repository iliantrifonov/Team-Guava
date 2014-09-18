define(["Mustache", "jquery"], function (Mustache) {
    var htmlRenderer = (function () {
        function renderUsername(username) {
            $('span#username').html = username;
        }

        function renderAllExams(data){
            var examUl = $('<ul>');
            var template = '<li><a><span>{{{Name}}}</span><p>{{{StartTime}}}</p><p>{{{EndTime}}}</p></a></li>';

            for (var i = 0; i < data.length; i++) {
                examUl.append(Mustache.to_html(template, data[i]));
            }

            $('#exams-container').html(examUl);
        }

        function renderOneExam(data) {
            var $exam = $('<div>');
            var template = '<a>{{{Name}}}</a><span>{{{StartDate}}}</span><span>{{{EndDate}}}</span>';
            $exam.append(Mustache.to_html(template, data));

            $('#exam-container').html($exam);
        }

        return {
            renderUsername: renderUsername,
            renderAllExam: renderAllExams,
            renderOneExam: renderOneExam
        }
    })();
    return htmlRenderer;
});