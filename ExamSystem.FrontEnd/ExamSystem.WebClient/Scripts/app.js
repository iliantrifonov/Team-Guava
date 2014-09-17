(function () {

    require.config({
        paths: {
            jquery: './Libs/jquery-2.1.1.min',
            Q: './Libs/q',
            Sammy: './Libs/sammy-0.7.5',
            Mustache: './Libs/mustache',
            httpRequester: './Modules/http-requester',
            DataPersister: './Modules/data-persister',
            controller: './Modules/controller',
            htmlRenderer: './Modules/html-renderer'
        }
    });

    require(['jquery', 'Sammy', 'controller'], function (jquery, Sammy, controller) {

        controller.attachEvents();

        var app = Sammy('#main', function () {
            this.get("#/", function () {
                $('#main').load('Views/Welcome.html');
            });

            this.get("#/UserHomepage", function () {
                $('#main').load('Views/UserHomepage.html');
            });

            this.get("#/AdminHomepage", function () {
                $('#main').load('Views/AdminHomepage.html');
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