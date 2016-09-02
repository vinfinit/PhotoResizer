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