$(document).ready(function () {
    //Detect enter key
    $('#barcodeValue').keyup(function (event) {
        if (event.keyCode == 13) {
            validateString($("#barcodeValue").val(), $("#barcodeType option:selected").val());
        }
    });

    // Hide the value textbox
    $("#barcodeValue").hide();
    $("#CreateButton").hide();
    $("#alertBox").hide();
    $("#progressBar").hide();
    $("#infoLink").hide();

    // Drop down changed
    $("#barcodeType").change(function () {
        // show the value textbox
        $("#barcodeValue").show();
        $("#CreateButton").show();

        // Prepend the textbox if necessary
        shouldPrepend();
    });

    function shouldPrepend() {
        var valueToCheck = $("#barcodeType option:selected").val();
        switch (valueToCheck) {
            case "isbn":
                $("#barcodeValue").val("978");
                break;
            case "bookland":
                $("#barcodeValue").val("978");
                break;
            default:
                break;
        }
    }

    // Create QR code
    $("#CreateButton").click(function () {
        // Validate the values
        validateString($("#barcodeValue").val(), $("#barcodeType option:selected").val());
    });

    function createImage() {
        // First check the code type
        var finalUrl;
        if ($("#barcodeType option:selected").val() == "qrcode") {

            finalUrl = '<img src="';
            finalUrl += "http://chart.apis.google.com/chart?cht=qr&chs=200x200&chl=";
            finalUrl += $("#barcodeValue").val();
            finalUrl += "&chld=H|0";
            finalUrl += '" />';

        } else {
            // Get our full Url
            var url = '/Code/Barcode';

            // Build up a url
            url += "?type=";
            url += $("#barcodeType option:selected").val();
            url += "&value=";
            url += $("#barcodeValue").val();

            // url string
            finalUrl = '<img src="';
            finalUrl += url;
            finalUrl += '" />';
        }

        // Hide the progress bar
        $("#progressBar").hide();

        $('#image').html('');
        $('#image').prepend('<p>Your code is: </p>' + finalUrl);
        $('#image').css("border", "1px dotted #CCCCCC");
        $('#image').append('<br/><p>To download your image, Right click and choose "Save image as".</p>');


        $("#infoLink").show();

        // Save the users codes to local storage
        saveToLocalStorage(finalUrl);
    }

    function validateString(valueToValidate, codeType) {

        $("#progressBar").show();

        // Build Url
        var url = '/Code/Validate';
        url += "?type=";
        url += codeType;
        url += "&value=";
        url += valueToValidate;

        $.ajax({
            url: url,
            success: function (data) {
                // Check if valid
                if (data.IsValid == false) {
                    $("#alertBox").show();
                    $("#barcodeValue").addClass("error");
                    $("#alertBox").text(data.ErrorMessage.toString());

                    // Hide the progress bar
                    $("#progressBar").hide();

                } else {
                    $("#alertBox").hide();
                    $("#barcodeValue").addClass("success");
                    createImage();
                }
            }
        });
    }

    function saveToLocalStorage(imageUrl) {
        // Check if user can handle localStorage first.
        if (typeof (localStorage) != 'undefined') {
            // Get the old values
            var savedCodes = localStorage.getItem('savedCodes');

            if (savedCodes == null) {
                localStorage.setItem("savedCodes", imageUrl); //saves to the database, "key", "value"
            } else {
                imageUrl += "<br/>" + savedCodes;
                localStorage.setItem("savedCodes", imageUrl);
            }
        }
    }
});