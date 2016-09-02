;
(function() {
    $('#stopButton')
        .click(function() {
            $.post('@Url.Action("Stop", "Home")',
                function(data) {
                    console.log(data);
                });
        });
}());

(function() {
    var inputSection = $("inputImages"),
        outputSection = $("outputImages");

    var es = new EventSource('/home/message');
    es.onmessage = function (e) {
        console.log(e.data);
    };
    es.onerror = function () {
        console.log(arguments);
    };
}());