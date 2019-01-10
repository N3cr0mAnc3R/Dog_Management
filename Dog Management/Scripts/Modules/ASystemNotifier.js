webApp.factory("systemNotifier", function ($timeout) {

    var removeDelay = 5000,
        notifierScope = {
            messages: [],
            clear: function () {
                var self = this;
                self.messages = [];
            }
        };

    function notifier(incomeMessages) {
        if (incomeMessages instanceof Array) {
            incomeMessages.forEach(function (message, index) {
                var messageWrapper = {
                    source: message,
                    remove: function () {
                        var self = this;

                        var index = notifierScope.messages.indexOf(self);

                        if (index !== -1) {
                            notifierScope.messages.splice(index, 1);
                        }
                    },
                    class: [
                        "delay-" + (index + 1)
                    ],
                    index: index
                };
                notifierScope.messages.unshift(messageWrapper);

                $timeout(function () {
                    messageWrapper.remove();
                }, removeDelay * (message.Type === 'success' ? 1 : 2));

            });
        }
    };


    return {
        getScope: function () {
            return notifierScope;
        },
        getNotifier: function () {
            return notifier;
        }
    };
});