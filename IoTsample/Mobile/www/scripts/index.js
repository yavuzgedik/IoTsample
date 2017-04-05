
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady, false);

    function onDeviceReady() {
        document.addEventListener('pause', onPause, false);
        document.addEventListener('resume', onResume, false);
        document.getElementById("Btn").addEventListener("click", BtnClick);
        document.getElementById("BtnState").addEventListener("click", Control);
        Control();
    };

    function onPause() {
    };

    function onResume() {
    };

    function BtnClick() {
        $.ajax({
            url: "{WebApiUrl}",
            type: "POST",
            data: { "ID": null, "IsOpen": state, "Date": null },
            dataType: "json",
        });

        if (state == true) {
            document.getElementById("txtResult").innerText = "Durum: Açık";
            document.getElementById("Btn").innerText = "Kapat";
            state = false;
        }
        else {
            document.getElementById("txtResult").innerText = "Durum: Kapalı";
            document.getElementById("Btn").innerText = "Aç";
            state = true;
        }
    }

    var state = false;

    function Control() {
        $.ajax({
            url: "{WebApiUrl}",
            type: "GET",
            dataType: "json",
            contentType: 'application/json',
            success: function (data) {

                //alert(data.IsOpen);

                var result = data.IsOpen;

                if (result == true) {
                    document.getElementById("txtResult").innerText = "Durum: Açık";
                    document.getElementById("Btn").innerText = "Kapat";
                    state = false;
                }
                else {
                    document.getElementById("txtResult").innerText = "Durum: Kapalı";
                    document.getElementById("Btn").innerText = "Aç";
                    state = true;
                }
            }
        });
    }
})();