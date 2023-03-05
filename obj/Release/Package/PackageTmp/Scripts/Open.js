$(document).ready(
    function() {
        console.log("here");
        $("#startBtn").click(function () {
            $.ajax({
                url: "/Home/StartNewOpening",
            }).done(function (res) {
                $("#container").html(res);
            });
        });


        function getExistingProcess() {
            $.ajax({
                url: "/Home/StartExistingOpening",
            }).done(function (res) {
                $("#container").html(res);
            });
        }

        function bindAction() {
            $("#action").click(function() {
                this;
                $.ajax({
                    url: "/Home/something",
                }).done(function(res) {
                    $("#container").html(res);
                });
            }
        }
        
    });


