(function () {

    require.config({
        paths: {
            jquery: 'Libs/jquery-2.1.1.min',
            Q: 'Libs/q.min',
            Sammy: 'Libs/sammy-0.7.5',
            httpRequester: 'Modules/http-requester',
            DataPersister: 'Modules/data-persister',
            controller: 'Modules/controller'
        }
    });

    require(['jquery', 'Sammy', 'controller'], function (jquery, Sammy, controller) {

        controller.attachEvents();

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