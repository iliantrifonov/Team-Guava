(function () {

    require.config({
        paths: {
            jquery: 'Libs/jquery-2.1.1.min',
            Q: 'Libs/q.min',
            Sammy: 'Libs/sammy-0.7.5'
        }
    });

    require(['jquery', 'sammy'], function ($, Sammy) {

        //var app = Sammy('#main', function () {
        //    this.get("#/", function () {
        //        $('#main').load('Views/UserLogIn.html');
        //    });

        //    this.get("#/chatroom", function () {
        //        $('#main').load('Views/ChatRoom.html');
        //    });

        //});
        //$(function () {
        //    app.run('#/');
        //})
       
    });

}());