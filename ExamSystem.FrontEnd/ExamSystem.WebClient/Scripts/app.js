﻿(function () {

    require.config({
        paths: {
            jquery: 'Libs/jquery-2.1.1.min',
            Q: 'Libs/q.min',
            Sammy: 'Libs/sammy-0.7.5'
        }
    });

    require(['jquery', 'Sammy'], function (jquery, Sammy) {

        var app = Sammy('#main', function () {
            this.get("#/", function () {
                $('#main').load('Views/Welcome.html');
            });

            this.get("#/UserLogIn", function () {
                $('#main').load('Views/UserLogIn.html');
            });

            this.get("#/Register", function () {
                $('#main').load('Views/Register.html');
            });
        });
        $(function () {
            app.run('#/');
        })

    });

}());